using System.Activities;
using Impac.Mosaiq.Core.Globals;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.ActiveValues
{
    /// <summary>
    /// Gets Active Patient ID
    /// </summary>
    [Tools_ActivityGroup]
    [ActiveValue_Category]
    [GetActivePatientIdActivity_DisplayName]
    public class GetActivePatientIdActivity : MosaiqCodeActivity
    {
        #region Properties (Output Arugments)
        /// <summary> Active Patient Id, if the value is less then 1 then it is null. </summary>
        [OutputParameterCategory]
        [GetActivePatientIdActivity_PatientId_DisplayName]
        [GetActivePatientIdActivity_PatientId_Description]
        public OutArgument<int?> PatientId { get; set; }
        #endregion

        /// <summary>
        /// Returns the Patient Id for the active user, null otherwise
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            PatientId.Set(context, Global.PatientId1 > 0 ? (int?)Global.PatientId1 : null);
        }
    }
}
