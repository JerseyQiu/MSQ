using System;
using System.Activities;
using System.Data.SqlTypes;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using IdeaBlade.Persistence;

namespace Impac.Mosaiq.IQ.Activities.Queries.RadOnc
{
    /// <summary> Activity used to display an information message </summary>
    [FindDoseHistoryByPatId1Activity_DisplayName]
    [RadiationOncology_Category]
    [Queries_ActivityGroup]
    public class FindDoseHistoryByPatId1Activity : MosaiqQueryListActivity<DoseHst>
    {

        #region Properties (Input Parameters)
        /// <summary>  The primary key of the patient to query. </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindDoseHistoryByPatId1Activity_PatId_DisplayName]
        [FindDoseHistoryByPatId1Activity_PatId_Description]
        public InArgument<int> PatId1 { get; set; }

        /// <summary> SIT_ID of Rx Site to query as input, if not provided than for all Sites </summary>
        [InputParameterCategory]
        [FindDoseHistoryByPatId1Activity_SIT_ID_DisplayName]
        [FindDoseHistoryByPatId1Activity_SIT_ID_Description]
        public InArgument<int> SitId { get; set; }

        /// <summary> Treatment Start Date to use in the in between clause if supplied </summary>
        [InputParameterCategory]
        [FindDoseHistoryByPatId1Activity_StartDate_DisplayName]
        [FindDoseHistoryByPatId1Activity_StartDate_Description]
        public InArgument<DateTime> StartDate { get; set; }

        /// <summary> Treatment End Date to use in the in between clause if supplied </summary>
        [InputParameterCategory]
        [FindDoseHistoryByPatId1Activity_EndDate_DisplayName]
        [FindDoseHistoryByPatId1Activity_EndDate_Description]
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
            // Activity inputs
            int patId1 = PatId1.Get(context);
            int sitId = (SitId.Expression != null) ? SitId.Get(context) : 0;
            DateTime startDate = CheckDateTime(StartDate.Get(context));
            DateTime endDate = CheckDateTime(EndDate.Get(context));

            // Get the Dose_Hst entries for the given patient
            pQuery.AddClause(DoseHst.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);

            // Filter on SIT_ID if supplied
            if (sitId != 0)
            {
               pQuery.AddClause(DoseHst.SIT_IDEntityColumn, EntityQueryOp.EQ, sitId);
            }

            // Filter on StartDate if supplied
            if (startDate > SqlDateTime.MinValue.Value)
            {
                pQuery.AddClause(DoseHst.Tx_DtTmEntityColumn, EntityQueryOp.GE, startDate);
            }

            // Filter on EndDate if supplied
            if (endDate > SqlDateTime.MinValue.Value)
            {
                pQuery.AddClause(DoseHst.Tx_DtTmEntityColumn, EntityQueryOp.LE, endDate);
            }

        }
        #endregion
    }
}