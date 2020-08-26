using System;
using System.Collections.Generic;
using Impac.Mosaiq.Core.Toolbox.LINQ;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.UI.InputTemplates.DateTimeTemplates.InputDateRange;

namespace Impac.Mosaiq.IQ.Common.Variable
{
	/// <summary>
	/// A class that represents an ObsDef item and its thresholds
	/// </summary>
	[Serializable]
	public class DataItemThresholds
	{
		#region Public Properties
		/// <summary>
		/// The OBD_GUID of the ObsDef item
		/// </summary>
		public Guid ObdGuid { get; set; }

		/// <summary>
		/// The date range for items of type Date
		/// </summary>
		public InputDateRangeValue DateRange { get; set; }

		/// <summary>
		/// The start date - used when the date range type is 'Specific Dates' because 
		/// InputDateRangeValue doesn't persist the specific dates
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// The end date - used when the date range type is 'Specific Dates' because 
		/// InputDateRangeValue doesn't persist the specific dates
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// The lower limit for items of type Numeric
		/// </summary>
		public double? LowerLimit { get; set; }
		/// <summary>
		/// The upport limit for items of type Numeric
		/// </summary>
		public double? UpperLimit { get; set; }

		/// <summary>
		/// The table items for items of type Table
		/// </summary>
		public List<Guid> TableItems { get; set; }

		/// <summary>
		/// The string algorithm (an enum) for items of type Text
		/// </summary>
		public int StringAlgorithm { get; set; }
		/// <summary>
		/// The string input for items of type Text
		/// </summary>
		public string StringInput { get; set; }
		#endregion

		public enum StringAlgorithms {StrEquals, StrEqualsIgnoreCase, StrContains, StrContainsIgnoreCase, StrRegex, UpperLowerLimits}

		#region Overrides
		/// <summary> </summary>
		public override bool Equals(object obj)
		{
			var typedObj = obj as DataItemThresholds;
			if (typedObj == null)
				return false;

			// ObdGuid
			if (ObdGuid != typedObj.ObdGuid)
				return false;

			// DateRange
			if (DateRange == null && typedObj.DateRange != null)
				return false;
			if (DateRange != null && !DateRange.Equals(typedObj.DateRange))
				return false;

			// Lower and Upper limits
			if (LowerLimit != typedObj.LowerLimit || UpperLimit != typedObj.UpperLimit)
				return false;

			// String Algorithm
			if (StringAlgorithm != typedObj.StringAlgorithm)
				return false;
			// String Input
			if (String.IsNullOrEmpty(StringInput) && !String.IsNullOrEmpty(typedObj.StringInput))
				return false;
			if (!String.IsNullOrEmpty(StringInput) &&
				!StringInput.Equals(typedObj.StringInput, StringComparison.InvariantCultureIgnoreCase))
				return false;

			// TableItems
			// If both are null, return true
			if (TableItems == null && typedObj.TableItems == null)
				return true;
			// If only one is null, return false.
			if (TableItems == null || typedObj.TableItems == null)
				return false;

			// If they don't have the same # of items, return false.
			if (TableItems.Count != typedObj.TableItems.Count)
				return false;

			// See if the first list contains all of the items in the second list.  If not, return false.
			if (!TableItems.ContainsAll(typedObj.TableItems))
				return false;

			return true;
		}

		/// <summary> </summary>
		public override int GetHashCode()
		{
			int hashCode = base.GetHashCode() ^ ObdGuid.GetHashCode()
				^ LowerLimit.GetHashCode() ^ UpperLimit.GetHashCode()
				^ StringAlgorithm.GetHashCode();

			if (DateRange != null)
				hashCode ^= DateRange.GetHashCode();

			if (!String.IsNullOrEmpty(StringInput))
				hashCode ^= StringInput.GetHashCode();
			return hashCode;
		}
		#endregion
	}

	/// <summary> IQ Variable which holds a selection of DataItemThresholds items </summary>
	[Serializable]
	public class IQDataItemThresholdsVar : IQVariableReference<DataItemThresholds>
	{
	}

}
