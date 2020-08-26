using System;

using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Types.Other.DicomData
{
    class IViewGTAutoIsocenterScript : DicomScriptTypeBase
    {
        #region Overrides of ScriptTypeBase

        /// <summary> The unique identifier for this script type. </summary>
        public override Guid Guid
        {
            get { return ScriptTypeGuids.IViewGTAutoIsocenterScript; }
        }

        /// <summary> The display name of this script type </summary>
        public override string Name
        {
            get { return Strings.IViewGTAutoIsocenterScript_Name; }
        }

        /// <summary> The display description of this script type </summary>
        public override string Description
        {
            get { return Strings.IViewGTAutoIsocenterScript_Description; }
        }

        /// <summary> The functional area of MOSAIQ which this script type applies to </summary>
        public override string Domain
        {
            get { return Strings.Category_DICOM; }
        }

        /// <summary> Indicates whether preferences can be created in the IQ Script Editor for this script type. </summary>
        public override bool SupportsDesignTimePreferences
        {
            get { return false; }
        }

        #endregion
    }
}
