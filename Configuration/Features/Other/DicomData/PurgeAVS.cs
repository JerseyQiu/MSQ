using System;
using System.Collections.Generic;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;
using Impac.Mosaiq.IQ.Common.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Features.DicomData
{
    class PurgeAVS : ScriptFeatureBase
    {
        #region Overrides of ScriptFeatureBase

        /// <summary> The unique identifier for this feature. </summary>
        public override Guid Guid
        {
            get { return ScriptFeatureGuids.PurgeAVS;  }
        }

        /// <summary> The display name of this feature </summary>
        public override string Name
        {
            get { return Strings.PurgeAVS_Name; }
        }

        /// <summary> The display description of this feature </summary>
        public override string Description
        {
            get { return Strings.PurgeAVS_Description; }
        }

        /// <summary> The functional area of MOSAIQ which this feature applies to </summary>
        public override string Domain
        {
            get { return Strings.Category_DICOM; }
        }

        /// <summary> The functional sub are of MOSAIQ which this feature applies to </summary>
        public override string FunctionalArea
        {
            get { return Strings.SubCategory_DicomSeries; }
        }

        /// <summary> The script assignment mode which this feature uses </summary>
        public override ScriptAssignMode ScriptAssignMode
        {
            get { return ScriptAssignMode.Sequential; }
        }

        public override bool IsDepartmentSpecific
        {
            get { return false; }
        }

        /// <summary>
        /// Returns the GUID's of the script types which are supported by this Feature.
        /// </summary>
        /// <returns></returns>
        protected override void GetSupportedScriptTypeGuids(IList<Guid> scriptTypeGuids)
        {
            scriptTypeGuids.Add(ScriptTypeGuids.PurgeAVSScript);
        }

        #endregion
    }
}
