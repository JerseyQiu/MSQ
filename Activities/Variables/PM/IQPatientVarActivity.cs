using System;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Core.Globals;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Activities.Variables.PM.Details;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM
{
    #region Activity Class
    /// <summary>
    /// This activity allows the user to select a patient in the UI and outputs the primary key to
    /// the output parameter.
    /// </summary>
    [IQPatientVarActivity_DisplayName]
    [PracticeManagement_Category]
    [Variables_ActivityGroup]
    public sealed class IQPatientVarActivity : IQVariableActivityEntity<int, Patient, IQIntegerVar, IQPatientVarDetail, IQPatientVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> The configuration class for the IQVarPatientActivity</summary>
    [Serializable]
    public class IQPatientVarConfig : IQVariableConfigEntity<int, Patient, IQIntegerVar, IQPatientVarDetail>
    {
        #region Overriden Methods
        /// <summary> Opens the patient browse </summary>
        /// <returns></returns>
        protected override IQOpResult<int> OpenEditor(int defaultValue, IQVarElementTarget target)
        {
            OnBeforeClarionEditorInvoke(this);
            int patId1 = CallClarion.CallPatientBro(true, true, true);
            OnAfterClarionEditorInvoke(this);

            //If the user hit "close", don't update the primaryKeyValue
            return (patId1 != -1)
                       ? new IQOpResult<int> { Result = OpResultEnum.Completed, Value = patId1}
                       : new IQOpResult<int> { Result = OpResultEnum.Cancelled };
        }

        /// <summary> Current is supported for "Patient" </summary>
        public override bool SupportsCurrent
        {
            get { return true; }
        }

        /// <summary> 
        /// Override of the CurrentValueText property, so that the user selected "Active Patient" instead of "Current"
        /// </summary>
        public override string CurrentValueText { get { return Strings.IQPatientVarActivity_ActivePatient; } }

        /// <summary>
        /// Returns the "current" value of the object at script execution time.  Only used for activities that support
        /// the "current" state. 
        /// </summary>
        /// <returns></returns>
        protected override int GetCurrentValue()
        {
            return Global.PatientId1;
        }
        #endregion
    }
    #endregion
}