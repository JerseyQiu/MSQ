#region Using Statements
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;

using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Core.Globals;
using Impac.Mosaiq.Core.Xlate;
using Impac.Mosaiq.UI.Framework;


#endregion

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors
{
    /// <summary>
    /// Manage CDS (Clinical Decision Support) References stored in the CDSReference table
    /// </summary>
    public partial class CdsReferenceSelector : XtraForm
    {
        private readonly ImpacPersistenceManager _pm;

		// The CDSR_ID of the selected reference
		public int Value { get; set; }

        /// <summary> </summary>
        public CdsReferenceSelector()
        {
            InitializeComponent();
            gridViewCdsReference.OptionsSelection.EnableAppearanceFocusedCell = false;

            _pm = Global.DefaultStaffImpacPM;
        }

		private void CdsReferenceSelector_Load(object sender, EventArgs e)
		{
			try
			{
				var query = new ImpacRdbQuery(typeof(CDSReference));
				query.AddClause(CDSReferenceDataRow.InactiveEntityColumn, EntityQueryOp.EQ, false);
				query.AddOrderBy(CDSReferenceDataRow.TitleEntityColumn);

				EntityList<CDSReference> list = _pm.GetEntities<CDSReference>(query);
				bsCdsReference.DataSource = list;

				gridCdsReference.ForceInitialize();
				for (int i = 0; i < gridViewCdsReference.RowCount; i++)
				{
					int index = gridViewCdsReference.GetDataSourceRowIndex(i);
					var cdsRef = (CDSReference)bsCdsReference[index];
					if (cdsRef != null && cdsRef.CDSR_ID == Value)
					{
						gridViewCdsReference.FocusedRowHandle = i;
						break;
					}
				}
			}
			catch (Exception)
			{
				bsCdsReference.Clear();
			}
		}

		private void impacGridViewCdsReference_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			int index = gridViewCdsReference.GetDataSourceRowIndex(e.FocusedRowHandle);
			var cdsReference = (CDSReference) bsCdsReference[index];
			richTextBoxDetail.Rtf = cdsReference.Content;
		}

		private void richTextBoxDetail_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(e.LinkText);
			}
			catch (Exception ex)
			{
				XlateMessageBox.Error(ex.Message);
			}
		}

		private void gridCdsReference_DoubleClick(object sender, EventArgs e)
		{
			CloseDialog();
		}

        /// <summary>
        /// </summary>
        private void btnSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
        	CloseDialog();
        }

		private void CloseDialog()
		{
			var cdsReference = bsCdsReference.Current as CDSReference;
			if (cdsReference != null)
				Value = cdsReference.CDSR_ID;

			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_ItemClick(object sender, ItemClickEventArgs e)
		{
			DialogResult = DialogResult.Cancel;
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