using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Core.Defs.Enumerations;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors
{
    /// <summary>
    /// An editor that allows the user to select a flowsheet tab view and then
    /// select/deselect individual items from that view.
    /// </summary>
    public partial class FlowsheetTabSelector : XtraForm
    {
        readonly private ImpacPersistenceManager _pm;
        private EntityList<ObsDef> _tabNames;

        #region Constructor and initialization
        /// <summary> Default ctor </summary>
        public FlowsheetTabSelector()
        {
            InitializeComponent();

            _pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
        }

        private void FlowsheetTabSelector_Load(object sender, EventArgs e)
        {
            try
            {
				var query = new ImpacRdbQuery(typeof(ObsDef));
                query.AddClause(ObsDefDataRow.TypeEntityColumn, EntityQueryOp.EQ, (int)MedDefs.ObdType.ViewFilter);
                _tabNames = _pm.GetEntities<ObsDef>(query);

                listTabNames.DisplayMember = "Label";
                listTabNames.ValueMember = "OBD_GUID";
                listTabNames.DataSource = _tabNames;

                // Select the current value if there is one and if the mode is sinlge-select
				if (TabGuid == Guid.Empty)
					return;

                for (int i = 0; i < listTabNames.ItemCount; i++)
                {
					var item = (ObsDef) listTabNames.GetItem(i);
                    if (item == null)
                        continue;
                    if (item.OBD_GUID != TabGuid)
                        continue;

                    listTabNames.SelectedIndex = i;
                    break;
                }
            }
            catch (Exception ex)
            {
				System.Diagnostics.Trace.WriteLine("FlowsheetTabSelector: " + ex);
            }
        }
        #endregion

        /// <summary>
		/// The OBD_GUID of the ObsDef record for the tab name
        /// </summary>
		public Guid TabGuid { get; set; }

    	#region Event Handlers
        private void barButtonItemOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CloseDialog();
        }

		private void listTabNames_DoubleClick(object sender, EventArgs e)
		{
			CloseDialog();
		}

        private void barButtonItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ValidateChildren();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        private void CloseDialog()
        {
        	var tab = (ObsDef)listTabNames.SelectedItem;
			if (tab == null)
				return;

        	TabGuid = tab.OBD_GUID;

            ValidateChildren();
            DialogResult = DialogResult.OK;
            Close();
        }

        #region Overriden Methods
        /// <summary> Add special handling for enter and escape key presses </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    CloseDialog();
                    e.Handled = true;
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
