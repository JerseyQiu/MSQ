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
    /// <summary>Parse XVI 5.x SRO to expose content to other activities</summary>
    [ParseXVI5xSpatialRegistrationObject_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class ParseXVI5xSpatialRegistrationObject : MosaiqCodeActivity
    {
        /// <summary> </summary>
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessSpatialRegistration _processSpatialRegistrationObject;

        /// <summary>  Input Unit Id to perform work on </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [SroUnitId_DisplayName]
        [SroUnitId_Description]
        public InArgument<string> SroUnitId { get; set; }

        /// <summary> Input Unit Id (DCMInstance ID) of the associated RPS object </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [RpsUnitId_DisplayName]
        [RpsUnitId_Description]
        public InArgument<string> RpsUnitId { get; set; }

        /// <summary> The SOP Instance UID values for the referenced planning images </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<IList<string>> PlanningImageSopInstanceValues { get; set; }

        /// <summary> The SOP Instance UID values for the referenced verification images </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<IList<string>> VerificationImageSopInstanceValues { get; set; }


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
            string sroDcmInstanceId = SroUnitId.Get(context);
            int nSroDcmInstanceId = Convert.ToInt32(sroDcmInstanceId);

            string rpsDcmInstanceId = RpsUnitId.Get(context);
            int nRpsDcmInstanceId = Convert.ToInt32(rpsDcmInstanceId);

            IList<string> planningImageSopInstanceUidValues = new List<string>();
            IList<string> verificationImageSopInstanceUidValues = new List<string>();


            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            _processSpatialRegistrationObject.ParseXVI5xSpatialRegistration(nSroDcmInstanceId, nRpsDcmInstanceId,
                ref planningImageSopInstanceUidValues, ref verificationImageSopInstanceUidValues);

            // did we successfully extract the lists of image sop instance uids ?
            if (planningImageSopInstanceUidValues.Count > 0 && verificationImageSopInstanceUidValues.Count > 0)
            {
                localStatus = "SUCCESS";
            }

            PlanningImageSopInstanceValues.Set(context, planningImageSopInstanceUidValues);
            VerificationImageSopInstanceValues.Set(context, verificationImageSopInstanceUidValues);
            ActivityStatus.Set(context, localStatus);
        }
    }
}
