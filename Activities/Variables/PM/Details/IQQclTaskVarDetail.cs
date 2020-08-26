using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM.Details
{
    public class IQQclTaskVarDetail : IQVariableDetail<QCLTask, IQVarElement<int>>
    {
        #region IIQVariableDetail Implementation
        /// <summary> Human readible display string for the value stored.</summary>
        public override string DesignTimeDisplayString
        {
            get { return Data.Description.Trim(); }
        }

        /// <summary> Display string for the key value stored.</summary>
        public override string DesignTimeKeyString
        {
            get { return Data.TSK_ID.ToString(); }
        }
        #endregion
    }
}
