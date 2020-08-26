using System.Activities;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using Impac.Mosaiq.Dicom.DataServicesLayerInterface;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessImageSeries
{
    /// <summary />
    [ConvertExistingVolumesActivity_DisplayName]
    [DICOM_ActivityGroup]
    [ImageSeries_Category]
    public class ConvertExistingVolumes : MosaiqCodeActivity
    {
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessImageSeries _processImageSeries;

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
            ActivityStatus.Set(context, "FAILED");
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            string localStatus = _processImageSeries.ConvertExistingVolumes(UnitId.Get(context));

            ActivityStatus.Set(context, localStatus);
        }
    }
}
