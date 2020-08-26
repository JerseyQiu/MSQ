using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Editor
{
    /// <summary> Simple editor class for changing the value of a string. </summary>
    public partial class BooleanValueEditor : XtraForm
    {
        #region Constructor
        /// <summary> Default ctor </summary>
        public BooleanValueEditor()
        {
            InitializeComponent();

            comboBoxValue.Properties.Items.Add(Strings.True);
            comboBoxValue.Properties.Items.Add(Strings.False);
        }
        #endregion

        #region Properties
        /// <summary> The value to edit </summary>
        public Boolean Value
        {
            get { return comboBoxValue.SelectedIndex == 0; }
            set { comboBoxValue.SelectedIndex = value ? 0 : 1; }
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
