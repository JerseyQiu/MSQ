using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM.Editors
{
    /// <summary> Simple editor class for selecting a department. </summary>
    public partial class DepartmentSelector : XtraForm
    {
    	private readonly int _cfgId;

        #region Constructor
        /// <summary> Default ctor </summary>
        public DepartmentSelector(int cfgId)
        {
        	_cfgId = cfgId;

            InitializeComponent();

			//Hack to disable the text editor in the combo box.
			var editor = inputDeptControl1.BaseEdit as ComboBoxEdit;
			if (editor != null)
			{
				editor.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
				layoutControlItem1.Control = inputDeptControl1.BaseEdit;
			}
        }
        #endregion

        #region Properties
        /// <summary> The value to edit </summary>
        public int Value
        {
            get { return inputDeptControl1.CFG_ID.GetValueOrDefault(); }
        }

        #endregion

        #region Event Handlers

		private void DepartmentSelector_Load(object sender, System.EventArgs e)
		{
			if (_cfgId > 0)
				inputDeptControl1.CFG_ID = _cfgId;
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
    }
}
