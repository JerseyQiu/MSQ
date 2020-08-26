using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;
using System;
using System.Collections.Generic;

namespace Impac.Mosaiq.IQ.Configuration.Features.Business.GeneralCharting.CdsCarePlans
{
    /// <summary>
    /// IQ script trigger for when CDS care plan import is canceled by a user.
    /// </summary>
    /// <seealso cref="BusinessScriptFeature" />
    class CdsCarePlanCanceled : BusinessScriptFeature 
    {
        #region Overridden Properties
        /// <summary> The application event guid </summary>
        public override Guid Guid
        {
            get { return ScriptFeatureGuids.CdsCarePlanCanceled; }
        }

        /// <summary> The application event name </summary>
        public override string Name
        {
            get { return Strings.CdsCarePlanCanceled_Name; }
        }

        /// <summary> The application event description </summary>
        public override string Description
        {
            get { return Strings.CdsCarePlanCanceled_Description; }
        }

        /// <summary> The functional area of MOSAIQ which this script type applies to </summary>
        public override string Domain
        {
            get { return Strings.Category_GeneralCharting; }
        }

        /// <summary> The functional sub area of MOSAIQ which this script type applies to </summary>
        public override string FunctionalArea
        {
            get { return Strings.SubCategory_CdsCarePlans; }
        }

        /// <summary> The script assignment mode which this feature uses </summary>
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
            scriptTypeGuids.Add(ScriptTypeGuids.CdsCarePlan);
        }
        #endregion
    }
}
