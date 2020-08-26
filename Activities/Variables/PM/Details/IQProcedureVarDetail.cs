using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM.Details
{
    /// <summary>
    /// IQProcedureVarDetail implementation for procedures and supplies objects.
    /// </summary>
    public class IQProcedureVarDetail : IQVariableDetail<Cpt, IQVarElement<int>>
    {
        #region IIQVariableDetail Implementation
        /// <summary>
        /// Displaying the observation order short string as the selected item.
        /// </summary>
        public override string DesignTimeDisplayString
        {
            get { return Data.Short_Desc.Trim(); }
        }

        /// <summary>
        /// The primary key of the CPT table
        /// </summary>
        public override string DesignTimeKeyString
        {
            get { return Data.PRS_ID.ToString(); }
        }

        /// <summary>
        /// Returns the category of the observation order.
        /// </summary>
        public override string Category
        {
            get { return Data.CGroup.Trim(); }
        }
        #endregion
    }
}
