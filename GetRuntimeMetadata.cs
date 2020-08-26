using System;
using System.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Runtime.Extensions;

namespace Impac.Mosaiq.IQ.Activities.Tools.Runtime
{
    /// <summary>
    /// Attempts to retreive a value from the runtime cache.
    /// </summary>
    [GetRuntimeMetadata_DisplayName]
    [Runtime_Category]
    [Tools_ActivityGroup]
    public class GetRuntimeMetadata : MosaiqCodeActivity
    {
        #region Properties (Output Parameters)
        /// <summary> The primary key of the IQ Script being executed. </summary>
        [OutputParameterCategory]
        [GetRuntimeMetadata_IqsGuid_DisplayName]
        [GetRuntimeMetadata_IqsGuid_Description]
        public OutArgument<Guid> IqsGuid { get; set; }

        /// <summary> The primary key of the IQ Script Revision being executed. </summary>
        [OutputParameterCategory]
        [GetRuntimeMetadata_IqsrGuid_DisplayName]
        [GetRuntimeMetadata_IqsrGuid_Description]
        public OutArgument<Guid> IqsrGuid { get; set; }

        /// <summary>
        /// The primary key of the IQ Script Preference being used (null if no preference is used).
        /// </summary>
        [OutputParameterCategory]
        [GetRuntimeMetadata_IqpGuid_DisplayName]
        [GetRuntimeMetadata_IqpGuid_Description]
        public OutArgument<Guid?> IqpGuid { get; set; }

        /// <summary>
        /// The primary key of the IQ Script Assign used (null if no assignment is used).
        /// </summary>
        [OutputParameterCategory]
        [GetRuntimeMetadata_IqaGuid_DisplayName]
        [GetRuntimeMetadata_IqaGuid_Description]
        public OutArgument<Guid?> IqaGuid { get; set; }

        /// <summary> The type of the IQ Script being executed. </summary>
        [OutputParameterCategory]
        [GetRuntimeMetadata_ScriptType_DisplayName]
        [GetRuntimeMetadata_ScriptType_Description]
        public OutArgument<IScriptType> ScriptType { get; set; }

        /// <summary> The script feature invoked. </summary>
        [OutputParameterCategory]
        [GetRuntimeMetadata_ScriptFeature_DisplayName]
        [GetRuntimeMetadata_ScriptFeature_Description]
        public OutArgument<IScriptFeature> ScriptFeature { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Attempts to get a value from the runtime cache. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            var ext = context.GetExtension<RuntimeMetadataExt>();
            IqsGuid.Set(context, ext.IqsGuid);
            IqsrGuid.Set(context, ext.IqsrGuid);
            IqpGuid.Set(context, ext.IqpGuid);
            IqaGuid.Set(context, ext.IqaGuid);
            ScriptType.Set(context, ext.ScriptType);
            ScriptFeature.Set(context, ext.ScriptFeature);
        }
        #endregion
    }
}