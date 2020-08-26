using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Queries.RadOnc
{
    /// <summary>
    /// Returns a list of Treatment Calendar items (items in given session) from the database.
    /// </summary>
    [FindCalendarItemsByPciIdActivity_DisplayName]
    [RadiationOncology_Category]
    [Queries_ActivityGroup]
    public class FindCalendarItemsByPciIdActivity : MosaiqQueryListActivity<PatTxCal>
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The primary key of the Patient
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindCalendarItemsByPciIdActivity_PatId_DisplayName]
        [FindCalendarItemsByPciIdActivity_PatId_Description]
        public InArgument<int> PatId { get; set; }

        /// <summary> 
        /// The primary key of the Patient CarePlan item (Calendar session) of which to
        /// return the Treatment Calendar objects from the database. 
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindCalendarItemsByPciIdActivity_PciId_DisplayName]
        [FindCalendarItemsByPciIdActivity_PciId_Description]
        public InArgument<int> PciId { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Populates a query object created by the base class. </summary>
        /// <param name="context"></param>
        /// <param name="pQuery"></param>
        protected override void PopulateQuery(CodeActivityContext context, ImpacRdbQuery pQuery)
        {
            // Activity input
            int patId = PatId.Get(context);
            int pciId = PciId.Get(context);

            // Build the query
            pQuery.AddClause(PatTxCal.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId);
            pQuery.AddClause(PatTxCal.PCI_IDEntityColumn, EntityQueryOp.EQ, pciId);
        }
        #endregion
    }
}
