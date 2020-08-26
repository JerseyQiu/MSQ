using System;
using System.Collections.Generic;
using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration.Arguments;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Types.Common
{
    /// <summary>
    /// Defines a script type which can be launched from any decision support trigger that operates in the context
    /// of a patient.
    /// </summary>
    public sealed class PatientScript : BusinessScriptType
    {
        #region Overrides of ScriptConfigBase

        /// <summary> The unique identifier for this script type. </summary>
        public override Guid Guid
        {
            get { return ScriptTypeGuids.PatientScript; }
        }

        /// <summary> The display name of this script type </summary>
        public override string Name
        {
            get { return Strings.PatientScript_Name; }
        }

        /// <summary> The display description of this script type </summary>
        public override string Description
        {
            get { return Strings.PatientScript_Description; }
        }

        /// <summary> The technical usage usage of this script type</summary>
        public override string Domain
        {
            get { return Strings.Category_Common; ; }
        }

        /// <summary> Indicates whether preferences can be created in the IQ Script Editor for this script type. </summary>
        public override bool SupportsDesignTimePreferences
        {
            get { return true; }
        }

        /// <summary> 
        /// Returns a list of arguments that must be present on the script. 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IQArgument> GetScriptArguments()
        {
            yield return new IQInArgument<int>(ScriptTypeConstants.ArgPatId);
        }
        #endregion
    }
}
