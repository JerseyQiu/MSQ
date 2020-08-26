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
    [UpdateFrameOfReferenceRelationshipRecord_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class UpdateFrameOfReferenceRelationshipRecord : MosaiqCodeActivity
    {
        /// <summary> </summary>
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessSpatialRegistration _processSpatialRegistrationObject;

        /// <summary> The Frame of Reference Record Id being updated </summary>
        [InputParameterCategory]
        public InArgument<int> FrameOfReferenceId { get; set; }
        /// <summary> Frame of Reference UIDs of the DICOM object(s) registered with the reference or planning image(s) </summary>
        [InputParameterCategory]
        public InArgument<string> SourceFrameOfReferenceUID { get; set; }
        /// <summary> Frame of Reference UID of the reference or planning image(s) </summary>
        [InputParameterCategory]
        public InArgument<string> TargetFrameOfReferenceUID { get; set; }
        /// <summary> The list of Transformation Matrices found in the registration sequences </summary>
        [InputParameterCategory]
        public InArgument<IList<double>> TransformationMatrix { get; set; }
        /// <summary> The list of comments associated with each Transformation Matrix </summary>
        [InputParameterCategory]
        public InArgument<string> TransformationMatrixComment { get; set; }

        /// <summary> The Frame of Referenfce Record Id being created </summary>
        [OutputParameterCategory]
        public OutArgument<int> UpdatedFrameOfReferenceRecordId { get; set; }

        /// <summary> </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            string sourceFrameOfReferenceUid = SourceFrameOfReferenceUID.Get(context);
            string targetFrameOfReferenceUid = TargetFrameOfReferenceUID.Get(context);
            IList<double> transformationMatrix = TransformationMatrix.Get(context);
            string transformationComment = TransformationMatrixComment.Get(context);
            int frameOfReferenceRecordId = FrameOfReferenceId.Get(context);

            int newFrameOfReferenceRecordId = 0;
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            // Update the frame of reference relationship record
            //_processSpatialRegistrationObject.UpdateFrameOfRefRelationshipRecord(frameOfReferenceRecordId, transformationMatrix.ToArray(), transformationComment, sourceFrameOfReferenceUid);

            UpdatedFrameOfReferenceRecordId.Set(context, newFrameOfReferenceRecordId);
        }
    }
}
