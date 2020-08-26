using System.Collections.Generic;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration.Arguments;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Types.Other.DicomData
{
    abstract class DicomScriptTypeBase : ScriptTypeBase
    {
        /// <summary> Defines the input and output arguments required by this script type </summary>
        public override IEnumerable<IQArgument> GetScriptArguments()
        {
            yield return new IQInArgument<string>(DicomScriptArgumentNames.InArgumentUnitId);
            yield return new IQOutArgument<string>(DicomScriptArgumentNames.OutArgumentWorkflowStatus);
        }
    }
}
