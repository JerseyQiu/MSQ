using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM.Details
{
    /// <summary>
    /// IQVarDetail implementation for staff objects.
    /// </summary>
    public class IQStaffVarDetail : IQVariableDetail<Staff, IQVarElement<int>>
    {
        #region IIQVariableDetail Implementation
        /// <summary> </summary>
        public override string DesignTimeDisplayString
        {
            get { return Data.FullName; }
        }
        
        /// <summary> </summary>
        public override string DesignTimeKeyString
        {
            get { return Data.Staff_ID.ToString(); }
        }
        #endregion
    }
}
