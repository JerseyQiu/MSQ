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
    [IQTimeSpanRelativeVarActivity_DisplayName]
    [Common_Category]
    [Variables_ActivityGroup]
    public class IQTimeSpanRelativeVarActivity : IQVariableActivitySimple<TimeSpanRelative, IQTimeSpanRelativeVar, IQTimeSpanRelativeVarDetail, IQTimeSpanRelativeVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> The configuration class for the IQVarStringActivity class</summary>
    [Serializable]
    public class IQTimeSpanRelativeVarConfig : IQVariableConfigSimple<TimeSpanRelative, IQTimeSpanRelativeVar, IQTimeSpanRelativeVarDetail>
    {
        #region Overrides
        /// <summary> Opens the string editor. </summary>
        /// <returns></returns>
        protected override IQOpResult<TimeSpanRelative> OpenEditor(TimeSpanRelative defaultValue, IQVarElementTarget target)
        {
            if (defaultValue == null)
                defaultValue = new TimeSpanRelative {Count = 0, Unit = TimeSpanRelativeUnit.Days};

            var editor = new TimeSpanRelativeValueEditor {Value = defaultValue};

            return (editor.ShowDialog() == DialogResult.OK)
                       ? new IQOpResult<TimeSpanRelative> {Result = OpResultEnum.Completed, Value = editor.Value}
                       : new IQOpResult<TimeSpanRelative> {Result = OpResultEnum.Cancelled};
        }
        #endregion
    }
    #endregion
}
