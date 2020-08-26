using System;
using System.Activities;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Impac.Mosaiq.Dicom.DataServicesLayerInterface;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessPlan
{

    /// <summary> </summary>
    [PopulatePlanAndBeamTablesActivity_DisplayName]
    [DICOM_ActivityGroup]
    [RTPlan_Category]
    public class PopulatePlanAndBeamTablesActivity : MosaiqCodeActivity
    {
        [Import]
        private IProcessPlan _processConventionalPlan;

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

            string localStatus = _processConventionalPlan.PopulatePlanAndBeamTables(UnitId.Get(context));

            ActivityStatus.Set(context, localStatus);
            
        }
    }
}
