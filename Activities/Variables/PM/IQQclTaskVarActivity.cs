using System;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Core.Defs.Constants;
using Impac.Mosaiq.Core.Security.SecurityLib;
using Impac.Mosaiq.IQ.Activities.Variables.PM.Details;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;
using Impac.Mosaiq.UI.Framework;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM
{
    #region Activity Class
    /// <summary>
    /// This activity allows the user to select a QCL Task in the UI and outputs the primary key (Tsk_ID) to
    /// the output parameter.
    /// </summary>
    [IQQclTaskVarActivity_DisplayName]
    [PracticeManagement_Category]
    [Variables_ActivityGroup]
    public sealed class IQQclTaskVarActivity : IQVariableActivityEntity<int, QCLTask, IQIntegerVar, IQQclTaskVarDetail, IQQclTaskVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> The configuration class for the IQVarQclProcActivity</summary>
    [Serializable]
    public class IQQclTaskVarConfig : IQVariableConfigEntity<int, QCLTask, IQIntegerVar, IQQclTaskVarDetail>
    {
        /// <summary> Opens the prompt browse for QCL Procedures. </summary>
        /// <returns></returns>
        protected override IQOpResult<int> OpenEditor(int currentValue, IQVarElementTarget target)
        {
            var wc = new WidgetContract(NTStrings.IQQclTaskVarActivity_QclTaskBrowse_ClassName, 
                NTStrings.IQQclTaskVarActivity_QclTaskBrowse_InstanceName, 
                WidgetContract.RequestOptions.DialogResult);

            // If the current value is 0 do not pass it into the control since we internalize 0 as not valid
            if (currentValue != 0)
            {
                wc.CustomData.Add(StringConstants.KEY_SELECTEDID, currentValue);
            }
            wc.WidgetTitle = Strings.IQQclTaskVarActivity_WidgetTitle;
            wc.CustomData.Add(NTStrings.IQQclTaskVarActivity_QclTaskBrowse_CD_NoEdit, true);
            wc.CustomData.Add(NTStrings.IQQclTaskVarActivity_QclTaskBrowse_CD_CheckSecurity, true);
            wc.SecEnums = SecurityEnums.EC_GEN;

            WidgetManager.AddWidgetModule(NTStrings.IQQclTaskVarActivity_QclTaskBrowse_AssemblyName);
            WidgetManager.ActivateWidget(null, wc);
            
            // Open the browse as a modal window
            wc.ActiveWidget.ShowDialog();

            //If the user hit "close", don't update the primaryKeyValue
            return wc.Result == WidgetContract.ResultOptions.Completed
                ? new IQOpResult<int> {Result = OpResultEnum.Completed, Value = (int) wc.CustomData[StringConstants.KEY_SELECTEDID]}
                : new IQOpResult<int> { Result = OpResultEnum.Cancelled };
        }
    }
    #endregion
}