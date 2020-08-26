using System.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Runtime.Extensions;

namespace Impac.Mosaiq.IQ.Activities.Tools.Runtime
{
    /// <summary>
    /// Sets a value in the runtime value cache.
    /// </summary>
    [SetRuntimeCacheValueActivity_DisplayName]
    [Runtime_Category]
    [Tools_ActivityGroup]
    public class SetRuntimeCacheValueActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary> They key of the value to be set in the runtime value cache. </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [Key_DisplayName]
        [SetRuntimeCacheValueActivity_Key_Description]
        public InArgument<string> Key { get; set; }

        /// <summary> The value to be set in the runtime value cache. </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [Value_DisplayName]
        [SetRuntimeCacheValueActivity_Value_Description]
        public InArgument<string> Value { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Sets the runtime value in the cache. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            var ext = context.GetExtension<IRuntimeValueCache>();
            ext.SetRuntimeValue(Key.Get(context), Value.Get(context));
        }
        #endregion
    }
}