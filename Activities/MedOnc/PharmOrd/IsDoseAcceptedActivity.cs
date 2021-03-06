using System.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Extensions.Interface;

namespace Impac.Mosaiq.IQ.Activities.MedOnc.PharmOrd
{
    /// <summary>
    /// This activity checks to see if a calc factor variance passed in has already been allowed by an order
    /// processed earlier in the batch
    /// </summary>
    [IsDoseAcceptedActivity_DisplayName]
    [PharmOrd_Category]
    [MedOnc_ActivityGroup]
    public class IsDoseAcceptedActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary> The primary key of the drug for which the variance is being checked. </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [Drg_Id_DisplayName]
        [IsDoseAcceptedActivity_Drg_Id_Description]
        public InArgument<int> DrgId { get; set; }

        /// <summary>
        /// The current difference between the calc factor on the drug and the current calc factor.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [IsDoseAcceptedActivity_Dose_DisplayName]
        [IsDoseAcceptedActivity_Dose_Description]
        public InArgument<float> Dose { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary> Returns whether the variance is allowed or not </summary>
        [OutputParameterCategory]
        [IsDoseAcceptedActivity_Result_Description]
        [IsDoseAcceptedActivity_Result_DisplayName]
        public OutArgument<bool> Result { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Checks if the calc factor variance is valid based on previous acceptances. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            int drgId = DrgId.Get(context);
            float dose = Dose.Get(context);
            var ext = context.GetExtension<IOrderApprovalExtension>();
            Result.Set(context, ext.IsDoseAccepted(drgId, dose));
        }
        #endregion
    }
}