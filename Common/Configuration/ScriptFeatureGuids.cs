using System;

namespace Impac.Mosaiq.IQ.Common.Configuration
{
    /// <summary> Defines GUID identifiers for IQ Script Features </summary>
    public static class ScriptFeatureGuids
    {
        /// <summary> Guid for pharmacy order created trigger </summary>
        public static readonly Guid PharmOrdCreated = new Guid("DE800EF2-31CF-43A7-B958-3EBF969B1941");

        /// <summary> Guid for pharmacy order modified trigger </summary>
        public static readonly Guid PharmOrdModified = new Guid("4932CD77-A814-4B3C-9D61-4B5C1E59B58D");

        /// <summary>Guid for pharmacy order approving trigger </summary>
        public static readonly Guid PharmOrdApproving = new Guid("9CDAFD48-6BB7-4e87-9975-F2490C7B71A4");

        /// <summary> Guid for pharmacy order approved trigger </summary>
        public static readonly Guid PharmOrdApproved = new Guid("CBF97ECB-59C1-406E-866E-35C959C36E65");

        /// <summary>Guid for pharmacy order verifying trigger </summary>
        public static readonly Guid PharmOrdVerifying = new Guid("2378AE60-F350-4852-AD4F-8E5E7CC925DD");

        /// <summary> Guid for pharmacy order verified trigger </summary>
        public static readonly Guid PharmOrdVerified = new Guid("B14627FE-E347-4A40-BFD5-FE850FCA9330");

        /// <summary> Guid for pharmacy order administering trigger </summary>
        public static readonly Guid PharmOrdAdministering = new Guid("248E365A-318B-48D5-B2ED-186B6D9941DB");

        /// <summary> Guid for pharmacy order administered trigger </summary>
        public static readonly Guid PharmOrdAdminstered = new Guid("7C6B73F6-1C8E-4E2D-AE2D-AE842353F99B");

        /// <summary> Guid for pharmacy order marking ready to treat trigger. </summary>
        public static readonly Guid PharmOrdMarkingReadyToTreat = new Guid("39251293-7F3D-4FC9-8702-C4EC9D348289");
 
        /// <summary> Guid for pharmacy order marked ready to treat trigger. </summary>
        public static readonly Guid PharmOrdMarkedReadyToTreat = new Guid("7FC58C03-460B-4D1B-BF35-DCDE35F71BF4");

        /// <summary> Guid for pharmacy order prepping trigger </summary>
        public static readonly Guid PharmOrdPrepping = new Guid("FBC967EE-2F37-402C-9D6B-B2281484ECF3");

        /// <summary> Guid for pharmacy order prepped trigger </summary>
        public static readonly Guid PharmOrdPrepped = new Guid("24CD2C4C-35E8-4335-86F2-6E35C23FC586");

        /// <summary>Guid for quick orders generated trigger. </summary>
        public static readonly Guid QuickOrderGenerated = new Guid("973E9916-7BC6-4b4e-9D5E-98ADFBFFF93C");

        /// <summary> Guid for observation order created trigger </summary>
        public static readonly Guid ObsOrderCreated = new Guid("00E9843C-B7A1-4FD0-9E78-3F38179A0E15");

        /// <summary> Guid for observation order approving trigger.  </summary>
        public static readonly Guid ObsOrderApproving = new Guid("B9C3A3E8-CB7B-4861-B51F-FD924DFBDB4B");

        /// <summary> Guid for observation order approved trigger. </summary>
        public static readonly Guid ObsOrderApproved = new Guid("6E23D457-E070-4A79-8701-7CFE9CB9FEFE");

        /// <summary> Guid for observation order modified trigger. </summary>
        public static readonly Guid ObsOrderModified = new Guid("C119A63B-0A6B-4489-92C5-D21A9394FD7E");

        /// <summary> Guid for observation order marked ready to treat. </summary>
        public static readonly Guid ObsOrdMarkedReadyToTreat = new Guid("07EC8B28-8CA9-463C-BB7D-E7A68ACAA229");

        /// <summary> Guid for observation order marking ready to treat </summary>
        public static readonly Guid ObsOrdMarkingReadyToTreat = new Guid("B050E23F-200B-41A3-BBC4-69694FC37FD8");

