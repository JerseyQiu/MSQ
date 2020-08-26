using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Impac.Mosaiq.BOM.Entities;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors
{
	/// <summary> This form allows the user select and reorder the available date ranges for IQVarDateRangeActivity </summary>
    public partial class FlowsheetItemTypeSelector : XtraForm
    {
        #region Constructor
        /// <summary> Default ctor </summary>
        public FlowsheetItemTypeSelector()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Fields
	    private List<FlowsheetItemType> _availableTypes;
        #endregion

        #region Public Properties
        /// <summary> </summary>
        public List<ObsDefDataFormat> SelectedValues { get; set; }
        #endregion

        #region Event Handlers

        private void FlowsheetItemTypeSelector_Load(object sender, EventArgs e)
        {
            _availableTypes = GetStandardFlowsheetTypes().OrderBy(f => f.DisplayName).ToList();

            listItemTypes.DataSource = _availableTypes;

            foreach(FlowsheetItemType t in _availableTypes)
            {
                listItemTypes.SetItemChecked(
                    _availableTypes.IndexOf(t),
                    SelectedValues.Contains(t.ItemType));
            }
        }

        private void barButtonOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void barButtonCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FlowsheetItemTypeSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            ValidateChildren();

            if (DialogResult != DialogResult.OK)
                return;
        }

        private void btnSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (FlowsheetItemType t in _availableTypes)
                listItemTypes.SetItemChecked(_availableTypes.IndexOf(t), true);
        }

        private void btnClearAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (FlowsheetItemType t in _availableTypes)
                listItemTypes.SetItemChecked(_availableTypes.IndexOf(t), false);
        }

        private void listItemTypes_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            var value = (ObsDefDataFormat)listItemTypes.GetItemValue(e.Index);

            if (e.State == CheckState.Checked && !SelectedValues.Contains(value))
                SelectedValues.Add(value);

            if (e.State == CheckState.Unchecked)
                SelectedValues.Remove(value);
        }
        #endregion

        #region Overriden Methods
        /// <summary> Add special handling for enter and escape key presses </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    DialogResult = DialogResult.OK;
                    e.Handled = true;
                    Close();
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

        #region Static Methods
        /// <summary>
        /// </summary>
        private static IEnumerable<FlowsheetItemType> GetStandardFlowsheetTypes()
        {
            return new List<FlowsheetItemType>
		            {
                        new FlowsheetItemType {DisplayName = Strings.ObsDefType_Numeric, ItemType = ObsDefDataFormat.Numeric},
                        new FlowsheetItemType {DisplayName = Strings.ObsDefType_String, ItemType = ObsDefDataFormat.String},
                        new FlowsheetItemType {DisplayName = Strings.ObsDefType_CheckBox, ItemType = ObsDefDataFormat.CheckBox},
                        new FlowsheetItemType {DisplayName = Strings.ObsDefType_Date, ItemType = ObsDefDataFormat.Date},
                        new FlowsheetItemType {DisplayName = Strings.ObsDefType_Time, ItemType = ObsDefDataFormat.Time}
        			};
        }
        #endregion
    }

    #region Helper Classes
    /// <summary>Represents the type of flowsheet item. </summary>
    public class FlowsheetItemType
    {
        /// <summary> </summary>
        public string DisplayName { get; set; }

        /// <summary> </summary>
        public ObsDefDataFormat ItemType { get; set; }
    }
    #endregion
}
