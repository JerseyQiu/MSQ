using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Core.Globals;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.Core.Defs.Enumerations;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.UI.InputTemplates.DateTimeTemplates.InputDateRange;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors
{
	/// <summary>
	/// An editor that allows the user to select an ObsDef data item and  then 
	/// specify thresholds for that item depending on its data type
	/// </summary>
    public partial class DataItemThresholdsSelector : XtraForm
	{
		private readonly int[] _stringAlgoriths = {
		                                          	(int) DataItemThresholds.StringAlgorithms.StrEquals,
		                                          	(int) DataItemThresholds.StringAlgorithms.StrEqualsIgnoreCase,
		                                          	(int) DataItemThresholds.StringAlgorithms.StrContains,
		                                          	(int) DataItemThresholds.StringAlgorithms.StrContainsIgnoreCase,
		                                          	(int) DataItemThresholds.StringAlgorithms.StrRegex, 
													(int) DataItemThresholds.StringAlgorithms.UpperLowerLimits
		                                          };

		/// <summary> </summary>
		public static string StringAlgorithmIdToString(int id)
		{
			string alg;

			switch (id)
			{
				case (int) DataItemThresholds.StringAlgorithms.StrEquals:
					alg = Strings.StringAlgorithmEquals;
					break;
				case (int) DataItemThresholds.StringAlgorithms.StrEqualsIgnoreCase:
					alg = Strings.StringAlgorithmEqualsIgnoreCase;
					break;
				case (int) DataItemThresholds.StringAlgorithms.StrContains:
					alg = Strings.StringAlgorithmContains;
					break;
				case (int) DataItemThresholds.StringAlgorithms.StrContainsIgnoreCase:
					alg = Strings.StringAlgorithmContainsIgnoreCase;
					break;
				case (int) DataItemThresholds.StringAlgorithms.StrRegex:
					alg = Strings.StringAlgorithmRegEx;
					break;
				case (int) DataItemThresholds.StringAlgorithms.UpperLowerLimits:
					alg = Strings.StringAlgorithmUpperLowerLimits;
					break;
				default:
					alg = String.Empty;
					break;
			}

			return alg;
		}

		/// <summary> </summary>
		public static bool UseStringNumericLimits(int id)
		{
			return id == (int) DataItemThresholds.StringAlgorithms.UpperLowerLimits;
		}


		#region Constructor and initialization
        /// <summary> Default ctor </summary>
		public DataItemThresholdsSelector()
        {
        	InitializeComponent();
        	foreach (int alg in _stringAlgoriths)
        		comboStringAlgorithm.Properties.Items.Add(StringAlgorithmIdToString(alg));
        }

		private void DataItemThresholdsSelector_Load(object sender, EventArgs e)
		{
			try
			{
				HideChildControls();
				Height = 180;

				if (Value == null)
					return;

				ObsDef obd = ObsDef.GetEntityByObdGuid(Value.ObdGuid);
				PopulateChildControls(obd);

				if (obd.Type == (short) MedDefs.ObdType.ItemTable)
				{
					if (Value.TableItems != null)
						foreach (CheckedListBoxItem item in comboTableItems.Properties.Items)
						{
							var obdGuid = (Guid) item.Value;
							if (Value.TableItems.Contains(obdGuid))
								item.CheckState = CheckState.Checked;
						}
					comboTableItems.RefreshEditValue();
				}
				else if (obd.Type == (short) MedDefs.ObdType.ItemData)
				{
					switch (obd.ObsDefDataFormat)
					{
						case ObsDefDataFormat.Numeric:
							textLowerLimit.EditValue = Value.LowerLimit;
							textUpperLimit.EditValue = Value.UpperLimit;
							break;
						case ObsDefDataFormat.Date:
							inputDateRange.DateRange = new InputDateRangeValue(Value.DateRange.Type,
							                                                   Value.StartDate, Value.EndDate,
							                                                   Value.DateRange.SpinControlValue);
							break;
						case ObsDefDataFormat.String:
						case ObsDefDataFormat.Memo:
							int idx = 0;
							for (int i = 0; i < _stringAlgoriths.Length; i++)
							{
								if (_stringAlgoriths[i] == Value.StringAlgorithm)
								{
									idx = i;
									break;
								}
							}
							comboStringAlgorithm.SelectedIndex = idx;
							if (UseStringNumericLimits(Value.StringAlgorithm))
							{
								textLowerLimit.EditValue = Value.LowerLimit;
								textUpperLimit.EditValue = Value.UpperLimit;
							}
							else
							{
								textStringInput.Text = Value.StringInput;
							}
							break;
					}
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine("DataItemThresholdsSelector: " + ex);
			}
		}
        #endregion

		/// <summary>
		/// </summary>
		public DataItemThresholds Value { get; set; }

        #region Form close
        private void barButtonItemOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        	CloseDialog();
        }

        private void barButtonItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			ValidateChildren();
            DialogResult = DialogResult.Cancel;
            Close();
        }

		/// <summary> Add special handling for enter and escape key presses </summary>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					CloseDialog();
					e.Handled = true;
					break;
				case Keys.Escape:
					DialogResult = DialogResult.Cancel;
					e.Handled = true;
					Close();
					break;
			}

			base.OnKeyDown(e);
		}

        #endregion

		#region Private methods
		private void CloseDialog()
		{
			ObsDef obd = null;
			if (buttonDataItem.Tag != null)
				obd = buttonDataItem.Tag as ObsDef;

			if (obd != null)
			{
				Value = new DataItemThresholds();
				Value.ObdGuid = obd.OBD_GUID;

				if (obd.Type == (short) MedDefs.ObdType.ItemTable)
				{
					Value.TableItems = new List<Guid>();
					foreach (CheckedListBoxItem item in comboTableItems.Properties.Items)
					{
						if (item.CheckState != CheckState.Checked)
							continue;

						var obdGuid = (Guid) item.Value;
						if (obdGuid != Guid.Empty)
							Value.TableItems.Add(obdGuid);
					}
				}
				else if (obd.Type == (short) MedDefs.ObdType.ItemData)
				{
					double d;
					switch (obd.ObsDefDataFormat)
					{
						case ObsDefDataFormat.Numeric:
							if (Double.TryParse(textLowerLimit.Text, out d))
								Value.LowerLimit = d;
							if (Double.TryParse(textUpperLimit.Text, out d))
								Value.UpperLimit = d;
							break;
						case ObsDefDataFormat.Date:
							Value.DateRange = inputDateRange.DateRange;
							Value.StartDate = inputDateRange.DateRange.StartDate;
							if (Value.StartDate == DateTime.MinValue)
								Value.StartDate = null;
							Value.EndDate = inputDateRange.DateRange.EndDate;
							if (Value.EndDate == DateTime.MaxValue)
								Value.EndDate = null;
							break;
						case ObsDefDataFormat.String:
						case ObsDefDataFormat.Memo:
							int algorithmId = comboStringAlgorithm.SelectedIndex >= 0 ? comboStringAlgorithm.SelectedIndex : 0;
							Value.StringAlgorithm = _stringAlgoriths[algorithmId];
							if (UseStringNumericLimits(Value.StringAlgorithm))
							{
								if (Double.TryParse(textLowerLimit.Text, out d))
									Value.LowerLimit = d;
								if (Double.TryParse(textUpperLimit.Text, out d))
									Value.UpperLimit = d;
							}
							else
							{
								Value.StringInput = textStringInput.Text;
							}
							break;
					}
				}
			}

			ValidateChildren();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void HideChildControls()
		{
			layoutItemStringAlgorithm.HideToCustomization();
			layoutItemStringInput.HideToCustomization();
			layoutItemLowerLimit.HideToCustomization();
			layoutItemUpperLimit.HideToCustomization();
			layoutItemDateRange.HideToCustomization();
			layoutItemTableItems.HideToCustomization();
		}

		private void PopulateChildControls(ObsDef obd)
		{
			buttonDataItem.EditValue = obd.LabelInactiveIndicator;
			buttonDataItem.Tag = obd;

			HideChildControls();

			if (obd.Type == (short) MedDefs.ObdType.ItemTable)
			{
				layoutItemTableItems.RestoreFromCustomization();
				var query = new ImpacRdbQuery(typeof(ObsDef));
				query.AddClause(ObsDefDataRow.TypeEntityColumn, EntityQueryOp.EQ, (int) MedDefs.ObdType.Choice);
				query.AddClause(ObsDefDataRow.TblEntityColumn, EntityQueryOp.EQ, obd.Tbl);
                query.AddClause(ObsDefDataRow.ActiveEntityColumn, EntityQueryOp.EQ, true);
				var tblItems = Global.DefaultStaffImpacPM.GetEntities<ObsDef>(query);

				comboTableItems.Properties.Items.Clear();
				foreach (var tblItem in tblItems)
					comboTableItems.Properties.Items.Add(tblItem.OBD_GUID, tblItem.FullDescription);

				//comboTableItems.Properties.DataSource = tblItems;
				//comboTableItems.Properties.DisplayMember = "FullDescription";
				//comboTableItems.Properties.ValueMember = "OBD_GUID";
				//comboTableItems.Properties.DropDownRows = tblItems.Count + 2;
			}
			else if (obd.Type == (short) MedDefs.ObdType.ItemData)
			{
				switch (obd.ObsDefDataFormat)
				{
					case ObsDefDataFormat.Numeric:
						layoutItemLowerLimit.RestoreFromCustomization();
						layoutItemUpperLimit.RestoreFromCustomization();
						break;
					case ObsDefDataFormat.Date:
						layoutItemDateRange.RestoreFromCustomization();
						inputDateRange.DateRange = new InputDateRangeValue(InputDateRangeControlType.Today,
							DateTime.Today, DateTime.Today, 1);
						break;
					case ObsDefDataFormat.String:
					case ObsDefDataFormat.Memo:
						layoutItemStringAlgorithm.RestoreFromCustomization();
						if (comboStringAlgorithm.SelectedIndex == -1)
							comboStringAlgorithm.SelectedIndex = 0;
						EnableStringControls();
						break;
				}
			}
		}

		private void comboStringAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
		{
			EnableStringControls();
		}

		private void EnableStringControls()
		{
			int algorithmId = comboStringAlgorithm.SelectedIndex >= 0 ? _stringAlgoriths[comboStringAlgorithm.SelectedIndex] : 0;
			if (UseStringNumericLimits(algorithmId))
			{
				layoutItemStringInput.HideToCustomization();
				layoutItemLowerLimit.RestoreFromCustomization();
				layoutItemUpperLimit.RestoreFromCustomization();
			}
			else
			{
				layoutItemStringInput.RestoreFromCustomization();
				layoutItemLowerLimit.HideToCustomization();
				layoutItemUpperLimit.HideToCustomization();
			}
		}

		private void buttonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			IQDataItemThresholdVarConfig.MinimizeIqEditor(this);
			int obdId = CallClarion.GetObsDefDataItem(0);
			IQDataItemThresholdVarConfig.RestoreIqEditor(this);
			if (obdId == 0)
				return;

			ObsDef obd = ObsDef.GetEntityById(obdId);
			PopulateChildControls(obd);
		}
		#endregion
	}
}
