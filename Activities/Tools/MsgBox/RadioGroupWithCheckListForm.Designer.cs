namespace Impac.Mosaiq.IQ.Activities.Tools.MsgBox
{
    partial class RadioGroupWithCheckListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RadioGroupWithCheckListForm));
            this.okButton = new Impac.Mosaiq.UI.Controls.BrowseButtons.OKButton();
            this.cancelButton = new Impac.Mosaiq.UI.Controls.BrowseButtons.CancelButton();
            this.rdoRadioGroup = new DevExpress.XtraEditors.RadioGroup();
            this.rgbRadioGroupBox = new Impac.Mosaiq.UI.Controls.RoundedGroupBoxes.RoundedGroupBox(this.components);
            this.rgbCheckListGroupBox = new Impac.Mosaiq.UI.Controls.RoundedGroupBoxes.RoundedGroupBox(this.components);
            this.chkCheckedListBox = new DevExpress.XtraEditors.CheckedListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.rdoRadioGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbRadioGroupBox)).BeginInit();
            this.rgbRadioGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgbCheckListGroupBox)).BeginInit();
            this.rgbCheckListGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckedListBox)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("okButton.Appearance.Font")));
            this.okButton.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("cancelButton.Appearance.Font")));
            this.cancelButton.Appearance.Options.UseFont = true;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // rdoRadioGroup
            // 
            this.rdoRadioGroup.AutoSizeInLayoutControl = true;
            resources.ApplyResources(this.rdoRadioGroup, "rdoRadioGroup");
            this.rdoRadioGroup.Name = "rdoRadioGroup";
            this.rdoRadioGroup.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("rgbRadioGroup.Properties.Appearance.Font")));
            this.rdoRadioGroup.Properties.Appearance.Options.UseFont = true;
            this.rdoRadioGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdoRadioGroup.Properties.Columns = 1;
            this.rdoRadioGroup.EditValueChanged += new System.EventHandler(this.rgbRadioGroup_EditValueChanged);
            // 
            // rgbRadioGroupBox
            // 
            resources.ApplyResources(this.rgbRadioGroupBox, "rgbRadioGroupBox");
            this.rgbRadioGroupBox.AppearanceCaption.BackColor = ((System.Drawing.Color)(resources.GetObject("rgbRadioGroupBox.AppearanceCaption.BackColor")));
            this.rgbRadioGroupBox.AppearanceCaption.BackColor2 = ((System.Drawing.Color)(resources.GetObject("rgbRadioGroupBox.AppearanceCaption.BackColor2")));
            this.rgbRadioGroupBox.AppearanceCaption.Options.UseBackColor = true;
            this.rgbRadioGroupBox.Controls.Add(this.rdoRadioGroup);
            this.rgbRadioGroupBox.Name = "rgbRadioGroupBox";
            // 
            // rgbCheckListGroupBox
            // 
            resources.ApplyResources(this.rgbCheckListGroupBox, "rgbCheckListGroupBox");
            this.rgbCheckListGroupBox.AppearanceCaption.BackColor = ((System.Drawing.Color)(resources.GetObject("rgbCheckListGroupBox.AppearanceCaption.BackColor")));
            this.rgbCheckListGroupBox.AppearanceCaption.BackColor2 = ((System.Drawing.Color)(resources.GetObject("rgbCheckListGroupBox.AppearanceCaption.BackColor2")));
            this.rgbCheckListGroupBox.AppearanceCaption.Options.UseBackColor = true;
            this.rgbCheckListGroupBox.Controls.Add(this.chkCheckedListBox);
            this.rgbCheckListGroupBox.Name = "rgbCheckListGroupBox";
            // 
            // chkCheckedListBox
            // 
            this.chkCheckedListBox.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("rgbCheckedListBox.Appearance.BackColor")));
            this.chkCheckedListBox.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("rgbCheckedListBox.Appearance.Font")));
            this.chkCheckedListBox.Appearance.Options.UseBackColor = true;
            this.chkCheckedListBox.Appearance.Options.UseFont = true;
            this.chkCheckedListBox.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.chkCheckedListBox.CheckOnClick = true;
            resources.ApplyResources(this.chkCheckedListBox, "chkCheckedListBox");
            this.chkCheckedListBox.Name = "chkCheckedListBox";
            // 
            // RadioGroupWithCheckListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.rgbCheckListGroupBox);
            this.Controls.Add(this.rgbRadioGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RadioGroupWithCheckListForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RadioGroupWithCheckListForm_FormClosing);
            this.Load += new System.EventHandler(this.RadioGroupWithCheckListFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.rdoRadioGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbRadioGroupBox)).EndInit();
            this.rgbRadioGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgbCheckListGroupBox)).EndInit();
            this.rgbCheckListGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckedListBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Controls.BrowseButtons.OKButton okButton;
        private UI.Controls.BrowseButtons.CancelButton cancelButton;
        private DevExpress.XtraEditors.RadioGroup rdoRadioGroup;
        private UI.Controls.RoundedGroupBoxes.RoundedGroupBox rgbRadioGroupBox;
        private UI.Controls.RoundedGroupBoxes.RoundedGroupBox rgbCheckListGroupBox;
        private DevExpress.XtraEditors.CheckedListBoxControl chkCheckedListBox;


    }
}