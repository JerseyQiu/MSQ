using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Impac.Mosaiq.Dicom.DataServicesLayerInterface;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessSpatialRegistration
{
    /// <summary> </summary>
    [CreateFrameOfReferenceRelationshipRecord_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class CreateFrameOfReferenceRelationshipRecord : MosaiqCodeActivity
    {
        /// <summary> </summary>
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessSpatialRegistration _processSpatialRegistrationObject;

        /// <summary> Frame of Reference UIDs of the DICOM object(s) registered with the reference or planning image(s) </summary>
        [InputParameterCategory]
        public InArgument<IList<string>> SourceFrameOfReferenceUID { get; set; }
        /// <summary> Frame of Reference UID of the reference or planning image(s) </summary>
        [InputParameterCategory]
        public InArgument<string> TargetFrameOfReferenceUID { get; set; }
        /// <summary> The list of Transformation Matrices found in the registration sequences </summary>
        [InputParameterCategory]
        public InArgument<IList<IList<double>>> TransformationMatrix { get; set; }
        /// <summary> The list of comments associated with each Transformation Matrix </summary>
        [InputParameterCategory]
        public InArgument<IList<string>> TransformationMatrixComment { get; set; }

        /// <summary> The Frame of Reference Record Id being created </summary>
        [OutputParameterCategory]
        public OutArgument<int> FrameOfReferenceRecordId { get; set; }

        /// <summary> </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            IList<string> sourceFrameOfReferenceUid = SourceFrameOfReferenceUID.Get(context);
            string targetFrameOfReferenceUid = TargetFrameOfReferenceUID.Get(context);
            IList<IList<double>> transformationMatrix = TransformationMatrix.Get(context);
            IList<string> transformationComment = TransformationMatrixComment.Get(context);
            int frameOfReferenceRecordId = 0;

            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            _processSpatialRegistrationObject.CreateFrameOfRefRelationshipRecord(transformationMatrix, transformationComment, sourceFrameOfReferenceUid, targetFrameOfReferenceUid, ref frameOfReferenceRecordId);

            FrameOfReferenceRecordId.Set(context, frameOfReferenceRecordId);
        }
    }
}
