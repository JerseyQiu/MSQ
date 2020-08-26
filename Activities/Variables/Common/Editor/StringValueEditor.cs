using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Editor
{
    /// <summary> Simple editor class for changing the value of a string. </summary>
    public partial class StringValueEditor : XtraForm
    {
        #region Constructor
        /// <summary> Default ctor </summary>
        public StringValueEditor()
        {
            InitializeComponent();

            layoutControlItem2.Visibility =
                        DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        /// <summary> Default ctor </summary>
        public StringValueEditor(int maxLength)
        {
            InitializeComponent();

            if (maxLength > 0)
            {
                textEdit1.Properties.MaxLength = maxLength;
                labelControl1.Text += maxLength.ToString();
                layoutControlItem2.Visibility =
                        DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
                layoutControlItem2.Visibility =
                        DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        #endregion

        #region Properties
        /// <summary> The value to edit </summary>
        public string Value
        {
            get { return (string)textEdit1.EditValue; }
            set { textEdit1.EditValue = value; }
        }
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
    }
}
