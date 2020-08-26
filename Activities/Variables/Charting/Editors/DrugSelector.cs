using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Charting.CommonUtils;
using Impac.Mosaiq.Charting.ExternalFormulary;
using Impac.Mosaiq.Core.Xlate;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors
{
	/// <summary>
	/// An editor that allows the user to select a drug
	/// </summary>
    public partial class DrugSelector : XtraForm
	{
        private readonly ExternalFormularyHelper _formularyHelper = new ExternalFormularyHelper();
        private readonly ImpacPersistenceManager _pm;

        /// <summary> Default ctor </summary>
		public DrugSelector()
        {
        	InitializeComponent();
            InitializeDrugControl();
            _pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
        }

		private void DrugSelector_Load(object sender, EventArgs e)
		{
			try
			{
                if (Value > 0)
                    LoadDrugControlsData(Value);				
			}
			catch (Exception ex)
			{
                System.Diagnostics.Trace.WriteLine("DrugSelector_Load: " + ex);
			}
		}

        // Establish default settings for drug search, route, dose form and strength
        private void InitializeDrugControl()
        {
            drugSearchControl.SearchAgainstFDBAndManualEntries(false);
            
            if (_formularyHelper.UseFDBUK)
            {
                drugSearchControl.IncludeStrength = true;
                drugSearchControl.AllowStrengthSelection = false;
                drugSearchControl.ShowFullDescription = true;
                drugSearchControl.UseBrand = true;
            }
            else if (_formularyHelper.UseFDBAustralia)
            {
                drugSearchControl.IncludeStrength = true;
                drugSearchControl.UseBrand = true;
            }
            else
            {
                drugSearchControl.IncludeStrength = false;
                drugSearchControl.UseBrand = true;
            }
        }

        private void LoadDrugControlsData(int drugId)
        {
            Drug selectedDrug = Drug.GetEntityByID(drugId, _pm);
            if (EntityUtility.IsEntityAvailable(selectedDrug))
            {
                if (!selectedDrug.ExternalDispensableDrugInd && selectedDrug.ExternalRoutedDrugInd)
                {
                    // Have link to a routed drug in FDB
                    drugSearchControl.IncludeStrength = false;
                }
                else
                {
                    // Defaut or have link to a dispensable drug in FDB
                    drugSearchControl.IncludeStrength = true;
                }
                drugSearchControl.Drug_ID = drugId;
            }
        }

        /// <summary>
        /// Return DRG_ID if selected drug is already in the Drug table
        /// </summary>
        private int GetDrugIdForSelectedDrug()
        {
            int drugId = 0;

            if (drugSearchControl.SelectedIndex >= 0)
            {
                // Search internal use only drug id. We don't want to use drug records exposed in the formulary
                drugId = drugSearchControl.Drug_ID_InternalOnly;                
            }

            return drugId;
        }

        /// <summary>
        /// Import the currently selected drug to the Drug table
        /// </summary>
        private int ImportSelectedDrug()
	    {
            try
            {
				int drugId = Mosaiq.Charting.Controls.DrugSearchControl.ImportDrugFromFDB(drugSearchControl, _formularyHelper);
	            return drugId;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("DrugSelector:ImportSelectedDrug: " + ex);
                return -1;
            }
	    }

	    /// <summary>
		/// </summary>
		public int Value { get; set; }

        private void barButtonItemOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        	CloseDialog();
        }

        private void barButtonItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

		/// <summary> Add special handling for enter and escape key presses </summary>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					e.Handled = true;
					CloseDialog();
					break;
				case Keys.Escape:
					e.Handled = true;
					DialogResult = DialogResult.Cancel;
					Close();
					break;
			}

			base.OnKeyDown(e);
		}

		private void CloseDialog()
		{
            if (drugSearchControl.SelectedIndex < 0)
            {
                XlateMessageBox.Error(Strings.PleaseEnterADrug);
                return;
            }

            // Get DRG_ID of the selected drug
            int drugId = GetDrugIdForSelectedDrug();

            // Import the drug to Drug table if doesn't exist
            if (drugId <= 0)
                drugId = ImportSelectedDrug();

            if (drugId <= 0)
            {
                Value = 0;
                DialogResult = DialogResult.Cancel;
                Close();
            }
            else
            {
                Value = drugId;
                ValidateChildren();
                DialogResult = DialogResult.OK;
                Close();
            }
		}
	}
}
