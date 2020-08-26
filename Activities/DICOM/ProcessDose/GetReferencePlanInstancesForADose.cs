using System;
using System.Activities;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Impac.Mosaiq.Dicom.DataServicesLayerInterface;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessDose
{

    /// <summary> </summary>
    [GetReferencePlanInstancesForADose_DisplayName]
    [DICOM_ActivityGroup]
    [RTDose_Category]
    public class GetReferencePlanInstancesForADose : MosaiqCodeActivity
    {
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessDose _createDoseVolume;

        /// <summary> </summary>
        /// <summary>  Input Unit Id of a DOSE Instance to perform work on </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [UnitId_DisplayName]
        [UnitId_Description]
        public InArgument<string> UnitId { get; set; }

        /// <summary>  Result of the Activity </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [ReferencedPlanSOPInstances_DisplayName]
        [ReferencedPlanSOPInstances_Description]
        public OutArgument<string[]> ReferencedPlanSOPInstances { get; set; }

        /// <summary> </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);
            ReferencedPlanSOPInstances.Set(context, _createDoseVolume.GetReferencedPlanInfo(Int32.Parse(UnitId.Get(context))).Item2);
        }
    }
}
