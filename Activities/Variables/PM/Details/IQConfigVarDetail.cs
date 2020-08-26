using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM.Details
{
	/// <summary>
	/// IQVarDetail implementation for staff objects.
	/// </summary>
	public class IQConfigVarDetail : IQVariableDetail<Config, IQVarElement<int>>
	{
		#region IIQVariableDetail Implementation
		/// <summary> Human readible display string for the value stored.</summary>
		public override string DesignTimeDisplayString
		{
			get { return Data.Inst_Name; }
		}

		/// <summary> Display string for the key value stored.</summary>
		public override string DesignTimeKeyString
		{
			get { return Data.Inst_ID.ToString(); }
		}
		#endregion
	}
}
