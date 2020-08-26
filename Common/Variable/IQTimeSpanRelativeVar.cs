using System;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;

namespace Impac.Mosaiq.IQ.Common.Variable
{
    /// <summary> IQ Variable which uses a timespan type. </summary>
    [Serializable]
    public class IQTimeSpanRelativeVar : IQVariableReference<TimeSpanRelative>
    {
    }

    /// <summary>
    /// Stores a relative timespan which can be applied to a DateTime object.  The difference between this and the 
    /// .NET timespan is that this class can represent something like "x months" which is not an exact range but
    /// rather relies on a start date.  The AddSpan
    /// </summary>
    [Serializable]
    public class TimeSpanRelative
    {
        #region Public Static Properties
        /// <summary> Display Value for Day </summary>
        public static string Day { get { return Strings.Day; } }

        /// <summary> Display Value for Days </summary>
        public static string Days { get { return Strings.Days; } }

        /// <summary> Display Value for Week </summary>
        public static string Week { get { return Strings.Week; } }

        /// <summary> Display Value for Weeks </summary>
        public static string Weeks { get { return Strings.Weeks; } }

        /// <summary> Display Value for Month </summary>
        public static string Month { get { return Strings.Month; } }

        /// <summary> Display Value for Months </summary>
        public static string Months { get { return Strings.Months; } }

        /// <summary> Display Value for Year </summary>
        public static string Year { get { return Strings.Year; } }
        
        /// <summary> Display Value for Years </summary>
        public static string Years { get { return Strings.Years; } }
        #endregion

        #region Public Properties
        /// <summary> Gets or sets the "Count" of the span. (ie. the "x" in "x days", "x weeks", etc) </summary>
        public int Count { get; set; }

        /// <summary> Gets or sets the units represented by the span. </summary>
        public TimeSpanRelativeUnit Unit { get; set; }

        /// <summary> Gets the value of the span in human readable form. </summary>
        public string DisplayString
        {
            get
            {
                return (Count == 1 || Count == -1)
                    ? String.Format("{0} {1}", Count, GetUnitStringSingle())
                    : String.Format("{0} {1}", Count, GetUnitStringMultiple());
            }
        }
        #endregion

        #region Public Methods
        /// <summary> Takes a DateTime value as input and adds the relative span onto it. </summary>
        /// <param name="pStartDate"></param>
        /// <returns></returns>
        public DateTime AddSpan(DateTime pStartDate)
        {
            switch(Unit)
            {
                case TimeSpanRelativeUnit.Days:
                    return pStartDate.AddDays(Count);

                case TimeSpanRelativeUnit.Weeks:
                    return pStartDate.AddDays(Count*7);  //No "AddWeeks method :(

                case TimeSpanRelativeUnit.Months:
                    return pStartDate.AddMonths(Count);

                case TimeSpanRelativeUnit.Years:
                    return pStartDate.AddYears(Count);

                default:
                    throw new InvalidOperationException(Unit + " is not supported.");
            }
        }
        
        /// <summary> Takes a DateTime value and subtracts the relative span from it. </summary>
        /// <param name="pStartDate"></param>
        /// <returns></returns>
        public DateTime SubtractSpan(DateTime pStartDate)
        {
            switch (Unit)
            {
                case TimeSpanRelativeUnit.Days:
                    return pStartDate.AddDays(-1 * Count);

                case TimeSpanRelativeUnit.Weeks:
                    return pStartDate.AddDays(-1* Count * 7);  //No "AddWeeks method :(

                case TimeSpanRelativeUnit.Months:
                    return pStartDate.AddMonths(-1 * Count);

                case TimeSpanRelativeUnit.Years:
                    return pStartDate.AddYears(-1 * Count);

                default:
                    throw new InvalidOperationException(Unit + " is not supported.");
            }
        }
        #endregion

        #region Private Methods
        private string GetUnitStringSingle()
        {
            switch (Unit)
            {
                case TimeSpanRelativeUnit.Days:
                    return Strings.Day;

                case TimeSpanRelativeUnit.Weeks:
                    return Strings.Week;

                case TimeSpanRelativeUnit.Months:
                    return Strings.Month;

                case TimeSpanRelativeUnit.Years:
                    return Strings.Year;

                default:
                    throw new InvalidOperationException(Unit + " is not supported.");
            }
        }

        private string GetUnitStringMultiple()
        {
            switch (Unit)
            {
                case TimeSpanRelativeUnit.Days:
                    return Strings.Days;

                case TimeSpanRelativeUnit.Weeks:
                    return Strings.Weeks;

                case TimeSpanRelativeUnit.Months:
                    return Strings.Months;

                case TimeSpanRelativeUnit.Years:
                    return Strings.Years;

                default:
                    throw new InvalidOperationException(Unit + " is not supported.");
            }
        }

        #endregion

        #region Overloaded Operators
        /// <summary>
        /// Overload "+" operator so that a TimeSpanRelative an be added to a DateTime object using the + operator.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static DateTime operator +(DateTime c1, TimeSpanRelative c2)
        {
            return c2.AddSpan(c1);
        }

        /// <summary>
        /// Overload "-" operator so that a TimeSpanRelative an be added to a DateTime object using the - operator.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static DateTime operator -(DateTime c1, TimeSpanRelative c2)
        {
            return c2.SubtractSpan(c1);
        }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Overriden Equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var curObj = obj as TimeSpanRelative;

            //return false if the object is not an IQ Enum
            if (curObj == null)
                return false;

            //Return false if strings differ
            if (curObj.Unit != Unit)
                return false;

            //return false if keys differ
            if (curObj.Count != Count)
                return false;

            return true;
        }

        /// <summary>
        /// need to override GetHashCode() when we override Equals(), otherwise weird things happen (e.g. Intersect() 
        /// doesn't always work)
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Unit.GetHashCode() ^ Count.GetHashCode();
        }
        #endregion
    }

    /// <summary>
    /// Indicates whether the units representing the timespan are days, weeks, months or years.
    /// </summary>
    public enum TimeSpanRelativeUnit
    {
        /// <summary> Span is represented in days. </summary>
        Days = 1,

        /// <summary> Span is represented in weeks. </summary>
        Weeks = 2,

        /// <summary> Span is represented in months. </summary>
        Months = 3,

        /// <summary> Span is represented in years. </summary>
        Years = 4
    }
}
