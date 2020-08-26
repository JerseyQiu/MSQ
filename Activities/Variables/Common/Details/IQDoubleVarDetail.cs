using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Details
{
    /// <summary>
    /// IQVarDetail implementation for a string.
    /// </summary>
    public class IQDoubleVarDetail : IQVariableDetailSimple<double>
    {
        /// <summary> Display Value </summary>
        public override string DesignTimeDisplayString
        {
            get { return Element.Value.ToString(); }
        }

        /// <summary> The key value to display </summary>
        public override string DesignTimeKeyString
        {
            get { return Element.Value.ToString(); }
        }
    }
}
