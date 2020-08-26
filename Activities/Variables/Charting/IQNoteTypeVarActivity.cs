using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Core.Defs.Enumerations;
using Impac.Mosaiq.Core.Globals;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Details;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
	#region Activity Class
	/// <summary> IQ variable activity for collecting a "note type" value </summary>
	[IQNoteTypeVarActivity_DisplayName]
	[GeneralCharting_Category]
	[Variables_ActivityGroup]
	public class IQNoteTypeVarActivity : IQVariableActivityEntity<int, Prompt, IQIntegerVar, IQNoteTypeVarDetail, IQNoteTypeVarConfig>
	{
	}
	#endregion

	#region Configuration Class
	/// <summary> The configuration class for the IQVarStringActivity class</summary>
	[Serializable]
	public class IQNoteTypeVarConfig : IQVariableConfigEntity<int, Prompt, IQIntegerVar, IQNoteTypeVarDetail>
	{
		#region Overrides

		/// <summary>
		/// Overriden method for looking up details by ID (since we're not using the primary key of Prompt)
		/// </summary>
		protected override IDictionary<int, Prompt> GetEntitiesDictFromIds(IEnumerable<int> ids, ImpacPersistenceManager pm)
		{
			var query = new ImpacRdbQuery(typeof(Prompt));
			query.AddClause(PromptDataRow.PGroupEntityColumn, EntityQueryOp.EQ, "#NT1");
			query.AddClause(PromptDataRow.EnumEntityColumn, EntityQueryOp.In, ids.ToList());
			return pm.GetEntities<Prompt>(query, QueryStrategy.Normal).ToDictionary(e => (int) e.Enum, e => e);
		}

	    /// <summary>
	    /// Opens the selection window used to select a single instance of the object type the configuration object works with.
	    /// This method must be drived in all derived configuration classes.
	    /// </summary>
		protected override IQOpResult<int> OpenEditor(int currentValue, IQVarElementTarget target)
		{
			List<int> values = currentValue != 0 ? new List<int> { currentValue } : null;

		    var editor = new NoteTypeSelector {IsMultiSelect = false, Value = values};
		    return editor.ShowDialog() == DialogResult.OK
					   ? new IQOpResult<int> { Result = OpResultEnum.Completed, Value = editor.Value.Count > 0 ? editor.Value[0] : 0 }
					   : new IQOpResult<int> { Result = OpResultEnum.Cancelled };
		}

	    /// <summary>
	    /// An optional method which, if provided, will allow the IQ Variable to open a dialog that allows multi selection
	    /// so that a user can select more than one item at a time.
	    /// </summary>
		protected override Func<IQOpResult<IEnumerable<int>>> OpenEditorMultiSelectSimple { get { return InvokeEditorMultiSelect; } }

	    /// <summary> Custom values not supported for IQEnumVar activities. </summary>
		public override bool SupportsCustomValues { get { return false; } }
		#endregion

        #region Private Methods (Static)
        /// <summary>Invokes NoteTypeSelector in multi-selection mode. </summary>
		private static IQOpResult<IEnumerable<int>> InvokeEditorMultiSelect()
        {
            var editor = new NoteTypeSelector { IsMultiSelect = true };
            return editor.ShowDialog() == DialogResult.OK
					   ? new IQOpResult<IEnumerable<int>> { Result = OpResultEnum.Completed, Value = editor.Value }
					   : new IQOpResult<IEnumerable<int>> { Result = OpResultEnum.Cancelled };
        }
        #endregion

		static internal string FixNoteTypeName(short noteType, string name)
		{
			// prepend the text 'Clinical-' to the user-configured clinical notes types
			Converter<string, string> fixNoteType =
				arg => String.IsNullOrEmpty(arg) ? name : Strings.ClinicalNotePrefix + arg;

			string result = name;
			switch ((ClinicalNoteTypes) noteType)
			{
				case ClinicalNoteTypes.OncHx:
					result = fixNoteType(Global.NoteTitleOncHx);
					break;
				case ClinicalNoteTypes.PastHx:
					result = fixNoteType(Global.NoteTitlePastHx);
					break;
				case ClinicalNoteTypes.FamHx:
					result = fixNoteType(Global.NoteTitleFamHx);
					break;
				case ClinicalNoteTypes.SocHx:
					result = fixNoteType(Global.NoteTitleSocHx);
					break;
				case ClinicalNoteTypes.RadSum:
					result = fixNoteType(Global.NoteTitleRadSum);
					break;
			}
			return result.Trim();
		}
    }
	#endregion
}
