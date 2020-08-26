using System.Runtime.InteropServices;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Configuration.Features.UI.GeneralCharting.Careplans;
using Impac.Mosaiq.IQ.Configuration.Types.UI.GeneralCharting.Careplan;
using Impac.Mosaiq.IQ.Core.Definitions.IQForms;
using Impac.Mosaiq.IQ.Core.Framework.IQForms;

namespace Impac.Mosaiq.IQ.Interop.Forms.MedOnc
{
    /// <summary>
    /// This class invokes the Patient Care Plan Form IQ Script.  This workflow is integrated with PCPFrm in ECH.APP
    /// </summary>
    [Guid("5a17dd81-0d5a-4160-93ef-5c6aa595a0d7")]
    [ClassInterface(ClassInterfaceType.AutoDual), ComVisible(true), ClarionExport]
    public class PcpFrmIQWrapper : ClarionIQFormBase
    {
        #region Public Methods

        ///<summary>
        /// Initializes the IQ Form Wrapper.  Sets default values, performs control hookups and calls Load method on 
        /// the base class.
        ///</summary>
        ///<param name="pIsReadOnly"></param>
        ///<param name="pWinMgrRequest"></param>
        public void Initialize(int pWinMgrRequest, bool pIsReadOnly)
        {
            IQFormRequest request = pIsReadOnly ? IQFormRequest.View : (IQFormRequest) pWinMgrRequest;
            Load<PatientCarePlanFormFeature, PatientCarePlanFormScript>(request);
        }
        #endregion

        #region Public Properties

		/// <summary> Gets or sets the pcp_Group_Id of selected care plan </summary>
		public int PcpGroupId
		{
			get { return GetControlValue<int>(PatientCarePlanFormScript.PatCPlanControl); }
			set { SetControlValue(PatientCarePlanFormScript.PatCPlanControl, value); }
		}

        /// <summary> Gets or sets the cpl_Id of selected care plan </summary>
        public int PcpCplId
        {
            get { return GetControlValue<int>(PatientCarePlanFormScript.CareplanControl); }
            set { SetControlValue(PatientCarePlanFormScript.CareplanControl, value); }
        }

        /// <summary> Gets or sets the cpl_Set_Id of selected care plan </summary>
        public int PcpCplSetId
        {
            get { return GetControlValue<int>(PatientCarePlanFormScript.CareplanControl); }
            set { SetControlValue(PatientCarePlanFormScript.CareplanControl, value); }
        }

        /// <summary> Gets or sets the Med_Id of selected diagnosis </summary>
        public int PcpMedId
        {
            get { return GetControlValue<int>(PatientCarePlanFormScript.DiagnosisControl); }
            set { SetControlValue(PatientCarePlanFormScript.DiagnosisControl, value); }
        }

        /// <summary> Gets or sets the intent value </summary>
        public string PcpIntent
        {
            get { return GetControlValue<string>(PatientCarePlanFormScript.IntentControl); }
            set { SetControlValue(PatientCarePlanFormScript.IntentControl, value); }
        }

        /// <summary> Gets and sets whether the form is running in append mode. </summary>
        public bool IsAppending
        {
            get { return GetControlValue<bool>(PatientCarePlanFormScript.IsAppending); }
            set { SetControlValue(PatientCarePlanFormScript.IsAppending, value); }
        }

        /// <summary> Gets and sets whether the form is running in RO Mode. </summary>
        public bool IsRoCarePlan
        {
            get { return GetControlValue<bool>(PatientCarePlanFormScript.IsRoCarePlan); }
            set { SetControlValue(PatientCarePlanFormScript.IsRoCarePlan, value); }
        }
        #endregion
    }
}
