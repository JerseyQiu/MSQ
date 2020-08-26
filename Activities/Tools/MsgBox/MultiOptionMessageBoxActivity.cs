using System;
using System.Activities;
using System.Collections.Generic;
using System.Windows.Forms;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.MsgBox
{
    /// <summary> Determines the form style used. </summary>
    public enum MultiOptionMessageBoxStyleEnum
    {
        /// <summary> Use a window with buttons. </summary>
        Button = 1,

        /// <summary> Use a window with a drop down </summary>
        DropDown = 2
    }

    /// <summary>
    /// This activity allows the workflow designer to configure a list of text values that will be 
    /// presented to the user in a list format.  When the workflow executes, the user may select 
    /// one item from the list and click OK.  The item they selected can then be used to determine
    /// the execution flow of the workflow.
    /// </summary>
    [MultiOptionMessageBoxActivity_DisplayName]
    [Messaging_Category]
    [Tools_ActivityGroup]
    public class MultiOptionMessageBoxActivity : MosaiqUICodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary>
        /// The caption that will be displayed in the prompt window
        /// </summary>
        [InputParameterCategory]
        [Caption_Description]
        [Caption_DisplayName]
        public InArgument<string> Caption { get; set; }

        /// <summary>
        /// The message which will display in the dialog.
        /// </summary>
        [InputParameterCategory]
        [Message_Description]
        [Message_DisplayName]
        public InArgument<string> Message { get; set; }

        /// <summary>
        /// The collection of prompt items that will appear on the form.
        /// </summary>
        [InputParameterCategory]
        [MultiOptionMessageBoxActivity_Items_Description]
        [MultiOptionMessageBoxActivity_Items_DisplayName]
        public InArgument<List<string>> Items { get; set; }

        /// <summary> The style of the form used </summary>
        [InputParameterCategory]
        [MultiOptionMessageBoxActivity_Style_DisplayName]
        [MultiOptionMessageBoxActivity_Style_Description]
        public InArgument<MultiOptionMessageBoxStyleEnum> Style { get; set; }

        /// <summary> Indicates whether the cancel button is displayed on the form </summary>
        [InputParameterCategory]
        [MultiOptionMessageBoxActivity_ShowCancelButton_DisplayName]
        [MultiOptionMessageBoxActivity_ShowCancelButton_Description]
        public InArgument<bool> ShowCancelButton { get; set; }

        #endregion

        #region Properties (Output Parameters)
        /// <summary>
        /// The index of the selected item (starting with 0)
        /// </summary>
        [OutputParameterCategory]
        [MultiOptionMessageBoxActivity_SelectedIndex_Description]
        [MultiOptionMessageBoxActivity_SelectedIndex_DisplayName]
        public OutArgument<int> SelectedIndex { get; set; }

        /// <summary>
        /// The text of the selected item
        /// </summary>
        [OutputParameterCategory]
        [MultiOptionMessageBoxActivity_SelectedText_Description]
        [MultiOptionMessageBoxActivity_SelectedText_DisplayName]
        public OutArgument<string> SelectedText { get; set; }

        /// <summary>
        /// The dialog of the form.
        /// </summary>
        [OutputParameterCategory]
        [DialogResult_Description]
        [DialogResult_DisplayName]
        public OutArgument<DialogResult> DialogResult { get; set; }

        #endregion

        #region Overriden Methods
        /// <summary>
        /// Open the form and get a return value.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            MultiOptionMessageBoxStyleEnum style = (Style.Expression != null)
                                                       ? Style.Get(context)
                                                       : MultiOptionMessageBoxStyleEnum.DropDown;

            bool showCancelButton = (ShowCancelButton.Expression != null)
                                        ? ShowCancelButton.Get(context)
                                        : true;

            string windowTitle = String.IsNullOrEmpty(Caption.Get(context))
                   ? " "
                   : Caption.Get(context);

            List<string> items = Items.Get(context);
            string message = Message.Get(context);
            string values = String.Empty;

            foreach (string str in items)
            {
                if (items.IndexOf(str) != 0)
                    values += ", ";

                values += str;
            }

            Track("Items", values);

            DialogResult result;
            if (style == MultiOptionMessageBoxStyleEnum.DropDown)
            {
                var form = new MultiOptionDropDownForm
                               {
                                   Choices = items,
                                   Text = windowTitle,
                                   Message = message,
                                   ShowCancelButton = showCancelButton
                               };

                result = form.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    SelectedIndex.Set(context, form.SelectedIndex);
                    SelectedText.Set(context, form.SelectedText);
                }
                DialogResult.Set(context, result);
            }
            else
            {
                var form = new MultiOptionButtonForm
                               {
                                   Choices = items,
                                   Text = windowTitle,
                                   Message = message,
                                   ShowCancelButton = showCancelButton
                               };

                result = form.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    SelectedIndex.Set(context, form.SelectedIndex);
                    SelectedText.Set(context, form.SelectedText);
                }
                DialogResult.Set(context, result);
            }
        }
        #endregion
    }
}