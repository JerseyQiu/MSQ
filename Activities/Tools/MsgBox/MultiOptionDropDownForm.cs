using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Impac.Mosaiq.Core.Toolbox.Forms;

namespace Impac.Mosaiq.IQ.Activities.Tools.MsgBox
{
    /// <summary>
    /// This form is use in conjunction with the ListPromptActivity.  This form displays the
    /// options that the user can select from as defined in the ListPromptActivity.
    /// </summary>
    public partial class MultiOptionDropDownForm : XtraForm
    {
        #region Constructor
        /// <summary>
        /// Default Ctor.
        /// </summary>
        public MultiOptionDropDownForm()
        {
            InitializeComponent();
            _labelInitialHeight = labelControlMessage.Height;
        }
        #endregion

        #region Fields
        private readonly int _labelInitialHeight;
        #endregion

        #region Properties
        /// <summary>
        /// List of options which will appear in the form.
        /// </summary>
        public List<String> Choices { get; set; }

        /// <summary>
        /// Message which will appear above the drop down.
        /// </summary>
        public string Message 
        {
            get { return labelControlMessage.Text; }
            set { labelControlMessage.Text = value; }
        }

        /// <summary> Determines if the cancel button will be visible on the form. </summary>
        public bool ShowCancelButton { get; set; }

        /// <summary>
        /// The index of the selected item
        /// </summary>
        public int SelectedIndex
        {
            get { return Choices.IndexOf((String) lookupEditItems.EditValue); }
        }

        /// <summary>
        /// The text of the selected item.
        /// </summary>
        public string SelectedText
        {
            get { return (string) lookupEditItems.EditValue; }
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Loads the Prompt Items into the list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListPromptForm_Load(object sender, EventArgs e)
        {
            if (Choices != null && Choices.Count > 0)
            {
                lookupEditItems.Properties.DataSource = Choices;
                lookupEditItems.Properties.DropDownRows = Choices.Count;
                lookupEditItems.EditValue = Choices[0];
            }

            if (!ShowCancelButton)
                layoutMain.HideItem(lciCancelButton);

            SetWindowHeight();
            
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
        /// Handles the edit value changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookupEditItems_EditValueChanged(object sender, EventArgs e)
        {
            lookupEditItems.ToolTip = SelectedText;
        }
        #endregion

        #region Private Methods
        private void SetWindowHeight()
        {
            int curHeight = labelControlMessage.Height;
            int diff = curHeight - _labelInitialHeight;


            int newHeight = Height + diff + 5;

            if (!ShowCancelButton)
                newHeight -= 12;

            Size = new Size(Width, newHeight);
        }
        #endregion
    }
}