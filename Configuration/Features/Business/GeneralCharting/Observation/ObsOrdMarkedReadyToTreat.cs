using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Features.Business.GeneralCharting.Observation
{
    class ObsOrdMarkedReadyToTreat : BusinessScriptFeature
    {
        #region Overriden Properties

        /// <summary> The unique identifier for this feature. </summary>
        public override Guid Guid
        {
            get { return ScriptFeatureGuids.ObsOrdMarkedReadyToTreat; }
        }

        /// <summary> The display name of this feature </summary>
        public override string Name
        {
            get { return Strings.ObsOrdMarkedReadyToTreat_Name; }
        }

        /// <summary> The display description of this feature </summary>
        public override string Description
        {
            get { return Strings.ObsOrdMarkedReadyToTreat_Description; }
        }

        /// <summary> The functional area of MOSAIQ which this feature applies to </summary>
        public override string Domain
        {
            get { return Strings.Category_GeneralCharting; }
        }

        /// <summary> The functional sub are of MOSAIQ which this feature applies to </summary>
        public override string FunctionalArea
        {
            get { return Strings.SubCategory_ObservationOrders; }
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
        protected override void GetSupportedScriptTypeGuids(IList<Guid> scriptTypeGuids)
        {
            base.GetSupportedScriptTypeGuids(scriptTypeGuids);
            scriptTypeGuids.Add(ScriptTypeGuids.OrderScript);
        }
        #endregion
    }
}
