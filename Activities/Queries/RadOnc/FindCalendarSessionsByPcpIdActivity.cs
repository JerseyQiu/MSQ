using System;
using System.Activities;
using System.Data.SqlTypes;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Queries.RadOnc
{
    /// <summary>
    /// Returns a list of Patient CarePlan items (calendar sessions) from the database.
    /// </summary>
    [FindCalendarSessionsByPcpIdActivity_DisplayName]
    [RadiationOncology_Category]
    [Queries_ActivityGroup]
    public class FindCalendarSessionsByPcpIdActivity : MosaiqQueryListActivity<PatCItem>
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The primary key of the Patient
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindCalendarSessionsByPcpIdActivity_PatId_DisplayName]
        [FindCalendarSessionsByPcpIdActivity_PatId_Description]
        public InArgument<int> PatId { get; set; }

        /// <summary> 
        /// The primary key of the Course (Care Plan) of which to return the calendar sessions
        /// from the database. 
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindCalendarSessionsByPcpIdActivity_PcpId_DisplayName]
        [FindCalendarSessionsByPcpIdActivity_PcpId_Description]
        public InArgument<int> PcpId { get; set; }

        /// <summary> Session Start Date (Due_DtTm) to use in the in between clause if supplied </summary>
        [InputParameterCategory]
        [FindCalendarSessionsByPcpIdActivity_StartDate_DisplayName]
        [FindCalendarSessionsByPcpIdActivity_StartDate_Description]
        public InArgument<DateTime> StartDate { get; set; }

        /// <summary> Session End Date (Due_DtTm) to use in the in between clause if supplied </summary>
        [InputParameterCategory]
        [FindCalendarSessionsByPcpIdActivity_EndDate_DisplayName]
        [FindCalendarSessionsByPcpIdActivity_EndDate_Description]
        public InArgument<DateTime> EndDate { get; set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// Check limits of a DateTime value
        /// </summary>
        /// <param name="dt">DateTime value to check</param>
        /// <returns>DateTime value within limits of SqlDateTime</returns>
        private DateTime CheckDateTime(DateTime dt)
        {
            if (dt <= SqlDateTime.MinValue.Value)
            {
                return SqlDateTime.MinValue.Value;
            }
            else if (dt >= SqlDateTime.MaxValue.Value)
            {
                return SqlDateTime.MaxValue.Value;
            }
            return dt;
        }

        #endregion

        #region Overriden Methods
        /// <summary> Populates a query object created by the base class. </summary>
        /// <param name="context"></param>
        /// <param name="pQuery"></param>
        protected override void PopulateQuery(CodeActivityContext context, ImpacRdbQuery pQuery)
        {
            // Activity input
            int patId = PatId.Get(context);
            int pcpId = PcpId.Get(context);

            // In case startDate/endDate has input specified (Null/Nothing) it defaults to
            // DateTime.MinValue (0001-01-01 01:01:01).
            DateTime startDate = CheckDateTime(StartDate.Get(context));
            DateTime endDate = CheckDateTime(EndDate.Get(context));

            // Build the query
            pQuery.AddClause(PatCItem.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId);
            pQuery.AddClause(PatCItem.PCP_IDEntityColumn, EntityQueryOp.EQ, pcpId);

            // Filter on StartDate if supplied
            if (startDate > SqlDateTime.MinValue.Value)
            {
                pQuery.AddClause(PatCItem.Due_DtTmEntityColumn, EntityQueryOp.GE, startDate);
            }

            // Filter on EndDate if supplied
            if (endDate > SqlDateTime.MinValue.Value)
            {
                pQuery.AddClause(PatCItem.Due_DtTmEntityColumn, EntityQueryOp.LE, endDate);
            }
       }
        #endregion
    }
}
