using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Editor
{
    /// <summary> Simple editor class for changing the value of a memo text. </summary>
    public partial class MemoValueEditor : XtraForm
    {
        #region Constructor
        /// <summary> Default ctor </summary>
        public MemoValueEditor()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        /// <summary> The value to edit </summary>
        public string Value
        {
            get
            {
                return memoEdit1.EditValue as string;
            }
            set
            {
                memoEdit1.EditValue = value;
            }
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
                // Do not close form when Enter is pressed, since the memo edit control needs to receive Enter as part of the input value.
                //case Keys.Enter:
                //    DialogResult = DialogResult.OK;
                //    e.Handled = true;
                //    Close();
                //    break;
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
