using System;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Details
{
	/// <summary>
	/// </summary>
    public class IQFlowsheetVarDetail : IQVariableDetailSimple<FlowsheetSelection>
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
			return Element.Value.ViewGuid != Guid.Empty
					? IQFlowsheetVarConfig.GetObdLabel(Element.Value.ViewGuid)
			       	: Strings.NoViewSelected;
		}
	}
}
