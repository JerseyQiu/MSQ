using System;
using System.Activities;
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

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessStructureSet
{
    /// <summary> </summary>
    [SaveStructureSetSpatialIsocenterCoordinates_DisplayName]
    [DICOM_ActivityGroup]
    [RTStructureSet_Category]
    public class SaveSpatialIsocenterCoordinates : MosaiqCodeActivity
    {
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessStructureSet _saveRTSSIsocenter;

        /// <summary> </summary>
        /// <summary>  Input Unit Id to perform work on </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [UnitId_DisplayName]
        [UnitId_Description]
        public InArgument<string> UnitId { get; set; }

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

            ActivityStatus.Set(context, localStatus);
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            bool result = _saveRTSSIsocenter.SaveSpatialIsocenterCoordinates(nDcmInstanceId);
            if (result)
            {
                localStatus = "SUCCESS";
            }
            ActivityStatus.Set(context, localStatus);
        }
    }
}
