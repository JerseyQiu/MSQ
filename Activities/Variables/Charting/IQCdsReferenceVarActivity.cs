using System;
using System.Windows.Forms;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Details;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
	/// <summary> </summary>
	[GeneralCharting_Category]
	[Variables_ActivityGroup]
	[IQCdsReferenceVarActivity_DisplayName]
	public class IQCdsReferenceVarActivity : IQVariableActivityEntity<int, CDSReference, IQIntegerVar, IQCdsReferenceVarDetail, IQCdsReferenceVarConfig>
	{
	}

	/// <summary> The configuration class for the IQCdsReferenceVarActivity class</summary>
	[Serializable]
	public class IQCdsReferenceVarConfig : IQVariableConfigEntity<int, CDSReference, IQIntegerVar, IQCdsReferenceVarDetail>
	{
		protected override IQOpResult<int> OpenEditor(int currentValue, IQVarElementTarget target)
		{
			var editor = new CdsReferenceSelector { Value = currentValue };
			return editor.ShowDialog() == DialogResult.OK
					? new IQOpResult<int> { Result = OpResultEnum.Completed, Value = editor.Value }
					: new IQOpResult<int> { Result = OpResultEnum.Cancelled };
		}
	}
}
