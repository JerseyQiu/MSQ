using System.Activities;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.MedOnc.Pharmacy
{
    /// <summary> Performs dose adjustment on the order passed in. </summary>
    [DoseAdjustFormActivity_DisplayName]
    [PharmOrd_Category]
    [MedOnc_ActivityGroup]
    public class DoseAdjustFormActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary> The primary key of the drug for which the variance is being checked. </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [DoseAdjustFormActivity_Orc_Set_Id_DisplayName]
        [DoseAdjustFormActivity_Orc_Set_Id_Description]
        public InArgument<int> OrcSetId { get; set; }

        /// <summary>
        /// The current difference between the calc factor on the drug and the current calc factor.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [DoseAdjustFormActivity_UseRecentBSA_DisplayName]
        [DoseAdjustFormActivity_UseRecentBSA_Description]
        public InArgument<bool> UseRecentBSA { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary> Returns the accepted variance for drug and current variance passed in.  </summary>
        [OutputParameterCategory]
        [DoseAdjustFormActivity_Result_DisplayName]
        [DoseAdjustFormActivity_Result_Description]
        public OutArgument<bool> Result { get; set; }
        #endregion

        #region Overrides of MosaiqCodeActivity
        /// <summary> Launches the dose adjustment window. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            int orcSetId = OrcSetId.Get(context);
            bool useRecentBSA = UseRecentBSA.Get(context);

            Orders order = Orders.GetEntityBySetIDAndVersion0(orcSetId, PM);

			var patId = order.Pat_ID1.GetValueOrDefault(0);
			if (patId != Mosaiq.Core.Globals.Global.PatientId1)
				CallClarion.CallSetPatient(patId);

            bool result = CallClarion.CallReviewRXO(orcSetId, useRecentBSA ? 1 : 2, false);
            Result.Set(context, result);
        }
        #endregion
    }
}
