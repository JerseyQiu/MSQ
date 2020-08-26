using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Details;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Formatter;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
    /// <summary>
    /// IQ Variable activity for collecting a selection of flowsheet items
    /// </summary>
    [IQFlowsheetVarActivity_DisplayName]
    [GeneralCharting_Category]
    [Variables_ActivityGroup]
    public sealed class IQFlowsheetVarActivity : IQVariableActivitySimple<FlowsheetSelection, IQFlowsheetVar, IQFlowsheetVarDetail, IQFlowsheetVarConfig>
    {
    }

    /// <summary>
    /// The configuration class for IQVarFlowsheetSelectionActivity
    /// </summary>
    [Serializable]
    public class IQFlowsheetVarConfig : IQVariableConfigSimple<FlowsheetSelection, IQFlowsheetVar, IQFlowsheetVarDetail>
    {
    	private bool _tabViewChanged = false;
        /// <summary>
        /// Stores the OBD_GUID of the ObsDef record for the tab name
        /// </summary>
        [RestoreInclude]
        [ConfigurationActivity_Category]
        [FlowsheetTab_DisplayName]
        public Guid TabGuid { get; set; }

		/// <summary>
		/// A flag to indicate whether the 'Other Labs' function item should be included
		/// </summary>
		[RestoreInclude]
		[ConfigurationActivity_Category]
		[IncludeOtherLabs_DisplayName]
		public bool IncludeOtherLabs { get; set; }

		/// <summary>
		/// Stores the Label of the ObsDef record for the tab name
		/// </summary>
		internal string TabName
		{
			get { return GetObdLabel(TabGuid); }
		}

		internal static string GetObdLabel(Guid obdGuid)
		{
			try
			{
				var pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
				var obd = ObsDef.GetEntityByObdGuid(obdGuid, pm);
				return obd.IsNullEntity ? String.Empty : obd.LabelInactiveIndicator;
			}
			catch (Exception)
			{
				return String.Empty;
			}
		}

    	#region Overrides
		/// <summary>
        /// Opens the flowsheet selection editor.
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        protected override IQOpResult<FlowsheetSelection> OpenEditor(FlowsheetSelection currentValue, IQVarElementTarget target)
		{
		    if (TabGuid == Guid.Empty)
		        return new IQOpResult<FlowsheetSelection> {Result = OpResultEnum.Cancelled};

			if (currentValue == null)
				currentValue = new FlowsheetSelection {TabGuid = TabGuid};

			var editor = new FlowsheetItemSelector { Value = currentValue, IncudeOtherLabs = IncludeOtherLabs };
		    return (editor.ShowDialog() == DialogResult.OK)
		               ? new IQOpResult<FlowsheetSelection> {Result = OpResultEnum.Completed, Value = editor.Value}
		               : new IQOpResult<FlowsheetSelection> {Result = OpResultEnum.Cancelled};
		}

        /// <summary> Custom values not supported for IQEnumVar activities. </summary>
		public override bool SupportsCustomValues { get { return false; } }

		/// <summary>
		/// </summary>
		public override RepositoryItem GetPropertyEditor(string propertyName)
		{
			if (propertyName == "TabGuid")
			{
				var formatter = new FlowsheetTabFormatter { Config = this };
				var flowsheetTabSelector = new RepositoryItemButtonEdit();
				flowsheetTabSelector.ButtonClick += flowsheetTabSelector_ButtonClick;
				flowsheetTabSelector.DoubleClick += flowsheetTabSelector_DoubleClick;
				flowsheetTabSelector.DisplayFormat.Format = formatter;
				flowsheetTabSelector.DisplayFormat.FormatType = FormatType.Custom;
				flowsheetTabSelector.EditFormat.Format = formatter;
				flowsheetTabSelector.EditFormat.FormatType = FormatType.Custom;
				flowsheetTabSelector.TextEditStyle = TextEditStyles.DisableTextEditor;
				return flowsheetTabSelector;
			}

			return base.GetPropertyEditor(propertyName);
		}

        /// <summary>
        /// Overriden to extend cleanup when the selected flowsheet object does not belong to the selected tab view.
        /// </summary>
        public override void Cleanup()
        {
            base.Cleanup();

            if (_tabViewChanged)
            {
            	_tabViewChanged = false;
                Value.State = AllowNone ? IQVarState.None : IQVarState.NotSet;
                Value.SelectedElement = null;
                Value.SelectedElements.Clear();
            }
        }

        public override void Validate(IList<ValidationError> errors, ValidationMode pValidationMode)
        {
            base.Validate(errors, pValidationMode);

            if (TabGuid == Guid.Empty)
                errors.Add(new ValidationError(Strings.IQFlowsheetVarActivity_TabNameNotSet));
        }
		#endregion

        #region Event Handlers
        private void flowsheetTabSelector_DoubleClick(object sender, EventArgs e)
        {
            InvokeflowsheetTabSelectorForm();
        }

        private void flowsheetTabSelector_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            InvokeflowsheetTabSelectorForm();
        }
        #endregion

        #region Private Methods
		private void InvokeflowsheetTabSelectorForm()
		{
			var dlg = new FlowsheetTabSelector { TabGuid = TabGuid };
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				_tabViewChanged = TabGuid != dlg.TabGuid;
				TabGuid = dlg.TabGuid;
				OnRefreshRequested();
			}
		}
		#endregion
    }
}
