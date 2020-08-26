using System;
using System.Windows.Forms;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Activities.Variables.PM.Details;
using Impac.Mosaiq.IQ.Activities.Variables.PM.Editors;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM
{
	#region Activity Class
	/// <summary>
	/// This activity allows the user to select a department and outputs the primary key to the output parameter.
	/// </summary>
	[IQDepartmentVarActivity_DisplayName]
	[PracticeManagement_Category]
	[Variables_ActivityGroup]
	public sealed class IQDepartmentVarActivity : IQVariableActivityEntity<int, Config, IQIntegerVar, IQConfigVarDetail, IQDepartmentVarConfig>
	{
	}
	#endregion

	#region Configuration Class
	/// <summary>Configuration class for the IQDepartmentVarActivity </summary>
	[Serializable]
	public class IQDepartmentVarConfig : IQVariableConfigEntity<int, Config, IQIntegerVar, IQConfigVarDetail>
	{
		/// <summary> </summary>
		protected override IQOpResult<int> OpenEditor(int defaultValue, IQVarElementTarget target)
		{
			var editor = new DepartmentSelector(defaultValue);
			return (editor.ShowDialog() == DialogResult.OK)
					   ? new IQOpResult<int> { Result = OpResultEnum.Completed, Value = editor.Value }
					   : new IQOpResult<int> { Result = OpResultEnum.Cancelled };
		}
	}
	#endregion
}
