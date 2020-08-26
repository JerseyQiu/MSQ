using System;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Formatter
{
	/// <summary>
	/// 
	/// </summary>
	public class FlowsheetTabFormatter : IFormatProvider, ICustomFormatter
	{
		internal IQFlowsheetVarConfig Config { private get; set; }

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
			return Config.TabName;
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
