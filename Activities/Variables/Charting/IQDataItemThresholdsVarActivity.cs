using System;
using System.Windows.Forms;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Details;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
	#region Activity Classs
	/// <summary>
	/// This activity allows the user to select an observation item (data item or table item) in the UI and 
	/// outputs the GUID key to the output parameter.
	/// </summary>
	[IQDataItemThresholdsVarActivity_DisplayName]
	[GeneralCharting_Category]
	[Variables_ActivityGroup]
	public sealed class IQDataItemThresholdVarActivity : IQVariableActivitySimple<DataItemThresholds, IQDataItemThresholdsVar, IQDataItemThresholdsVarDetail, IQDataItemThresholdVarConfig>
	{
	}
	#endregion

	#region Configuration Class
	/// <summary> Configuration class for the IQVarDataItemActivity class</summary>
	[Serializable]
	public class IQDataItemThresholdVarConfig : IQVariableConfigSimple<DataItemThresholds, IQDataItemThresholdsVar, IQDataItemThresholdsVarDetail>
	{
		#region Overriden Methods
		/// <summary>
		/// Opens the data item browse
		/// </summary>
		/// <returns></returns>
		protected override IQOpResult<DataItemThresholds> OpenEditor(DataItemThresholds defaultValue, IQVarElementTarget target)
		{
			var editor = new DataItemThresholdsSelector();
			editor.Value = defaultValue;
			if (editor.ShowDialog() != DialogResult.OK)
				return new IQOpResult<DataItemThresholds> { Result = OpResultEnum.Cancelled };

			return new IQOpResult<DataItemThresholds> { Result = OpResultEnum.Completed, Value = editor.Value };
		}

		#endregion

		public static void MinimizeIqEditor(object sender)
		{
			OnBeforeClarionEditorInvoke(sender);
		}

		public static void RestoreIqEditor(object sender)
		{
			OnAfterClarionEditorInvoke(sender);
		}
	}

	#endregion
}