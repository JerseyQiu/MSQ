using System;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration;

namespace Impac.Mosaiq.IQ.Extensions.Interface
{
    /// <summary>
    /// This interface defines information about a script that is currently executing
    /// </summary>
    public interface IRuntimeMetadata
    {
        /// <summary> The primary key of the IQ Script being executed. </summary>
        Guid IqsGuid { get; }
        
        /// <summary> The primary key of the IQ Script Revision being executed. </summary>
        Guid IqsrGuid { get; }

        /// <summary>
        /// The primary key of the IQ Script Preference being used (null if no preference is used).
        /// </summary>
        Guid? IqpGuid { get; }

        /// <summary>
        /// The primary key of the IQ Script Assign used (null if no assignment is used).
        /// </summary>
        Guid? IqaGuid { get; }

        /// <summary> The type of the IQ Script being executed. </summary>
        IScriptType ScriptType { get; }

        /// <summary> The script feature invoked. </summary>
        IScriptFeature ScriptFeature { get; }
    }
}
