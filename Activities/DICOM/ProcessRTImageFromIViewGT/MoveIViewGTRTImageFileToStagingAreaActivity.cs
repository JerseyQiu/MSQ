using System;
using System.Activities;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Impac.Mosaiq.Dicom.DataServicesLayerInterface;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessRTImageFromIViewGT
{
    /// <summary>
    /// Activity that moves an iViewGT DICOM RT-Image file from its current
    /// location into the parent directory, assumed to be a Staging Area for import
    /// </summary>
    [MoveIViewGTRTImageFileToStagingAreaActivity_DisplayName]
    [DICOM_ActivityGroup]
    [IViewGTRTImage_Category]
    public class MoveIViewGTRTImageFileToStagingAreaActivity : MosaiqCodeActivity
    {

        [Import]
        private IProcessRTImageFromIViewGT _processRtImageFromIView;

        /// <summary>
        /// Input filename: DCM file to be moved to parent folder
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [IViewGTPortalImageFilename_DisplayName]
        [IViewGTPortalImageFilename_Description]
        public InArgument<string> IViewGTPortalImageFilename { get; set; }

        /// <summary>  Result of the Activity </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [ActivityStatus_DisplayName]
        [ActivityStatus_Description]
        public OutArgument<string> ActivityStatus { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            ActivityStatus.Set(context, "FAILED");
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            string localStatus = _processRtImageFromIView.MoveIViewGTImageToParentStagingArea(
                    IViewGTPortalImageFilename.Get(context));
            ActivityStatus.Set(context, localStatus);
        }
    }

}