using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Details
{
    /// <summary>
    /// IQVarDetail implementation for staff objects.
    /// </summary>
    public class IQCareplanVarDetail : IQVariableDetail<CPlan, IQVarElement<int>>
    {
        #region IIQVariableDetail Implementation
        /// <summary>
        /// The human readable display string for a design time value (ie. something looked up from the database from
        /// a stored primary key or a value entered directly from the user.
        /// </summary>
        public override string DesignTimeDisplayString
        {
            get { return Data.CPlan_Name.Trim(); }
        }

        /// <summary>
        /// The a string displaying the key for a design time value.  This could be the primary key of a selected value
        /// something typed in by the user, such as a number or string.
        /// </summary>
        public override string DesignTimeKeyString
        {
            get { return Data.CPL_ID.ToString(); }
        }

        /// <summary>
        /// returns the category of the care plan.
        /// </summary>
        public override string Category
        {
            get { return Data.Category.Trim(); }
        }
        #endregion
    }
}
