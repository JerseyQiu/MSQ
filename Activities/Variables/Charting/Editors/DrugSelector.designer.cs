namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors
{
    partial class DrugSelector
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrugSelector));
			this.impacLayoutControl1 = new Impac.Mosaiq.UI.Controls.LayoutControls.ImpacLayoutControl();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.barManager = new DevExpress.XtraBars.BarManager();
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
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.drugSearchControl = new Impac.Mosaiq.Charting.Controls.DrugSearchControl();
			((System.ComponentModel.ISupportInitialize) (this.impacLayoutControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.lciMinValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.lciMaxValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.impacLayoutControl2)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlGroup2)).BeginInit();
			this.SuspendLayout();
			// 
			// impacLayoutControl1
			// 
			resources.ApplyResources(this.impacLayoutControl1, "impacLayoutControl1");
			this.impacLayoutControl1.Name = "impacLayoutControl1";
			this.impacLayoutControl1.OptionsCustomizationForm.EnableUndoManager = false;
			this.impacLayoutControl1.Root = this.layoutControlGroup1;
			// 
			// layoutControlGroup1
			// 
			resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(412, 50);
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
			// lciMinValue
			// 
			this.lciMinValue.AppearanceItemCaption.Font = ((System.Drawing.Font) (resources.GetObject("lciMinValue.AppearanceItemCaption.Font")));
			this.lciMinValue.AppearanceItemCaption.Options.UseFont = true;
			this.lciMinValue.AppearanceItemCaption.Options.UseTextOptions = true;
			this.lciMinValue.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lciMinValue.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			resources.ApplyResources(this.lciMinValue, "lciMinValue");
			this.lciMinValue.Location = new System.Drawing.Point(0, 0);
			this.lciMinValue.Name = "lciMinValue";
			this.lciMinValue.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 20, 5, 5);
			this.lciMinValue.Size = new System.Drawing.Size(491, 34);
			this.lciMinValue.TextSize = new System.Drawing.Size(58, 20);
			this.lciMinValue.TextToControlDistance = 5;
			// 
			// lciMaxValue
			// 
			this.lciMaxValue.AppearanceItemCaption.Font = ((System.Drawing.Font) (resources.GetObject("lciMaxValue.AppearanceItemCaption.Font")));
			this.lciMaxValue.AppearanceItemCaption.Options.UseFont = true;
			this.lciMaxValue.AppearanceItemCaption.Options.UseTextOptions = true;
			this.lciMaxValue.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lciMaxValue.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			resources.ApplyResources(this.lciMaxValue, "lciMaxValue");
			this.lciMaxValue.Location = new System.Drawing.Point(0, 34);
			this.lciMaxValue.Name = "lciMaxValue";
			this.lciMaxValue.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 20, 5, 5);
			this.lciMaxValue.Size = new System.Drawing.Size(491, 34);
			this.lciMaxValue.TextSize = new System.Drawing.Size(58, 20);
			this.lciMaxValue.TextToControlDistance = 5;
			// 
			// impacLayoutControl2
			// 
			resources.ApplyResources(this.impacLayoutControl2, "impacLayoutControl2");
			this.impacLayoutControl2.Name = "impacLayoutControl2";
			this.impacLayoutControl2.OptionsCustomizationForm.EnableUndoManager = false;
			this.impacLayoutControl2.Root = this.layoutControlGroup2;
			// 
			// layoutControlGroup2
			// 
			resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup2.Name = "layoutControlGroup1";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(412, 50);
			this.layoutControlGroup2.TextVisible = false;
			// 
			// drugSearchControl
			// 
			this.drugSearchControl.AllowClassSearch = false;
			this.drugSearchControl.AllowStrengthSelection = true;
			resources.ApplyResources(this.drugSearchControl, "drugSearchControl");
			this.drugSearchControl.Drug_ID = 0;
			this.drugSearchControl.IncludeStrength = false;
			this.drugSearchControl.Name = "drugSearchControl";
			this.drugSearchControl.SearchGenericsOnly = false;
			this.drugSearchControl.ShowFullDescription = false;
			this.drugSearchControl.ShowOnPBSColumn = false;
			this.drugSearchControl.UseBrand = true;
			// 
			// DrugSelector
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.drugSearchControl);
			this.Controls.Add(this.impacLayoutControl2);
			this.Controls.Add(this.impacLayoutControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Name = "DrugSelector";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.DrugSelector_Load);
			((System.ComponentModel.ISupportInitialize) (this.impacLayoutControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.lciMinValue)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.lciMaxValue)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.impacLayoutControl2)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlGroup2)).EndInit();
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
		private Mosaiq.Charting.Controls.DrugSearchControl drugSearchControl;
    }
}