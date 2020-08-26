using System;
using System.Linq;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Formatter
{
	/// <summary>
	/// Formatter class for converting a list of ObsDefDataFormat objects to a string.
	/// </summary>
    internal class FlowsheetItemTypeFormatter : IFormatProvider, ICustomFormatter
    {
        #region Internal Properties
        internal IQDataItemVarConfig Config { private get; set; }
        #endregion Internal Properties

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
            if (Config.AllowedDataItemTypes.Count == 0)
                return Strings.ObsDefType_NoneSelected;

            return String.Join(", ", Config.AllowedDataItemTypes.Select(IQDataItemVarConfig.ConvertToString).OrderBy(e => e));
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
