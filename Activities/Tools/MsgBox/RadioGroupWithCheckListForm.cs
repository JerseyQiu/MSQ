using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using Impac.Mosaiq.Core.Toolbox.Forms;
using DevExpress.XtraEditors;

namespace Impac.Mosaiq.IQ.Activities.Tools.MsgBox
{
    /// <summary>
    /// This form is use in conjunction with the ListPromptActivity.  This form displays the
    /// options that the user can select from as defined in the ListPromptActivity.
    /// </summary>
    public partial class RadioGroupWithCheckListForm : XtraForm
    {
        #region Constructor
        /// <summary>
        /// Default Ctor.
        /// </summary>
        public RadioGroupWithCheckListForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Message which will appear above the Radio group box.
        /// </summary>
        public string RadioGroupMessage
        {
            get { return rgbRadioGroupBox.Text; }
            set { rgbRadioGroupBox.Text = value; }
        }

        /// <summary>
        /// List of items that will appear in the Radio group.
        /// </summary>
        public List<String> RadioGroupItems { get; set; }

        /// <summary> Determines the default selection for the Radio group. </summary>
        public int DefaultIndex { get; set; }

        /// <summary>
        /// The index of the selected item
        /// </summary>
        public int SelectedIndex
        {
            get { return rdoRadioGroup.SelectedIndex; }
        }

        /// <summary>
        /// Message which will appear above the Radio group box.
        /// </summary>
        public string CheckListMessage
        {
            get { return rgbCheckListGroupBox.Text; }
            set { rgbCheckListGroupBox.Text = value; }
        }

        /// <summary> List of items that will appear in the Check list box. </summary>
        public List<string> CheckListItems { get; set; }

        /// <summary>
        /// The result of the check boxes
        /// </summary>
        public List<bool> CheckListBoxResult
        {
            get
            {
                List<bool> tempList = new List<bool>();
                for (int i = 0; i < chkCheckedListBox.Items.Count; i++)
                {
                    tempList.Add(chkCheckedListBox.GetItemCheckState(i) == CheckState.Checked);
                }
                return tempList;
            }
        }

        /// <summary>
        /// The text of the selected item.
        /// </summary>
        public string SelectedText
        {
            get { return rdoRadioGroup.Properties.Items[rdoRadioGroup.SelectedIndex].ToString(); }
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Loads the Prompt Items into the radio group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioGroupWithCheckListFormLoad(object sender, EventArgs e)
        {
            // Init the height of the form based on number of items in radio group and check list
            int controlHeight = 0;

            // Add supplied items to the radio group box
            if (RadioGroupItems != null)
            {
                // Make sure supplied default index is within valid range
                if (DefaultIndex < -1 || DefaultIndex > RadioGroupItems.Count - 1)
                {
                    DefaultIndex = -1;
                }

                // Loop through items and add each to the radio group
                for (int i = 0; i < RadioGroupItems.Count; i++)
                {
                    if (RadioGroupItems[i] == null)
                    {
                        rdoRadioGroup.Properties.Items.Add(new RadioGroupItem(i, string.Empty)); 
                        rdoRadioGroup.Properties.Items[i].Enabled = false;                        
                        if (DefaultIndex == i)
                        {
                            DefaultIndex = -1;
                        }
                    }
                    else
                    {
                        rdoRadioGroup.Properties.Items.Add(new RadioGroupItem(i, RadioGroupItems[i].Trim())); 
                    }
                    controlHeight = controlHeight + rdoRadioGroup.Font.Height + 10;
                }

                // Set the default selection of the radio group
                rdoRadioGroup.SelectedIndex = DefaultIndex;

                // To address Defect-8782, handle mouse wheel specially               
                rdoRadioGroup.MouseWheel += rdoRadioGroupMouseWheel;
            }
            else
            {
                rgbRadioGroupBox.Hide(); // Hide radio group box if no items available
            }

            // Set height of radio group based on number of items
            rgbRadioGroupBox.Height = controlHeight + 25;
            rgbRadioGroupBox.Top = 0;
            rgbRadioGroupBox.Left = 0;
            
            // Add supplied items to check list box
            controlHeight = 0;
            if (CheckListItems != null)
            {
                for (int i = 0; i < CheckListItems.Count; i++)
                {
                    if (CheckListItems[i] == null)
                    {
                        chkCheckedListBox.Items.Add(new CheckedListBoxItem(i, " "));
                        chkCheckedListBox.Items[i].Enabled = false;
                    }
                    else
                    {
                        chkCheckedListBox.Items.Add(new CheckedListBoxItem(i, CheckListItems[i].Trim()));                        
                    }
                    controlHeight = controlHeight + chkCheckedListBox.Font.Height + 4;
                }
                chkCheckedListBox.SelectedIndex = -1;
            }
            else
            {
                rgbCheckListGroupBox.Hide(); // Hide check list box in case of no items
            }

            // Set height of check list box based on number of items
            rgbCheckListGroupBox.Height = controlHeight + 25;
            rgbCheckListGroupBox.Top = rgbRadioGroupBox.Bottom;
            rgbCheckListGroupBox.Left = 0;

            // Position the ok and cancel buttons
            okButton.Top = rgbCheckListGroupBox.Bottom + 10;
            cancelButton.Top = okButton.Top;

            // Set the height of the complete form
            Height = cancelButton.Bottom + 5;

            BringToFront();
            Focus();
            WinFormsUtility.DoEvents();

        }
        /// <summary>
        /// Handles the click of the OK button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handle the cancel button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// To address Defect-8782 (Treatment Chart-PotD/plan of the day IQ Script:The mouse scroll wheel can easily 
        /// change their selection from the planned treatment to another without the user noticing),
        /// prevent mouse wheel scrolling on this radio button selection control. 
        /// It's used for Plan-of-the-Day Rx selection. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoRadioGroupMouseWheel(object sender, MouseEventArgs e)
        {
            ((DXMouseEventArgs)e).Handled = true;
        }

        /// <summary>
        /// Handles the edit value changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rgbRadioGroup_EditValueChanged(object sender, EventArgs e)
        {
            rdoRadioGroup.ToolTip = SelectedText;
        }

        private void RadioGroupWithCheckListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Ignore)
            {
                e.Cancel = true;
            }
        }
        #endregion
    }
}