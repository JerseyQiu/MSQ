using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Details
{
	/// <summary>
	/// </summary>
    public class IQDateRangeVarDetail : IQVariableDetail<DateRange, IQDateRangeElement>
	{
		/// <summary> Display Value </summary>
        public override string DesignTimeDisplayString
		{
			get { return Element.GetDisplayString(); }
		}

		/// <summary> The key value to display </summary>
        public override string DesignTimeKeyString
		{
			get { return Element.GetDisplayString(); }
		}
	}
}
