using System;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Impac.Mosaiq.IQ.Activities.Variables.Common.Details;
using Impac.Mosaiq.IQ.Activities.Variables.Common.Editor;
using Impac.Mosaiq.IQ.Activities.Variables.Common.Formatter;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common
{
	#region Activity Class
	/// <summary>
	/// 
	/// </summary>
	[Common_Category]
    [Variables_ActivityGroup]
	[IQDateRangeVarActivity_DisplayName]
	public class IQDateRangeVarActivity : IQVariableActivity<DateRange, DateRange, IQDateRangeVar, IQDateRangeVarDetail, IQDateRangeElement, IQDateRangeVarConfig>
	{
	}
	#endregion

	#region Configuration Class
	/// <summary>
	/// 
	/// </summary>
    [Serializable]
	public class IQDateRangeVarConfig : IQVariableConfig<DateRange, DateRange, IQDateRangeVar, IQDateRangeVarDetail, IQDateRangeElement>
    {
		private bool _availableRangesChanged = false;
		/// <summary>
		/// The currently configured standard date ranges
		/// </summary>
		[RestoreInclude]
		[ConfigurationActivity_Category]
		[AvailableDateRanges_DisplayName]
		public List<int> DateRanges
		{
			get { return _dateRanges ?? IQDateRangeElement.GetAvailableRanges(); }
			set { _dateRanges = value; }
		}
		private List<int> _dateRanges;

        #region Overrides
        /// <summary> Opens the date range editor. </summary>
		protected override IQOpResult<IQDateRangeElement> OpenEditor(IQDateRangeElement defaultValue, IQVarElementTarget target)
        {
            var dateRanges = DateRanges;
            if (dateRanges.Count == 0)
				return new IQOpResult<IQDateRangeElement> { Result = OpResultEnum.Cancelled };

            var editor = new DateRangeValueEditor
                             {
                                 DateRanges = dateRanges,
                                 Value = defaultValue
                             };
            return (editor.ShowDialog() == DialogResult.OK)
					   ? new IQOpResult<IQDateRangeElement> { Result = OpResultEnum.Completed, Value = editor.Value }
					   : new IQOpResult<IQDateRangeElement> { Result = OpResultEnum.Cancelled };
        }

	    /// <summary>
		/// Overriden to extend cleanup when the list of available ranges changes
		/// </summary>
		public override void Cleanup()
		{
			base.Cleanup();

			if (_availableRangesChanged)
			{
				_availableRangesChanged = false;
				Value.State = AllowNone ? IQVarState.None : IQVarState.NotSet;
				Value.SelectedElement = null;
				Value.SelectedElements.Clear();
			}
		}


	    /// <summary>
	    /// Allows the developer to define a property editor for a property in a class derived from IQVariableConfig_T class.
	    /// </summary>
	    /// <param name="propertyName"></param>
	    /// <returns></returns>
	    public override RepositoryItem GetPropertyEditor (string propertyName)
		{
			if (propertyName == "DateRanges")
			{
				var formatter = new DateRangesFormatter {Config = this};
				var dateRangesSelector = new RepositoryItemButtonEdit();
				dateRangesSelector.ButtonClick += dateRangesSelector_ButtonClick;
				dateRangesSelector.DoubleClick += dateRangesSelector_DoubleClick;
				dateRangesSelector.DisplayFormat.Format = formatter;
				dateRangesSelector.DisplayFormat.FormatType = FormatType.Custom;
				dateRangesSelector.EditFormat.Format = formatter;
				dateRangesSelector.EditFormat.FormatType = FormatType.Custom;
				dateRangesSelector.TextEditStyle = TextEditStyles.DisableTextEditor;
				return dateRangesSelector;
			}

			return base.GetPropertyEditor(propertyName);
		}
		#endregion

        #region Event Handlers
        private void dateRangesSelector_DoubleClick(object sender, EventArgs e)
        {
            InvokeDateRangesSelectorForm();
        }

        private void dateRangesSelector_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            InvokeDateRangesSelectorForm();
        }
        #endregion

        #region Private Methods
		private void InvokeDateRangesSelectorForm()
		{
			var dlg = new DateRangesSelectForm { Value = DateRanges };
			Backup();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				_availableRangesChanged = true;
				DateRanges = dlg.Value;
				Commit();
				OnRefreshRequested();
			}
			else
			{
				Rollback();
			}
		}
		#endregion
	}
	#endregion
}
