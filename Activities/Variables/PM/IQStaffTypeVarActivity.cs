using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
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
	/// Select a document type from Prompt
	/// </summary>
	[IQStaffTypeVarActivity_DisplayName]
	[PracticeManagement_Category]
	[Variables_ActivityGroup]
	public sealed class IQStaffTypeVarActivity : IQVariableActivityEntity<int, Prompt, IQIntegerVar, IQPromptVarDetail, IQStaffTypeVarConfig>
	{
	}
	#endregion

	#region Configuration Class
	/// <summary> The configuration class for the IQStaffTypeVarActivity</summary>
	[Serializable]
	public class IQStaffTypeVarConfig : IQVariableConfigEntity<int, Prompt, IQIntegerVar, IQPromptVarDetail>
	{
		/// <summary> Opens the prompt browse for staff types. </summary>
		/// <returns></returns>
		protected override IQOpResult<int> OpenEditor(int currentValue, IQVarElementTarget target)
		{
			var pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
			var query = GetStaffTypesQuery();
			var staffTypes = pm.GetEntities<Prompt>(query);
			return null;
			/*
			var editor = new StaffTypeOrCategorySelector {Value = currentValue, Text = Strings.SelectStaffType};
			// It seems that we cannot use the Prompt.Enum value for staff type because the database
			// contains several entries that have Prompt.Enum = 0
			foreach (var staffType in staffTypes)
				//editor.AddItem(staffType.Enum, staffType.Text);
				editor.AddItem(staffType.Pro_ID, staffType.Text);

			//If the user hit "close", don't update the primaryKeyValue)
			return editor.ShowDialog() == DialogResult.OK
					? new IQOpResult<int> { Result = OpResultEnum.Completed, Value = editor.Value }
					: new IQOpResult<int> { Result = OpResultEnum.Cancelled };
			 */
		}

		/*
		/// <summary>
		/// Overriden method for looking up details by ID (since we're not using the primary key of Prompt)
		/// </summary>
		protected override IDictionary<int, Prompt> GetEntitiesDictFromIds(IEnumerable<int> ids, ImpacPersistenceManager pm)
		{
			var query = GetStaffTypesQuery();
			query.AddClause(PromptDataRow.EnumEntityColumn, EntityQueryOp.In, ids.ToList());
			return pm.GetEntities<Prompt>(query, QueryStrategy.Normal).ToDictionary(e => (int) e.Enum, e => e);
		}
		 */

		private static ImpacRdbQuery GetStaffTypesQuery()
		{
			var query = new ImpacRdbQuery(typeof(Prompt));
			query.AddClause(PromptDataRow.PGroupEntityColumn, EntityQueryOp.EQ, "STF0");
			query.AddClause(PromptDataRow.TextEntityColumn, EntityQueryOp.NE, "Location");
			return query;
		}
	}
	#endregion

}
