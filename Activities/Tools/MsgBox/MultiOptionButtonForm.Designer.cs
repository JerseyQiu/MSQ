namespace Impac.Mosaiq.IQ.Activities.Tools.MsgBox
{
    partial class MultiOptionButtonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiOptionButtonForm));
            this.layoutMain = new Impac.Mosaiq.UI.Controls.LayoutControls.ImpacLayoutControl();
            this.labelControlMessage = new DevExpress.XtraEditors.LabelControl();
            this.cancelButton = new Impac.Mosaiq.UI.Controls.BrowseButtons.CancelButton();
            this.lcgMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciMessage = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupButtons = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).BeginInit();
            this.layoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupButtons)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutMain
            // 
            this.layoutMain.AllowCustomizationMenu = false;
            this.layoutMain.Controls.Add(this.labelControlMessage);
            this.layoutMain.Controls.Add(this.cancelButton);
            resources.ApplyResources(this.layoutMain, "layoutMain");
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.Root = this.lcgMain;
            // 
            // labelControlMessage
            // 
            this.labelControlMessage.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControlMessage.Appearance.Font")));
            resources.ApplyResources(this.labelControlMessage, "labelControlMessage");
            this.labelControlMessage.Name = "labelControlMessage";
            this.labelControlMessage.StyleController = this.layoutMain;
            // 
            // cancelButton
            // 
            this.cancelButton.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("cancelButton.Appearance.Font")));
            this.cancelButton.Appearance.Options.UseFont = true;
            this.cancelButton.Appearance.Options.UseTextOptions = true;
            this.cancelButton.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
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
            this.lciMessage,
            this.lciCancel,
            this.layoutControlGroupButtons});
            this.lcgMain.Location = new System.Drawing.Point(0, 0);
            this.lcgMain.Name = "lcgMain";
            this.lcgMain.OptionsItemText.TextToControlDistance = 0;
            this.lcgMain.Size = new System.Drawing.Size(174, 104);
            this.lcgMain.TextVisible = false;
            // 
            // lciMessage
            // 
            this.lciMessage.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lciMessage.AppearanceItemCaption.Font")));
            this.lciMessage.AppearanceItemCaption.Options.UseFont = true;
            this.lciMessage.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciMessage.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lciMessage.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lciMessage.Control = this.labelControlMessage;
            resources.ApplyResources(this.lciMessage, "lciMessage");
            this.lciMessage.Location = new System.Drawing.Point(0, 0);
            this.lciMessage.Name = "lciMessage";
            this.lciMessage.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciMessage.Size = new System.Drawing.Size(172, 26);
            this.lciMessage.TextSize = new System.Drawing.Size(0, 0);
            this.lciMessage.TextToControlDistance = 0;
            this.lciMessage.TextVisible = false;
            // 
            // lciCancel
            // 
            this.lciCancel.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lciCancel.AppearanceItemCaption.Font")));
            this.lciCancel.AppearanceItemCaption.Options.UseFont = true;
            this.lciCancel.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciCancel.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lciCancel.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lciCancel.Control = this.cancelButton;
            resources.ApplyResources(this.lciCancel, "lciCancel");
            this.lciCancel.Location = new System.Drawing.Point(0, 67);
            this.lciCancel.Name = "lciCancel";
            this.lciCancel.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciCancel.Size = new System.Drawing.Size(172, 35);
            this.lciCancel.TextSize = new System.Drawing.Size(0, 0);
            this.lciCancel.TextToControlDistance = 0;
            this.lciCancel.TextVisible = false;
            // 
            // layoutControlGroupButtons
            // 
            this.layoutControlGroupButtons.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroupButtons.AppearanceItemCaption.Font")));
            this.layoutControlGroupButtons.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroupButtons, "layoutControlGroupButtons");
            this.layoutControlGroupButtons.GroupBordersVisible = false;
            this.layoutControlGroupButtons.Location = new System.Drawing.Point(0, 26);
            this.layoutControlGroupButtons.Name = "layoutControlGroupButtons";
            this.layoutControlGroupButtons.OptionsItemText.TextToControlDistance = 0;
            this.layoutControlGroupButtons.Size = new System.Drawing.Size(172, 41);
            // 
            // MultiOptionButtonForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ControlBox = false;
            this.Controls.Add(this.layoutMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LookAndFeel.SkinName = "MosaiqSkin";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "MultiOptionButtonForm";
            this.Load += new System.EventHandler(this.MultiOptionButtonForm_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.MultiOptionButtonForm_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).EndInit();
            this.layoutMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupButtons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Impac.Mosaiq.UI.Controls.LayoutControls.ImpacLayoutControl layoutMain;
        private DevExpress.XtraEditors.LabelControl labelControlMessage;
        private UI.Controls.BrowseButtons.CancelButton cancelButton;
        private DevExpress.XtraLayout.LayoutControlGroup lcgMain;
        private DevExpress.XtraLayout.LayoutControlItem lciCancel;
        private DevExpress.XtraLayout.LayoutControlItem lciMessage;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupButtons;
    }
}