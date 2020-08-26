using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Details
{
	/// <summary>
	/// </summary>
	public class IQNoteTypeVarDetail : IQVariableDetail<Prompt, IQVarElement<int>>
	{
		#region IIQVariableDetail Implementation
		/// <summary>
		/// Note type display string
		/// </summary>
		public override string DesignTimeDisplayString
		{
			get { return IQNoteTypeVarConfig.FixNoteTypeName(Data.Enum, Data.Text); }
		}

		/// <summary>
		/// The note type code
		/// </summary>
		public override string DesignTimeKeyString
		{
			get { return Element.Value.ToString(); }
		}

		#endregion
	}
}
