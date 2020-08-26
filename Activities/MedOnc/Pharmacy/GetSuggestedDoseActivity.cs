using System;
using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Charting.CommonUtils;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.MedOnc.Pharmacy
{
    /// <summary> This activity gets a suggested dose for a pharmacy order based on the BSA provided. </summary>
    [GetSuggestedDoseActivity_DisplayName]
    [PharmOrd_Category]
    [MedOnc_ActivityGroup]
    public class GetSuggestedDoseActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary> Primary key of the PharmOrd record which we will get a suggested dose for.</summary>
        [InputParameterCategory]
        [RequiredArgument]
        [GetSuggestedDoseActivity_Rxo_Id_DisplayName]
        [GetSuggestedDoseActivity_Rxo_Id_Description]
        public InArgument<int> RxoId { get; set; }

        /// <summary> The BSA which will be used for the calculation </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [GetSuggestedDoseActivity_CalcFactor_DisplayName]
        [GetSuggestedDoseActivity_CalcFactor_Description]
        public InArgument<Double> CalcFactor { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary> The suggested dose calculated from this activity. </summary>
        [OutputParameterCategory]
        [GetSuggestedDoseActivity_SuggestedDose_DisplayName]
        [GetSuggestedDoseActivity_SuggestedDose_Description]
        public OutArgument<Double> SuggestedDose { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Estimates the dose based on the pharmacy order and calculation factor passed in.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            int rxoId = RxoId.Get(context);
            double bsa = CalcFactor.Get(context);

            var order = PM.GetEntity<PharmOrd>(new PrimaryKey(typeof(PharmOrd), rxoId));

            if (!order.IsNullEntity)
            {
                double calculatedDose;
                if (DoseCalcUtils.EstimateOrderingDose(order, bsa, out calculatedDose) == DoseCalcUtils.CalcStatus.CalcSucceed)
                    SuggestedDose.Set(context, calculatedDose);
            }
        }
        #endregion
    }
}
