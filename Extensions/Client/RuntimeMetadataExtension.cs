using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration;
using Impac.Mosaiq.IQ.Extensions.Interface;

namespace Impac.Mosaiq.IQ.Extensions.Client
{
    ///<summary>Implements IRuntimeMetadata interface.</summary>
    public class RuntimeMetadataExtension : IRuntimeMetadata
    {
        /// <summary> The primary key of the IQ Script being executed. </summary>
        public Guid IqsGuid { get; set; }

        /// <summary> The primary key of the IQ Script Revision being executed. </summary>
        public Guid IqsrGuid { get; set; }

        /// <summary>
        /// The primary key of the IQ Script Preference being used (null if no preference is used).
        /// </summary>
        public Guid? IqpGuid { get; set; }

        /// <summary>
        /// The primary key of the IQ Script Assign used (null if no assignment is used).
        /// </summary>
        public Guid? IqaGuid { get; set; }

        /// <summary> The type of the IQ Script being executed. </summary>
        public IScriptType ScriptType { get; set; }

        /// <summary> The script feature invoked. </summary>
        public IScriptFeature ScriptFeature { get; set; }
    }
}
