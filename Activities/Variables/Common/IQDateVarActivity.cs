using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Impac.Mosaiq.IQ.Activities.Variables.Common.Details;
using Impac.Mosaiq.IQ.Activities.Variables.Common.Editor;
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
    [IQDateTimeVarActivity_DisplayName]
    public class IQDateVarActivity : IQVariableActivity<DateTime, DateTime, IQDateTimeVar, IQDateTimeVarDetail, IQDateTimeElement, IQDateTimeVarConfig>
	{
	}
	#endregion

	#region Configuration Class
	/// <summary>
	/// 
	/// </summary>
    [Serializable]
    public class IQDateTimeVarConfig : IQVariableConfig<DateTime, DateTime, IQDateTimeVar, IQDateTimeVarDetail, IQDateTimeElement>
    {
        //private bool _availableRangesChanged = false;
        ///// <summary>
        ///// 
        ///// </summary>
        //[RestoreInclude]
        //[ConfigurationActivity_Category]
        //[AvailableDateRanges_DisplayName]
        //public List<DateRangeData> DateRanges
        //{
        //    get { return _dateRanges ?? AvailableDateRanges.GetRanges(); }
        //    set { _dateRanges = value; }
        //}
        //private List<DateRangeData> _dateRanges;
    
        #region Overrides
        /// <summary> Opens the date range editor. </summary>
        protected override IQOpResult<IQDateTimeElement> OpenEditor(IQDateTimeElement defaultValue, IQVarElementTarget target)
        {
            //var dateRanges = DateRanges.Where(item => item.Selected).ToList();
            //if (dateRanges.Count == 0)
            //    return new IQOpResult<IQDateTimeElement> {Result = OpResultEnum.Cancelled};

            var editor = new DateValueEditor
                             {
                                 Value = defaultValue.Value,
                                 Type = defaultValue.Type
                             };


            return (editor.ShowDialog() == DialogResult.OK)
                       ? new IQOpResult<IQDateTimeElement>
                             {
                                 Result = OpResultEnum.Completed,
                                 Value =
                                     new IQDateTimeElement
                                         {
                                             ElementType = IQVarElementType.Standard,
                                             Type = editor.Type,
                                             Value = editor.Value
                                         }
                             }
                       : new IQOpResult<IQDateTimeElement> {Result = OpResultEnum.Cancelled};
        }

        ///// <summary>
        ///// Overriden to extend cleanup when the list of available ranges changes
        ///// </summary>
        //public override void Cleanup()
        //{
        //    base.Cleanup();

        //    if (_availableRangesChanged)
        //    {
        //        _availableRangesChanged = false;
        //        Value.State = AllowNone ? IQVarState.None : IQVarState.NotSet;
        //        Value.SelectedElement = null;
        //        Value.SelectedElements.Clear();
        //    }
        //}

        ///// <summary>
        ///// </summary>
        //public override RepositoryItem GetPropertyEditor (string propertyName)
        //{
        //    //if (propertyName == "DateRanges")
        //    //{
        //    //    var formatter = new DateRangesFormatter {Config = this};
        //    //    var dateRangesSelector = new RepositoryItemButtonEdit();
        //    //    dateRangesSelector.ButtonClick += dateRangesSelector_ButtonClick;
        //    //    dateRangesSelector.DoubleClick += dateRangesSelector_DoubleClick;
        //    //    dateRangesSelector.DisplayFormat.Format = formatter;
        //    //    dateRangesSelector.DisplayFormat.FormatType = FormatType.Custom;
        //    //    dateRangesSelector.EditFormat.Format = formatter;
        //    //    dateRangesSelector.EditFormat.FormatType = FormatType.Custom;
        //    //    dateRangesSelector.TextEditStyle = TextEditStyles.DisableTextEditor;
        //    //    return dateRangesSelector;
        //    //}

        //    return base.GetPropertyEditor(propertyName);
        //}

        ///// <summary>
        ///// </summary>
        //public override void SetRuntimeValue()
        //{
        //    //TODO:  Will need a little work to support multi selection.
        //    base.SetRuntimeValue();

        //    //if (Value.State != IQVarState.Selected)
        //    //    return;

        //    ////Gets all of the elements which have to have their date range calculated.  This call gets all standard value elements,
        //    ////regardless of whether single or multi-selection is used.  In single selection, the value of SelectedElement is also present
        //    ////in the "SelectedElements" list.
        //    //var elementsToUpdate = new List<IQVarElement<DateRange>>(Value.SelectedElements.Where(e=> e.IsStandardValue));
            
        //    ////Loop through and update the elements
        //    //foreach (IQVarElement<DateRange> element in elementsToUpdate)
        //    //{
        //    //    DateRange dateRange = element.Value;
        //    //    // In this case the start and end dates are already set
        //    //    if (dateRange.RangeType == (int) InputDateRangeControlType.SpecificDates)
        //    //        return;

        //    //    // In this case there is no date filter at all
        //    //    if (dateRange.RangeType == (int) InputDateRangeControlType.All)
        //    //        return;

        //    //    element.Value.StartDate = InputDateRangeValue.CalcStartDate(
        //    //        (InputDateRangeControlType) dateRange.RangeType, dateRange.SpinControlValue, dateRange.StartDate);
        //    //    element.Value.EndDate = InputDateRangeValue.CalcEndDate(
        //    //        (InputDateRangeControlType) dateRange.RangeType, dateRange.SpinControlValue, dateRange.EndDate);
        //    //}
        //}
		#endregion

		#region private methods
        //void dateRangesSelector_DoubleClick(object sender, EventArgs e)
        //{
        //    InvokeDateRangesSelectorForm();
        //}

        //void dateRangesSelector_ButtonClick(object sender, ButtonPressedEventArgs e)
        //{
        //    InvokeDateRangesSelectorForm();
        //}

        //private void InvokeDateRangesSelectorForm()
        //{
        //    var dlg = new DateRangesSelectForm { Value = DateRanges };
        //    this.Backup();
        //    if (dlg.ShowDialog() == DialogResult.OK)
        //    {
        //        _availableRangesChanged = true;
        //        DateRanges = dlg.Value;
        //        this.Commit();
        //        OnRefreshRequested();
        //    }
        //    else
        //    {
        //        this.Rollback();
        //    }
        //}
		#endregion
	}

	#endregion
}
