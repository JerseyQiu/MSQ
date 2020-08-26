using System;
using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Configuration.Types.UI.GeneralCharting.Careplan;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Features.UI.GeneralCharting.Careplans
{
    /// <summary> The library item for the Patient Care Plan Form Feature </summary>
    public class PatientCarePlanFormFeature : UIScriptFeature<PatientCarePlanFormScript>
    {
        #region Overriden Properties
        /// <summary> The application event guid </summary>
        public override Guid Guid
        {
            get { return ScriptFeatureGuids.PatientCarePlanForm; }
        }

        /// <summary> The application event name </summary>
        public override string Name
        {
            get { return Strings.PatientCarePlanFormFeature_Name; }
        }

        /// <summary> The application event description </summary>
        public override string Description
        {
            get { return Strings.PatientCarePlanFormFeature_Description; }
        }

        /// <summary> The functional area of MOSAIQ which this script type applies to </summary>
        public override string Domain
        {
            get { return Strings.Category_GeneralCharting; }
        }

        /// <summary> The functional sub area of MOSAIQ which this script type applies to </summary>
        public override string FunctionalArea
        {
            get { return Strings.SubCategory_CarePlans; }
        }
        #endregion
    }
}