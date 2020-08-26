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
    /// Activity that checks  an iViewGT DICOM RT-Image file
    /// to determine whether it is suitable for Auto-Isocenter processing</summary>
    [CheckIViewGTAutoIsocExclusionsActivity_DisplayName]
    [DICOM_ActivityGroup]
    [IViewGTRTImage_Category]
    public class CheckIViewGTAutoIsocExclusionsActivity : MosaiqCodeActivity
    {

        [Import]
        private IProcessRTImageFromIViewGT _processRtImageFromIView;

        /// <summary>
        /// Input filename: DCM file containing DICOM RT-Image to check
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [IViewGTPortalImageFilename_DisplayName]
        [IViewGTPortalImageFilename_Description]
        public InArgument<string> IViewGTPortalImageFilename { get; set; }

        /// <summary>
        /// Output status: Is this RT Image suitable for automated isocenter processing ?
        /// </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<bool> IsOkToProcessIsocenter { get; set; }
        
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

            bool bIsOk = _processRtImageFromIView.CheckIViewGTImageForAutoIsocenterExclusions(
                    IViewGTPortalImageFilename.Get(context));

            IsOkToProcessIsocenter.Set(context, bIsOk);
            ActivityStatus.Set(context, "SUCCESS");
        }
    }

}