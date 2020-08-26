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
    [CreateRegistrationRecord_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class CreateRegistrationRecord : MosaiqCodeActivity
    {
        /// <summary> </summary>
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessSpatialRegistration _processSpatialRegistrationObject;

        /// <summary> Input Unit Id to perform work on </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [UnitId_DisplayName]
        [UnitId_Description]
        public InArgument<string> UnitId { get; set; }
        /// <summary> Matrix Registration Sequence Type </summary>
        [InputParameterCategory]
        public InArgument<byte> RegistrationType { get; set; }
        /// <summary> Manufacturer </summary>
        [InputParameterCategory]
        public InArgument<string> Manufacturer { get; set; }
        /// <summary> Model Name </summary>
        [InputParameterCategory]
        public InArgument<string> ModelName { get; set; }
        /// <summary> Software Version </summary>
        [InputParameterCategory]
        public InArgument<string> SoftwareVersions { get; set; }
        /// <summary> The list of Transformation Matrices found in the registration sequences </summary>
        [InputParameterCategory]
        public InArgument<IList<IList<double>>> TransformationMatrix { get; set; }
        /// <summary> The list of comments associated with each Transformation Matrix </summary>
        [InputParameterCategory]
        public InArgument<IList<string>> TransformationMatrixComment { get; set; }
        /// <summary> Frame of Reference UIDs of the DICOM object(s) registered with the reference or planning image(s) </summary>
        [InputParameterCategory]
        public InArgument<IList<string>> SourceFrameOfReferenceUID { get; set; }
        /// <summary> Frame of Reference UID of the reference or planning image(s) </summary>
        [InputParameterCategory]
        public InArgument<string> TargetFrameOfReferenceUID { get; set; }
        /// <summary> Offset Id  </summary>
        [InputParameterCategory]
        public InArgument<string> OffsetID { get; set; }

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
            string _dcmInstanceId = UnitId.Get(context);
            int nDcmInstanceId = Convert.ToInt32(_dcmInstanceId);
            byte nRegistrationType = RegistrationType.Get(context);
            string _manufacturer = Manufacturer.Get(context);
            string _model = ModelName.Get(context);
            string _softwareVersion = SoftwareVersions.Get(context);
            IList<string> sourceFrameOfReferenceUid = SourceFrameOfReferenceUID.Get(context);
            string targetFrameOfReferenceUid = TargetFrameOfReferenceUID.Get(context);
            IList<IList<double>> transformationMatrix = TransformationMatrix.Get(context);
            IList<string> transformationComment = TransformationMatrixComment.Get(context);
            string _offsetId = OffsetID.Get(context);
            int nOffsetId = 0;
            if (_offsetId.Length > 0)
            {
                nOffsetId = Convert.ToInt32(_offsetId);
            }

            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            try
            {
                _processSpatialRegistrationObject.CreateRegistrationRecord(nDcmInstanceId, transformationMatrix, transformationComment, sourceFrameOfReferenceUid, targetFrameOfReferenceUid, nRegistrationType, _manufacturer, _model, _softwareVersion, nOffsetId);
                localStatus = "SUCCESS";
                ActivityStatus.Set(context, localStatus);
            }
            catch (Exception ex)
            {
                ActivityStatus.Set(context, localStatus);
            }
        }
    }
}
