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
    /// This activity allows the user to select a location in the UI and outputs the primary key to
    /// the output parameter.
    /// </summary>
    [IQLocationVarActivity_DisplayName]
    [PracticeManagement_Category]
    [Variables_ActivityGroup]
    public sealed class IQLocationVarActivity : IQVariableActivityEntity<int, Staff, IQIntegerVar, IQStaffVarDetail, IQLocationVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary>Configuration class for the IQVarLocationActivity </summary>
    [Serializable]
    public class IQLocationVarConfig : IQVariableConfigEntity<int, Staff, IQIntegerVar, IQStaffVarDetail>
    {
        /// <summary>
        /// Opens the location browse.
        /// </summary>
        /// <returns></returns>
        protected override IQOpResult<int> OpenEditor(int defaultValue, IQVarElementTarget target)
        {
            OnBeforeClarionEditorInvoke(this);
            int intVal = CallClarion.CallStaffLocBro();
            OnAfterClarionEditorInvoke(this);

            //If the user hit "close", don't update the primaryKeyValue
            return (intVal != 0)
                       ? new IQOpResult<int> {Result = OpResultEnum.Completed, Value = intVal}
                       : new IQOpResult<int> {Result = OpResultEnum.Cancelled};
        }
    }
    #endregion
}