using System;
using System.Activities;
using System.Collections.Generic;
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

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessDose
{
    /// <summary> </summary>
    [WaitForDoseObjectPlanActivity_DisplayName]
    [DICOM_ActivityGroup]
    [RTDose_Category]
    public class WaitForDoseObjectPlanData : MosaiqBookmarkActivity<string>
    {
        private const string RTDoseSopClass = "1.2.840.10008.5.1.4.1.1.481.2";
        private readonly Logger _logger = DefaultFactory.Instance().CreateLogger(typeof(WaitForDoseObjectPlanData).Name);

        private Variable<string> BookmarkMetadata { get; set; }
        private Variable<string> FrameOfReferenceUid { get; set;  }

        private readonly string _activityBookmarkName = "RTDose_" + Guid.NewGuid();

        /// <summary>
        /// This class need to be thread-safe and should only deal with
        /// Behaviors.
        /// </summary>
        [Import]
        private IAsyncWrapperBookmarkedWaitData<string> _waitDataWrapper;

        /// <summary>
        /// This class need to be thread-safe and should only deal with
        /// Behaviors.
        /// </summary>
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)] 
        private IProcessDose _processDose;

        /// <summary>
        /// Allows one to create the relevant Workflow application and associate
        /// the necessary handlers.
        /// </summary>
        [Import]
        private IBookmarkedDataWaitHandlersRepository _waitHandlersRepository;

        /// <summary> </summary>
        /// <summary>  Input Unit Id to perform work on </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [UnitId_DisplayName]
        [UnitId_Description]
        public InArgument<string> PlanUid { get; set; }

        /// <summary>  Result of the Activity </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [OutPlanUnitId_DisplayName]
        [OutPlanUnitId_Description]
        public OutArgument<string> OutRTPlanUnitId { get; set; }


        ///<summary>
        /// CStor
        ///</summary>
        public WaitForDoseObjectPlanData()
        {
            BookmarkMetadata = new Variable<string> {Name = "BookmarkMetadata"};
            base.BookmarkName = typeof(WaitForDoseObjectPlanData).Name + Guid.NewGuid();
        }

        #region Overrides of MosaiqBookmarkActivity<int>

        protected override void BeforeDoWork(NativeActivityContext context)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            _logger.Log(EventSeverity.Debug, method, 
                string.Format("[{0}] Processing UnitId: [{1}], Bookmark name: [{2}], Extension Recorded Id:[{3}]",
                Thread.CurrentThread.ManagedThreadId,
                PlanUid.Get(context), BookmarkName, _processDose.ConstructedBy));
        }

        protected override void OnBeforeCreateBookmark(NativeActivityContext context, out bool pCancelBookmarkCreation)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;

            pCancelBookmarkCreation = false;
            _logger.Log(EventSeverity.Debug, method,
                string.Format("[{0}] Enter: Processing UnitId: [{1}], Bookmark name: [{2}], Extension Recorded Id:[{3}]",
                Thread.CurrentThread.ManagedThreadId,
                PlanUid.Get(context), BookmarkName, _processDose.ConstructedBy));

            var metadata = _processDose.BuildReferencedPlanMonitorMetadata(PlanUid.Get(context));

            //
            // If the dataset we are planning to wait is already available
            // skip the bookmark creation and proceed to next activity.
            //
            if (_waitDataWrapper.IsDataAvailable(metadata))
            {
                pCancelBookmarkCreation = true;
                var dcmInstanceId = _processDose.GetReferencePlanDcmInstanceId(PlanUid.Get(context));
                OutRTPlanUnitId.Set(context, dcmInstanceId);
            }
            else
            {
                BookmarkMetadata.Set(context, metadata.ToString());
               _waitDataWrapper.InitiateWaitForData(context.WorkflowInstanceId, BookmarkName, 
                    metadata, _waitHandlersRepository.Get("WorkQueueElement"));
            }

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Processing UnitId: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, PlanUid.Get(context), BookmarkName));

        }

        protected override void OnAddToWorklist(NativeActivityContext context, Guid instanceId)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Enter: Processing UnitId: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, PlanUid.Get(context), BookmarkName));

            var bookmarkManager = context.GetExtension<IQWorklistManager>();
            string bookmarkDesc = BookmarkDesc.Get(context);
            bookmarkDesc = String.IsNullOrEmpty(bookmarkDesc) ? String.Empty : bookmarkDesc.Trim();
            bookmarkManager.AddWorklistItem(BookmarkName, IQBookmarkType.DBMonitor, bookmarkDesc, BookmarkMetadata.Get(context));

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Processing UnitId: [{1}], Context Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, PlanUid.Get(context), BookmarkName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bookmark"></param>
        /// <param name="obj"></param>
        protected override void OnBookmarkCallback(NativeActivityContext context, Bookmark bookmark, string obj)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;

            Debug.Assert(bookmark.Name == BookmarkName);

            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);
            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Enter: Finished Processing UnitId: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, PlanUid.Get(context), bookmark.Name));

            var dcmInstanceId = _processDose.GetReferencePlanDcmInstanceId(PlanUid.Get(context));
            OutRTPlanUnitId.Set(context, dcmInstanceId);
            
            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Finished Processing UnitId: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, PlanUid.Get(context), bookmark.Name));

        }

        #endregion

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddImplementationVariable(BookmarkMetadata);
            metadata.AddImplementationVariable(FrameOfReferenceUid);
        }
    }
}
