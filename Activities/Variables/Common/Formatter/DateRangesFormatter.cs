using System;
using System.Linq;
using Impac.Mosaiq.IQ.Common.Variable;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Formatter
{
	/// <summary>
	/// 
	/// </summary>
	public class DateRangesFormatter : IFormatProvider, ICustomFormatter
	{
		internal IQDateRangeVarConfig Config { private get; set; }

		#region ICustomFormatter Members

		/// <summary>
		/// Returns the method for performing the format.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="arg"></param>
		/// <param name="formatProvider"></param>
		/// <returns></returns>
		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			return String.Join(", ", Config.DateRanges.Select(IQDateRangeElement.GetRangeDisplayName));
		}
		#endregion

		#region IFormatProvider Members

		/// <summary>
		/// Returns an ICustomFormatter (which is this class also).
		/// </summary>
		/// <param name="formatType"></param>
		/// <returns></returns>
		public object GetFormat(Type formatType)
		{
			return this;
		}
		#endregion
	}
}
