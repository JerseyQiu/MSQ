using System;
using System.Activities;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Impac.Mosaiq.Dicom.DataServicesLayerInterface;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessStructureSet
{

    /// <summary> </summary>
    [GenerateSurfaceMeshActivity_DisplayName]
    [DICOM_ActivityGroup]
    [RTStructureSet_Category]
    public class GenerateSurfaceMesh : MosaiqCodeActivity
    {
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessStructureSet _createAnatomySets;

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

            string localStatus = _createAnatomySets.GenerateSurfaceMesh(UnitId.Get(context));
            ActivityStatus.Set(context, localStatus);
        }
    }
}
