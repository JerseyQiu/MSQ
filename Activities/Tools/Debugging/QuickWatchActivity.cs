using System.Activities;
using Impac.Mosaiq.Core.Toolbox.DebugTools.QuickWatch;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.Debugging
{
    /// <summary> Opens the MOSAIQ Quick Watch window.</summary>
    [Debug_Category]
    [Tools_ActivityGroup]
    [QuickWatchActivity_DisplayName]
    [QuickWatchActivity_Description]
    public class QuickWatchActivity : MosaiqUICodeActivity
    {
        #region Properties
        /// <summary>The object to be displayed in the quick watch window.</summary>
        [InputParameterCategory]
        [RequiredArgument]
        [QuickWatchActivity_Object_DisplayName]
        [QuickWatchActivity_Object_Description]
        public InArgument<object> Object { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Activity heavy lifting.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            object obj = Object.Get(context);
            var d = new QuickWatchDialog { Obj = obj };
            d.ShowDialog();
        }
        #endregion
    }
}