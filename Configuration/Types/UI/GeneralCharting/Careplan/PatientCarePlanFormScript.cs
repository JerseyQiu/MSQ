using System;
using System.Collections.Generic;
using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Core.Definitions.IQForms;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;
using Impac.Mosaiq.IQ.Core.Framework.IQForms;

namespace Impac.Mosaiq.IQ.Configuration.Types.UI.GeneralCharting.Careplan
{
    /// <summary>
    /// Configuration class for the patient care plan form IQ Script Type.
    /// </summary>
    public sealed class PatientCarePlanFormScript : UIScriptType
    {
        #region Static Members
        static PatientCarePlanFormScript()
        {
            Controls = new List<IIQFormControl>
                           {
							   PatCPlanControl,
                               CareplanControl, 
                               DiagnosisControl, 
                               IntentControl,
                               IsAppending,
                               IsRoCarePlan
                           };
        }

        /// <summary> Static Collection of controls for this class </summary>
        public static List<IIQFormControl> Controls { get; private set; }

		///<summary>Control object representing the selected patient care plan.</summary>
		public static IIQFormControl PatCPlanControl = new IQFormControl<int>
		{
			DisplayName = "PatCPlan",
			ControlName = "Pcp_Group_Id",
			ArgumentName = "_pcpGroupId"
		};

        ///<summary>Control object representing the selected care plan.</summary>
        public static IIQFormControl CareplanControl = new IQFormControl<int>
                                                           {
                                                               DisplayName = "Care Plan",
                                                               ControlName = "Pcp_Cpl_Set_Id",
                                                               ArgumentName = "_cplSetId"
                                                           };
        ///<summary>Control object representing the selected diagnosis.</summary>
        public static IIQFormControl DiagnosisControl = new IQFormControl<int>
                                                            {
                                                                DisplayName = "Dx",
                                                                ControlName = "Pcp_Med_Id",
                                                                ArgumentName = "_medId"
                                                            };
        ///<summary>Control object representing the selected intent.</summary>
        public static IIQFormControl IntentControl = new IQFormControl<string>
                                                         {
                                                             DisplayName = "Intent",
                                                             ControlName = "Pcp_Intent",
                                                             ArgumentName = "_intent"
                                                         };
        ///<summary>Control object representing whether the form is in "Append" mode.</summary>
        public static IIQFormControl IsAppending = new IQFormControl<bool>
                                                       {
                                                           DisplayName = "Is Appending",
                                                           ControlName = "Is_Appending",
                                                           ArgumentName = "_isAppending",
                                                           IsHiddenProperty = true
                                                       };
        ///<summary>Control object representing whether the form handling an RO care plan.</summary>
        public static IIQFormControl IsRoCarePlan = new IQFormControl<bool>
                                                        {
                                                            DisplayName = "Is RO Care Plan",
                                                            ControlName = "Is_RO_CarePlan",
                                                            ArgumentName = "_isRoCarePlan",
                                                            IsHiddenProperty = true
                                                        };
        #endregion

        #region IIQFormScriptTypeBase Implementation
        /// <summary> The unique identifier for this script type. </summary>
        public override Guid Guid
        {
            get { return ScriptTypeGuids.PatientCarePlanForm; }
        }

        /// <summary> The display name of this script type </summary>
        public override string Name
        {
            get { return Strings.PatientCarePlanFormScript_Name; }
        }

        /// <summary> The display description of this script type </summary>
        public override string Description
        {
            get { return Strings.PatientCarePlanForm_Description; }
        }

        /// <summary> The functional area of MOSAIQ which this script type applies to </summary>
        public override string Domain
        {
            get { return Strings.Category_GeneralCharting; }
        }

        /// <summary> The functional sub area of MOSAIQ which this script type applies to </summary>
        public override string Category
        {
            get { return Strings.SubCategory_CarePlans; }
        }

        /// <summary> Indicates whether preferences can be created in the IQ Script Editor for this script type. </summary>
        public override bool SupportsDesignTimePreferences
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IIQFormControl> GetFormControls()
        {
            return Controls;
        }
        #endregion
    }
}