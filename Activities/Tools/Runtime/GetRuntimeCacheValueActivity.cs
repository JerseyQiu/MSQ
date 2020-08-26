using System.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Runtime.Extensions;

namespace Impac.Mosaiq.IQ.Activities.Tools.Runtime
{
    /// <summary>
    /// Attempts to retreive a value from the runtime cache.
    /// </summary>
    [GetRuntimeCacheValueActivity_DisplayName]
    [Runtime_Category]
    [Tools_ActivityGroup]
    public class GetRuntimeCacheValueActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary> They key of the value to lookup in the runtime value cache </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [Key_DisplayName]
        [GetRuntimeCacheValueActivity_Key_Description]
        public InArgument<string> Key { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary> The value found in the runtime value cache. </summary>
        [OutputParameterCategory]
        [Value_DisplayName]
        [GetRuntimeCacheValueActivity_Value_Description]
        public InArgument<string> Value { get; set; }

        /// <summary> Returns whether a value was found in the runtime value cache. </summary>
        [OutputParameterCategory]
        [GetRuntimeCacheValueActivity_ResultFound_DisplayName]
        [GetRuntimeCacheValueActivity_ResultFound_Description]
        public InArgument<bool> ResultFound { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Attempts to get a value from the runtime cache. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            var ext = context.GetExtension<IRuntimeValueCache>();
            string value;
            bool resultFound = ext.TryGetRuntimeValue(Key.Get(context), out value);

            ResultFound.Set(context, resultFound);
            Value.Set(context, value);
        }
        #endregion
    }
}