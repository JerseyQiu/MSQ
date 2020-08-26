using System;
using System.Collections.Generic;
using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration.Arguments;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Types.Business.GeneralCharting
{
	/// <summary> Defines a script type which can be invoked from a document trigger</summary>
	public sealed class DocumentScript : BusinessScriptType
	{
		/// <summary> The unique identifier for this script type. </summary>
		public override Guid Guid
		{
			get { return ScriptTypeGuids.DocumentScript; }
		}

		/// <summary> The display name of this script type </summary>
		public override string Name
		{
			get { return Strings.DocumentScript_Name; }
		}

		/// <summary> The display description of this script type </summary>
		public override string Description
		{
			get { return Strings.DocumentScript_Description; }
		}

		/// <summary> The functional area of MOSAIQ which this script type applies to </summary>
		public override string Domain
		{
			get { return Strings.Category_GeneralCharting; }
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
			yield return new IQInArgument<int>(ScriptTypeConstants.ArgObjId);
		}
	}
}
