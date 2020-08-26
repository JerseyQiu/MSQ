using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Core.Globals;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessSpatialRegistration
{
    /// <summary> </summary>
    [DisplaySpatialRegistrationDataNavigator_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class DisplaySpatialRegistrationDataNavigator : MosaiqCodeActivity
    {
        /// <summary> Internal Patient ID </summary>
        [RequiredArgument]
        [InputParameterCategory]
        public InArgument<int> PatientID { get; set; }

        /// <summary> </summary>
        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void DoWork(CodeActivityContext context)
        {
            int pat_ID = PatientID.Get(context);
            ProcessStartInfo startInfo;
            Process myProc;
            string cmd = "";
            cmd = "Impac.Mosaiq.DICOM.Tools.SROBrowser.exe";
            startInfo = new ProcessStartInfo(cmd);
            startInfo.Arguments = pat_ID.ToString();
            myProc = Process.Start(startInfo);
        }
    }
}
