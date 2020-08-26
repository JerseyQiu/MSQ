using System;
using System.Text;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Core.Defs.Enumerations;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;
using Impac.Mosaiq.UI.InputTemplates.DateTimeTemplates.InputDateRange;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Details
{
	/// <summary></summary>
	public class IQDataItemThresholdsVarDetail : IQVariableDetailSimple<DataItemThresholds>
	{
		/// <summary>
		/// The human readable display string for a design time value (ie. something looked up from the database from
		/// a stored primary key or a value entered directly from the user.
		/// </summary>
		public override string DesignTimeDisplayString
		{
			get { return GetDisplayString(); }
		}

		/// <summary>
		/// The a string displaying the key for a design time value.  This could be the primary key of a selected value
		/// something typed in by the user, such as a number or string.
		/// </summary>
		public override string DesignTimeKeyString
		{
			get { return GetDisplayString(); }
		}

		private string GetDisplayString()
		{
			if (Element.Value.ObdGuid == Guid.Empty)
				return String.Empty;

			try
			{
				var obd = ObsDef.GetEntityByObdGuid(Element.Value.ObdGuid);
				if (obd.IsNullEntity)
					return String.Empty;

				string str = String.Empty;

				var value = Element.Value;

				if (obd.Type == (short) MedDefs.ObdType.ItemTable)
				{
					if (value.TableItems != null)
					{
						var sb = new StringBuilder();
						foreach (var item in value.TableItems)
						{
							var obdItem = ObsDef.GetEntityByObdGuid(item);
                            sb.AppendFormat("{0}; ",obdItem.LabelDescriptionInactiveIndicator.Trim());
						}

						str = sb.ToString().Trim().TrimEnd(';');
					}
				}
				else
				{
					switch (obd.ObsDefDataFormat)
					{
						case ObsDefDataFormat.Numeric:
							str = FormatNumericThresholds(value);
							break;
						case ObsDefDataFormat.Date:
							if (value.DateRange.Type == InputDateRangeControlType.SpecificDates)
							{
								bool startDateValid = value.StartDate.HasValue && value.StartDate.Value > DateTime.MinValue;
								bool endDateValid = value.EndDate.HasValue && value.EndDate.Value < DateTime.MaxValue;
								if (startDateValid && endDateValid)
									str = String.Format("{0} - {1}",
														value.StartDate.Value.ToShortDateString(),
														value.EndDate.Value.ToShortDateString());
								else if (startDateValid)
									str = String.Format(">= {0}", value.StartDate.Value.ToShortDateString());
								else if (endDateValid)
									str = String.Format("<= {0}", value.EndDate.Value.ToShortDateString());
							}
							else
							{
								str = Element.Value.DateRange.ToReadableString();
							}
							break;
						case ObsDefDataFormat.String:
						case ObsDefDataFormat.Memo:
							if (DataItemThresholdsSelector.UseStringNumericLimits(Element.Value.StringAlgorithm))
							{
								str = FormatNumericThresholds(value);
							}
							else
							{
								str = String.Format("{0} \"{1}\"",
								                    DataItemThresholdsSelector.StringAlgorithmIdToString(Element.Value.StringAlgorithm),
								                    Element.Value.StringInput);
							}
							break;
					}
				}

				if (String.IsNullOrEmpty(str))
					return obd.LabelInactiveIndicator.Trim();

				return String.Format("{0} ({1})", obd.LabelInactiveIndicator.Trim(), str);
			}
			catch (Exception)
			{
				return String.Empty;
			}
		}

		private static string FormatNumericThresholds(DataItemThresholds value)
		{
			string str = String.Empty;
			if (value.LowerLimit.HasValue && value.UpperLimit.HasValue)
				str = String.Format("{0} - {1}", value.LowerLimit.Value, value.UpperLimit.Value);
			else if (value.LowerLimit.HasValue)
				str = String.Format(">= {0}", value.LowerLimit.Value);
			else if (value.UpperLimit.HasValue)
				str = String.Format("<= {0}", value.UpperLimit.Value);

			return str;
		}
	}
}
