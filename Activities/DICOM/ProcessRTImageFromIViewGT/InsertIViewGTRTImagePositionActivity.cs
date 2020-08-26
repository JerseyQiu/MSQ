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
    /// Activity that inserts specified values for DICOM RT Image Position (3002, 0012)
    /// into an existing DICOM RT-Image file
    /// </summary>
    [InsertIViewGTRTImagePositionActivity_DisplayName]
    [DICOM_ActivityGroup]
    [IViewGTRTImage_Category]
    public class InsertIViewGTRTImagePositionActivity : MosaiqCodeActivity
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

        /// <summary>
        /// X Coordinate to set for DICOM RT Image Position (3002, 0012)
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [RtImagePositionXmm_DisplayName]
        [RtImagePositionXmm_Description]
        public InArgument<Double> RtImagePositionXmm { get; set;  }

        /// <summary>
        /// Y Coordinate to set for DICOM RT Image Position (3002, 0012)
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [RtImagePositionYmm_DisplayName]
        [RtImagePositionYmm_Description]
        public InArgument<Double> RtImagePositionYmm { get; set; }

        /// <summary>
        /// New DICOM RT Image file with updated RT Image Position
        /// </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [UpdatedFilename_DisplayName]
        [UpdatedFilename_Description]
        public OutArgument<string> UpdatedFilename { get; set; }



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
            string updatedRTImageFilename = "";

            string localStatus = _processRtImageFromIView.InsertIViewGTRtImagePosition(
                    IViewGTPortalImageFilename.Get(context),
                    RtImagePositionXmm.Get(context), RtImagePositionYmm.Get(context),
                    ref updatedRTImageFilename);

            UpdatedFilename.Set(context, updatedRTImageFilename);
            ActivityStatus.Set(context, localStatus);
        }
    }
    
}