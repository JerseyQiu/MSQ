using System;
using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Charting.CommonUtils;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.MedOnc.PharmOrd
{
    /// <summary> This activity gets a suggested dose for a pharmacy order based on the BSA provided. </summary>
    [GetSuggestedDoseBsaActivity_DisplayName]
    [PharmOrd_Category]
    [MedOnc_ActivityGroup]
    public class GetSuggestedDoseActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary> Primary key of the PharmOrd record which we will get a suggested dose for.</summary>
        [InputParameterCategory]
        [RequiredArgument]
        [GetSuggestedDoseBsaActivity_Rxo_Id_DisplayName]
        [GetSuggestedDoseBsaActivity_Rxo_Id_Description]
        public InArgument<int> RxoId { get; set; }

        /// <summary> The BSA which will be used for the calculation </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [GetSuggestedDoseBsaActivity_BSA_DisplayName]
        [GetSuggestedDoseBsaActivity_BSA_Description]
        public InArgument<Double> BSA { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary> The suggested dose calculated from this activity. </summary>
        [OutputParameterCategory]
        [GetSuggestedDoseBsaActivity_BSA_DisplayName]
        [GetSuggestedDoseBsaActivity_BSA_Description]
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
            double bsa = BSA.Get(context);

            ImpacPersistenceManager pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
            var order = pm.GetEntity<BOM.Entities.PharmOrd>(new PrimaryKey(typeof(BOM.Entities.PharmOrd), rxoId));

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
