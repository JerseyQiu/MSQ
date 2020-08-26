using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Queries.RadOnc
{
    /// <summary>
    /// Returns a list of Dose Tracking Sites items from the database.
    /// </summary>
    [FindDoseTrackingSitesByPatId1Activity_DisplayName]
    [RadiationOncology_Category]
    [Queries_ActivityGroup]
    public class FindDoseTrackingSitesByPatId1Activity : MosaiqQueryListActivity<Region>
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The patient ID (Pat_Id1) for which to retrieve dose tracking site objects 
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindDoseTrackingSitesByPatId1Activity_PatId1_DisplayName]
        [FindDoseTrackingSitesByPatId1Activity_PatId1_Description]
        public InArgument<int> PatId1 { get; set; }

        /// <summary>
        /// If true, only primary dose tracking site objects will be returned
        /// </summary>
        [InputParameterCategory]
        [FindDoseTrackingSitesByPatId1Activity_PrimaryDoseSitesOnly_DisplayName]
        [FindDoseTrackingSitesByPatId1Activity_PrimaryDoseSitesOnly_Description]
        public InArgument<bool> PrimaryDoseSitesOnly { get; set; }

        /// <summary>
        /// If true, only secondary dose tracking site objects will be returned
        /// </summary>
        [InputParameterCategory]
        [FindDoseTrackingSitesByPatId1Activity_SecondaryDoseSitesOnly_DisplayName]
        [FindDoseTrackingSitesByPatId1Activity_SecondaryDoseSitesOnly_Description]
        public InArgument<bool> SecondaryDoseSitesOnly { get; set; }

        /// <summary> 
        /// The Site ID (Sit_Id) for which to return the dose tracking site object
        /// </summary>
        [InputParameterCategory]
        [FindDoseTrackingSitesByPatId1Activity_SitId_DisplayName]
        [FindDoseTrackingSitesByPatId1Activity_SitId_Description]
        public InArgument<int> SitId { get; set; }

        #endregion

        #region Overriden Methods
        /// <summary> Populates a query object created by the base class. </summary>
        /// <param name="context">Activity context</param>
        /// <param name="pQuery">MOSAIQ BOM Query object</param>
        protected override void PopulateQuery(CodeActivityContext context, ImpacRdbQuery pQuery)
        {
            // Activity input
            int patId1 = PatId1.Get(context);
            bool primaryDoseSitesOnly = PrimaryDoseSitesOnly.Get(context);
            bool secondaryDoseSitesOnly = SecondaryDoseSitesOnly.Get(context);
            int sitId = SitId.Get(context);

            // Build the query
            pQuery.AddClause(Region.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);

            // Primary Dose Sites Only
            if (primaryDoseSitesOnly)
            {
                pQuery.AddClause(Region.SIT_Set_IDEntityColumn, EntityQueryOp.IsNotNull);
            }

            // Secondary Dose Sites Only
            if (secondaryDoseSitesOnly)
            {
                pQuery.AddClause(Region.SIT_Set_IDEntityColumn, EntityQueryOp.IsNull);
            }

            // Specific Sit_ID
            if (sitId != 0)
            {
                pQuery.AddClause(Region.SIT_Set_IDEntityColumn, EntityQueryOp.EQ, sitId);      
            }
        }
        #endregion
    }
}
