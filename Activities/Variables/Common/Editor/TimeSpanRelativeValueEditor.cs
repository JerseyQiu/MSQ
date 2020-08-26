using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Impac.Mosaiq.Core.Toolbox.Forms;
using Impac.Mosaiq.IQ.Common.Variable;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Editor
{
    /// <summary> Simple editor class for changing the value of a string. </summary>
    public partial class TimeSpanRelativeValueEditor : XtraForm
    {
        #region Constructor
        /// <summary> Default ctor </summary>
        public TimeSpanRelativeValueEditor()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        /// <summary> The value to edit </summary>
        public TimeSpanRelative Value { get; set; }
        #endregion

        #region Event Handlers
        private void TimeSpanRelativeValueEditor_Load(object sender, EventArgs e)
        {
            if (Value == null)
                Value = new TimeSpanRelative { Count = 0, Unit = TimeSpanRelativeUnit.Days };

            
            spinCount.EditValue = Value.Count;

            switch (Value.Unit)
            {
                case TimeSpanRelativeUnit.Days:
                    comboUnits.SelectedIndex = 0;
                    break;

                case TimeSpanRelativeUnit.Weeks:
                    comboUnits.SelectedIndex = 1;
                    break;

                case TimeSpanRelativeUnit.Months:
                    comboUnits.SelectedIndex = 2;
                    break;

                case TimeSpanRelativeUnit.Years:
                    comboUnits.SelectedIndex = 3;
                    break;

                default:
                    throw new InvalidOperationException(Value.Unit + " is not supported.");
            }

            UpdatePluralization();
        }

        private void TimeSpanRelativeValueEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                return;

            Value.Count = (int)spinCount.Value;

            switch (comboUnits.SelectedIndex)
            {
                case 0:
                    Value.Unit = TimeSpanRelativeUnit.Days;
                    break;

                case 1:
                    Value.Unit = TimeSpanRelativeUnit.Weeks;
                    break;

                case 2:
                    Value.Unit = TimeSpanRelativeUnit.Months;
                    break;

                case 3:
                    Value.Unit = TimeSpanRelativeUnit.Years;
                    break;

                default:
                    throw new InvalidOperationException(Value.Unit + " is not supported.");
            }
        }

        private void spinCount_EditValueChanged(object sender, EventArgs e)
        {
            UpdatePluralization();
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
        /// <summary>
        /// Sets values to either singular or plural depending on the number picked in the spin edit.
        /// </summary>
        private void UpdatePluralization()
        {
            bool useSingular = (int)spinCount.Value == 1 || (int)spinCount.Value == -1;
            int index = comboUnits.SelectedIndex;

            comboUnits.Properties.Items[0] = useSingular ? TimeSpanRelative.Day : TimeSpanRelative.Days;
            comboUnits.Properties.Items[1] = useSingular ? TimeSpanRelative.Week : TimeSpanRelative.Weeks;
            comboUnits.Properties.Items[2] = useSingular ? TimeSpanRelative.Month : TimeSpanRelative.Months;
            comboUnits.Properties.Items[3] = useSingular ? TimeSpanRelative.Year : TimeSpanRelative.Years;

            comboUnits.SelectedIndex = index;

            if (comboUnits.SelectedIndex >= 0)
            {
                comboUnits.EditValue = comboUnits.Properties.Items[comboUnits.SelectedIndex];
                WinFormsUtility.DoEvents(); 
            }
        }
        #endregion
    }
}
