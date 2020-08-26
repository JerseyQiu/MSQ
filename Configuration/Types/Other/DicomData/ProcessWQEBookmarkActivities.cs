using System;
using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Types.DicomData
{
    /// <summary />
    public class ProcessWQEBookmarkActivities : DicomObjectProcessingScriptType
    {
        #region Overrides of ScriptTypeBase

        /// <summary> The unique identifier for this script type. </summary>
        public override Guid Guid
        {
            get { return ScriptTypeGuids.ProcessWQEBookmarkActivities; }
        }

        /// <summary> The display name of this script type </summary>
        public override string Name
        {
            get { return "ProcessWQEBookmarkActivities"; }
        }

        /// <summary> The display description of this script type </summary>
        public override string Description
        {
            get { return "ProcessWQEBookmarkActivities"; }
        }

        /// <summary> The functional area of MOSAIQ which this script type applies to </summary>
        public override string Domain
        {
            get { return "DICOM"; }
        }

        /// <summary> Indicates whether preferences can be created in the IQ Script Editor for this script type. </summary>
        public override bool SupportsDesignTimePreferences
        {
            get { return false; }
        }

        #endregion

    }
}
