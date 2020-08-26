using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Queries.Config
{
    /// <summary>
    /// Returns a Config object from the database.
    /// </summary>
    [FindDepartmentConfigurationActivity_DisplayName]
    [Configuration_Category]
    [Queries_ActivityGroup]
    public class FindDepartmentConfigurationActivity : MosaiqQueryActivity<BOM.Entities.Config>
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The Institution ID of the department to return the config of (default = 1)
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindDepartmentConfigurationActivity_InstId_DisplayName]
        [FindDepartmentConfigurationActivity_InstId_Description]
        public InArgument<int> InstId { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Populates a query object created by the base class. </summary>
        /// <param name="context"></param>
        /// <param name="pQuery"></param>
        protected override void PopulateQuery(CodeActivityContext context, ImpacRdbQuery pQuery)
        {
            // Activity input
            int instId = InstId.Get(context);

            // Build the query
            pQuery.AddClause(BOM.Entities.Config.Inst_IDEntityColumn, EntityQueryOp.EQ, instId);

        }
        #endregion

    }
}
