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
    /// Activity that computes suitable values for DICOM RT Image Position (3002, 0012)
    /// for a DICOM RT-Image file from iViewGT by matching
    /// the apparent field shape within the portal image and comparing it with the
    /// prescribed field associated with the image.
    /// </summary>
    [LocateIViewGTRTImagePositionActivity_DisplayName]
    [DICOM_ActivityGroup]
    [IViewGTRTImage_Category]
    public class LocateIViewGTRTImagePositionActivity : MosaiqCodeActivity
    {
        [Import]
        private IProcessRTImageFromIViewGT _processRtImageFromIView;

        /// <summary>
        /// Input filename: DCM file containing DICOM RT-Image
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
        /// X Coordinate computed for DICOM RT Image Position (3002, 0012)
        /// </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [RtImagePositionXmm_DisplayName]
        [RtImagePositionXmm_Description]
        public OutArgument<Double> RtImagePositionXmm { get; set; }

        /// <summary>
        /// Y Coordinate computed for DICOM RT Image Position (3002, 0012)
        /// </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [RtImagePositionYmm_DisplayName]
        [RtImagePositionYmm_Description]
        public OutArgument<Double> RtImagePositionYmm { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            ActivityStatus.Set(context, "FAILED");
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            double xCoord = -9999.99;
            double yCoord = -9999.99;

            string localStatus = _processRtImageFromIView.ComputeIViewGTRtImagePosition(
                IViewGTPortalImageFilename.Get(context), ref xCoord, ref yCoord );

            RtImagePositionXmm.Set(context, xCoord);
            RtImagePositionYmm.Set(context, yCoord);
            ActivityStatus.Set(context, localStatus);
        }
    }

}