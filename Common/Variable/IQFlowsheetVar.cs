using System;
using System.Collections.Generic;
using Impac.Mosaiq.Core.Toolbox.LINQ;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;

namespace Impac.Mosaiq.IQ.Common.Variable
{
	/// <summary>
	/// A class that represents a selection of flowsheet items, all belonging to the same tab view
	/// </summary>
	[Serializable]
	public class FlowsheetSelection
    {
        #region Public Properties
        // The Tbl value of the flowsheet view (Labs - 20010, Vitals - 20020, Assessments - 20030)
		//public int Tbl { get; set; }
		/// <summary>
		/// The OBD_GUID of the Tab name ObsDef record that is used as filter for the tab views
		/// </summary>
		public Guid TabGuid { get; set; }
		/// <summary>
		/// The OBD_GUID of the selected tab view
		/// </summary>
		public Guid ViewGuid { get; set; }
		/// <summary>
		/// A list of item Guids ordered in the same order as they are in the tab view
		/// </summary>
		public List<Guid> ItemGuids { get; set; }

		/// <summary>
		/// The GUID of the 'Other Labs' function item
		/// </summary>
		public const string OtherLabsGuidString = "C9CFA70B-E950-4AA7-BF3E-3D25362A43DB";
        #endregion

        #region Overrides
        /// <summary> </summary>
        public override bool Equals(object obj)
        {
            var typedObj = obj as FlowsheetSelection;

            if (typedObj == null)
                return false;

            if (TabGuid != typedObj.TabGuid)
                return false;

            if (ViewGuid != typedObj.ViewGuid)
                return false;

            //If both are null, return true.
            if (ItemGuids == null && typedObj.ItemGuids == null)
                return true;

            //If only one is null, return false.
            if (ItemGuids == null || typedObj.ItemGuids == null)
                return false;

            //If they don't have the same # of items, return false.
            if (ItemGuids.Count != typedObj.ItemGuids.Count)
                return false;

            //See if the first list contains all of the items in the second list.  If not, return false.
            if (!ItemGuids.ContainsAll(typedObj.ItemGuids))
                return false;

            return true;
        }

        /// <summary> </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode()
                ^ TabGuid.GetHashCode()
                ^ ViewGuid.GetHashCode();

            if (ItemGuids != null)
                hashCode = hashCode ^ ItemGuids.GetHashCode();

            return hashCode;
        }
        #endregion
    }

	/// <summary> IQ Variable which holds a selection of flowsheet items </summary>
	[Serializable]
    public class IQFlowsheetVar : IQVariableReference<FlowsheetSelection>
	{
	}
}
