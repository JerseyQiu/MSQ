using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Impac.Mosaiq.Core.Security.DBConnectionLib;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Export;
using Impac.Mosaiq.IQ.Core.Framework.Runtime;

namespace IQBuildVerify
{
    class Program
    {
        /// <summary> Exit code enumeration </summary>
        private enum ExitCode
        {
            Success = 0,
            Failure = 1
        }

        [STAThread]
        static int Main(string[] args)
        {
			new ObSS();

            string logFile = args.Length > 0 ? args[0] : null;

            //Disable IQ Engine Activity Validation.  The "GetEnglishStrings()" call below invokes the mechanism used 
            //to validate IQ Scripts in MOSAIQ.  We can't disable that mechanism from firing, but we can disable the 
            //validtion calls what we make within that mechanism, which is what we do below.
            IQSettings.ActivityValidationEnabled = false;

            //Get the list of IQ Export files.
            var iqExportFiles = new List<string>();
            foreach (var iqArchivePath in IQEngine.IQArchivePaths.Where(Directory.Exists))
                iqExportFiles.AddRange(Directory.GetFiles(iqArchivePath, "*.iq", SearchOption.TopDirectoryOnly));

            //Loop through the files and validate them.
            var errors = new StringBuilder();
            foreach(string file in iqExportFiles)
            {
                IQExportFile iqFile = ImportExportUtil.GetFileContents(file);
                String fileErrors = iqFile.ValidateContents();
                if (!String.IsNullOrWhiteSpace(fileErrors))
                {
                    errors.AppendLine("=================================================================");
                    errors.AppendLine(file);
                    errors.AppendLine("=================================================================");
                    errors.AppendLine(fileErrors);
                    errors.AppendLine();
                }
            }

            //Convert the string builder to a string.
            string errorString = errors.ToString();

            //if no errors are found, bail out w/ Success Code.
            if (String.IsNullOrWhiteSpace(errorString))
            {
                const string msgSuccess = @"IQ Export Files Verified Succesfully.";
                Console.WriteLine(msgSuccess);
                return (int) ExitCode.Success;
            }

            //Otherwise, write the errors to console in red letters and return failure.
            Console.ForegroundColor = ConsoleColor.Red;
            string msgFail = Environment.NewLine + errorString.TrimEnd();
            Console.WriteLine(msgFail);

            if (!String.IsNullOrWhiteSpace(logFile))
                WriteToOutputFile(args[0], msgFail);
            Console.ResetColor();

            return (int) ExitCode.Failure;
        }

        private static void WriteToOutputFile(string pPath, string pContents)
        {
            File.WriteAllText(pPath, pContents);
        }
    }
}
