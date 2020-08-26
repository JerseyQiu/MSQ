using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Impac.Mosaiq.Core.Toolbox.LINQ;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;

namespace Impac.Mosaiq.IQ.Common.Variable
{
    /// <summary>
    /// IQVariableType which stores a key/value pair of int/string.
    /// </summary>
    [Serializable]
    public class IQEnumVar : IQVariableReference<IQEnum>
    {
        /// <summary>
        /// Returns the selected key if a single enum is selected or zero otherwise.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedKey
        {
            get { return SelectedValue != null ? SelectedValue.Key : default(int); }
        }

        /// <summary>
        /// Returns the selected key if a single enum is selected or null otherwise.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? SelectedKeyEx
        {
            get { return SelectedValue != null ? SelectedValue.Key : (int?)null; }
        }

        /// <summary> 
        /// Returns the key values of every element in the selected elements list, regardless of whether the element
        /// stores a standard value, a custom value, or a current value.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<int> SelectedKeysAll
        {
            get { return SelectedElements.Select(e => e.Value.Key).ToList(); }
        }

        /// <summary>
        /// Returns all unique keys, as values assigned to custom values when the script executes could be the same
        /// as a standard value (or current value) provided by the user.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<int> SelectedKeys
        {
            get { return SelectedKeysAll.Distinct().ToList(); }
        }

        /// <summary>
        /// Support function to evaluate whether the IQEnum with the key passed in has been selected.
        /// </summary>
        public bool Contains(int key)
        {
            bool retVal;

            switch (State)
            {
                case IQVarState.All:
                    //If there are selected elements, then it means preselected values are being used and that the 
                    //SelectedElements are populated with all of the preselected values in the designer.  If there are no
                    //SelectedElements, then preselected values are NOT used, and then we return "true" by default because
                    //any value is considered to match "ALL".
                    retVal = SelectedElements.Count > 0
                                 ? SelectedKeysAll.Contains(key)
                                 : true;
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
                    retVal = SelectedKeysAll.Contains(key);
                    break;
                default:
                    throw new InvalidOperationException(State + "is not a supported IQVarState value.");
            }

            return retVal;
        }

        /// <summary>
        /// Support function to evaluate whether any IQEnums with the list of keys passed in have been selected.
        /// </summary>
        public bool ContainsAny(IEnumerable<int> keys)
        {
            bool retVal;

            switch (State)
            {
                case IQVarState.All:
                    //If there are selected elements, then it means preselected values are being used and that the 
                    //SelectedElements are populated with all of the preselected values in the designer.  If there are no
                    //SelectedElements, then preselected values are NOT used, and then we return "true" by default because
                    //any value is considered to match "ALL".
                    retVal = SelectedElements.Count > 0
                                 ? SelectedKeysAll.ContainsAny(keys)
                                 : true;
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
                    retVal = SelectedKeysAll.ContainsAny(keys);
                    break;
                default:
                    throw new InvalidOperationException(State + "is not a supported IQVarState value.");
            }

            return retVal;
        }

        /// <summary>
        /// Support function to evaluate whether any IQEnums with the list of keys passed in have been selected.
        /// </summary>
        public bool ContainsAll(IEnumerable<int> keys)
        {
            bool retVal;

            switch (State)
            {
                case IQVarState.All:
                    //If there are selected elements, then it means preselected values are being used and that the 
                    //SelectedElements are populated with all of the preselected values in the designer.  If there are no
                    //SelectedElements, then preselected values are NOT used, and then we return "false" by default because
                    //all value is very unlikely to be in the list to match "ALL".
                    retVal = SelectedElements.Count > 0
                                 ? SelectedKeysAll.ContainsAll(keys)
                                 : false;
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
                    retVal = SelectedKeysAll.ContainsAll(keys);
                    break;
                default:
                    throw new InvalidOperationException(State + "is not a supported IQVarState value.");
            }

            return retVal;
        }

        /// <summary>
        /// Support function to evaluate whether the values equals the IQ Variable object.
        /// Order of the entries are ignored.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool Equals(IEnumerable<int> keys)
        {
            bool retVal;

            switch (State)
            {
                case IQVarState.All:
                    //If there are selected elements, then it means preselected values are being used and that the 
                    //SelectedElements are populated with all of the preselected values in the designer.  If there are no
                    //SelectedElements, then preselected values are NOT used, and then we return "false" by default because
                    //all values is very unlikely to be contained in the match "ALL" case.
                    retVal = SelectedElements.Count > 0
                                 ? SelectedKeysAll.Equals(keys)
                                 : false;
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
                    retVal = SelectedKeysAll.Equals(keys);
                    break;
                default:
                    throw new InvalidOperationException(State + "is not a supported IQVarState value.");
            }

            return retVal;
        }

        /// <summary>
        /// Support function to evaluate whether the values is a subset of the IQ Variable object.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool IsSubSetOf(IEnumerable<int> keys)
        {
            bool retVal;

            switch (State)
            {
                case IQVarState.All:
                    //If there are selected elements, then it means preselected values are being used and that the 
                    //SelectedElements are populated with all of the preselected values in the designer.  If there are no
                    //SelectedElements, then preselected values are NOT used, and then we return "false" by default because
                    //all values is very unlikely to be contained in the match "ALL" case.
                    retVal = SelectedElements.Count > 0
                                 ? SelectedKeysAll.IsSubsetOf(keys)
                                 : false;
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
                    retVal = SelectedKeysAll.IsSubsetOf(keys);
                    break;
                default:
                    throw new InvalidOperationException(State + "is not a supported IQVarState value.");
            }

            return retVal;
        }
    }
}
