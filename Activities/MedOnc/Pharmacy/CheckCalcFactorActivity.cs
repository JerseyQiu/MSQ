using System;
using System.Linq;
using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.MedOnc.Pharmacy
{
    /// <summary>
    /// Checks whether a calculation factor for this drug has previously been accepted.
    /// </summary>
    [CheckCalcFactorActivity_DisplayName]
    [PharmOrd_Category]
    [MedOnc_ActivityGroup]
    public class CheckCalcFactorActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary> The Rxo_Set_Id of the pharmacy order to check. </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [CheckCalcFactorActivity_RxoSetId_DisplayName]
        [CheckCalcFactorActivity_RxoSetId_Description]
        public InArgument<int> RxoSetId { get; set; }

        /// <summary> The accepted calculation factor. </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [CheckCalcFactorActivity_CalcFactor_DisplayName]
        [CheckCalcFactorActivity_CalcFactor_Description]
        public InArgument<Double> CalcFactor { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary> Returns whether the calculation factor has been previously accepted </summary>
        [OutputParameterCategory]
        [CheckCalcFactorActivity_Result_Description]
        [CheckCalcFactorActivity_Result_DisplayName]
        public OutArgument<bool> Result { get; set; }
        #endregion

        #region Overrides of MosaiqCodeActivity
        /// <summary>
        /// Checks the database to see if the PharmCalcAccept table contains a record indicating that the
        /// calculation factor passed has been accepted..
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            int rxoSetId = RxoSetId.Get(context);
            double calcFactor = CalcFactor.Get(context);
            bool result = false;

            //Lookup all of the PharmCalcAccept records to get the minimum and maximum accepted values.
            EntityList<PharmCalcAccept> acceptRecords = PharmCalcAccept.FindByRxoSetId(PM, QueryStrategy.Normal, rxoSetId);
            PharmOrd pharmOrd = PharmOrd.GetEntityByRxoSetIdAndVersion0(rxoSetId, PM);

            var maxRecords = acceptRecords.Where(e => e.Accepted_High.HasValue);
            var minRecords = acceptRecords.Where(e => e.Accepted_Low.HasValue);

            double? maxAccepted = maxRecords.Count() == 0 ? (double?)null : maxRecords.Max(e => e.Accepted_High.Value);
            double? minAccepted = minRecords.Count() == 0 ? (double?)null : minRecords.Min(e => e.Accepted_Low.Value);

            if (maxAccepted != null && calcFactor <= maxAccepted && calcFactor >= pharmOrd.BSA)
                result = true;

            if (minAccepted != null && calcFactor >= minAccepted && calcFactor <= pharmOrd.BSA)
                result = true;

            Result.Set(context, result);
        }
        #endregion
    }
}
