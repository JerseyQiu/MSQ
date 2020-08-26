using System;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Details
{
    /// <summary>
    /// IQVarDetail implementation for a string.
    /// </summary>
    public class IQBooleanVarDetail : IQVariableDetailSimple<Boolean>
    {
        /// <summary> Display Value </summary>
        public override string DesignTimeDisplayString
        {
            get { return XlateValue; }
        }

        /// <summary> The key value to display </summary>
        public override string DesignTimeKeyString
        {
            get { return XlateValue; }
        }

        //Gets the string representation of the value.
        private string XlateValue
        {
            get { return Element.Value ? Strings.True : Strings.False; }
        }
    }
}
