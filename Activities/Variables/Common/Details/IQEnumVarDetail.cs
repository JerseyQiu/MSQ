using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Details
{
    /// <summary>
    /// Detail Class used with the IQVarEnumActivity
    /// </summary>
    public class IQEnumVarDetail : IQVariableDetailSimple<IQEnum>
    {
        /// <summary> The display value to show. </summary>
        public override string DesignTimeDisplayString
        {
            get { return Element.Value.Value; }
        }

        /// <summary> The key value to display </summary>
        public override string DesignTimeKeyString
        {
            get { return Element.Value.Key.ToString(); }
        }
    }
}
