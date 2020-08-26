using System.Windows.Forms;
using DevExpress.XtraEditors;
using Impac.Mosaiq.Core.Xlate;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Editor
{
    /// <summary> Simple editor class for changing the value of a string. </summary>
    public partial class DoubleValueEditor : XtraForm
    {
        #region Constructor
        /// <summary> Default ctor </summary>
        public DoubleValueEditor()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        /// <summary> The value to edit </summary>
        public double Value
        {
            get { return (double)textEdit1.EditValue; }
            set { textEdit1.EditValue = value; }
        }

        /// <summary>The minimum value allowed </summary>
        public double? MinValue { get; set; }
        
        /// <summary> The maximum value allowed </summary>
        public double? MaxValue { get; set; }
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

        private void DoubleValueEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(DialogResult == DialogResult.OK)
            {
                if ((MinValue.HasValue && Value < MinValue) || (MaxValue.HasValue && Value > MaxValue))
                {
                    ShowError();
                    e.Cancel = true;
                }
            }
        }

        private void ShowError()
        {
            string message = string.Empty;
            if (MinValue.HasValue && MaxValue.HasValue)
                message = string.Format(Strings.RangeErrorBetween, MinValue, MaxValue);
            else if (MinValue.HasValue)
                message = string.Format(Strings.RangeErrorBelowMin, MinValue);
            else if (MaxValue.HasValue)
                message = string.Format(Strings.RangeErrorAboveMax, MaxValue);

            if (!string.IsNullOrWhiteSpace(message))
                XlateMessageBox.Information(message);

        }
    }
}
