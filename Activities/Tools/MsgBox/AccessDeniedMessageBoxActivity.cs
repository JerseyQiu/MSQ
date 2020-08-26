using System;
using System.Activities;
using Impac.Mosaiq.Core.Xlate;
using Impac.Mosaiq.Core.Xlate.GenericMessage;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.MsgBox
{
    /// <summary> Activity used to display an access denied message </summary>
    [AccessDeniedMessageBoxActivity_DisplayName]
    [Messaging_Category]
    [Tools_ActivityGroup]
    public class AccessDeniedMessageBoxActivity : MosaiqUICodeActivity
    {
        #region Properties
        /// <summary>The action being denied.</summary>
        [InputParameterCategory]
        [AccessDeniedMessageBoxActivity_Action_Description]
        [AccessDeniedMessageBoxActivity_Action_DisplayName]
        public InArgument<AccessAction> Action { get; set; }

        /// <summary>The caption of the window to be displayed</summary>
        [InputParameterCategory]
        [Caption_Description]
        [Caption_DisplayName]
        public InArgument<string> Caption { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Activity heavy lifting.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            //Get Argument Values
            AccessAction action = (Action.Expression != null) ? Action.Get(context) : AccessAction.AccessNotAllowed;
            string caption = Caption.Get(context) ?? String.Empty;

            //Heavy Lifting
            if(String.IsNullOrWhiteSpace(caption))
                XlateMessageBox.AccessDenied(action);
            else
                XlateMessageBox.AccessDenied(action, caption);
        }
        #endregion
    }
}