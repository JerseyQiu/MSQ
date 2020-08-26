using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Impac.Mosaiq.Core.Xlate;
using Impac.Mosaiq.IQ.Activities.Variables.Common.Details;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Dialogs;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common
{
    #region Activity Class
    /// <summary> IQ variable activity for collecting a "double" value </summary>
    [IQEnumVarActivity_DisplayName]
    [Common_Category]
    [Variables_ActivityGroup]
    public class IQEnumVarActivity : IQVariableActivitySimple<IQEnum, IQEnumVar, IQEnumVarDetail, IQEnumVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> The configuration class for the IQVarStringActivity class</summary>
    [Serializable]
    public class IQEnumVarConfig : IQVariableConfigSimple<IQEnum, IQEnumVar, IQEnumVarDetail>
    {
        #region Overrides

        /// <summary>
        /// Opens the IQEnumEditor
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        protected override IQOpResult<IQEnum> OpenEditor(IQEnum currentValue, IQVarElementTarget target)
        {
            if (target == IQVarElementTarget.Selected)
            {
                XlateMessageBox.Information(Strings.IQEnumVarActivity_PreselectedRequired);
                return new IQOpResult<IQEnum> {Result = OpResultEnum.Cancelled};
            }

            if (currentValue == null)
                currentValue = new IQEnum {Key = GetNextKey(), Value = String.Empty};

            var editor = new IQEnumEditor {Value = currentValue};
            return editor.ShowDialog() == DialogResult.OK
                       ? new IQOpResult<IQEnum> {Result = OpResultEnum.Completed, Value = editor.Value}
                       : new IQOpResult<IQEnum> {Result = OpResultEnum.Cancelled};
        }

        /// <summary>
        /// Overriden to prevent user from opening the selction window if no pre-defined values are selected
        /// </summary>
        /// <returns></returns>
        protected override OpResultEnum OpenSelectionWindowImpl()
        {
            if(!HasPreselectedValues)
            {
                XlateMessageBox.Information(Strings.IQEnumVarActivity_PreselectedRequired);
                return OpResultEnum.Cancelled;
            }

            return base.OpenSelectionWindowImpl();
        }

        /// <summary> Custom values not supported for IQEnumVar activities. </summary>
        public override bool SupportsCustomValues { get { return false; } }

        /// <summary> Indicates whether the key should be displayed in the pre-defined values grid. </summary>
        public override bool ShowKey { get { return true; } }

        /// <summary>
        /// Performs additional validation of pre-defined values for the IQVarEnumActivity.
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="pValidationMode"></param>
        public override void ValidatePreselectedValues(IList<ValidationError> errors, ValidationMode pValidationMode)
        {
            if(PreselectedValues.Count == 0)
            {
                //Hack to clear the selected values if no pre-selected values exist.
                if (SetToNoneAction != null)
                    SetToNoneAction();
                else
                    SetStateToNotSet();
            }

            var groups = PreselectedValues.GroupBy(e => e.Value);

            if (groups.Any(group => group.Count() > 1))
                errors.Add(new ValidationError(Strings.IQEnumVarActivity_DuplicateKey, false, "Config"));

            base.ValidatePreselectedValues(errors, pValidationMode);
        }

        /// <summary> Performs validation of the object. </summary>
        /// <param name="errors"></param>
        /// <param name="pValidationMode"></param>
        public override void Validate(IList<ValidationError> errors, ValidationMode pValidationMode)
        {
            base.Validate(errors, pValidationMode);

            //Causes the check for pre-selected values to occur on closing of the IQVariableConfigForm rather than on closing
            //of the preselected values form.
            if (PreselectedValues.Count < 1)
                errors.Add(new ValidationError(Strings.IQEnumVarActivity_PreselectedValueRequired, false, "Config"));
        }

        /// <summary>
        /// Method to extract all translatable strings from an object.
        /// </summary>
        /// <returns></returns>
        public override List<string> GetXlateStrings()
        {
            List<string> xlateStrings = base.GetXlateStrings();

            xlateStrings.AddRange(PreselectedValues
                .Where(e => e.IsStandardValue)
                .Select(e => e.Value.Value));

            return xlateStrings;
        }

        /// <summary>
        /// Method to set all translatable strings on an object.  The "key" is the original string
        /// value and the "value" is the translated string value.
        /// </summary>
        /// <param name="xlateStrings"></param>
        public override void SetXlateStrings(IDictionary<string, string> xlateStrings)
        {
            base.SetXlateStrings(xlateStrings);

            foreach (IQVarElement<IQEnum> element in PreselectedValues.Where(e => e.IsStandardValue))
            {
                string xlateString;
                if (xlateStrings.TryGetValue(element.Value.Value, out xlateString))
                    element.Value.Value = xlateString;
            }
        }

        /// <summary>
        /// Method which can be called on a configuration object to clean it up.  Will generally be used in the IQ Script Editor
        /// to remove any values from the pre-defined values or default values which do not match records in the database.
        /// </summary>
        public override void Cleanup()
        {
            base.Cleanup();

            //Adding additional functionality for IQ Enum variables.  Whenever we do a cleanup (both when we load a preference and
            //execute a variable activity), we want to make sure the standard elements are using IQEnums that are defined in the
            //activity configuration in order to ensure that display names are properly synched up.
            if (Value.State == IQVarState.Selected)
            {
                RefreshStandardElement(Value.SelectedElement);

                foreach (IQVarElement<IQEnum> element in Value.SelectedElements)
                    RefreshStandardElement(element);
            }
        }
        #endregion

        #region Private Methods
        private int GetNextKey()
        {
            var orderedEnums = PreselectedValues.Select(e => e.Value).OrderBy(e => e.Key);

            int currentValue = 1;

            foreach(IQEnum e in orderedEnums)
            {
                if (e.Key != currentValue)
                    return currentValue;

                currentValue++;
            }
            return currentValue;
        }

        private void RefreshStandardElement(IQVarElement<IQEnum> pElement)
        {
            if (pElement == null || !pElement.IsStandardValue)
                return;

            pElement.Value = FindPreselectedIQEnumByKey(pElement.Value.Key);
        }

        private IQEnum FindPreselectedIQEnumByKey(int pKey)
        {
            return PreselectedValues.Where(e => e.IsStandardValue)
                .Select(e => e.Value)
                .FirstOrDefault(e => e.Key == pKey);
        }
        #endregion
    }
    #endregion
}
