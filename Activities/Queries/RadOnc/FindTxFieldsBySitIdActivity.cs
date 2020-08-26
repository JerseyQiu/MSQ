using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Queries.RadOnc
{
    /// <summary>
    /// Returns a list of Tx Field objects from the database.
    /// </summary>
    [FindTxFieldsBySitIdActivity_DisplayName]
    [RadiationOncology_Category]
    [Queries_ActivityGroup]
    public class FindTxFieldsBySitIdActivity : MosaiqQueryListActivity<Field>
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The primary key of the Site of which to return Tx Field objects from the database. 
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindTxFieldsBySitIdActivity_PatId_DisplayName]
        [FindTxFieldsBySitIdActivity_PatId_Description]
        public InArgument<int> PatId { get; set; }

        /// <summary> 
        /// The primary key of the Site of which to return Tx Field objects from the database. 
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindTxFieldsBySitIdActivity_SitId_DisplayName]
        [FindTxFieldsBySitIdActivity_SitId_Description]
        public InArgument<int> SitId { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Populates a query object created by the base class. </summary>
        /// <param name="context"></param>
        /// <param name="pQuery"></param>
        protected override void PopulateQuery(CodeActivityContext context, ImpacRdbQuery pQuery)
        {
            // Activity input
            int patId = PatId.Get(context);
            int sitId = SitId.Get(context);

            var pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();

            // Get the Sit_Set_Id of the Site (Sit_Id) given
            int sitSetId;
            var query = new ImpacRdbQuery(typeof(PrescriptionSite));
            query.AddClause(PrescriptionSite.SIT_IDEntityColumn, EntityQueryOp.EQ, sitId);
            var site = pm.GetEntities<PrescriptionSite>(query);
            if (site.Count != 1)
            {
                sitSetId = -1;
            }
            else
            {
                sitSetId = (int)site[0].SIT_SET_ID;    
            }

            // Build the query
            pQuery.AddClause(Field.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId); 
            pQuery.AddClause(Field.SIT_Set_IDEntityColumn, EntityQueryOp.EQ, sitSetId);
            pQuery.AddClause(Field.VersionEntityColumn, EntityQueryOp.EQ, 0);
            pQuery.AddOrderBy(Field.DisplaySequenceEntityColumn);
        }
        #endregion
    }
}
