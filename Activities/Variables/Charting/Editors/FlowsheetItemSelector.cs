using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.Core.Defs.Enumerations;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors
{
	/// <summary>
	/// An editor that allows the user to select a flowsheet tab view and then 
	/// select/deselect individual items from that view.
	/// </summary>
    public partial class FlowsheetItemSelector : XtraForm
    {
		readonly private ImpacPersistenceManager _pm;
		private EntityList<ObsDef> _tabNames;
		private EntityList<ObsDef> _tabViews;
		private bool _initializingItemState;

		private readonly Guid _otherLabsItemGuid = Guid.Parse(FlowsheetSelection.OtherLabsGuidString);

		const string DisplayFieldName = "Label";

		#region Constructor and initialization
        /// <summary> Default ctor </summary>
        public FlowsheetItemSelector()
        {
            InitializeComponent();

			_pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
		}

		private void FlowsheetItemSelector_Load(object sender, EventArgs e)
		{
			try
			{
				var query = new ImpacRdbQuery(typeof(ObsDef));
				query.AddClause(ObsDefDataRow.TypeEntityColumn, EntityQueryOp.EQ, (int) MedDefs.ObdType.ViewFilter);
                query.AddClause(ObsDefDataRow.ActiveEntityColumn, EntityQueryOp.EQ, true);
                query.AddOrderBy(ObsDefDataRow.LabelEntityColumn);
				_tabNames = _pm.GetEntities<ObsDef>(query);
				InitDropDown(lookUpEditFlowsheetTab, _tabNames, Value.TabGuid);

				LoadTabViews();
				InitDropDown(lookUpEditFlowsheetViews, _tabViews, Value.ItemGuids != null ? Value.ViewGuid : Guid.Empty);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine("FlowsheetItemSelector: " + ex);
			}

			if (Value.ItemGuids == null)
				return;

			// disable the logic in treeTabViewItems_AfterCheck in this case, because
			// we will set the Checked flag on each node individually
			_initializingItemState = true;
			int pos = 0;
			SetCheckedItems(treeTabViewItems.Nodes, Value.ItemGuids, ref pos);
			_initializingItemState = false;
		}
        #endregion

		/// <summary>
		/// The flowsheet selection to be displayed or returned
		/// </summary>
		public FlowsheetSelection Value { get; set; }

		/// <summary>
		/// A flag to indicate whether the 'Other Labs' function item should be incuded
		/// </summary>
		public bool IncudeOtherLabs { get; set; }

        #region Event Handlers
		private void lookUpEditFlowsheetTab_EditValueChanged(object sender, EventArgs e)
		{
			var item = (ObsDef) lookUpEditFlowsheetTab.EditValue;
			Value.TabGuid = item.OBD_GUID;

			LoadTabViews();
			InitDropDown(lookUpEditFlowsheetViews, _tabViews, Guid.Empty);

			treeTabViewItems.Nodes.Clear();
		}

		private void lookUpEditFlowsheetViews_EditValueChanged(object sender, EventArgs e)
		{
			if (lookUpEditFlowsheetViews.EditValue == null)
				return;
			var item = (ObsDef) lookUpEditFlowsheetViews.EditValue;
			//_tbl = item.Tbl;
			PopulateTree(item.View_ID);
			treeTabViewItems.Focus();
		}

		private void treeTabViewItems_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (_initializingItemState)
				return;
			// setting a child node's Checked property will call this handler recursively
			foreach (TreeNode node in e.Node.Nodes)
				node.Checked = e.Node.Checked;
		}

		private void treeTabViewItems_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
		{
			// don't allow collapsing of the tree nodes
			e.Cancel = true;
		}

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
        #endregion

		#region Private methods

		private static void InitDropDown(LookUpEdit control, EntityList<ObsDef> items, Guid currentValue)
		{
			control.Properties.DataSource = null;
			control.Properties.Columns.Clear();

			control.Properties.DisplayMember = DisplayFieldName;
			control.Properties.DataSource = items;
			var lookUpColumnInfo = new LookUpColumnInfo(DisplayFieldName);
			control.Properties.Columns.Add(lookUpColumnInfo);

			control.Properties.DropDownRows = items.Count > 20 ? 20 : items.Count;

			control.EditValue = null;
			if (currentValue == Guid.Empty)
				return;
			foreach (var item in items)
				if (item.OBD_GUID == currentValue)
				{
					control.EditValue = item;
					break;
				}
		}

		private void LoadTabViews()
		{
			var query = new ImpacRdbQuery(typeof(ObsDef));
			query.AddClause(ObsDefDataRow.OBD_GUIDEntityColumn, EntityQueryOp.EQ, Value.TabGuid);
			var tab = _pm.GetEntity<ObsDef>(query);

			query = new ImpacRdbQuery(typeof(ObsDef));
			query.AddClause(ObsDefDataRow.TypeEntityColumn, EntityQueryOp.EQ, 1);
			query.AddClause(ObsDefDataRow.TblEntityColumn, EntityQueryOp.EQ, tab.Tbl);
            query.AddClause(ObsDefDataRow.ActiveEntityColumn, EntityQueryOp.EQ, true);
            query.AddOrderBy(ObsDefDataRow.LabelEntityColumn);
			_tabViews = _pm.GetEntities<ObsDef>(query);
		}

		private void CloseDialog()
		{
			Value = new FlowsheetSelection { TabGuid = Value.TabGuid };

			if (lookUpEditFlowsheetViews.EditValue != null && lookUpEditFlowsheetViews.EditValue is ObsDef)
			{
				var viewItems = new List<Guid>();
				AddCheckedItems(treeTabViewItems.Nodes, viewItems);

				var obsDef = lookUpEditFlowsheetViews.EditValue as ObsDef;
				Value.ViewGuid = obsDef.OBD_GUID;
				Value.ItemGuids = viewItems;
			}
			else
			{
				Value.ViewGuid = Guid.Empty;
				Value.ItemGuids = new List<Guid>();
			}

			ValidateChildren();
			DialogResult = DialogResult.OK;
			Close();
		}

		private enum FolderType
		{
			Root,
			MajorHeading,
			MinorHeading
		}

		private void PopulateTree(int viewId)
		{
			// load the items belonging to the selected tab view
			treeTabViewItems.Nodes.Clear();
			IEnumerable<ObsDef> items = LoadTabViewItems(viewId);

			// populate the tree with the view items so that it looks similar to the flowsheet
			TreeNode currentFolder = null;
			FolderType currentFolderType = FolderType.Root;
			ObsDef otherLabsEntity = null;
			foreach (var obsDef in items)
			{
                if (obsDef == null)
                    continue;

				// skip the 'Other Labs' function item
				if (obsDef.OBD_GUID == _otherLabsItemGuid)
				{
					otherLabsEntity = obsDef;
					continue;
				}
				var newNode = new TreeNode(obsDef.Label) { Tag = obsDef.OBD_GUID };
				var itemType = (MedDefs.ObdType) obsDef.Type;
				if (itemType == MedDefs.ObdType.MajorHeading || itemType == MedDefs.ObdType.MinorHeading)
				{
					newNode.BackColor = Color.DarkGray;
					newNode.ForeColor = Color.White;
				}

				TreeNode insertInNode = null; // an exisitng tree node in which the new node is to be inserted
				if (itemType == MedDefs.ObdType.MajorHeading)
				{
					// major headings are inserted at root level (i.e. insertInNode is null)
					currentFolder = newNode;
					currentFolderType = FolderType.MajorHeading;
				}
				else if (itemType == MedDefs.ObdType.MinorHeading)
				{
					// minor headings are inserted either as children of major headings or at root level
					if (currentFolderType == FolderType.MajorHeading)
						// insert inside the current major heading
						insertInNode = currentFolder;
					else if (currentFolderType == FolderType.MinorHeading && currentFolder != null)
						// insert at the same level as the previous minor heading
						insertInNode = currentFolder.Parent;

					currentFolder = newNode;
					currentFolderType = FolderType.MinorHeading;
				}
				else
				{
					// all other items are inserted in the current folder
					insertInNode = currentFolder;
				}

				if (insertInNode != null)
					insertInNode.Nodes.Add(newNode);
				else
					treeTabViewItems.Nodes.Add(newNode);


				/*
				if (itemType == MedDefs.ObdType.MajorHeading)
				{
					treeTabViewItems.Nodes.Add(newNode);
					currentFolder = newNode;
					currentFolderType = FolderType.MajorHeading;
				}
				else if (itemType == MedDefs.ObdType.MinorHeading)
				{
					if (currentFolderType == FolderType.MajorHeading && currentFolder != null)
						currentFolder.Nodes.Add(newNode);
					else if (currentFolderType == FolderType.MinorHeading && currentFolder != null)
						currentFolder.Parent.Nodes.Add(newNode);
					else
						treeTabViewItems.Nodes.Add(newNode);

					currentFolder = newNode;
					currentFolderType = FolderType.MinorHeading;
				}
				else
				{
					if (currentFolder != null)
						currentFolder.Nodes.Add(newNode);
					else
						treeTabViewItems.Nodes.Add(newNode);
				}
				 */
			}

			if (IncudeOtherLabs)
			{
				if (otherLabsEntity == null)
				{
					var query = new ImpacRdbQuery(typeof(ObsDef));
					query.AddClause(ObsDefDataRow.OBD_GUIDEntityColumn, EntityQueryOp.EQ, _otherLabsItemGuid);
					otherLabsEntity = _pm.GetEntity<ObsDef>(query);
				}

				var node = new TreeNode(otherLabsEntity.Label) { Tag = otherLabsEntity.OBD_GUID };
				node.BackColor = Color.DarkGray;
				node.ForeColor = Color.White;
				treeTabViewItems.Nodes.Add(node);
			}

			foreach (TreeNode nd in treeTabViewItems.Nodes)
				nd.Checked = true;

			treeTabViewItems.ExpandAll();
			if (treeTabViewItems.Nodes.Count > 0)
				treeTabViewItems.Nodes[0].EnsureVisible();
		}

		private IEnumerable<ObsDef> LoadTabViewItems(int viewId)
		{
			var subQuery = new ImpacRdbQuery(typeof(ObsDef));
			subQuery.AddClause(ObsDefDataRow.TypeEntityColumn, EntityQueryOp.EQ, 12);
			subQuery.AddClause(ObsDefDataRow.View_IDEntityColumn, EntityQueryOp.EQ, viewId);
            subQuery.AddOrderBy(ObsDefDataRow.SeqEntityColumn);     // chart items (obsdef.type=12) are internal only and don't need Active filter.

			var defs = _pm.GetEntities<ObsDef>(subQuery);
			var ids = defs.Select(o => o.Item_ID).ToList();
			var query = new ImpacRdbQuery(typeof(ObsDef));
			query.AddClause(ObsDefDataRow.OBD_IDEntityColumn, EntityQueryOp.In, ids);
            query.AddClause(ObsDefDataRow.ActiveEntityColumn, EntityQueryOp.EQ, true);
            var obsDefs = _pm.GetEntities<ObsDef>(query);

			// sort the result in the same order of IDs as in the 'ids' list
			var result = ids.Select(id => obsDefs.Find(o => o.OBD_ID == id)).ToList();
			return result.ToArray();
		}


		// Walk the tree from top to bottom
		private static void AddCheckedItems(TreeNodeCollection nodes, List<Guid> result)
		{
			foreach (TreeNode node in nodes)
			{
				if (node.Checked)
					result.Add((Guid) node.Tag);
				AddCheckedItems(node.Nodes, result);
			}
		}

		// walk the tree from top to bottom, the same order it is enumerated in AddCheckedItems
		private static void SetCheckedItems(TreeNodeCollection nodes, List<Guid> items, ref int pos)
		{
			foreach (TreeNode node in nodes)
			{
				node.Checked = pos < items.Count ? (Guid) node.Tag == items[pos] : false;
				if (node.Checked)
					pos++;
				SetCheckedItems(node.Nodes, items, ref pos);
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
