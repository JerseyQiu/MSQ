using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Impac.Mosaiq.Core.Toolbox.Ropes;
using Impac.Mosaiq.Core.Xlate;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Editor
{
    /// <summary> Simple editor class for changing the value of a string. </summary>
    public partial class IntegerValueEditor : XtraForm
    {
        #region Constructor
        /// <summary> Default ctor </summary>
        public IntegerValueEditor()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        /// <summary> The value to edit </summary>
        public int Value
        {
            get { return Decimal.ToInt32((Decimal)spinEdit.EditValue); }
            set { spinEdit.EditValue = value; }
        }

        /// <summary>The minimum value allowed </summary>
        public int? MinValue { get; set; }
        
        /// <summary> The maximum value allowed </summary>
        public int? MaxValue { get; set; }
        #endregion

        #region Event Handlers
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

        private void IntegerValueEditor_Load(object sender, EventArgs e)
        {
            if (MinValue.HasValue)
                spinEdit.Properties.MinValue = MinValue.Value;

            if (MaxValue.HasValue)
                spinEdit.Properties.MaxValue = MaxValue.Value;
        }

        private void IntegerValueEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if ((MinValue.HasValue && Value < MinValue) || (MaxValue.HasValue && Value > MaxValue))
                {
                    ShowError();
                    e.Cancel = true;
                }
            }
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
        private void ShowError()
        {
            string message = string.Empty;
            if (MinValue.HasValue && MaxValue.HasValue)
                message = Rope.Format(Strings.RangeErrorBetween, MinValue, MaxValue);
            else if (MinValue.HasValue)
                message = Rope.Format(Strings.RangeErrorBelowMin, MinValue);
            else if (MaxValue.HasValue)
                message = Rope.Format(Strings.RangeErrorAboveMax, MaxValue);

            if (!string.IsNullOrWhiteSpace(message))
                XlateMessageBox.Information(message);
        }
        #endregion
    }
}
