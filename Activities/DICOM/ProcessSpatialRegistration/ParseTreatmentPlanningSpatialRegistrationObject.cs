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
    [ParseTreatmentPlanningSpatialRegistrationObject_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class ParseTreatmentPlanningSpatialRegistrationObject : MosaiqCodeActivity
    {
        /// <summary> </summary>
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessSpatialRegistration _processSpatialRegistrationObject;

        /// <summary>  Input Unit Id to perform work on </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [UnitId_DisplayName]
        [UnitId_Description]
        public InArgument<string> UnitId { get; set; }

        /// <summary> Frame of Reference UIDs of the DICOM object(s) registered with the reference or planning image(s) </summary>
        [OutputParameterCategory]
        public OutArgument<IList<string>> SourceFrameOfReferenceUID { get; set; }
        /// <summary> Frame of Reference UID of the reference or planning image(s) </summary>
        [OutputParameterCategory]
        public OutArgument<string> TargetFrameOfReferenceUID { get; set; }
        /// <summary> The list of Transformation Matrices found in the registration sequences </summary>
        [OutputParameterCategory]
        public OutArgument<IList<IList<double>>> TransformationMatrix { get; set; }
        /// <summary> The list of comments associated with each Transformation Matrix </summary>
        [OutputParameterCategory]
        public OutArgument<IList<string>> TransformationMatrixComment { get; set; }
        /// <summary> Matrix Registration Sequence Type </summary>
        [OutputParameterCategory]
        public OutArgument<byte> RegistrationType { get; set; }
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
            IList<string> sourceFrameOfReferenceUid = new List<string>();
            string targetFrameOfReferenceUid = "";
            IList<IList<double>> transformationMatrix = new List<IList<double>>();
            IList<string> transformationComment = new List<string>();
            byte nRegistrationType = 0;

            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            _processSpatialRegistrationObject.ParseTreatmentPlanningSpatialRegistration(nDcmInstanceId, ref sourceFrameOfReferenceUid, ref targetFrameOfReferenceUid, ref transformationMatrix, ref transformationComment, ref nRegistrationType);

            if (sourceFrameOfReferenceUid != null && sourceFrameOfReferenceUid.Count > 0)
            {
                localStatus = "SUCCESS";
            }

            SourceFrameOfReferenceUID.Set(context, sourceFrameOfReferenceUid);
            TargetFrameOfReferenceUID.Set(context, targetFrameOfReferenceUid);
            TransformationMatrix.Set(context, transformationMatrix);
            TransformationMatrixComment.Set(context, transformationComment);
            RegistrationType.Set(context, nRegistrationType);
            ActivityStatus.Set(context, localStatus);
        }
    }
}
