using System.Activities;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using IdeaBlade.Persistence;

// 
namespace Impac.Mosaiq.IQ.Activities.RadOnc.DoseTrackingSite
{
    /// <summary> Activity used to display an information message </summary>
    [CreateSecondaryDoseSiteActivity_DisplayName]
    [DoseTrackingSite_Category]
    [RadOnc_ActivityGroup]
    public class CreateSecondaryDoseSiteActivity : MosaiqCodeActivity
    {

        #region Properties (Input Parameters)
        /// <summary> Patient ID of current open patient as input </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CreateSecondaryDoseSiteActivity_PatId_DisplayName]
        [CreateSecondaryDoseSiteActivity_PatId_Description]
        public InArgument<int> PatId { get; set; }

        /// <summary> List of Sites that shall contribute to the Secondary Dose Site </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CreateSecondaryDoseSiteActivity_RxSite_DisplayName]
        [CreateSecondaryDoseSiteActivity_RxSite_Description]
        public InArgument<EntityList<PrescriptionSite>> RxSite { get; set; }
        
        /// <summary> Name for the Secondary Dose Site </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CreateSecondaryDoseSiteActivity_RegionName_DisplayName]
        [CreateSecondaryDoseSiteActivity_RegionName_Description]
        public InArgument<string> RegionName { get; set; }

        /// <summary> Dose coefficient of each field to the Secondary Dose Site </summary>
        [InputParameterCategory]
        [CreateSecondaryDoseSiteActivity_DoseCoeff_DisplayName]
        [CreateSecondaryDoseSiteActivity_DoseCoeff_Description]
        public InArgument<double> DoseCoeff { get; set; }

        /// <summary>
        /// The persistence manager to use with the query. If not provided, a new persistence manager will be created.
        /// </summary>
        [InputParameterCategory]
        [CreateSecondaryDoseSiteActivity_PersistenceManager_DisplayName]
        [CreateSecondaryDoseSiteActivity_PersistenceManager_Description]
        public InArgument<ImpacPersistenceManager> PersistenceManager { get; set; }

        #endregion

        #region Properties (Output Parameters)
        /// <summary> The result of the activity </summary>
        [OutputParameterCategory]
        [CreateSecondaryDoseSiteActivity_DoseSiteCreated_DisplayName]
        [CreateSecondaryDoseSiteActivity_DoseSiteCreated_Description]
        public OutArgument<bool> DoseSiteCreated { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Activity heavy lifting.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            // Activity inputs
            int patId1 = PatId.Get(context);
            EntityList<PrescriptionSite> rxSite = RxSite.Get(context);
            string regionName = RegionName.Get(context);
            double doseCoeff = DoseCoeff.Expression != null ? DoseCoeff.Get(context) : 1.000;
            ImpacPersistenceManager pm = PersistenceManager.Expression != null
                                             ? PersistenceManager.Get(context)
                                             : PM;


            // Activity output
            DoseSiteCreated.Set(context, false); // Initialize the Activity output to False

            // Make sure rxSite is not supplied as a null (e.g. Nothing)
            if (rxSite == null)
            {
                return;
            }

            // Make sure none of the given Sites is Null
            if (rxSite.Count == 0)
            {
                return;
            }
            
            for (int i = 0; i < rxSite.Count; i++)
            {
                if (rxSite[i] == null)
                {
                    return;
                }
            }

            // Only continue if doseCoeff given is within valid range
            if ((doseCoeff < 0.0) || (doseCoeff > 9.9990))
            {
                return;
            }

            // Name for the Region cannot be empty or null
            if (string.IsNullOrEmpty(regionName))
            {
                return;
            }

            // Trim regionName to max allowed chars
            const int maxSiteNameLength = 20;
            if (regionName.Length > maxSiteNameLength)
            {
                regionName = regionName.Substring(0, maxSiteNameLength);
            }

            // First check if supplied siteName exists already in the database
            var query = new ImpacRdbQuery(typeof(Region));
            query.AddClause(Region.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);
            query.AddClause(Region.Region_NameEntityColumn, EntityQueryOp.EQ, regionName);
            EntityList<Region> queryRegion = pm.GetEntities<Region>(query);
            if (queryRegion.Count > 0)
            {
                return;
            }

            // Check if any of the tip revision or historic Rx sites are already using the name
            var rxSitesQuery = new ImpacRdbQuery(typeof(PrescriptionSite));
            rxSitesQuery.AddClause(PrescriptionSiteDataRow.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);
            rxSitesQuery.AddClause(PrescriptionSiteDataRow.SiteNameEntityColumn, EntityQueryOp.EQ, regionName);
            if (pm.GetEntities<PrescriptionSite>(rxSitesQuery).Count > 0)
            {
                return;
            }

            // Create an entry in the Region table (new REG_ID will be used later)
            Region region = Region.Create(pm);
            region.Pat_ID1 = patId1;
            region.Region_Name = regionName;
            try
            {
                pm.SaveChanges();
            }
            catch
            {
                return;
            }

            // Go through each Rx Site given and collect the treatment field properties
            var txField = new EntityList<Field>();
            foreach (var t in rxSite)
            {
                query = new ImpacRdbQuery(typeof(PrescriptionSite));
                query.AddClause(PrescriptionSite.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);
                query.AddClause(PrescriptionSite.SIT_IDEntityColumn, EntityQueryOp.EQ, t.SIT_ID);
                query.AddClause(PrescriptionSite.VersionEntityColumn, EntityQueryOp.EQ, 0);
                EntityList<PrescriptionSite> site = pm.GetEntities<PrescriptionSite>(query);

                if (site.Count == 1)
                {
                    query = new ImpacRdbQuery(typeof (Field));
                    query.AddClause(Field.SIT_Set_IDEntityColumn, EntityQueryOp.EQ, site[0].SIT_SET_ID);
                    query.AddClause(Field.VersionEntityColumn, EntityQueryOp.EQ, 0);
                    txField.AddRange(pm.GetEntities<Field>(query));
                }
            }

            // Now add for each field an entry in the Coeff table with a link to the Region entry earlier
            int fieldsInserted = 0;
            try
            {
                foreach (var t in txField)
                {
                    Coeff coeff = Coeff.Create(pm);
                    coeff.Pat_ID1 = patId1;
                    coeff.FLD_SET_ID = t.FLD_SET_ID;
                    coeff.REG_ID = region.REG_ID;
                    coeff.Reg_Coeff = doseCoeff;
                    coeff.IsFromDataImport = false;
                    coeff.IsModifiedAfterDataImport = false;
                    pm.SaveChanges();
                    fieldsInserted++;
                }
            }
            finally 
            {
                DoseSiteCreated.Set(context, fieldsInserted == txField.Count);
            }
        }

        #endregion
    }
}