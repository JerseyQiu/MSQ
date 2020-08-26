using System;
using System.Activities;
using Impac.Mosaiq.Core.Xlate;
using Impac.Mosaiq.Core.Xlate.GenericMessage;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.MsgBox
{
    /// <summary> Activity used to display a Yes/Yes All/No/No All/Cancel message </summary>
    [YesAllNoAllCancelMessageBoxActivity_DisplayName]
    [Messaging_Category]
    [Tools_ActivityGroup]
    public class YesAllNoAllCancelMessageBoxActivity : MosaiqUICodeActivity
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

        /// <summary> The default button selected when the dialog opens </summary>
        [InputParameterCategory]
        [DefaultButtons_Description]
        [DefaultButtons_DisplayName]
        public InArgument<XlateMessageBoxDefaultButtons> DefaultButtons { get; set; }

        /// <summary>
        /// The result of the users selection in the caution box.
        /// </summary>
        [OutputParameterCategory]
        [DialogResult_Description]
        [DialogResult_DisplayName]
        public OutArgument<YesAllNoAllDialogResult> DialogResult { get; set; }
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
            XlateMessageBoxDefaultButtons defaultButtons = DefaultButtons.Get(context);

            //Heavy Lifting
            YesAllNoAllDialogResult result = XlateMessageBox.YesAllNoAllCancel(message, caption, defaultButtons);

            DialogResult.Set(context, result);
        }
        #endregion
    }
}