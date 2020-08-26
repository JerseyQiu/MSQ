using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Core.Xlate;
using Impac.Mosaiq.IQ.Common.Variable;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM.Editors
{
    /// <summary>
    /// An editor that allows the selection of a staff type or category
    /// </summary>
    public partial class StaffTypeOrCategorySelector : XtraForm
    {
		class KeyLabelInfo
		{
			public int Key { get; set; }
			public string Label { get; set; }
		}

		// The list of items to diplay
    	readonly IList<KeyLabelInfo> _items = new List<KeyLabelInfo>();

    	readonly ImpacPersistenceManager _pm;


		/// <summary>
		/// The staff category value to be displayed or returned
		/// </summary>
		public StaffTypeOrCategory Value { get; set; }

        #region Constructor and initialization
        /// <summary> Default ctor </summary>
        public StaffTypeOrCategorySelector()
        {
            InitializeComponent();
        	_pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
        }

        private void StaffCategorySelector_Load(object sender, EventArgs e)
        {
			if (Value == null)
				return;

            try
            {
            	comboBoxClassifier.SelectedIndex = Value.ClassifierType;

            	foreach (KeyLabelInfo t in _items)
            	{
            		if (t.Key == Value.Value)
            		{
            			lookUpValue.EditValue = t;
            			break;
            		}
            	}
            }
            catch (Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(this.Text + ": " + ex);
            }
        }
        #endregion

        #region Dialog closing
        private void barButtonItemOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			CloseDialog();
        }

        private void barButtonItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ValidateChildren();
            DialogResult = DialogResult.Cancel;
            Close();
        }

		private void CloseDialog()
		{
			if (comboBoxClassifier.SelectedIndex == -1)
				return;
			var info = lookUpValue.EditValue as KeyLabelInfo;
			if (info == null)
				return;

			Value = new StaffTypeOrCategory
			        	{
			        		ClassifierType = comboBoxClassifier.SelectedIndex,
			        		Value = info.Key
			        	};

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

        #endregion

		#region Event handlers
		private void comboBoxClassifier_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				lookUpValue.Properties.DataSource = null;
				lookUpValue.Properties.Columns.Clear();
				_items.Clear();

				switch (comboBoxClassifier.SelectedIndex)
				{
					case (int) StaffTypeOrCategory.Classifiers.StaffType:
						GetStaffTypes();
						FillInValueDropdown();
						break;
					case (int) StaffTypeOrCategory.Classifiers.StaffCategory:
						GetStaffCategories();
						FillInValueDropdown();
						break;
				}
			}
			catch (Exception ex)
			{
				XlateMessageBox.Error(ex.Message);
			}
		}

    	private void GetStaffTypes()
		{
			var query = new ImpacRdbQuery(typeof(Prompt));
			query.AddClause(PromptDataRow.PGroupEntityColumn, EntityQueryOp.EQ, "STF0");
			query.AddClause(PromptDataRow.TextEntityColumn, EntityQueryOp.NE, "Location");
			var staffTypes = _pm.GetEntities<Prompt>(query);
			foreach (var staffType in staffTypes)
			{
				var item = new KeyLabelInfo { Key = staffType.Pro_ID, Label = staffType.Text };
				_items.Add(item);
			}
		}

		private void GetStaffCategories()
		{
			AddStaffCategory(BOM.Entities.Defs.StaffRole.Physician);
			AddStaffCategory(BOM.Entities.Defs.StaffRole.Resident);
			AddStaffCategory(BOM.Entities.Defs.StaffRole.Therapist);
			AddStaffCategory(BOM.Entities.Defs.StaffRole.SystemManager);
			AddStaffCategory(BOM.Entities.Defs.StaffRole.Physicist);
			AddStaffCategory(BOM.Entities.Defs.StaffRole.ServiceEngineer);
			AddStaffCategory(BOM.Entities.Defs.StaffRole.Dosimetrist);
			AddStaffCategory(BOM.Entities.Defs.StaffRole.Nurse);
			AddStaffCategory(BOM.Entities.Defs.StaffRole.Administrator);
			AddStaffCategory(BOM.Entities.Defs.StaffRole.Clerical);
			AddStaffCategory(BOM.Entities.Defs.StaffRole.Billing);
			AddStaffCategory(BOM.Entities.Defs.StaffRole.PPS);
		}

		private void AddStaffCategory(BOM.Entities.Defs.StaffRole category)
		{
			var item = new KeyLabelInfo {Key = (int) category, Label = Staff.StaffRoleToString((short) category)};
			_items.Add(item);
		}

		private void FillInValueDropdown()
		{
			lookUpValue.Properties.DisplayMember = "Label";
			lookUpValue.Properties.DataSource = _items;
			var lookUpColumnInfo = new LookUpColumnInfo("Label");
			lookUpValue.Properties.Columns.Add(lookUpColumnInfo);

			int dropDownCount = _items.Count > 20 ? 20 : _items.Count;
			lookUpValue.Properties.DropDownRows = dropDownCount;

			lookUpValue.EditValue = null;
		}
		#endregion
	}
}
