using System.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Extensions.Interface;

namespace Impac.Mosaiq.IQ.Activities.MedOnc.PharmOrd
{
    /// <summary>
    /// This activity records a dose which has been accepted as part of the IQ Engine rule processing.  This information
    /// can be looked up scripts running against other drugs as part of the same "approval" batch, allowing logic defined
    /// in a script to be skipped if a decision has already been made for the same drug/dose combination.
    /// </summary>
    [AcceptDoseActivity_DisplayName]
    [PharmOrd_Category]
    [MedOnc_ActivityGroup]
    public class AcceptDoseActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary> The primary key of the drug. </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [Drg_Id_DisplayName]
        [AcceptDoseActivity_Drg_Id_Description]
        public InArgument<int> DrgId { get; set; }

        /// <summary> The accepted dose of the drug. </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [AcceptDoseActivity_AcceptedDose_Description]
        [AcceptDoseActivity_AcceptedDose_DisplayName]
        public InArgument<float> AcceptedDose { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Records the accepted variance passed in.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            int drgId = DrgId.Get(context);
            float acceptedDose = AcceptedDose.Get(context);
            var ext = context.GetExtension<IOrderApprovalExtension>();
            ext.AcceptDose(drgId, acceptedDose);
        }
        #endregion
    }
}