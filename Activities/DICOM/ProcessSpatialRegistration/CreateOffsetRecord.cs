using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Dicom.DataServicesLayerInterface;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessSpatialRegistration
{
    /// <summary> </summary>
    [CreateOffsetRecord_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class CreateOffsetRecord : MosaiqCodeActivity
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
        [InputParameterCategory]
        public InArgument<IList<string>> SourceFrameOfReferenceUID { get; set; }
        /// <summary> Frame of Reference UID of the reference or planning image(s) </summary>
        [InputParameterCategory]
        public InArgument<string> TargetFrameOfReferenceUID { get; set; }
        /// <summary> The image record id values for the referenced planning images </summary>
        [InputParameterCategory]
        public InArgument<IList<int>> PlanningImageIdValues { get; set; }
        /// <summary> The image record id values for the referenceda verification images </summary>
        [InputParameterCategory]
        public InArgument<IList<int>> VerificationImageIdValues { get; set; }
        /// <summary> The SOP Instance UID values for the referenced planning images </summary>
        [InputParameterCategory]
        public InArgument<IList<string>> PlanningImageSopInstanceValues { get; set; }
        /// <summary> The SOP Instance UID values for the referenced verification images </summary>
        [InputParameterCategory]
        public InArgument<IList<string>> VerificationImageSopInstanceValues { get; set; }
        /// <summary> The frame of reference UID values for the referenced planning images </summary>
        [InputParameterCategory]
        public InArgument<IList<string>> PlanningImageFrameOfReferenceUidValues { get; set; }
        /// <summary> Spatial Registration Object Type identifier </summary>
        [InputParameterCategory]
        public InArgument<string> SpatialRegistrationObjectType { get; set; }
        /// <summary> The list of Transformation Matrices found in the registration sequences </summary>
        [InputParameterCategory]
        public InArgument<IList<IList<double>>> TransformationMatrix { get; set; }
        /// <summary> Site ID </summary>
        [InputParameterCategory]
        public InArgument<string> SiteID { get; set; }

        /// <summary>  Result of the Activity </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [ActivityStatus_DisplayName]
        [ActivityStatus_Description]
        public OutArgument<string> ActivityStatus { get; set; }
        /// <summary> Result of the offset being created </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<string> OffsetID { get; set; }

        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<string> PatientID { get; set; }

        /// <summary>  Offset values in the order -
        /// Translation Offsets. X, Y, Z in IEC 61217 table top format
        /// Rotation Offset: XDeg, YDeg, ZDeg in IEC 61217 table top format
        /// </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<IList<decimal>> OffsetValues { get; set; }

        /// <summary>  Indicates if SRO was processed successfully </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<bool> SroProcessedResult { get; set; }   

        /// <summary> </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            string localStatus = "FAILED";
            string _dcmInstanceId = UnitId.Get(context);
            int nDcmInstanceId = Convert.ToInt32(_dcmInstanceId);
            IList<string> sourceFrameOfReferenceUid = SourceFrameOfReferenceUID.Get(context);
            string targetFrameOfReferenceUid = TargetFrameOfReferenceUID.Get(context);
            IList<IList<double>> transformationMatrix = TransformationMatrix.Get(context);
            IList<int> planningImageIdValues = PlanningImageIdValues.Get(context);
            IList<int> verificationImageIdValues = VerificationImageIdValues.Get(context);
            IList<string> planningImageSopInstanceUidValues = PlanningImageSopInstanceValues.Get(context);
            IList<string> verificationImageSopInstanceUidValues = VerificationImageSopInstanceValues.Get(context);
            IList<string> planningFrameOfReferenceUidValues = PlanningImageFrameOfReferenceUidValues.Get(context);
            string spatialRegistrationObjectType = SpatialRegistrationObjectType.Get(context);
            string siteID = SiteID.Get(context);
            IList<decimal> dOffsetValues = new List<decimal>(6);
            int nOffsetId = 0;
            int nSiteID = 0;
            int nPatientID = 0;
            if (siteID.Length > 0)
            {
                nSiteID = Convert.ToInt32(siteID);
            }

            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            
            bool result = 
                _processSpatialRegistrationObject.PrepAndCreateOffsetRecords(
                nDcmInstanceId,
                spatialRegistrationObjectType,
                planningImageIdValues,
                verificationImageIdValues,
                planningImageSopInstanceUidValues, 
                verificationImageSopInstanceUidValues,
                planningFrameOfReferenceUidValues, 
                sourceFrameOfReferenceUid,
                targetFrameOfReferenceUid, 
                transformationMatrix,
                nSiteID,
                ref nPatientID,
                ref nOffsetId,
                ref dOffsetValues);

            if (result)
            {
                localStatus = "SUCCESS";
            }
            PatientID.Set(context, nPatientID.ToString());
            OffsetID.Set(context, nOffsetId.ToString());
            SroProcessedResult.Set(context, result);
            OffsetValues.Set(context, dOffsetValues);
            ActivityStatus.Set(context, localStatus);
        }

    }
}
