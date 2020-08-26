using System;
using System.Activities;
using System.Collections.Generic;
using System.Windows.Forms;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.MsgBox
{

    /// <summary>
    /// This activity allows the workflow designer to configure a list of text values that will be 
    /// presented to the user in a list format.  When the workflow executes, the user may select 
    /// one item from the list and click OK.  The item they selected can then be used to determine
    /// the execution flow of the workflow.
    /// </summary>
    [RadioGroupWithCheckListBoxActivity_DisplayName]
    [Messaging_Category]
    [Tools_ActivityGroup]
    public class RadioGroupWithCheckListBoxActivity : MosaiqUICodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary>
        /// The caption that will be displayed in the prompt window
        /// </summary>
        [InputParameterCategory]
        [Caption_Description]
        [Caption_DisplayName]
        public InArgument<string> DialogCaption { get; set; }

        /// <summary>
        /// Title for the the Radio group box.
        /// </summary>
        [InputParameterCategory]
        [RadioGroupWithCheckListBoxActivity_RadioGroupTitle_Description]
        [RadioGroupWithCheckListBoxActivity_RadioGroupTitle_DisplayName]
        public InArgument<string> RadioGroupTitle { get; set; }

        /// <summary>
        /// The collection of items that will appear in the Radio group box
        /// </summary>
        [InputParameterCategory]
        [RadioGroupWithCheckListBoxActivity_RadioGroupItems_DisplayName]
        [RadioGroupWithCheckListBoxActivity_RadioGroupItems_Description]
        public InArgument<List<string>> RadioGroupItems { get; set; }

        /// <summary> Default radio button to select based on index </summary>
        [InputParameterCategory]
        [RadioGroupWithCheckListBoxActivity_RadioGroupIndex_DisplayName]
        [RadioGroupWithCheckListBoxActivity_RadioGroupIndex_Description]
        public InArgument<int> RadioGroupIndex { get; set; }

        /// <summary>
        /// Title for the the Check list box.
        /// </summary>
        [InputParameterCategory]
        [RadioGroupWithCheckListBoxActivity_CheckListTitle_Description]
        [RadioGroupWithCheckListBoxActivity_CheckListTitle_DisplayName]
        public InArgument<string> CheckListTitle { get; set; }

        /// <summary>
        /// The collection of check box items to add to the Radio Group form
        /// </summary>
        [InputParameterCategory]
        [RadioGroupWithCheckListBoxActivity_CheckListItems_DisplayName]
        [RadioGroupWithCheckListBoxActivity_CheckListItems_Description]
        public InArgument<List<string>> CheckListItems { get; set; }

        #endregion

        #region Properties (Output Parameters)
        /// <summary>
        /// The index of the selected item (starting with 0)
        /// </summary>
        [OutputParameterCategory]
        [RadioGroupWithCheckListBoxActivity_RadioGroupResult_DisplayName]
        [RadioGroupWithCheckListBoxActivity_RadioGroupResult_Description]
        public OutArgument<int> RadioGroupResult { get; set; }

        /// <summary>
        /// Boolean list corresponding with the checkbox list supplied
        /// </summary>
        [OutputParameterCategory]
        [RadioGroupWithCheckListBoxActivity_CheckListResult_DisplayName]
        [RadioGroupWithCheckListBoxActivity_CheckListResult_Description]
        public OutArgument<List<bool>> CheckListResult { get; set; }

        /// <summary>
        /// The dialog of the form.
        /// </summary>
        [OutputParameterCategory]
        [RadioGroupWithCheckListBoxActivity_DialogResult_DisplayName]
        [RadioGroupWithCheckListBoxActivity_DialogResult_Description]
        public OutArgument<DialogResult> DialogResult { get; set; }

        #endregion

        #region Overriden Methods
        /// <summary>
        /// Open the form and get a return value.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            // Title for the complete form
            string dialogCaption = String.IsNullOrEmpty(DialogCaption.Get(context)) ? " " : DialogCaption.Get(context);

            // Inputs for the Radio Group box
            string radioGroupTitle = String.IsNullOrEmpty(RadioGroupTitle.Get(context)) ? " " : RadioGroupTitle.Get(context);
            List<string> radioGroupItems = RadioGroupItems.Get(context);
            int rgIndex = RadioGroupIndex.Expression != null ? RadioGroupIndex.Get(context) : -1;

            // Inputs for the Check list box
            string checkListTitle = String.IsNullOrEmpty(CheckListTitle.Get(context)) ? " " : CheckListTitle.Get(context);
            List<string> checkListItems = CheckListItems.Get(context);

            var form = new RadioGroupWithCheckListForm
            {

                RadioGroupItems = radioGroupItems,
                Text = dialogCaption,
                RadioGroupMessage = radioGroupTitle,
                DefaultIndex = rgIndex,
                CheckListMessage = checkListTitle,
                CheckListItems = checkListItems
            };

            // Default to -1 for the Radio Group index and set all to False for the Check List
            RadioGroupResult.Set(context, -1);
            if (checkListItems != null)
            {
                List<bool> tempList = new List<bool>();
                for (int i = 0; i < checkListItems.Count; i++)
                {
                    tempList.Add(false);
                }
                CheckListResult.Set(context, tempList);
            }

            // Do not show the dialog if no items to show
            if ((radioGroupItems == null) && (checkListItems == null))
            {
                return;
            }

            DialogResult result = form.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RadioGroupResult.Set(context, form.SelectedIndex);
                CheckListResult.Set(context, form.CheckListBoxResult);
            }
            DialogResult.Set(context, result);
        }
        #endregion
    }
}