using System;
using System.Windows.Forms;
using Impac.Mosaiq.IQ.Activities.Variables.PM.Details;
using Impac.Mosaiq.IQ.Activities.Variables.PM.Editors;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;
using Impac.Mosaiq.BOM.Entities;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM
{

	#region Activity Class
	/// <summary>
	/// An IQ variable for staff type or role (category) selection
	/// </summary>
	[IQStaffTypeOrCategoryVarActivity_DisplayName]
	[PracticeManagement_Category]
	[Variables_ActivityGroup]
	public sealed class IQStaffTypeOrCategoryVarActivity : IQVariableActivitySimple<StaffTypeOrCategory, IQStaffTypeOrCategoryVar, IQStaffTypeOrCategoryVarDetail, IQStaffTypeOrCategoryVarConfig>
	{
	}
	#endregion

	#region Configuration Class
	/// <summary> The configuration class for the IQStaffTypeOrCategoryVarActivity class</summary>
	[Serializable]
	public class IQStaffTypeOrCategoryVarConfig : IQVariableConfigSimple<StaffTypeOrCategory, IQStaffTypeOrCategoryVar, IQStaffTypeOrCategoryVarDetail>
	{
		/// <summary> Opens the staff role selection window. </summary>
		/// <returns></returns>
		protected override IQOpResult<StaffTypeOrCategory> OpenEditor(StaffTypeOrCategory currentValue, IQVarElementTarget target)
		{
			var editor = new StaffTypeOrCategorySelector {Value = currentValue};
			return editor.ShowDialog() == DialogResult.OK
					? new IQOpResult<StaffTypeOrCategory> { Result = OpResultEnum.Completed, Value = editor.Value }
					: new IQOpResult<StaffTypeOrCategory> { Result = OpResultEnum.Cancelled };

			/*
			var editor = new StaffTypeOrCategorySelector {Value = currentValue, Text = Strings.SelectStaffCategory};

			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.Physician);
			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.Resident);
			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.Therapist);
			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.SystemManager);
			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.Physicist);
			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.ServiceEngineer);
			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.Dosimetrist);
			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.Nurse);
			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.Administrator);
			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.Clerical);
			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.Billing);
			AddStaffRoleInfo(editor, BOM.Entities.Defs.StaffRole.PPS);

			return editor.ShowDialog() == DialogResult.OK
			       	? new IQOpResult<int> {Result = OpResultEnum.Completed, Value = editor.Value}
			       	: new IQOpResult<int> {Result = OpResultEnum.Cancelled};
			 */
		}
	}
	#endregion

}
