namespace Impac.Mosaiq.IQ.Activities.Variables.PM.Editors
{
    partial class DepartmentSelector
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepartmentSelector));
			this.impacLayoutControl1 = new Impac.Mosaiq.UI.Controls.LayoutControls.ImpacLayoutControl();
			this.inputDeptControl1 = new Impac.Mosaiq.UI.InputTemplates.General.InputDeptControl();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.barButtonItemOK = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemCancel = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize) (this.impacLayoutControl1)).BeginInit();
			this.impacLayoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.barManager)).BeginInit();
			this.SuspendLayout();
			// 
			// impacLayoutControl1
			// 
			this.impacLayoutControl1.Controls.Add(this.inputDeptControl1);
			resources.ApplyResources(this.impacLayoutControl1, "impacLayoutControl1");
			this.impacLayoutControl1.Name = "impacLayoutControl1";
			this.impacLayoutControl1.OptionsCustomizationForm.EnableUndoManager = false;
			this.impacLayoutControl1.Root = this.layoutControlGroup1;
			// 
			// inputDeptControl1
			// 
			this.inputDeptControl1.Appearance.Font = ((System.Drawing.Font) (resources.GetObject("inputDeptControl1.Appearance.Font")));
			this.inputDeptControl1.Appearance.Options.UseFont = true;
			this.inputDeptControl1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.inputDeptControl1.CFG_ID = null;
			this.inputDeptControl1.CheckSecurity = true;
			this.inputDeptControl1.DefaultErrorString = "Please select departments from the list";
			this.inputDeptControl1.DisableCtrlDeleteFunction = true;
			this.inputDeptControl1.EnteredTextIsAlwaysValid = false;
			this.inputDeptControl1.ErrorString = "";
			this.inputDeptControl1.ImmediatePopup = false;
			this.inputDeptControl1.IncludeOption = Impac.Mosaiq.UI.InputTemplates.Enumerations.DepartmentIncludeOptionsEnum.DepartmentsOnly;
			resources.ApplyResources(this.inputDeptControl1, "inputDeptControl1");
			this.inputDeptControl1.MaxDisplayRows = 30;
			this.inputDeptControl1.Name = "inputDeptControl1";
			this.inputDeptControl1.Required = false;
			this.inputDeptControl1.ResetOnInvalidEntry = true;
			this.inputDeptControl1.SetGlobalDepartmentAsDefault = true;
			this.inputDeptControl1.UseAbbreviation = false;
			this.inputDeptControl1.UseControlErrorProvider = false;
			this.inputDeptControl1.Value = null;
			// 
			// layoutControlGroup1
			// 
			resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(494, 32);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.AppearanceItemCaption.Font = ((System.Drawing.Font) (resources.GetObject("layoutControlItem1.AppearanceItemCaption.Font")));
			this.layoutControlItem1.AppearanceItemCaption.ForeColor = ((System.Drawing.Color) (resources.GetObject("layoutControlItem1.AppearanceItemCaption.ForeColor")));
			this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem1.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.layoutControlItem1.Control = this.inputDeptControl1;
			resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(492, 30);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(68, 15);
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
			// DepartmentSelector
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.impacLayoutControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.KeyPreview = true;
			this.Name = "DepartmentSelector";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.DepartmentSelector_Load);
			((System.ComponentModel.ISupportInitialize) (this.impacLayoutControl1)).EndInit();
			this.impacLayoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.barManager)).EndInit();
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
		private UI.InputTemplates.General.InputDeptControl inputDeptControl1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}