namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Editor
{
    partial class DateValueEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateValueEditor));
            this.layoutControl = new Impac.Mosaiq.UI.Controls.LayoutControls.ImpacLayoutControl();
            this.customDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.comboDatePicker = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCustomDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemOK = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboDatePicker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCustomDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.AllowCustomizationMenu = false;
            this.layoutControl.Controls.Add(this.customDateEdit);
            this.layoutControl.Controls.Add(this.comboDatePicker);
            resources.ApplyResources(this.layoutControl, "layoutControl");
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AutoSize;
            this.layoutControl.Root = this.layoutControlGroup1;
            // 
            // customDateEdit
            // 
            resources.ApplyResources(this.customDateEdit, "customDateEdit");
            this.customDateEdit.Name = "customDateEdit";
            this.customDateEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.customDateEdit.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.customDateEdit.Properties.Appearance.Options.UseBackColor = true;
            this.customDateEdit.Properties.Appearance.Options.UseFont = true;
            this.customDateEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.customDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.customDateEdit.StyleController = this.layoutControl;
            // 
            // comboDatePicker
            // 
            resources.ApplyResources(this.comboDatePicker, "comboDatePicker");
            this.comboDatePicker.Name = "comboDatePicker";
            this.comboDatePicker.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.comboDatePicker.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.comboDatePicker.Properties.Appearance.Options.UseBackColor = true;
            this.comboDatePicker.Properties.Appearance.Options.UseFont = true;
            this.comboDatePicker.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9F);
            this.comboDatePicker.Properties.AppearanceDisabled.Options.UseFont = true;
            this.comboDatePicker.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9F);
            this.comboDatePicker.Properties.AppearanceDropDown.Options.UseFont = true;
            this.comboDatePicker.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.comboDatePicker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboDatePicker.Properties.Items.AddRange(new object[] {
            resources.GetString("comboDatePicker.Properties.Items"),
            resources.GetString("comboDatePicker.Properties.Items1"),
            resources.GetString("comboDatePicker.Properties.Items2"),
            resources.GetString("comboDatePicker.Properties.Items3")});
            this.comboDatePicker.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboDatePicker.StyleController = this.layoutControl;
            this.comboDatePicker.SelectedIndexChanged += new System.EventHandler(this.comboDatePicker_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciDate,
            this.lciCustomDate});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(493, 36);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciDate
            // 
            this.lciDate.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.lciDate.AppearanceItemCaption.Options.UseFont = true;
            this.lciDate.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciDate.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lciDate.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lciDate.Control = this.comboDatePicker;
            resources.ApplyResources(this.lciDate, "lciDate");
            this.lciDate.Location = new System.Drawing.Point(0, 0);
            this.lciDate.Name = "lciDate";
            this.lciDate.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciDate.Size = new System.Drawing.Size(186, 34);
            this.lciDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciDate.TextSize = new System.Drawing.Size(29, 20);
            // 
            // lciCustomDate
            // 
            this.lciCustomDate.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.lciCustomDate.AppearanceItemCaption.Options.UseFont = true;
            this.lciCustomDate.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciCustomDate.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lciCustomDate.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lciCustomDate.Control = this.customDateEdit;
            resources.ApplyResources(this.lciCustomDate, "lciCustomDate");
            this.lciCustomDate.Location = new System.Drawing.Point(186, 0);
            this.lciCustomDate.Name = "lciCustomDate";
            this.lciCustomDate.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciCustomDate.Size = new System.Drawing.Size(305, 34);
            this.lciCustomDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciCustomDate.TextSize = new System.Drawing.Size(76, 20);
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
            this.barButtonItemOK.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemOK.Glyph")));
            this.barButtonItemOK.Id = 0;
            this.barButtonItemOK.Name = "barButtonItemOK";
            this.barButtonItemOK.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemOK.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemOK_ItemClick);
            // 
            // barButtonItemCancel
            // 
            resources.ApplyResources(this.barButtonItemCancel, "barButtonItemCancel");
            this.barButtonItemCancel.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemCancel.Glyph")));
            this.barButtonItemCancel.Id = 1;
            this.barButtonItemCancel.Name = "barButtonItemCancel";
            this.barButtonItemCancel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCancel_ItemClick);
            // 
            // DateValueEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.KeyPreview = true;
            this.Name = "DateValueEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DateValueEditor_FormClosing);
            this.Load += new System.EventHandler(this.DateValueEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboDatePicker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCustomDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.Controls.LayoutControls.ImpacLayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOK;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancel;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.ComboBoxEdit comboDatePicker;
        private DevExpress.XtraLayout.LayoutControlItem lciDate;
        private DevExpress.XtraEditors.DateEdit customDateEdit;
        private DevExpress.XtraLayout.LayoutControlItem lciCustomDate;
    }
}