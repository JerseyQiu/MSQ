using System;
using System.Collections.Generic;
using System.Linq;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Activities.Variables.PM.Details;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
	#region Activity Class
	/// <summary>
	/// Select a document type from Prompt
	/// </summary>
	[IQDocumentTypeVarActivity_DisplayName]
	[GeneralCharting_Category]
	[Variables_ActivityGroup]
	public sealed class IQDocumentTypeVarActivity : IQVariableActivityEntity<int, Prompt, IQIntegerVar, IQPromptVarDetail, IQDocumentTypeVarConfig>
	{
	}
	#endregion

	#region Configuration Class
	/// <summary> The configuration class for the IQDocumentTypeVarActivity</summary>
	[Serializable]
	public class IQDocumentTypeVarConfig : IQVariableConfigEntity<int, Prompt, IQIntegerVar, IQPromptVarDetail>
	{
		/// <summary> Opens the prompt browse for document types. </summary>
		/// <returns></returns>
		protected override IQOpResult<int> OpenEditor(int defaultValue, IQVarElementTarget target)
		{
			OnBeforeClarionEditorInvoke(this);
			int docTypeId = CallClarion.CallPromptBro("ESC7", true, 2, false, Strings.DocumentType);
			OnAfterClarionEditorInvoke(this);
			var entity = Prompt.GetEntityByID(docTypeId);
			
			//If the user hit "close", don't update the primaryKeyValue)
			return (docTypeId != 0)
					   ? new IQOpResult<int> { Result = OpResultEnum.Completed, Value = entity.Enum }
					   : new IQOpResult<int> { Result = OpResultEnum.Cancelled };
		}

		/// <summary>
		/// Overriden method for looking up details by ID (since we're not using the primary key of Prompt)
		/// </summary>
		protected override IDictionary<int, Prompt> GetEntitiesDictFromIds(IEnumerable<int> ids, ImpacPersistenceManager pm)
		{
			var query = new ImpacRdbQuery(typeof(Prompt));
			query.AddClause(PromptDataRow.PGroupEntityColumn, EntityQueryOp.EQ, "ESC7");
			query.AddClause(PromptDataRow.EnumEntityColumn, EntityQueryOp.In, ids.ToList());
			return pm.GetEntities<Prompt>(query, QueryStrategy.Normal).ToDictionary(e => (int) e.Enum, e => e);
		}
	}
	#endregion

}
