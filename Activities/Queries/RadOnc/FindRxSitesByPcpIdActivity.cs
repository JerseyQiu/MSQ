using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Queries.RadOnc
{
    /// <summary>
    /// Returns a list of Site objects from the database.
    /// </summary>
    [FindRxSitesByPcpIdActivity_DisplayName]
    [RadiationOncology_Category]
    [Queries_ActivityGroup]
    public class FindRxSitesByPcpIdActivity : MosaiqQueryListActivity<PrescriptionSite>
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The primary key of the Patient
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindRxSitesByPcpIdActivity_PatId_DisplayName]
        [FindRxSitesByPcpIdActivity_PatId_Description]
        public InArgument<int> PatId { get; set; }

        /// <summary> 
        /// The primary key of the Course (Care Plan) for which to return the Site objects from the database. 
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindRxSitesByPcpIdActivity_PcpId_DisplayName]
        [FindRxSitesByPcpIdActivity_PcpId_Description]
        public InArgument<int> PcpId { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Populates a query object created by the base class. </summary>
        /// <param name="context"></param>
        /// <param name="pQuery"></param>
        protected override void PopulateQuery(CodeActivityContext context, ImpacRdbQuery pQuery)
        {
            // Activity inputs
            int patId = PatId.Get(context);
            int pcpId = PcpId.Get(context);

            pQuery.AddClause(PrescriptionSite.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId);
            pQuery.AddClause(PrescriptionSite.PCP_IDEntityColumn, EntityQueryOp.EQ, pcpId);
            pQuery.AddClause(PrescriptionSite.VersionEntityColumn, EntityQueryOp.EQ, 0);
            pQuery.AddOrderBy(PrescriptionSite.DisplaySequenceEntityColumn);
        }
        #endregion
    }
}
