namespace Impac.Mosaiq.IQ.Activities.General.Careplan
{
    partial class IntentPromptSelectorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntentPromptSelectorForm));
            this.selectButton = new Impac.Mosaiq.UI.Controls.BrowseButtons.SelectButton();
            this.bsPropmt = new System.Windows.Forms.BindingSource(this.components);
            this.listBoxControlPromptValues = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsPropmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlPromptValues)).BeginInit();
            this.SuspendLayout();
            // 
            // selectButton
            // 
            resources.ApplyResources(this.selectButton, "selectButton");
            this.selectButton.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.selectButton.Appearance.Options.UseFont = true;
            this.selectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.selectButton.Name = "selectButton";
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // bsPropmt
            // 
            this.bsPropmt.DataSource = typeof(Impac.Mosaiq.BOM.Entities.Prompt);
            // 
            // listBoxControlPromptValues
            // 
            resources.ApplyResources(this.listBoxControlPromptValues, "listBoxControlPromptValues");
            this.listBoxControlPromptValues.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.listBoxControlPromptValues.Appearance.Options.UseFont = true;
            this.listBoxControlPromptValues.DataSource = this.bsPropmt;
            this.listBoxControlPromptValues.DisplayMember = "Text";
            this.listBoxControlPromptValues.Name = "listBoxControlPromptValues";
            // 
            // IntentPromptSelectorForm
            // 
            this.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.Appearance.Options.UseFont = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.listBoxControlPromptValues);
            this.Controls.Add(this.selectButton);
            this.Name = "IntentPromptSelectorForm";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IntentPromptSelectorForm_FormClosing);
            this.Load += new System.EventHandler(this.IntentPromptSelectorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsPropmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlPromptValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Impac.Mosaiq.UI.Controls.BrowseButtons.SelectButton selectButton;
        private System.Windows.Forms.BindingSource bsPropmt;
        private DevExpress.XtraEditors.ListBoxControl listBoxControlPromptValues;
    }
}