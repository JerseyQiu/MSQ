using System;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM.Details
{
    /// <summary>
    /// IQVarDetail implementation for staff objects.
    /// </summary>
    public class IQPromptVarDetail : IQVariableDetail<Prompt, IQVarElement<int>>
    {
        #region IIQVariableDetail Implementation
        /// <summary> Human readible display string for the value stored.</summary>
        public override string DesignTimeDisplayString
        {
			get { return !String.IsNullOrEmpty(Data.Text) ? Data.Text.Trim() : String.Empty; }
        }

        /// <summary> Display string for the key value stored.</summary>
        public override string DesignTimeKeyString
        {
            get { return Data.Pro_ID.ToString(); }
        }
        #endregion
    }
}
