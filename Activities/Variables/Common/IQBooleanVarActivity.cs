using System;
using System.Windows.Forms;
using Impac.Mosaiq.IQ.Activities.Variables.Common.Details;
using Impac.Mosaiq.IQ.Activities.Variables.Common.Editor;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common
{
    #region Activity Class
    /// <summary> IQ variable activity for collecting a "double" value </summary>
    [IQBooleanVarActivity_DisplayName]
    [Common_Category]
    [Variables_ActivityGroup]
    public class IQBooleanVarActivity : IQVariableActivitySimple<Boolean, IQBooleanVar, IQBooleanVarDetail, IQBooleanVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> The configuration class for the IQVarStringActivity class</summary>
    [Serializable]
    public class IQBooleanVarConfig : IQVariableConfigSimple<Boolean, IQBooleanVar, IQBooleanVarDetail>
    {
        #region Overrides
        /// <summary> Opens the string editor. </summary>
        /// <returns></returns>
        protected override IQOpResult<Boolean> OpenEditor(Boolean defaultValue, IQVarElementTarget target)
        {
            var editor = new BooleanValueEditor {Value = defaultValue};

            return (editor.ShowDialog() == DialogResult.OK)
                       ? new IQOpResult<Boolean> {Result = OpResultEnum.Completed, Value = editor.Value}
                       : new IQOpResult<Boolean> {Result = OpResultEnum.Cancelled};
        }
        #endregion
    }
    #endregion
}
