namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors
{
    partial class CdsReferenceSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CdsReferenceSelector));
			this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
			this.gridCdsReference = new Impac.Mosaiq.UI.Controls.ImpacGrids.ImpacGrid();
			this.bsCdsReference = new System.Windows.Forms.BindingSource();
			this.gridViewCdsReference = new Impac.Mosaiq.UI.Controls.ImpacGrids.ImpacGridView();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.richTextBoxDetail = new System.Windows.Forms.RichTextBox();
			this.menuCdsReferenceBrowse = new Impac.Mosaiq.UI.Framework.WidgetContextMenu();
			this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemChange = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.barManager = new Impac.Mosaiq.UI.Controls.ImpacToolbars.ImpacBarManager();
			this.barWidget = new DevExpress.XtraBars.Bar();
			this.btnSelect = new DevExpress.XtraBars.BarButtonItem();
			this.btnCancel = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.repositoryItemLookUpEditAlert = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.repositoryItemComboBoxStatuses = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
			this.splitContainerControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridCdsReference)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsCdsReference)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewCdsReference)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditAlert)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatuses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
			this.layoutControlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainerControl1
			// 
			resources.ApplyResources(this.splitContainerControl1, "splitContainerControl1");
			this.splitContainerControl1.Name = "splitContainerControl1";
			this.splitContainerControl1.Panel1.Controls.Add(this.gridCdsReference);
			resources.ApplyResources(this.splitContainerControl1.Panel1, "splitContainerControl1.Panel1");
			this.splitContainerControl1.Panel2.Controls.Add(this.richTextBoxDetail);
			resources.ApplyResources(this.splitContainerControl1.Panel2, "splitContainerControl1.Panel2");
			this.splitContainerControl1.SplitterPosition = 352;
			// 
			// gridCdsReference
			// 
			this.gridCdsReference.DataSource = this.bsCdsReference;
			resources.ApplyResources(this.gridCdsReference, "gridCdsReference");
			this.gridCdsReference.MainView = this.gridViewCdsReference;
			this.gridCdsReference.Name = "gridCdsReference";
			this.gridCdsReference.ShowDataSourceError = false;
			this.gridCdsReference.UseTagging = false;
			this.gridCdsReference.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCdsReference});
			this.gridCdsReference.DoubleClick += new System.EventHandler(this.gridCdsReference_DoubleClick);
			// 
			// bsCdsReference
			// 
			this.bsCdsReference.DataSource = typeof(Impac.Mosaiq.BOM.Entities.CDSReference);
			// 
			// gridViewCdsReference
			// 
			this.gridViewCdsReference.Appearance.EvenRow.Font = ((System.Drawing.Font)(resources.GetObject("impacGridViewCdsReference.Appearance.EvenRow.Font")));
			this.gridViewCdsReference.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewCdsReference.Appearance.FixedLine.BackColor = ((System.Drawing.Color)(resources.GetObject("impacGridViewCdsReference.Appearance.FixedLine.BackColor")));
			this.gridViewCdsReference.Appearance.FixedLine.Font = ((System.Drawing.Font)(resources.GetObject("impacGridViewCdsReference.Appearance.FixedLine.Font")));
			this.gridViewCdsReference.Appearance.FixedLine.Options.UseBackColor = true;
			this.gridViewCdsReference.Appearance.FixedLine.Options.UseFont = true;
			this.gridViewCdsReference.Appearance.FocusedCell.BackColor = ((System.Drawing.Color)(resources.GetObject("impacGridViewCdsReference.Appearance.FocusedCell.BackColor")));
			this.gridViewCdsReference.Appearance.FocusedCell.Font = ((System.Drawing.Font)(resources.GetObject("impacGridViewCdsReference.Appearance.FocusedCell.Font")));
			this.gridViewCdsReference.Appearance.FocusedCell.Options.UseBackColor = true;
			this.gridViewCdsReference.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewCdsReference.Appearance.FocusedRow.BackColor = ((System.Drawing.Color)(resources.GetObject("impacGridViewCdsReference.Appearance.FocusedRow.BackColor")));
			this.gridViewCdsReference.Appearance.FocusedRow.BackColor2 = ((System.Drawing.Color)(resources.GetObject("impacGridViewCdsReference.Appearance.FocusedRow.BackColor2")));
			this.gridViewCdsReference.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("impacGridViewCdsReference.Appearance.FocusedRow.Font")));
			this.gridViewCdsReference.Appearance.FocusedRow.ForeColor = ((System.Drawing.Color)(resources.GetObject("impacGridViewCdsReference.Appearance.FocusedRow.ForeColor")));
			this.gridViewCdsReference.Appearance.FocusedRow.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("impacGridViewCdsReference.Appearance.FocusedRow.GradientMode")));
			this.gridViewCdsReference.Appearance.FocusedRow.Options.UseBackColor = true;
			this.gridViewCdsReference.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewCdsReference.Appearance.FocusedRow.Options.UseForeColor = true;
			this.gridViewCdsReference.Appearance.GroupPanel.Font = ((System.Drawing.Font)(resources.GetObject("impacGridViewCdsReference.Appearance.GroupPanel.Font")));
			this.gridViewCdsReference.Appearance.GroupPanel.Options.UseFont = true;
			this.gridViewCdsReference.Appearance.GroupRow.Font = ((System.Drawing.Font)(resources.GetObject("impacGridViewCdsReference.Appearance.GroupRow.Font")));
			this.gridViewCdsReference.Appearance.GroupRow.Options.UseFont = true;
			this.gridViewCdsReference.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("impacGridViewCdsReference.Appearance.HeaderPanel.Font")));
			this.gridViewCdsReference.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewCdsReference.Appearance.HideSelectionRow.BackColor = ((System.Drawing.Color)(resources.GetObject("impacGridViewCdsReference.Appearance.HideSelectionRow.BackColor")));
			this.gridViewCdsReference.Appearance.HideSelectionRow.BackColor2 = ((System.Drawing.Color)(resources.GetObject("impacGridViewCdsReference.Appearance.HideSelectionRow.BackColor2")));
			this.gridViewCdsReference.Appearance.HideSelectionRow.Font = ((System.Drawing.Font)(resources.GetObject("impacGridViewCdsReference.Appearance.HideSelectionRow.Font")));
			this.gridViewCdsReference.Appearance.HideSelectionRow.ForeColor = ((System.Drawing.Color)(resources.GetObject("impacGridViewCdsReference.Appearance.HideSelectionRow.ForeColor")));
			this.gridViewCdsReference.Appearance.HideSelectionRow.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("impacGridViewCdsReference.Appearance.HideSelectionRow.GradientMode")));
			this.gridViewCdsReference.Appearance.HideSelectionRow.Options.UseBackColor = true;
			this.gridViewCdsReference.Appearance.HideSelectionRow.Options.UseFont = true;
			this.gridViewCdsReference.Appearance.HideSelectionRow.Options.UseForeColor = true;
			this.gridViewCdsReference.Appearance.OddRow.Font = ((System.Drawing.Font)(resources.GetObject("impacGridViewCdsReference.Appearance.OddRow.Font")));
			this.gridViewCdsReference.Appearance.OddRow.Options.UseFont = true;
			this.gridViewCdsReference.Appearance.Row.Font = ((System.Drawing.Font)(resources.GetObject("impacGridViewCdsReference.Appearance.Row.Font")));
			this.gridViewCdsReference.Appearance.Row.Options.UseFont = true;
			this.gridViewCdsReference.Appearance.Row.Options.UseTextOptions = true;
			this.gridViewCdsReference.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.gridViewCdsReference.Appearance.SelectedRow.BackColor = ((System.Drawing.Color)(resources.GetObject("impacGridViewCdsReference.Appearance.SelectedRow.BackColor")));
			this.gridViewCdsReference.Appearance.SelectedRow.BackColor2 = ((System.Drawing.Color)(resources.GetObject("impacGridViewCdsReference.Appearance.SelectedRow.BackColor2")));
			this.gridViewCdsReference.Appearance.SelectedRow.Font = ((System.Drawing.Font)(resources.GetObject("impacGridViewCdsReference.Appearance.SelectedRow.Font")));
			this.gridViewCdsReference.Appearance.SelectedRow.ForeColor = ((System.Drawing.Color)(resources.GetObject("impacGridViewCdsReference.Appearance.SelectedRow.ForeColor")));
			this.gridViewCdsReference.Appearance.SelectedRow.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("impacGridViewCdsReference.Appearance.SelectedRow.GradientMode")));
			this.gridViewCdsReference.Appearance.SelectedRow.Options.UseBackColor = true;
			this.gridViewCdsReference.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewCdsReference.Appearance.SelectedRow.Options.UseForeColor = true;
			this.gridViewCdsReference.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2});
			this.gridViewCdsReference.EnableColumnCustomizationMenuOption = true;
			this.gridViewCdsReference.EnableRemoveColumnMenuOption = true;
			this.gridViewCdsReference.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
			this.gridViewCdsReference.GridControl = this.gridCdsReference;
			this.gridViewCdsReference.HideBestFitAllMenuOption = false;
			this.gridViewCdsReference.HideBestFitMenuOption = false;
			this.gridViewCdsReference.HideColumnChooserMenuOption = false;
			this.gridViewCdsReference.HideFilterEditorMenuOption = false;
			this.gridViewCdsReference.HideGroupByThisColumnMenuOption = false;
			this.gridViewCdsReference.HideRemoveThisColumnMenuOption = false;
			this.gridViewCdsReference.HideShowAutoFilterRowMenuOption = false;
			this.gridViewCdsReference.HideShowFindPanelMenuOption = false;
			this.gridViewCdsReference.HideShowGroupByBoxMenuOption = false;
			this.gridViewCdsReference.Name = "gridViewCdsReference";
			this.gridViewCdsReference.OptionsBehavior.AllowIncrementalSearch = true;
			this.gridViewCdsReference.OptionsBehavior.Editable = false;
			this.gridViewCdsReference.OptionsLayout.Columns.StoreAllOptions = true;
			this.gridViewCdsReference.OptionsLayout.StoreAllOptions = true;
			this.gridViewCdsReference.OptionsView.RowAutoHeight = true;
			this.gridViewCdsReference.OptionsView.ShowDetailButtons = false;
			this.gridViewCdsReference.OptionsView.ShowGroupPanel = false;
			this.gridViewCdsReference.OptionsView.ShowIndicator = false;
			this.gridViewCdsReference.ShowDisplayFooterMenuOption = false;
			this.gridViewCdsReference.ShowExportDataMenuOption = false;
			this.gridViewCdsReference.ShowFixColumnLeftMenuOption = false;
			this.gridViewCdsReference.ShowFixColumnRightMenuOption = false;
			this.gridViewCdsReference.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.impacGridViewCdsReference_FocusedRowChanged);
			// 
			// gridColumn2
			// 
			this.gridColumn2.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("gridColumn2.AppearanceCell.Font")));
			this.gridColumn2.AppearanceCell.Options.UseFont = true;
			this.gridColumn2.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gridColumn2.AppearanceHeader.Font")));
			this.gridColumn2.AppearanceHeader.Options.UseFont = true;
			resources.ApplyResources(this.gridColumn2, "gridColumn2");
			this.gridColumn2.FieldName = "Title";
			this.gridColumn2.Name = "gridColumn2";
			// 
			// richTextBoxDetail
			// 
			this.richTextBoxDetail.BackColor = System.Drawing.SystemColors.Window;
			resources.ApplyResources(this.richTextBoxDetail, "richTextBoxDetail");
			this.richTextBoxDetail.Name = "richTextBoxDetail";
			this.richTextBoxDetail.ReadOnly = true;
			this.richTextBoxDetail.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxDetail_LinkClicked);
			// 
			// menuCdsReferenceBrowse
			// 
			this.menuCdsReferenceBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(217)))), ((int)(((byte)(216)))));
			resources.ApplyResources(this.menuCdsReferenceBrowse, "menuCdsReferenceBrowse");
			this.menuCdsReferenceBrowse.ImpacGridControl = null;
			this.menuCdsReferenceBrowse.Name = "contextMenuStrip1";
			this.menuCdsReferenceBrowse.ShowAdd = false;
			this.menuCdsReferenceBrowse.ShowDocking = true;
			this.menuCdsReferenceBrowse.ShowImageMargin = false;
			// 
			// toolStripMenuItemAdd
			// 
			this.toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
			resources.ApplyResources(this.toolStripMenuItemAdd, "toolStripMenuItemAdd");
			// 
			// toolStripMenuItemChange
			// 
			this.toolStripMenuItemChange.Name = "toolStripMenuItemChange";
			resources.ApplyResources(this.toolStripMenuItemChange, "toolStripMenuItemChange");
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			resources.ApplyResources(this.toolStripMenuItemDelete, "toolStripMenuItemDelete");
			// 
			// barManager
			// 
			this.barManager.AllowShowToolbarsPopup = false;
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barWidget});
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnSelect,
            this.btnCancel});
			this.barManager.MaxItemId = 35;
			// 
			// barWidget
			// 
			this.barWidget.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("barWidget.Appearance.Font")));
			this.barWidget.Appearance.Options.UseFont = true;
			this.barWidget.BarName = "barWidget";
			this.barWidget.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
			this.barWidget.DockCol = 0;
			this.barWidget.DockRow = 0;
			this.barWidget.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.barWidget.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelect),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancel)});
			this.barWidget.OptionsBar.AllowQuickCustomization = false;
			this.barWidget.OptionsBar.DisableCustomization = true;
			this.barWidget.OptionsBar.DrawDragBorder = false;
			this.barWidget.OptionsBar.RotateWhenVertical = false;
			this.barWidget.OptionsBar.UseWholeRow = true;
			resources.ApplyResources(this.barWidget, "barWidget");
			// 
			// btnSelect
			// 
			this.btnSelect.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.btnSelect.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnSelect.Appearance.Font")));
			this.btnSelect.Appearance.Options.UseFont = true;
			resources.ApplyResources(this.btnSelect, "btnSelect");
			this.btnSelect.Id = 32;
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelect_ItemClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.btnCancel.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnCancel.Appearance.Font")));
			this.btnCancel.Appearance.Options.UseFont = true;
			resources.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.Id = 34;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancel_ItemClick);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
			// 
			// repositoryItemLookUpEditAlert
			// 
			this.repositoryItemLookUpEditAlert.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemLookUpEditAlert.Buttons"))))});
			this.repositoryItemLookUpEditAlert.Name = "repositoryItemLookUpEditAlert";
			// 
			// repositoryItemComboBoxStatuses
			// 
			this.repositoryItemComboBoxStatuses.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemComboBoxStatuses.Buttons"))))});
			this.repositoryItemComboBoxStatuses.Name = "repositoryItemComboBoxStatuses";
			// 
			// layoutControlMain
			// 
			this.layoutControlMain.Controls.Add(this.splitContainerControl1);
			resources.ApplyResources(this.layoutControlMain, "layoutControlMain");
			this.layoutControlMain.Name = "layoutControlMain";
			this.layoutControlMain.Root = this.layoutControlGroupMain;
			// 
			// layoutControlGroupMain
			// 
			resources.ApplyResources(this.layoutControlGroupMain, "layoutControlGroupMain");
			this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroupMain.GroupBordersVisible = false;
			this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupMain.Name = "layoutControlGroupMain";
			this.layoutControlGroupMain.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
			this.layoutControlGroupMain.Size = new System.Drawing.Size(880, 469);
			this.layoutControlGroupMain.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.splitContainerControl1;
			resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(876, 465);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextToControlDistance = 0;
			this.layoutControlItem1.TextVisible = false;
			// 
			// CdsReferenceSelector
			// 
			this.Appearance.Options.UseBackColor = true;
			this.Appearance.Options.UseFont = true;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ContextMenuStrip = this.menuCdsReferenceBrowse;
			this.Controls.Add(this.layoutControlMain);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "CdsReferenceSelector";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.CdsReferenceSelector_Load);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
			this.splitContainerControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridCdsReference)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsCdsReference)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewCdsReference)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditAlert)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatuses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
			this.layoutControlMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private Impac.Mosaiq.UI.Framework.WidgetContextMenu menuCdsReferenceBrowse;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private UI.Controls.ImpacToolbars.ImpacBarManager barManager;
        private DevExpress.XtraBars.Bar barWidget;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChange;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditAlert;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxStatuses;
        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
		private System.Windows.Forms.BindingSource bsCdsReference;
		private DevExpress.XtraBars.BarButtonItem btnSelect;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private UI.Controls.ImpacGrids.ImpacGrid gridCdsReference;
		private UI.Controls.ImpacGrids.ImpacGridView gridViewCdsReference;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private System.Windows.Forms.RichTextBox richTextBoxDetail;
		private DevExpress.XtraBars.BarButtonItem btnCancel;
    }
}
