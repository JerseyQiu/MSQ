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
    [UpdateRegistrationRecord_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class UpdateRegistrationRecord : MosaiqCodeActivity
    {
        /// <summary> </summary>
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessSpatialRegistration _processSpatialRegistrationObject;

        /// <summary>  Input Unit Id to perform work on </summary>
        [InputParameterCategory]
        public InArgument<string> UnitId { get; set; }
        /// <summary> Internal Patient ID </summary>
        [InputParameterCategory]
        public InArgument<int> PatientID { get; set; }
        /// <summary> Frame Of Reference Record Id </summary>
        [InputParameterCategory]
        public InArgument<int> FrameOfReferenceId { get; set; }

        /// <summary> </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            int nPatientID = PatientID.Get(context);
            string _dcmInstanceId = UnitId.Get(context);
            int nDcmInstanceId = Convert.ToInt32(_dcmInstanceId);
            int nFrameOfReferenceId = FrameOfReferenceId.Get(context);

            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            // Update the registration record
            //_processSpatialRegistrationObject.UpdateRegistrationRecord(Guid, nDcmInstanceId, nPatientID, nFrameOfReferenceId);
        }
    }
}
