namespace Impac.Mosaiq.IQ.Activities.Tools.MsgBox
{
    partial class MultiOptionDropDownForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiOptionDropDownForm));
            this.okButton = new Impac.Mosaiq.UI.Controls.BrowseButtons.OKButton();
            this.layoutMain = new Impac.Mosaiq.UI.Controls.LayoutControls.ImpacLayoutControl();
            this.labelControlMessage = new DevExpress.XtraEditors.LabelControl();
            this.lookupEditItems = new DevExpress.XtraEditors.LookUpEdit();
            this.cancelButton = new Impac.Mosaiq.UI.Controls.BrowseButtons.CancelButton();
            this.lcgMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciChoicesCombo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciMessageLabel = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciOkButton = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCancelButton = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).BeginInit();
            this.layoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookupEditItems.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciChoicesCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMessageLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOkButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancelButton)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.okButton.Appearance.Options.UseFont = true;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.StyleController = this.layoutMain;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // layoutMain
            // 
            this.layoutMain.AllowCustomizationMenu = false;
            this.layoutMain.Controls.Add(this.labelControlMessage);
            this.layoutMain.Controls.Add(this.lookupEditItems);
            this.layoutMain.Controls.Add(this.cancelButton);
            this.layoutMain.Controls.Add(this.okButton);
            resources.ApplyResources(this.layoutMain, "layoutMain");
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.Root = this.lcgMain;
            // 
            // labelControlMessage
            // 
            this.labelControlMessage.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.labelControlMessage.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.labelControlMessage, "labelControlMessage");
            this.labelControlMessage.Name = "labelControlMessage";
            this.labelControlMessage.StyleController = this.layoutMain;
            // 
            // lookupEditItems
            // 
            resources.ApplyResources(this.lookupEditItems, "lookupEditItems");
            this.lookupEditItems.Name = "lookupEditItems";
            this.lookupEditItems.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.lookupEditItems.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lookupEditItems.Properties.Appearance.Options.UseFont = true;
            this.lookupEditItems.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 10F);
            this.lookupEditItems.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lookupEditItems.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 10F);
            this.lookupEditItems.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lookupEditItems.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 10F);
            this.lookupEditItems.Properties.AppearanceFocused.Options.UseFont = true;
            this.lookupEditItems.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupEditItems.Properties.HideSelection = false;
            this.lookupEditItems.Properties.NullText = global::Impac.Mosaiq.IQ.Activities.Tools.Strings.PersistenceManager_Description;
            this.lookupEditItems.Properties.PopupWidth = 400;
            this.lookupEditItems.Properties.ShowHeader = false;
            this.lookupEditItems.Properties.ShowLines = false;
            this.lookupEditItems.StyleController = this.layoutMain;
            this.lookupEditItems.EditValueChanged += new System.EventHandler(this.lookupEditItems_EditValueChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.cancelButton.Appearance.Options.UseFont = true;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.StyleController = this.layoutMain;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lcgMain
            // 
            resources.ApplyResources(this.lcgMain, "lcgMain");
            this.lcgMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciChoicesCombo,
            this.lciMessageLabel,
            this.lciOkButton,
            this.lciCancelButton});
            this.lcgMain.Location = new System.Drawing.Point(0, 0);
            this.lcgMain.Name = "lcgMain";
            this.lcgMain.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgMain.Size = new System.Drawing.Size(186, 72);
            this.lcgMain.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgMain.TextVisible = false;
            // 
            // lciChoicesCombo
            // 
            this.lciChoicesCombo.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.lciChoicesCombo.AppearanceItemCaption.Options.UseFont = true;
            this.lciChoicesCombo.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciChoicesCombo.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lciChoicesCombo.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lciChoicesCombo.Control = this.lookupEditItems;
            resources.ApplyResources(this.lciChoicesCombo, "lciChoicesCombo");
            this.lciChoicesCombo.Location = new System.Drawing.Point(0, 26);
            this.lciChoicesCombo.Name = "lciChoicesCombo";
            this.lciChoicesCombo.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciChoicesCombo.Size = new System.Drawing.Size(85, 44);
            this.lciChoicesCombo.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciChoicesCombo.TextSize = new System.Drawing.Size(0, 0);
            this.lciChoicesCombo.TextToControlDistance = 0;
            this.lciChoicesCombo.TextVisible = false;
            // 
            // lciMessageLabel
            // 
            this.lciMessageLabel.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.lciMessageLabel.AppearanceItemCaption.Options.UseFont = true;
            this.lciMessageLabel.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciMessageLabel.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lciMessageLabel.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lciMessageLabel.Control = this.labelControlMessage;
            resources.ApplyResources(this.lciMessageLabel, "lciMessageLabel");
            this.lciMessageLabel.Location = new System.Drawing.Point(0, 0);
            this.lciMessageLabel.Name = "lciMessageLabel";
            this.lciMessageLabel.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciMessageLabel.Size = new System.Drawing.Size(85, 26);
            this.lciMessageLabel.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciMessageLabel.TextSize = new System.Drawing.Size(0, 0);
            this.lciMessageLabel.TextToControlDistance = 0;
            this.lciMessageLabel.TextVisible = false;
            // 
            // lciOkButton
            // 
            this.lciOkButton.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.lciOkButton.AppearanceItemCaption.Options.UseFont = true;
            this.lciOkButton.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciOkButton.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lciOkButton.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lciOkButton.Control = this.okButton;
            resources.ApplyResources(this.lciOkButton, "lciOkButton");
            this.lciOkButton.Location = new System.Drawing.Point(85, 0);
            this.lciOkButton.Name = "lciOkButton";
            this.lciOkButton.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciOkButton.Size = new System.Drawing.Size(99, 35);
            this.lciOkButton.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciOkButton.TextSize = new System.Drawing.Size(0, 0);
            this.lciOkButton.TextToControlDistance = 0;
            this.lciOkButton.TextVisible = false;
            // 
            // lciCancelButton
            // 
            this.lciCancelButton.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.lciCancelButton.AppearanceItemCaption.Options.UseFont = true;
            this.lciCancelButton.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciCancelButton.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lciCancelButton.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lciCancelButton.Control = this.cancelButton;
            resources.ApplyResources(this.lciCancelButton, "lciCancelButton");
            this.lciCancelButton.Location = new System.Drawing.Point(85, 35);
            this.lciCancelButton.Name = "lciCancelButton";
            this.lciCancelButton.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciCancelButton.Size = new System.Drawing.Size(99, 35);
            this.lciCancelButton.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciCancelButton.TextSize = new System.Drawing.Size(0, 0);
            this.lciCancelButton.TextToControlDistance = 0;
            this.lciCancelButton.TextVisible = false;
            // 
            // MultiOptionDropDownForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ControlBox = false;
            this.Controls.Add(this.layoutMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MultiOptionDropDownForm";
            this.Load += new System.EventHandler(this.ListPromptForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).EndInit();
            this.layoutMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookupEditItems.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciChoicesCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMessageLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOkButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancelButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Impac.Mosaiq.UI.Controls.BrowseButtons.OKButton okButton;
        private DevExpress.XtraEditors.LookUpEdit lookupEditItems;
        private Impac.Mosaiq.UI.Controls.LayoutControls.ImpacLayoutControl layoutMain;
        private DevExpress.XtraEditors.LabelControl labelControlMessage;
        private UI.Controls.BrowseButtons.CancelButton cancelButton;
        private DevExpress.XtraLayout.LayoutControlGroup lcgMain;
        private DevExpress.XtraLayout.LayoutControlItem lciChoicesCombo;
        private DevExpress.XtraLayout.LayoutControlItem lciOkButton;
        private DevExpress.XtraLayout.LayoutControlItem lciCancelButton;
        private DevExpress.XtraLayout.LayoutControlItem lciMessageLabel;
    }
}