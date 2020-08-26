using Impac.Mosaiq.IQ.Activities.Variables.Common.Editor;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors
{
    partial class FlowsheetItemTypeSelector
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlowsheetItemTypeSelector));
            this.bmMain = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonOK = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.bsItems = new System.Windows.Forms.BindingSource(this.components);
            this.listItemTypes = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btnSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.btnClearAll = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bmMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listItemTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // bmMain
            // 
            this.bmMain.AllowCustomization = false;
            this.bmMain.AllowMoveBarOnToolbar = false;
            this.bmMain.AllowQuickCustomization = false;
            this.bmMain.AllowShowToolbarsPopup = false;
            this.bmMain.AutoSaveInRegistry = true;
            this.bmMain.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.bmMain.DockControls.Add(this.barDockControlTop);
            this.bmMain.DockControls.Add(this.barDockControlBottom);
            this.bmMain.DockControls.Add(this.barDockControlLeft);
            this.bmMain.DockControls.Add(this.barDockControlRight);
            this.bmMain.Form = this;
            this.bmMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonOK,
            this.barButtonCancel,
            this.btnSelectAll,
            this.btnClearAll});
            this.bmMain.MaxItemId = 8;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnClearAll, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonOK),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonCancel)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barButtonOK
            // 
            this.barButtonOK.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            resources.ApplyResources(this.barButtonOK, "barButtonOK");
            this.barButtonOK.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonOK.Glyph")));
            this.barButtonOK.Id = 2;
            this.barButtonOK.Name = "barButtonOK";
            this.barButtonOK.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonOK.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonOK_ItemClick);
            // 
            // barButtonCancel
            // 
            this.barButtonCancel.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            resources.ApplyResources(this.barButtonCancel, "barButtonCancel");
            this.barButtonCancel.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonCancel.Glyph")));
            this.barButtonCancel.Id = 3;
            this.barButtonCancel.Name = "barButtonCancel";
            this.barButtonCancel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonCancel_ItemClick);
            // 
            // bsItems
            // 
            this.bsItems.DataSource = typeof(Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors.FlowsheetItemType);
            // 
            // listItemTypes
            // 
            this.listItemTypes.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.listItemTypes.Appearance.Options.UseFont = true;
            this.listItemTypes.DataSource = this.bsItems;
            this.listItemTypes.DisplayMember = "DisplayName";
            resources.ApplyResources(this.listItemTypes, "listItemTypes");
            this.listItemTypes.Name = "listItemTypes";
            this.listItemTypes.ValueMember = "ItemType";
            this.listItemTypes.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.listItemTypes_ItemCheck);
            // 
            // btnSelectAll
            // 
            resources.ApplyResources(this.btnSelectAll, "btnSelectAll");
            this.btnSelectAll.Id = 6;
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectAll_ItemClick);
            // 
            // btnClearAll
            // 
            resources.ApplyResources(this.btnClearAll, "btnClearAll");
            this.btnClearAll.Id = 7;
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClearAll_ItemClick);
            // 
            // FlowsheetItemTypeSelector
            // 
            this.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.Appearance.Options.UseFont = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listItemTypes);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.KeyPreview = true;
            this.Name = "FlowsheetItemTypeSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FlowsheetItemTypeSelector_FormClosing);
            this.Load += new System.EventHandler(this.FlowsheetItemTypeSelector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bmMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listItemTypes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager bmMain;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonOK;
		private DevExpress.XtraBars.BarButtonItem barButtonCancel;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private System.Windows.Forms.BindingSource bsItems;
        private DevExpress.XtraEditors.CheckedListBoxControl listItemTypes;
        private DevExpress.XtraBars.BarButtonItem btnSelectAll;
        private DevExpress.XtraBars.BarButtonItem btnClearAll;
    }
}