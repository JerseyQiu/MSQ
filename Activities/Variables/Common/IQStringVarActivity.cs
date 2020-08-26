using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DevExpress.XtraEditors;

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
    /// <summary>
    /// This activity allows the user to select a staff in the UI and outputs the primary key to
    /// the output parameter.
    /// </summary>
    [IQStringVarActivity_DisplayName]
    [Common_Category]
    [Variables_ActivityGroup]
    public sealed class IQStringVarActivity : IQVariableActivitySimple<string, IQStringVar, IQStringVarDetail, IQStringVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> The configuration class for the IQVarStringActivity class</summary>
    [Serializable]
    public class IQStringVarConfig : IQVariableConfigSimple<string, IQStringVar, IQStringVarDetail>
    {
        #region Properties (Serializable)
        /// <summary>
        /// Whether the string editor should allow entering multiple lines or not.
        /// </summary>
        [RestoreInclude]
        [ConfigurationActivity_Category]
        [AllowMultipleLines_DisplayName]
        public bool AllowMultipleLines { get; set; }

        /// <summary>
        /// Maximum allowed length of string input
        /// </summary>
        [RestoreInclude]
        [ConfigurationActivity_Category]
        [MaxLength_DisplayName]
        public int MaxLength { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Opens the string editor. </summary>
        /// <returns></returns>
        protected override IQOpResult<string> OpenEditor(string defaultValue, IQVarElementTarget target)
        {
            //Replace null values with empty string.
            if (defaultValue == null)
                defaultValue = String.Empty;

            //This code ensures that all instances of a standalone "\n" get converted to "\r\n"  Due to rules with Xml serialzation, CRLF's get serialized just as
            //LF's in Xml and, when de-serialized, the full CRLF is not restored.  The "defaultValue" passed in will come from the IQPrefer.IQP_Data field if 
            //deserialized from a preference, meaning we need to convert all of the LF's to CRLF's.  However, iw simply do that replace, than all existing CRLFs would
            //get converted to CRLFLF, which is not waht we want.  The code below first strips off all of the CR's, ensuring that all newlines are encoded as just LF's.  
            //Then all LF's are restored to CRLF's, getting our value back to it's pre-seriailized state.

            defaultValue = defaultValue.Replace("\r\n", "\n");
            defaultValue = defaultValue.Replace("\n", "\r\n");

            if (AllowMultipleLines)
            {
                var editor = new MemoValueEditor { Value = defaultValue };
                return (editor.ShowDialog() == DialogResult.OK)
                               ? new IQOpResult<string> { Result = OpResultEnum.Completed, Value = editor.Value }
                               : new IQOpResult<string> { Result = OpResultEnum.Cancelled };
            }
            else
            {
                var editor = new StringValueEditor(MaxLength) {Value = defaultValue};
                return (editor.ShowDialog() == DialogResult.OK)
                               ? new IQOpResult<string>
                                     {Result = OpResultEnum.Completed, Value = editor.Value}
                               : new IQOpResult<string> {Result = OpResultEnum.Cancelled};
            }
        }

        /// <summary>
        /// Method to extract all translatable strings from an object.
        /// </summary>
        /// <returns></returns>
        public override List<string> GetXlateStrings()
        {
            var xlateStrings = base.GetXlateStrings();
            
            //Add preselected values to translation
            xlateStrings.AddRange(PreselectedValues
                .Where(e => e.IsStandardValue)
                .Select(element => element.Value));

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

            //Set preselected values.
            foreach (IQVarElement<string> element in PreselectedValues.Where(e => e.IsStandardValue))
            {
                string xlateString;
                if (xlateStrings.TryGetValue(element.Value, out xlateString))
                    element.Value = xlateString;
            }

            foreach (IQVarElement<string> element in Value.SelectedElements.Where(e => e.IsStandardValue))
            {
                string xlateString;
                if (xlateStrings.TryGetValue(element.Value, out xlateString))
                    element.Value = xlateString;
            }
        }
        #endregion
    }
    #endregion
}