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
    [ProcessXVI5xSpatialRegistration_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class ProcessXVI5xSpatialRegistrationObject : MosaiqCodeActivity
    {
        /// <summary> </summary>
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessSpatialRegistration _processSpatialRegistrationObject;

        /// <summary> Input Unit Id (DCMInstance ID)of the SRO object </summary>
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

        /// <summary>
        /// </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<bool> Result { get; set; }

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
            ActivityStatus.Set(context, localStatus);
            string sroDcmInstanceId = SroUnitId.Get(context);
            string rpsDcmInstanceId = RpsUnitId.Get(context);

            int nSroDcmInstanceId = Convert.ToInt32(sroDcmInstanceId);
            int nRpsDcmInstanceId = Convert.ToInt32(rpsDcmInstanceId);

            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            bool result = _processSpatialRegistrationObject.CreateElektaXVI5xSRORegistrationFoRREntries(nSroDcmInstanceId, nRpsDcmInstanceId);

            if (result)
            {
                localStatus = "SUCCESS";
            }

            Result.Set(context, result);
            ActivityStatus.Set(context, localStatus);
        }
    }
}
