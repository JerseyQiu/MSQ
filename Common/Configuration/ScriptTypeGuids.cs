using System;

namespace Impac.Mosaiq.IQ.Common.Configuration
{
    /// <summary> Guid values used to identify ScriptTypes</summary>
    public static class ScriptTypeGuids
    {
        /// <summary> Guid for a generic script</summary>
        public static readonly Guid GenericScript = new Guid("CE4980AC-AB0C-4444-AC2F-FEC5AE9342BE");

        /// <summary> Guid for a patient script </summary>
        public static readonly Guid PatientScript = new Guid("6551E40E-9CAD-464E-8E1C-6AE6F7BCEE1D");

        /// <summary> Guid for order script </summary>
        public static readonly Guid OrderScript = new Guid("88FE33D9-EEE1-4625-ADF1-E89E57476942");

        /// <summary> Guid for order script with cancelable option</summary>
        public static readonly Guid OrderScriptCancelable = new Guid("6C73A0CA-AB20-4620-965E-5A99B4D39174");

        /// <summary> Guid for QCL script </summary>
        public static readonly Guid QclScript = new Guid("E8A22A2B-5B62-4A8F-B7FD-3F12460E3B5A");
        
        /// <summary> Guid for EScribe merge field script Type </summary>
        public static readonly Guid EScribeMergeField = new Guid("12370CCE-C73D-4CF6-9889-33D8587CB39E");

		/// <summary> Guid for select eScribe template script </summary>
		public static readonly Guid EscribeSelectTemplate = new Guid("816DB2A9-0B06-48F0-80EB-788427F58AE1");

    	/// <summary> Guid for document script </summary>
    	public static readonly Guid DocumentScript = new Guid("E7D2DC9F-A41A-48FD-BE4F-DF4D7451D5FD");

    	/// <summary> Guid for ObsReq script </summary>
    	public static readonly Guid ObsReqScript = new Guid("74B00DFF-57FC-47FB-85C1-3488198E3DC3");

        /// <summary> Guid for appointment script </summary>
        public static readonly Guid AppointmentScript = new Guid("920C986A-0A98-4C0A-8299-BF2736C89641");

        /// <summary> Guid for Charge script </summary>
        public static readonly Guid ChargeScript = new Guid("7A924813-B23F-4A8E-B98F-4B4E0E88CF9C");

		/// <summary> Guid for Diagnosis script </summary>
		public static readonly Guid DiagnosisScript = new Guid("FB959C57-ABF2-4EE2-BF80-F224B8DEE8BE");

        /// <summary> Guid for patient care plan form IQ Script</summary>
        public static readonly Guid PatientCarePlanForm = new Guid("9F6FE8AA-72A8-4F8E-AA91-3344BB50406E");

        /// <summary> Guid for CDS Hooks care plan IQ Script</summary>
        public static readonly Guid CdsCarePlan = new Guid("64DA3948-6956-4505-863A-9B39BD799280");

        ///// <summary>ASTRO Script Type </summary>
        //public static readonly Guid RadiationPrescriptionForm = new Guid("B16207CA-1537-4a9e-9CCA-08934F33A957");

        ///// <summary>ASTRO Script Type </summary>
        //public static readonly Guid TreatmentFieldForm = new Guid("AD16E165-D852-41ff-8BBA-BE7D2CFDBB27");

        ///// <summary>ASTRO Script Type </summary>
        //public static readonly Guid TreatmentRecord = new Guid("46FCDF0B-4B30-4308-923F-EC89470BF152");

        /// <summary> Guid for creating AVS Volume script processing script</summary>
        public static readonly Guid ImageSeriesScript = new Guid("82C1BF75-C376-4D94-A62D-DE30B8514E52");

        ///// <summary> Guid for Purging AVS/XML Cache files script</summary>
        public static readonly Guid PurgeAVSScript = new Guid("684D29F9-9A5D-4F6D-83F7-08EEEA45B002");

        /// <summary> Guid for PRocessing RT Dose script processing script</summary>
        public static readonly Guid RTDoseInstanceScript = new Guid("1EF05E2C-4DF4-4244-B15B-F1AF60907D42");

        /// <summary> Guid for Processing RTSS script processing script</summary>
        public static readonly Guid StructureSetInstanceScript = new Guid("89A79A0E-39CE-47F2-B964-86AE6D93FAC0");

        /// <summary> Guid for Processing SRO object script processing script</summary>
        public static readonly Guid SROInstanceScript = new Guid("0F6C9D2E-7ABF-458B-8812-2E4EB3409EA7");

        /// <summary> Guid for RPS object script processing script</summary>
        public static readonly Guid RPSInstanceScript = new Guid("91ABB6D7-256F-464D-99CD-2091294F7F59");

        /// <summary> Guid for Processing RT Plan script processing script</summary>
        public static readonly Guid RTPlanInstanceScript = new Guid("0FD54937-1822-4706-BB97-27731A26C1F8");

        /// <summary> Guid for script to compute Isocenter for iViewGT RTImage </summary>
        public static readonly Guid IViewGTAutoIsocenterScript = new Guid("D7419A0D-A20A-4299-8F1B-F812E312F2B3");

        /// <summary> Guid for a patient script with cancelable option</summary>
        public static readonly Guid PatientScriptCancelable = new Guid("C59B43A2-73F8-49FC-A2FC-70549604D5BE");

        /// <summary> Guid for regenerating AVS for a given DCM Series</summary>
        public static readonly Guid RegenerateAVSScript = new Guid("BA148459-8BCF-4DC8-9F76-B6F29E078B7D");

        /// <summary> Guid for a POTD script with cancelable option</summary>
        public static readonly Guid PotdScriptCancelable = new Guid("EE816E4E-B9E3-4E62-AAE3-85E5ED11B901");
    }
}
