using System;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM.Details
{
	/// <summary>
	/// IQVarDetail implementation for the StaffTypeOrCategory variable activity
	/// </summary>
	public class IQStaffTypeOrCategoryVarDetail : IQVariableDetail<StaffTypeOrCategory, IQVarElement<StaffTypeOrCategory>>
	{
		#region IIQVariableDetail Implementation
		/// <summary> Human readible display string for the value stored.</summary>
		public override string DesignTimeDisplayString
		{
			get { return GetDisplayString(); }
		}

		/// <summary> Display string for the key value stored.</summary>
		public override string DesignTimeKeyString
		{
			get { return GetDisplayString(); }
		}

		private string GetDisplayString()
		{
			switch (Element.Value.ClassifierType)
			{
				case (int) StaffTypeOrCategory.Classifiers.StaffType:
					var prompt = Prompt.GetEntityByID(Element.Value.Value);
					return String.Format("{0} ({1})", prompt.Pro_Text, Strings.StaffTypeLabel);
				case (int) StaffTypeOrCategory.Classifiers.StaffCategory:
					return String.Format("{0} ({1})", Staff.StaffRoleToString((short) Element.Value.Value), Strings.StaffCategoryLabel);
			}
			return string.Empty;
		}

		#endregion
	}
}
