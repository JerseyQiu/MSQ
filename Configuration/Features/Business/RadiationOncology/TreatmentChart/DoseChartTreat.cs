using System;
using System.Collections.Generic;
using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Features.Business.RadiationOncology.TreatmentChart
{
    /// <summary> Defines the script feature for radiotherapy treatment </summary>
    public sealed class DoseChartTreat : BusinessScriptFeature
    {
        #region Overriden Properties

        /// <summary> The unique identifier for this feature. </summary>
        public override Guid Guid
        {
            get { return ScriptFeatureGuids.ROTreat; }
        }

        /// <summary> The display name of this feature </summary>
        public override string Name
        {
            get { return Strings.RODoseChartTreat; } 
        }

        /// <summary> The display description of this feature </summary>
        public override string Description
        {
            get { return Strings.RODoseChartTreat_Description; }
        }

        /// <summary> The functional area of MOSAIQ which this feature applies to </summary>
        public override string Domain
        {
            get { return Strings.Category_RadiationOncology; } 
        }

        /// <summary> The functional sub are of MOSAIQ which this feature applies to </summary>
        public override string FunctionalArea
        {
            get { return Strings.SubCategory_TreatmentChart; }
        }

        /// <summary> The script assignment mose which this feature uses </summary>
        public override ScriptAssignMode ScriptAssignMode
        {
            get { return ScriptAssignMode.Sequential; }
        }

        /// <summary>
        /// Returns the GUID's of the script types which are supported by this Feature.
        /// </summary>
        /// <returns></returns>
        protected override void  GetSupportedScriptTypeGuids(IList<Guid> scriptTypeGuids)
        {
            base.GetSupportedScriptTypeGuids(scriptTypeGuids);
            scriptTypeGuids.Add(ScriptTypeGuids.PatientScript);
            scriptTypeGuids.Add(ScriptTypeGuids.PatientScriptCancelable);
        }

        /// <summary>
        /// If true, forces the IQ tracking mechanism to log when the script started and completed, and script
        /// input/output values.
        /// </summary>
        public override bool DoMinimalLogging { get { return true; } }
        #endregion
    }
}