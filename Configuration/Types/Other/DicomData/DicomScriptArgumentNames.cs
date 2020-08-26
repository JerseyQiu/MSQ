using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impac.Mosaiq.IQ.Configuration.Types.Other.DicomData
{
    ///<summary>
    /// Defines the Input and Output Argument names which are associated with DICOM script.
    /// Each DICOM script based on WorkQueueElement based processing will process an Input
    /// Provided via InputArgument defined as 'UnitId'. This argument identifies the database 
    /// row of the table the script is operating on. The database table is specific to the
    /// context and is interpreted by the script.
    /// Thus a RTDOse/RTPLan script, the UnitId will be DcmInstance_ID and thus the associated
    /// script processing code will assume the UnitId related to the DCMInstance Table.
    ///</summary>
    public static class DicomScriptArgumentNames
    {
        /// <summary> Argument name for a cancel operation </summary>
        public static readonly string InArgumentUnitId = "UnitId";

        /// <summary> Argument name for an order set id </summary>
        public static readonly string OutArgumentWorkflowStatus = "WorkFlowStatus";
    }
}
