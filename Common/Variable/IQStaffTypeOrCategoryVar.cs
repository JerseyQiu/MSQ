using System;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;

namespace Impac.Mosaiq.IQ.Common.Variable
{

	/// <summary>
	/// A class that represents staff type or category
	/// </summary>
	[Serializable]
	public class StaffTypeOrCategory
	{
		/// <summary> </summary>
		public enum Classifiers { StaffType = 0, StaffCategory = 1 }

		/// <summary> 
		/// Staff classifier type - Type or Role/Category
		/// </summary>
		public int ClassifierType { get; set; }

		/// <summary> </summary>
		public int Value { get; set; }

		/// <summary> </summary>
		public override bool Equals(object obj)
		{
			var typedObj = obj as StaffTypeOrCategory;
			if (typedObj == null)
				return false;

			return typedObj.ClassifierType == ClassifierType && typedObj.Value == Value;
		}

		/// <summary> </summary>
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ ClassifierType.GetHashCode() ^ Value.GetHashCode();
		}
	}

	/// <summary> IQ Variable which holds a selection of StaffTypeOrCategory items </summary>
	[Serializable]
	public class IQStaffTypeOrCategoryVar : IQVariableReference<StaffTypeOrCategory>
	{
		/// <summary> 
		/// Check wether the current variable matches the provided staff category or type
		/// </summary>
		public bool Matches(int category, string type)
		{
			bool retVal;

			switch (State)
			{
				case IQVarState.All:
					retVal = true;
					break;
				case IQVarState.None:
				case IQVarState.NotSet:
					//"None" means an empty list and, since it's empty, nothing will match it.
					//"Not Set" means undefined and any value compared against undefined never matches either.
					retVal = false;
					break;
				case IQVarState.Selected:
					//If a selection has been made (Single or Multiple) then the selected value(s) will always be in the 
					//SelectedValuesAll shortcut property.
					retVal = false;
					foreach (var value in SelectedValuesAll)
					{
						if (value.ClassifierType == (int) StaffTypeOrCategory.Classifiers.StaffCategory)
						{
							if (value.Value == category)
							{
								retVal = true;
								break;
							}
						}
						else if (value.ClassifierType == (int) StaffTypeOrCategory.Classifiers.StaffType)
						{
							var prompt = Prompt.GetEntityByID(value.Value);
							if (String.Equals(prompt.Pro_Text.Trim(), type.Trim()))
							{
								retVal = true;
								break;
							}
						}
					}
					break;
				default:
					throw new InvalidOperationException(State + "is not a supported IQVarState value.");
			}

			return retVal;
		}
	}
}
