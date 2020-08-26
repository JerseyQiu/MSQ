using System;
using Impac.Mosaiq.BOM.Entities;
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
    /// This activity allows the user to select a QCL Procedure in the UI and outputs the primary key to
    /// the output parameter.
    /// </summary>
    [IQCodeGroupVarActivity_DisplayName]
    [PracticeManagement_Category]
    [Variables_ActivityGroup]
    public sealed class IQCodeGroupVarActivity : IQVariableActivityEntity<int, Prompt, IQIntegerVar, IQPromptVarDetail, IQCodeGroupVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> The configuration class for the IQVarQclProcActivity</summary>
    [Serializable]
    public class IQCodeGroupVarConfig : IQVariableConfigEntity<int, Prompt, IQIntegerVar, IQPromptVarDetail>
    {
        /// <summary> Opens the prompt browse for QCL Procedures. </summary>
        /// <returns></returns>
        protected override IQOpResult<int> OpenEditor(int defaultValue, IQVarElementTarget target)
        {
            OnBeforeClarionEditorInvoke(this);
            int proId = CallClarion.CallPromptBro("CHG1", true, 2, false, Strings.Group);
            OnAfterClarionEditorInvoke(this);

            //If the user hit "close", don't update the primaryKeyValue
            return (proId != 0)
                       ? new IQOpResult<int> { Result = OpResultEnum.Completed, Value =  proId}
                       : new IQOpResult<int> { Result = OpResultEnum.Cancelled };
        }
    }
    #endregion
}