using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Threading;
using Impac.Mosaiq.Core.Logger;
using Impac.Mosaiq.Dicom.DataServicesLayer;
using Impac.Mosaiq.Dicom.DataServicesLayerInterface;
using Impac.Mosaiq.IQ.Common.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Runtime.Extensions;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessSpatialRegistration
{
    /// <summary> </summary>
    [ExtractStructureSetSpatialIsocenterCoordinates_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class ExtractSpatialIsocenterCoordinates : MosaiqCodeActivity
    {
        /// <summary> </summary>
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessSpatialRegistration _processSpatialRegistrationObject;

        /// <summary>  Input Unit Id to perform work on </summary>
        [RequiredArgument]
        [InputParameterCategory]
        public InArgument<IList<string>> StructureSetIds { get; set; }

        /// <summary>  Result of the Activity </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [ActivityStatus_DisplayName]
        [ActivityStatus_Description]
        public OutArgument<string> ActivityStatus { get; set; }


        /// <summary>  Isocenter Coordinate Values </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<IList<string>> SpatialIsocenterCoordinates { get; set; }

        /// <summary> </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            string localStatus = "FAILED";
            IList<string> _dcmInstanceIds = StructureSetIds.Get(context);
            int nDcmInstanceId = 0;
            string isocenterCoordinates = "";
            IList<string> spatialIsocenters = new List<string>();
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);
            ActivityStatus.Set(context, localStatus);

            foreach (string dcmInstance in _dcmInstanceIds)
            {
                nDcmInstanceId = Convert.ToInt32(dcmInstance);
                isocenterCoordinates = _processSpatialRegistrationObject.ExtractSpatialIsocenterCoordinates(nDcmInstanceId);
                spatialIsocenters.Add(isocenterCoordinates);
            }

            localStatus = "SUCCESS";
            SpatialIsocenterCoordinates.Set(context, spatialIsocenters);
            ActivityStatus.Set(context, localStatus);
        }
    }
}