        /// <summary> Guid for observation reviewed trigger. </summary>
        public static readonly Guid ObsDataReviewed = new Guid("164324D6-91CD-4F53-975B-9480D8FC0A64");

        /// <summary> Guid for QCL task completed. </summary>
        public static readonly Guid QclCompleted = new Guid("2E5B17B5-F16F-41C8-9311-8B78318B8ED0");

        /// <summary> Guid for QCL task skipped. </summary>
        public static readonly Guid QclSkipped = new Guid("5A6AA22D-EC3B-422F-AA05-E9E00AA13793");

        /// <summary>Guid for RadOnc Treat trigger</summary>
        public static readonly Guid ROTreat = new Guid("B9A54338-6717-4277-B708-A11FF90E2CA7");

        ///<summary>Guid for POTD Treat trigger</summary>
        public static readonly Guid PotdTreat = new Guid("E65303C1-AD61-4B1B-97D2-392496886DCD");

        /// <summary>Guid for RadOnc QA Mode trigger</summary>
        public static readonly Guid ROQAMode = new Guid("8482BB17-95B9-4419-B10F-FCBD40C828FD");

		/// <summary> Guid for select eScribe template trigger.  </summary>
		public static readonly Guid EscribeSelectTemplate = new Guid("89EACF1C-96F1-4780-94EE-967D65B08B58");

    	/// <summary> Guid for inbound document trigger.  </summary>
    	public static readonly Guid InboundDocument = new Guid("C51254E5-BD18-4649-A5E4-D4FE357C32A9");

		/// <summary> Guid for inbound ObsReq trigger.  </summary>
		public static readonly Guid InboundObsReq = new Guid("63AA21EB-6235-44DF-A769-6A30034E8607");

        /// <summary> Guid for inbound ObsReq trigger.  </summary>
        public static readonly Guid ObsDataCreated = new Guid("780CD35E-F285-4C8D-8AB5-F86B7EF0FA94");
        
        /// <summary> Guid for appointment patient queued trigger. </summary>
        public static readonly Guid SchPatientQueued = new Guid("04CDAC0E-09E6-4DA3-97DE-6BC5A4A1A956");

		/// <summary> Guid for appointment scheduled trigger. </summary>
		public static readonly Guid SchAppointmentScheduled = new Guid("7296BB2B-6137-493E-A9F8-7DBEDA5992B3");

        /// <summary> Guid for appointment ended trigger. </summary>
        public static readonly Guid SchAppointmentEnded = new Guid("99EC382C-72B9-4F97-A0A3-032D27BB41DE");

        /// <summary> Guid for Code capture trigger. </summary>
        public static readonly Guid ChgCodeCaptured = new Guid("051A1F3F-6904-49B1-97FF-E4CDCB7225B7");

		/// <summary> Guid for Diagnosis Affirmed trigger. </summary>
		public static readonly Guid DiagnosisAffirmed = new Guid("0CADD958-6F1C-4780-A95B-260916BD5DFD");

		/// <summary> Guid for Open Patient Chart trigger. </summary>
		public static readonly Guid PatientOpenChart = new Guid("D6D7CBA1-BFCC-4808-B5DF-F3C27282C88C");
		/// <summary> Guid for Chart Review trigger. </summary>
		public static readonly Guid PatientChartReview = new Guid("71B832DC-674F-4024-A5A6-82EEDD51F1D2");
		/// <summary> Guid for CCDA Receipt trigger. </summary>
		public static readonly Guid PatientCCdaReceipt = new Guid("C0D7D9F9-B892-405C-B1F1-AFD6382E53B6");
		/// <summary> Guid for Medications Reconciled trigger. </summary>
		public static readonly Guid PatientMedicationsReconciled = new Guid("05AAB6DD-EFF3-4281-9F7E-75C55BF8327F");
		/// <summary> Guid for Diagnosis Reconciled trigger. </summary>
		public static readonly Guid PatientDiagnosisReconciled = new Guid("DEF74BAB-34BA-4AF0-B53A-C2E857FC4ED2");

