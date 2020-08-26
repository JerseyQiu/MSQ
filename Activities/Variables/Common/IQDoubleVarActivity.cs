using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Windows.Forms;
using Impac.Mosaiq.Core.Toolbox.Ropes;
using Impac.Mosaiq.IQ.Activities.Variables.Common.Details;
using Impac.Mosaiq.IQ.Activities.Variables.Common.Editor;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common
{
    #region Activity Class
    /// <summary> IQ variable activity for collecting a "double" value </summary>
    [IQDoubleVarActivity_DisplayName]
    [Common_Category]
    [Variables_ActivityGroup]
    public class IQDoubleVarActivity : IQVariableActivitySimple<double, IQDoubleVar, IQDoubleVarDetail, IQDoubleVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> The configuration class for the IQVarStringActivity class</summary>
    [Serializable]
    public class IQDoubleVarConfig : IQVariableConfigSimple<double, IQDoubleVar, IQDoubleVarDetail>
    {
        //Do not change the names or types of the properties in the region below as they are serialized to the IQ Script 
        //XAML.  Changing the names or types will cause any script which uses this activity to break.
        #region Properties (Serialized to XAML)
        /// <summary> Stores the minimum allowed value </summary>
        [RestoreInclude]
        [Constraints_Category]
        [MinimumValue_DisplayName]
        public Double? MinValue { get; set; }

        /// <summary> Stores the maximum allowed value </summary>
        [RestoreInclude]
        [Constraints_Category]
        [MaximumValue_DisplayName]
        public Double? MaxValue { get; set; }
        #endregion

        #region Overrides
        /// <summary> Opens the string editor. </summary>
        /// <returns></returns>
        protected override IQOpResult<double> OpenEditor(double defaultValue, IQVarElementTarget target)
        {
            var editor = new DoubleValueEditor
                             {
                                 //If a single select custom value is set, load that value.
                                 Value = defaultValue,
                                 MinValue = MinValue,
                                 MaxValue = MaxValue
                             };

            return (editor.ShowDialog() == DialogResult.OK)
                       ? new IQOpResult<double> {Result = OpResultEnum.Completed, Value = editor.Value}
                       : new IQOpResult<double> {Result = OpResultEnum.Cancelled};
        }

        /// <summary> Allows validation of a value. </summary>
        /// <param name="errors"></param>
        /// <param name="value"></param>
        /// <param name="valueType"></param>
        protected override void ValidateValue(IList<ValidationError> errors, double value, IQVarElementTarget valueType)
        {
            ValidateValueCheckInRange(errors, value, valueType);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Checks to see if the value is within the range of the constraints provided.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueType"></param>
        /// <param name="errors"></param>
        private void ValidateValueCheckInRange(IList<ValidationError> errors, double value, IQVarElementTarget valueType)
        {
            string message = String.Empty;
            //Min and max values are defined.
            if (MinValue.HasValue && MaxValue.HasValue && (value < MinValue.Value || value > MaxValue.Value))
            {
                message = Rope.Format(Strings.DoubleMustBeBetween,
                                      valueType == IQVarElementTarget.Selected
                                          ? Strings.SelectedValue
                                          : Strings.PredefinedValue,
                                      value, MinValue.Value, MaxValue.Value);
            }

            //Only minimum value is defined.
            else if (MinValue.HasValue && !MaxValue.HasValue && (value < MinValue.Value))
            {
                message = Rope.Format(Strings.DoubleMustBeGreaterThan,
                                      valueType == IQVarElementTarget.Selected
                                          ? Strings.SelectedValue
                                          : Strings.PredefinedValue,
                                      value, MinValue.Value);
            }

                //Only maximum value is defined.
            else if (!MinValue.HasValue && MaxValue.HasValue && (value > MaxValue.Value))
            {
                message = Rope.Format(Strings.DoubleMustBeLessThan,
                                      valueType == IQVarElementTarget.Selected
                                          ? Strings.SelectedValue
                                          : Strings.PredefinedValue,
                                      value, MaxValue.Value);
            }

            if (!String.IsNullOrWhiteSpace(message))
                errors.Add(new ValidationError(message, false, "Config"));
        }
        #endregion
    }
    #endregion
}
