using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Impac.Mosaiq.IQ.Common.Variable;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Editor
{
    /// <summary> Simple editor class for changing the value of a string. </summary>
    public partial class DateValueEditor : XtraForm
    {
        #region Constructor
        /// <summary> Default ctor </summary>
        public DateValueEditor()
        {
            InitializeComponent();
            Type = IQDateTimeType.Now;
            Value = DateTime.Today;
        }
        #endregion

        #region Properties
        /// <summary> The value to edit </summary>
        public DateTime Value { get; set; }

        /// <summary> The selected date time type. </summary>
        public IQDateTimeType Type { get; set; }
        #endregion

        #region Event Handlers
        private void DateValueEditor_Load(object sender, EventArgs e)
        {
            comboDatePicker.Properties.Appearance.BackColor = Color.White;
            layoutControl.SetDefaultLayout();
            
            switch (Type)
            {
                case IQDateTimeType.Now:
                case IQDateTimeType.Today:
                    comboDatePicker.SelectedIndex = 0;
                    break;
                case IQDateTimeType.Tomorrow:
                    comboDatePicker.SelectedIndex = 1;
                    break;
                case IQDateTimeType.Yesterday:
                    comboDatePicker.SelectedIndex = 2;
                    break;
                case IQDateTimeType.Other:
                    comboDatePicker.SelectedIndex = 3;
                    break;
                default:
                    throw new InvalidOperationException(Type + " is not supported.");
            }

            customDateEdit.EditValue = Type == IQDateTimeType.Other ? Value : DateTime.Today;

            UpdateDisplayedControls();
        }

        private void comboDatePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDisplayedControls();
        }

        private void DateValueEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                return;

            ValidateChildren();

            switch (comboDatePicker.SelectedIndex)
            {
                case 0:
                    Type = IQDateTimeType.Today;
                    Value = DateTime.MinValue;
                    break;
                case 1:
                    Type = IQDateTimeType.Tomorrow;
                    Value = DateTime.MinValue;
                    break;
                case 2:
                    Type = IQDateTimeType.Yesterday;
                    Value = DateTime.MinValue;
                    break;
                case 3:
                    Type = IQDateTimeType.Other;
                    Value = (DateTime)customDateEdit.EditValue;
                    break;
            }
        }

        private void barButtonItemOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ValidateChildren();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void barButtonItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ValidateChildren();
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        #region Overriden Methods
        /// <summary> Add special handling for enter and escape key presses </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    DialogResult = DialogResult.OK;
                    e.Handled = true;
                    Close();
                    break;
                case Keys.Escape:
                    DialogResult = DialogResult.Cancel;
                    e.Handled = true;
                    Close();
                    break;
            }

            base.OnKeyDown(e);
        }
        #endregion

        #region Private Methods
        private void UpdateDisplayedControls()
        {
            if (comboDatePicker.SelectedIndex == 3)
                layoutControl.RestoreDefaultLayout();
            else
                layoutControl.HideItem(lciCustomDate);
        }
        #endregion
    }
}
