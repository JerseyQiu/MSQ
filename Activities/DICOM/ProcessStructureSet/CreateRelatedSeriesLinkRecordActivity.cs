﻿using System;
using System.Activities;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Impac.Mosaiq.Dicom.DataServicesLayerInterface;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessStructureSet
{

    /// <summary> </summary>
    [CreateRelatedSeriesLinkRecordActivity_DisplayName]
    [DICOM_ActivityGroup]
    [RTStructureSet_Category]
    public class CreateRelatedSeriesLinkRecordActivity : MosaiqCodeActivity
    {
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessStructureSet _processStructureSet;

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

            string localStatus = _processStructureSet.CreateRelatedSeriesLinkRecord(UnitId.Get(context)) ? "SUCCESS" : "FAIL";
            ActivityStatus.Set(context, localStatus);
        }
    }
}
