namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors
{
    partial class FlowsheetTabSelector
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlowsheetTabSelector));
			this.impacLayoutControl1 = new Impac.Mosaiq.UI.Controls.LayoutControls.ImpacLayoutControl();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.barButtonItemOK = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemCancel = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.lciMinValue = new DevExpress.XtraLayout.LayoutControlItem();
			this.lciMaxValue = new DevExpress.XtraLayout.LayoutControlItem();
			this.impacLayoutControl2 = new Impac.Mosaiq.UI.Controls.LayoutControls.ImpacLayoutControl();
			this.listTabNames = new DevExpress.XtraEditors.ListBoxControl();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize) (this.impacLayoutControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.lciMinValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.lciMaxValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.impacLayoutControl2)).BeginInit();
			this.impacLayoutControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.listTabNames)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// impacLayoutControl1
			// 
			resources.ApplyResources(this.impacLayoutControl1, "impacLayoutControl1");
			this.impacLayoutControl1.Name = "impacLayoutControl1";
			this.impacLayoutControl1.Root = this.layoutControlGroup1;
			// 
			// layoutControlGroup1
			// 
			resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(262, 228);
			this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// barManager
			// 
			this.barManager.AllowCustomization = false;
			this.barManager.AllowMoveBarOnToolbar = false;
			this.barManager.AllowQuickCustomization = false;
			this.barManager.AllowShowToolbarsPopup = false;
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemOK,
            this.barButtonItemCancel});
			this.barManager.MaxItemId = 2;
			// 
			// bar1
			// 
			this.bar1.BarName = "Custom 2";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemOK),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCancel)});
			this.bar1.OptionsBar.AllowQuickCustomization = false;
			this.bar1.OptionsBar.DrawDragBorder = false;
			this.bar1.OptionsBar.UseWholeRow = true;
			resources.ApplyResources(this.bar1, "bar1");
			// 
			// barButtonItemOK
			// 
			this.barButtonItemOK.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			resources.ApplyResources(this.barButtonItemOK, "barButtonItemOK");
			this.barButtonItemOK.Glyph = ((System.Drawing.Image) (resources.GetObject("barButtonItemOK.Glyph")));
			this.barButtonItemOK.Id = 0;
			this.barButtonItemOK.Name = "barButtonItemOK";
			this.barButtonItemOK.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.barButtonItemOK.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemOK_ItemClick);
			// 
			// barButtonItemCancel
			// 
			resources.ApplyResources(this.barButtonItemCancel, "barButtonItemCancel");
			this.barButtonItemCancel.Glyph = ((System.Drawing.Image) (resources.GetObject("barButtonItemCancel.Glyph")));
			this.barButtonItemCancel.Id = 1;
			this.barButtonItemCancel.Name = "barButtonItemCancel";
			this.barButtonItemCancel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.barButtonItemCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCancel_ItemClick);
			// 
			// lciMinValue
			// 
			this.lciMinValue.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9F);
			this.lciMinValue.AppearanceItemCaption.Options.UseFont = true;
			this.lciMinValue.AppearanceItemCaption.Options.UseTextOptions = true;
			this.lciMinValue.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lciMinValue.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			resources.ApplyResources(this.lciMinValue, "lciMinValue");
			this.lciMinValue.Location = new System.Drawing.Point(0, 0);
			this.lciMinValue.Name = "lciMinValue";
			this.lciMinValue.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 20, 5, 5);
			this.lciMinValue.Size = new System.Drawing.Size(491, 34);
			this.lciMinValue.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lciMinValue.TextSize = new System.Drawing.Size(58, 20);
			// 
			// lciMaxValue
			// 
			this.lciMaxValue.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9F);
			this.lciMaxValue.AppearanceItemCaption.Options.UseFont = true;
			this.lciMaxValue.AppearanceItemCaption.Options.UseTextOptions = true;
			this.lciMaxValue.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lciMaxValue.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			resources.ApplyResources(this.lciMaxValue, "lciMaxValue");
			this.lciMaxValue.Location = new System.Drawing.Point(0, 34);
			this.lciMaxValue.Name = "lciMaxValue";
			this.lciMaxValue.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 20, 5, 5);
			this.lciMaxValue.Size = new System.Drawing.Size(491, 34);
			this.lciMaxValue.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lciMaxValue.TextSize = new System.Drawing.Size(58, 20);
			// 
			// impacLayoutControl2
			// 
			this.impacLayoutControl2.Controls.Add(this.listTabNames);
			resources.ApplyResources(this.impacLayoutControl2, "impacLayoutControl2");
			this.impacLayoutControl2.Name = "impacLayoutControl2";
			this.impacLayoutControl2.Root = this.layoutControlGroup2;
			// 
			// listTabNames
			// 
			this.listTabNames.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.listTabNames.Appearance.Options.UseFont = true;
			resources.ApplyResources(this.listTabNames, "listTabNames");
			this.listTabNames.Name = "listTabNames";
			this.listTabNames.StyleController = this.impacLayoutControl2;
			this.listTabNames.DoubleClick += new System.EventHandler(this.listTabNames_DoubleClick);
			// 
			// layoutControlGroup2
			// 
			resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup2.Name = "layoutControlGroup1";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(262, 228);
			this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9F);
			this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem1.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.layoutControlItem1.Control = this.listTabNames;
			resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
			this.layoutControlItem1.Size = new System.Drawing.Size(260, 226);
			this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextToControlDistance = 0;
			this.layoutControlItem1.TextVisible = false;
			// 
			// FlowsheetTabSelector
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.impacLayoutControl2);
			this.Controls.Add(this.impacLayoutControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.KeyPreview = true;
			this.Name = "FlowsheetTabSelector";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.FlowsheetTabSelector_Load);
			((System.ComponentModel.ISupportInitialize) (this.impacLayoutControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.lciMinValue)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.lciMaxValue)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.impacLayoutControl2)).EndInit();
			this.impacLayoutControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (this.listTabNames)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlItem1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private UI.Controls.LayoutControls.ImpacLayoutControl impacLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOK;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancel;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraLayout.LayoutControlItem lciMinValue;
        private DevExpress.XtraLayout.LayoutControlItem lciMaxValue;
        private UI.Controls.LayoutControls.ImpacLayoutControl impacLayoutControl2;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.ListBoxControl listTabNames;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}