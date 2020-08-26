using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.UI.InputTemplates.DateTimeTemplates.InputDateRange;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Editor
{
    /// <summary> Simple editor class for changing the value of a string. </summary>
    public partial class DateRangeValueEditor : XtraForm
    {
        #region Constructor
        /// <summary> Default ctor </summary>
        public DateRangeValueEditor()
        {
            InitializeComponent();
        }
        #endregion

    	/// <summary> The available date ranges </summary>
		public List<int> DateRanges { get; set; }

    	/// <summary> The value to edit </summary>
		public IQDateRangeElement Value { get; set; }

        #region Event Handlers
		private void DateRangeValueEditor_Load(object sender, EventArgs e)
		{
			inputDateRangeControl.ClearDateRanges();
			foreach (var dateRange in DateRanges)
				inputDateRangeControl.AddDateRangeType((InputDateRangeControlType) dateRange,
													   IQDateRangeElement.GetRangeDisplayName(dateRange));
			
			if (Value == null || Value.Value == null)
			{
				// If no current value is provided, select the first element from the available ranges list
				if (DateRanges.Count > 0)
					inputDateRangeControl.DateRange = new InputDateRangeValue((InputDateRangeControlType) DateRanges[0]);
				return;
			}

			DateTime? startDate = Value.Value.StartDate;
			DateTime? endDate = Value.Value.EndDate;
			if (Value.RangeType != (int) InputDateRangeControlType.SpecificDates)
			{
				// If the current range is not 'specific dates' we need to set the start and end dates to 'today', 
				// so that if the user selects 'specific dates' the default start and end dates are correctly initialized
				startDate = DateTime.Now.Date;                            
				endDate = DateTime.Now.Date;
			}

			// DR 46097: Remove the 'time' part of the datetime value so that it doesn't mess up
			// the calculation of the new start and end dates by the input date range control
			if (startDate.HasValue)
				startDate = startDate.Value.Date;
			if (endDate.HasValue)
				endDate = endDate.Value.Date;

			inputDateRangeControl.DateRange = new InputDateRangeValue(
				(InputDateRangeControlType) Value.RangeType, startDate, endDate, Value.PeriodCount);
		}

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
        #endregion

		private void CloseDialog()
		{
			InputDateRangeValue dateRange = inputDateRangeControl.DateRange;
			Value = new IQDateRangeElement
			        	{
			        		RangeType = (int) dateRange.Type,
							PeriodCount = dateRange.SpinControlValue,
							Value = new DateRange { StartDate = dateRange.StartDate, EndDate = dateRange.EndDate}
			        	};
			DialogResult = DialogResult.OK;
			Close();
		}

        #region Overriden Methods
        /// <summary> Add special handling for enter and escape key presses </summary>
        /// <param name="e"></param>
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
    }
}
