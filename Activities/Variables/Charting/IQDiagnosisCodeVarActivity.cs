using System;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Details;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
	#region Activity Classs
	/// <summary>
	/// Select a diagnosis code
	/// </summary>
	[IQDiagnosisCodeVarActivity_DisplayName]
	[GeneralCharting_Category]
	[Variables_ActivityGroup]
	public sealed class IQDiagnosisCodeVarActivity : IQVariableActivityEntity<int, Topog, IQIntegerVar, IQDiagnosisCodeVarDetail, IQDiagnosisCodeVarConfig>
	{
	}
	#endregion

	#region Configuration Class
	/// <summary> Configuration class for the IQDiagnosisCodeVarActivity class</summary>
	[Serializable]
	public class IQDiagnosisCodeVarConfig : IQVariableConfigEntity<int, Topog, IQIntegerVar, IQDiagnosisCodeVarDetail>
	{
		#region Overriden Methods
		/// <summary>
		/// Opens the Topog browse
		/// </summary>
		/// <returns></returns>
		protected override IQOpResult<int> OpenEditor(int currentValue, IQVarElementTarget target)
		{
			OnBeforeClarionEditorInvoke(this);
			int diagCode = CallClarion.CallDiagnosisBro(String.Empty, true, true, currentValue);
			OnAfterClarionEditorInvoke(this);

			return (diagCode != 0)
					   ? new IQOpResult<int> { Result = OpResultEnum.Completed, Value = diagCode }
					   : new IQOpResult<int> { Result = OpResultEnum.Cancelled };
		}
		#endregion
	}
	#endregion
}
