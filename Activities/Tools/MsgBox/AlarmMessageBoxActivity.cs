using System;
using System.Activities;
using Impac.Mosaiq.Core.Xlate;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.MsgBox
{
    /// <summary> Activity used to display an alarm message </summary>
    [AlarmMessageBoxActivity_DisplayName]
    [Messaging_Category]
    [Tools_ActivityGroup]
    public class AlarmMessageBoxActivity : MosaiqUICodeActivity
    {
        #region Properties
        /// <summary>The action being denied.</summary>
        [InputParameterCategory]
        [Message_Description]
        [Message_DisplayName]
        public InArgument<string> Message { get; set; }
        
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
            string message = Message.Get(context) ?? String.Empty;
            string caption = Caption.Get(context) ?? String.Empty;

            //Heavy Lifting
            if(String.IsNullOrWhiteSpace(caption))
                XlateMessageBox.Alarm(message);
            else
                XlateMessageBox.Alarm(message, caption);
        }
        #endregion
    }
}