        #region ASTRO 2010 Triggers
        /// <summary>PatientCareplanFormWorkflow Guid</summary>
        public static Guid PatientCarePlanForm = new Guid("5D8B7FA9-8254-4e6b-A9C7-83B7CDD41126");
        
        ///// <summary>ASTRO Script Type </summary>
        //public static Guid RadiationPrescriptionForm = new Guid("FE6D4D4F-FF58-4226-BB9A-904D30B2A151");

        ///// <summary>ASTRO Script Type </summary>
        //public static Guid TreatmentFieldForm = new Guid("C3B46714-ED48-4827-9131-881CB97FCCB5");

        ///// <summary>ASTRO Script Type </summary>
        //public static Guid TreatmentRecord = new Guid("6BACF814-0B33-47fe-9A6A-A64135F015F6");
        #endregion
            
        #region DICOM related Triggers
        /// <summary> Guid for RT Dose object Processing . </summary>
        public static readonly Guid RTDoseInstanceArrived = new Guid("300F9C83-ED6C-4BB7-B1C0-E3EE638CE63E");

        /// <summary> Guid for SRO object Processing . </summary>
        public static readonly Guid SROInstanceArrived = new Guid("5C1C8732-CDB6-4A98-9F14-2CCD51B4E21E");

        /// <summary> Guid for RTSS object Processing . </summary>
        public static readonly Guid StructureSetInstanceArrived = new Guid("451CBD76-ECC5-4470-B3BA-38D7E47F6319");

        /// <summary> Guid for PET image object Processing . </summary>
        public static readonly Guid RTPlanInstanceArrived = new Guid("4CAFD019-7178-4C15-9D14-2714A264ADC8");

        /// <summary> Guid for CT image object Processing . </summary>
        public static readonly Guid CTSeriesArrived = new Guid("2FBF48B3-9C4E-427F-8175-123AE440F3D9");

        /// <summary> Guid for MR image object Processing . </summary>
        public static readonly Guid MRSeriesArrived = new Guid("47412C6F-D5BD-4BCB-9195-D0953C0F04A7");

        /// <summary> Guid for PET image object Processing . </summary>
        public static readonly Guid PETSeriesArrived = new Guid("3DBDDBDD-83C1-4FED-BD8F-30DFA2DEBB36");

        /// <summary> Guid for RPS XVI Ref. Parameter Set object Processing . </summary>
        public static readonly Guid RPSInstanceArrived = new Guid("5B7BBFE3-D66C-4D3C-B77A-465E23EE54A5");

        /// <summary> Guid for Regenerating AVS files. </summary>
        public static readonly Guid RegenerateAVS = new Guid("B47089C8-2B84-4E4B-82F2-DD8A3D095C96");

        /// <summary> Guid for Puring AVS files. </summary>
        public static readonly Guid PurgeAVS = new Guid("795C5CAC-C83D-4C38-B817-2583E90EFD0B");

        /// <summary> Guid for computing isocenter for iViewGT RTImage </summary>
        public static readonly Guid IViewGTAutoIsocenter = new Guid("24500825-C01C-4F54-B56F-23A4C80CA61F");

        #endregion

        #region CDS Hooks Care Plan Import        
        /// <summary>
        /// The CDS care plan canceled
        /// </summary>
        public static readonly Guid CdsCarePlanCanceled = new Guid("76D16E13-2C6F-4D21-B4A5-86F7003F47C1");

        /// <summary>
        /// The CDS care plan approved
        /// </summary>
        public static readonly Guid CdsCarePlanApproved = new Guid("620AE44D-F4CD-43DB-85B9-C72A79C14131");

        /// <summary>
        /// The CDS care plan pending
        /// </summary>
        public static readonly Guid CdsCarePlanPending = new Guid("550B2F17-73AF-4103-B410-5902396B080D");

        /// <summary>
        /// The CDS care plan selected library
        /// </summary>
        public static readonly Guid CdsCarePlanSelectedLibrary = new Guid("F52C0675-3367-4EB2-AEB7-7A4BBED90C74");

        /// <summary>
        /// The CDS care plan error
        /// </summary>
        public static readonly Guid CdsCarePlanError = new Guid("4B6F61F4-0781-4336-B0AB-B014B1013EA0");
        #endregion CDS Hooks Care Plan Import
    }
}
