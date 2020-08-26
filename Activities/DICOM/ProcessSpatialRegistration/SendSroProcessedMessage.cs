using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Impac.Mosaiq.Dicom.DataServicesLayerInterface;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessSpatialRegistration
{
    /// <summary> </summary>
    [SendSroProcessedMessage_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class SendSroProcessedMessage : MosaiqCodeActivity
    {
        /// <summary> </summary>
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessSpatialRegistration _processSpatialRegistrationObject;

        /// <summary> Status indicating that SRO was processed successfully </summary>
        [RequiredArgument]
        [InputParameterCategory]
        public InArgument<bool> SroProcessedResult { get; set; }

        /// <summary> Site ID </summary>
        [InputParameterCategory]
        public InArgument<string> SiteID { get; set; }

        /// <summary> Patient ID </summary>
        [InputParameterCategory]
        public InArgument<string> PatientID { get; set; }

        /// <summary> The SOP Instance UID values for the referenced planning images </summary>
        [InputParameterCategory]
        public InArgument<IList<string>> PlanningImageSopInstanceValues { get; set; }

        [InputParameterCategory]
        public InArgument<IList<string>> VerificationImageSopInstanceValues { get; set; }
        /// <summary> The frame of reference UID values for the referenced planning images </summary>

        /// <summary>  Offset values in the order -
        /// Translation Offsets. X, Y, Z in IEC 61217 table top format
        /// Rotation Offset: XDeg, YDeg, ZDeg in IEC 61217 table top format
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        public InArgument<IList<decimal>> OffsetValues { get; set; }

        /// <summary>  Result of the Activity </summary>    
        [RequiredArgument]
        [OutputParameterCategory]
        [ActivityStatus_DisplayName]
        [ActivityStatus_Description]
        public OutArgument<string> ActivityStatus { get; set; }
        
        /// <summary> </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            string localStatus = "FAILED";
            bool status = SroProcessedResult.Get(context);
            string patientId = PatientID.Get(context);
            string siteId = SiteID.Get(context);
            IList<string> referencedImageSopInstanceValues = PlanningImageSopInstanceValues.Get(context);
            IList<string> verificationImageSopInstanceValues = VerificationImageSopInstanceValues.Get(context);
            IList<decimal> offsetValues = OffsetValues.Get(context);
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            bool result = _processSpatialRegistrationObject.SendSroProcessedMessage(status, Convert.ToInt32(patientId), Convert.ToInt32(siteId), referencedImageSopInstanceValues, verificationImageSopInstanceValues, offsetValues);
            if (result)
            {
                localStatus = "SUCCESS";
            }
            ActivityStatus.Set(context, localStatus);
        }

    }
}
