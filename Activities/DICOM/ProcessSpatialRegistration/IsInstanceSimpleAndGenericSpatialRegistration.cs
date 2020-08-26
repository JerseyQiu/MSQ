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
    /// <summary>
    /// </summary>
    [IsObjectSimpleAndGenericSpatialRegistration_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class IsInstanceSimpleAndGenericSpatialRegistration : MosaiqCodeActivity
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
            string _dcmInstanceId = UnitId.Get(context);
            int nDcmInstanceId = Convert.ToInt32(_dcmInstanceId);

            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            bool result = _processSpatialRegistrationObject.IsSimpleGenericSRO(nDcmInstanceId);

            if (result)
            {
                localStatus = "SUCCESS";
            }

            Result.Set(context, result);
            ActivityStatus.Set(context, localStatus);
        }
    }
}
