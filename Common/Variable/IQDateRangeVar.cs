using System;
using System.Collections.Generic;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.UI.InputTemplates.DateTimeTemplates.InputDateRange;

namespace Impac.Mosaiq.IQ.Common.Variable
{
    /// <summary>
    /// A custom type that holds a date range
    /// </summary>
    [Serializable]
    public class DateRange
    {
        #region Public Properties
        /// <summary>
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// </summary>
        public DateTime? EndDate { get; set; }
        #endregion

        #region Overrides
        /// <summary> </summary>
        public override bool Equals(object obj)
        {
            var typedObj = obj as DateRange;

            if (typedObj == null)
                return false;

            if (StartDate != typedObj.StartDate)
                return false;

            if (EndDate != typedObj.EndDate)
                return false;

            return true;
        }

        /// <summary> </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ StartDate.GetHashCode() ^ EndDate.GetHashCode();
        }
        #endregion
    }

    /// <summary>
    /// IQ Variable which holds a date range
    /// </summary>
    [Serializable]
    public class IQDateRangeVar : IQVariableReference<DateRange, IQDateRangeElement>
    {
    }


	/// <summary> Custom IQElementVar class for DateTime. </summary>
	[Serializable]
	public class IQDateRangeElement : IQVarElement<DateRange>
	{
		#region Constructor
		/// <summary> Default Ctor </summary>
		public IQDateRangeElement()
		{
			RangeType = (int) InputDateRangeControlType.Today;
			SetRuntimeValue();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Holds a value from the enumeration Impac.Mosaiq.UI.InputTemplates.DateTimeTemplates.InputDateRange.InputDateRangeControlType
		/// </summary>
		public int RangeType { get; set; }
		/// <summary>
		/// The number of days, week, etc. for date ranges like 'Next Days:', 'Next Weeks:'
		/// </summary>
		public int PeriodCount { get; set; }
		#endregion

		#region Overriden Methods

		/// <summary>
		/// Allows an element to set it's runtime value if the value picked must be resolved at runtime.
		/// </summary>
		public sealed override void SetRuntimeValue()
		{
			if (Value == null)
				return;

			// In this case the start and end dates are already set
			if (RangeType == (int) InputDateRangeControlType.SpecificDates)
				return;

			// In this case there is no date filter at all
			if (RangeType == (int) InputDateRangeControlType.All)
				return;

			Value.StartDate = InputDateRangeValue.CalcStartDate(
				(InputDateRangeControlType) RangeType, PeriodCount, Value.StartDate);
			Value.EndDate = InputDateRangeValue.CalcEndDate(
				(InputDateRangeControlType) RangeType, PeriodCount, Value.EndDate);
		}

		/// <summary> </summary>
		public override bool Equals(object obj)
		{
			var typedObj = obj as IQDateRangeElement;

			if (typedObj == null)
				return false;

			if (RangeType != typedObj.RangeType)
				return false;
			
            if (PeriodCount != typedObj.PeriodCount)
				return false;

			return base.Equals(obj);
		}

		/// <summary> </summary>
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ RangeType.GetHashCode() ^ PeriodCount.GetHashCode();
		}

	    /// <summary>
	    /// Instructs the .Equals method to skip comparing the "value" of two elements.
	    /// </summary>
	    /// <returns></returns>
	    protected override bool SkipValueOnEqualityCheck()
	    {
            var type = (InputDateRangeControlType)RangeType;
            return type != InputDateRangeControlType.SpecificDates;
	    }
	    #endregion

		#region Public Methods
		/// <summary>
		/// </summary>
		public string GetDisplayString()
		{
			string result = GetRangeDisplayName(RangeType);
			switch (RangeType)
			{
				case (int) InputDateRangeControlType.PreviousDays:
				case (int) InputDateRangeControlType.PreviousWeeks:
				case (int) InputDateRangeControlType.NextDays:
				case (int) InputDateRangeControlType.NextWeeks:
				case (int) InputDateRangeControlType.SurroundingDays:
				case (int) InputDateRangeControlType.SurroundingWeeks:
					result += " " + PeriodCount;
					break;
				case (int) InputDateRangeControlType.SpecificDates:
					result += " " + (Value.StartDate.HasValue ? Value.StartDate.Value.ToShortDateString() : String.Empty)
						+ " - " + (Value.EndDate.HasValue ? Value.EndDate.Value.ToShortDateString() : String.Empty);
					break;
			}
			return result;
		}
		#endregion

		#region Public Static Methods
		/// <summary>
		/// </summary>
		public static string GetRangeDisplayName(int rangeType)
		{
			string result = string.Empty;
			switch (rangeType)
			{
				case (int) InputDateRangeControlType.Today:			result = Strings.Today; break;
				case (int) InputDateRangeControlType.PreviousDays:	result = Strings.PreviousDays; break;
				case (int) InputDateRangeControlType.ThisWeek:		result = Strings.ThisWeek; break;
				case (int) InputDateRangeControlType.LastWeek:		result = Strings.LastWeek; break;
				case (int) InputDateRangeControlType.PreviousWeeks: result = Strings.PreviousWeeks; break;
				case (int) InputDateRangeControlType.LastMonth:		result = Strings.LastMonth; break;
				case (int) InputDateRangeControlType.LastYear:		result = Strings.LastYear; break;
				case (int) InputDateRangeControlType.AllToDate:		result = Strings.AllToDate; break;
				case (int) InputDateRangeControlType.All:			result = Strings.All; break;
				case (int) InputDateRangeControlType.TodayToAll:	result = Strings.TodayToAll; break;
				case (int) InputDateRangeControlType.NextDays:		result = Strings.NextDays; break;
				case (int) InputDateRangeControlType.NextWeek:		result = Strings.NextWeek; break;
				case (int) InputDateRangeControlType.NextWeeks:		result = Strings.NextWeeks; break;
				case (int) InputDateRangeControlType.NextMonth:		result = Strings.NextMonth; break;
				case (int) InputDateRangeControlType.NextYear:		result = Strings.NextYear; break;
				case (int) InputDateRangeControlType.SurroundingDays: result = Strings.SurroundingDays; break;
				case (int) InputDateRangeControlType.SurroundingWeeks: result = Strings.SurroundingWeeks; break;
				case (int) InputDateRangeControlType.SpecificDates:	result = Strings.DateRange; break;
			}
			return result;
		}

		/// <summary>
		/// </summary>
		public static List<int> GetAvailableRanges()
		{
			return new List<int>
			       	{
			       		(int) InputDateRangeControlType.Today,
			       		(int) InputDateRangeControlType.PreviousDays,
			       		(int) InputDateRangeControlType.ThisWeek,
			       		(int) InputDateRangeControlType.LastWeek,
			       		(int) InputDateRangeControlType.PreviousWeeks,
			       		(int) InputDateRangeControlType.LastMonth,
			       		(int) InputDateRangeControlType.LastYear,
			       		(int) InputDateRangeControlType.AllToDate,
			       		(int) InputDateRangeControlType.All,
			       		(int) InputDateRangeControlType.TodayToAll,
			       		(int) InputDateRangeControlType.NextDays,
			       		(int) InputDateRangeControlType.NextWeek,
			       		(int) InputDateRangeControlType.NextWeeks,
			       		(int) InputDateRangeControlType.NextMonth,
			       		(int) InputDateRangeControlType.NextYear,
			       		(int) InputDateRangeControlType.SurroundingDays,
			       		(int) InputDateRangeControlType.SurroundingWeeks,
			       		(int) InputDateRangeControlType.SpecificDates,
			       	};
		}

		#endregion
	}
}
