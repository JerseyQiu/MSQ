using System.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.Logging
{
    ///<summary>This activity writes a message to the user log.</summary>
    [Tools_ActivityGroup]
    [Logging_Category]
    [WriteToUserLog_DisplayName]
    public class WriteToUserLog : MosaiqCodeActivity
    {
        ///<summary>The log message to be written to the user log.</summary>
        [RequiredArgument]
        [InputParameterCategory]
        [WriteToUserLog_LogMessage_DisplayName]
        [WriteToUserLog_LogMessage_Description]
        public InArgument<string> LogMessage { get; set; }

        /// <summary> Write the log message. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            string logMessage = LogMessage.Get(context);
            WriteToUserLog(logMessage);
        }
    }
}
