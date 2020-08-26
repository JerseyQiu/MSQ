using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Details
{
	/// <summary> </summary>
	public class IQCdsReferenceVarDetail : IQVariableDetail<CDSReference, IQVarElement<int>>
	{
		/// <summary> </summary>
		public override string DesignTimeDisplayString
		{
			get { return Data.Title; }
		}

		/// <summary> </summary>
		public override string DesignTimeKeyString
		{
			get { return Data.CDSR_ID.ToString(); }
		}
	}
}
