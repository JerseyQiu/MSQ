using System;
using System.Collections.Generic;
using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration.Arguments;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Types.Other
{
    /// <summary>
	/// Defines a script which must be used for scripts that implement eScribe merge fields
    /// </summary>
    public sealed class EscribeScriptType : ScriptTypeBase
    {
		/// <summary> The unique identifier for this script type </summary>
        public override Guid Guid
        {
            get { return ScriptTypeGuids.EScribeMergeField; }
        }

		/// <summary> The display name of this script type </summary>
        public override string Name
        {
            get { return Strings.EscribeMergeFieldScript_Name; }
        }

		/// <summary> The display description of this script type </summary>
        public override string Description
        {
			get { return Strings.EscribeMergeFieldScript_Description; }
        }

		/// <summary> The functional area of MOSAIQ which this script type applies to </summary>
        public override string Domain
        {
            get { return Strings.SubCategory_Escribe; }
        }

        /// <summary> Indicates whether preferences can be created in the IQ Script Editor for this script type. </summary>
        public override bool SupportsDesignTimePreferences
        {
            get { return false; }
        }

        /// <summary> Defines the input and output arguments required by this script type </summary>
        public override IEnumerable<IQArgument> GetScriptArguments()
        {
            yield return new IQInArgument<int>(ScriptTypeConstants.ArgPatId);
            yield return new IQOutArgument<string>(ScriptTypeConstants.ArgHtml);
            yield return new IQOutArgument<string>(ScriptTypeConstants.ArgPlainText);
            //yield return new IQOutArgument<string>(ScriptTypeConstants.ArgObsReqs); only required for FlowsheetMergeFieldActivity
        }
    }
}