using System;
using System.Activities;
using System.ComponentModel;
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
using Impac.Mosaiq.Dicom.DataBaseMonitor;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessStructureSet
{
    /// <summary> </summary>
    [WaitForStructureSetObjectActivity_DisplayName]
    [DICOM_ActivityGroup]
    [RTStructureSet_Category]
    public class WaitForStructureSetObjectReferencedData : MosaiqBookmarkActivity<string>
    {
        private const string RTSSSopClass = "1.2.840.10008.5.1.4.1.1.481.3";
        private readonly Logger _logger = DefaultFactory.Instance().CreateLogger(typeof(WaitForStructureSetObjectReferencedData).Name);

        private Variable<string> BookmarkMetadata { get; set; }

        private readonly string _activityBookmarkName = "RTSS_" + Guid.NewGuid();

        /// <summary>
        /// This class need to be thread-safe and should only deal with
        /// Behaviors.
        /// </summary>
        [Import] private IAsyncWrapperBookmarkedWaitSetData<string> _waitSetDataWrapper;

        /// <summary>
        /// This class need to be thread-safe and should only deal with
        /// Behaviors.
        /// </summary>
        [Import] 
        private IProcessStructureSet _processSS;

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
        public InArgument<string> UnitId { get; set; }

        /// <summary>  Result of the Activity </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [OutUnitId_DisplayName]
        [OutUnitId_Description]
        public OutArgument<string> OutUnitId { get; set; }


        ///<summary>
        /// CStor
        ///</summary>
        public WaitForStructureSetObjectReferencedData()
        {
            BookmarkMetadata = new Variable<string> {Name = "BookmarkMetadata"};
            base.BookmarkName = typeof(WaitForStructureSetObjectReferencedData).Name + Guid.NewGuid();
        }

        #region Overrides of MosaiqBookmarkActivity<int>

        protected override void BeforeDoWork(NativeActivityContext context)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            _logger.Log(EventSeverity.Debug, method, 
                string.Format("[{0}] Processing UnitId: [{1}], Bookmark name: [{2}], Extension Recorded Id:[{3}]",
                Thread.CurrentThread.ManagedThreadId,
                UnitId.Get(context), BookmarkName, _processSS.ConstructedBy));
        }

        protected override void OnBeforeCreateBookmark(NativeActivityContext context, out bool pCancelBookmarkCreation)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;

            bool validRefForSequenceInSS   = true;

            pCancelBookmarkCreation = false;
            _logger.Log(EventSeverity.Debug, method,
                string.Format("[{0}] Enter: Processing UnitId: [{1}], Bookmark name: [{2}], Extension Recorded Id:[{3}]",
                Thread.CurrentThread.ManagedThreadId,
                UnitId.Get(context), BookmarkName, _processSS.ConstructedBy));

            var refStudyInfo = _processSS.GetReferencedStudyInfo(Int32.Parse(UnitId.Get(context)));
            foreach (var tuple in refStudyInfo)
            {
                string refForUid = _processSS.GetReferencedFrameOfRefUid(Int32.Parse(UnitId.Get(context)));                         
                var metadata = _processSS.BuildMonitorMetadata(tuple.Item2, tuple.Item3, refForUid);

                //
                // If the dataset we are planning to wait is already available
                // skip the bookmark creation and proceed to next activity.
                //
                if (_waitSetDataWrapper.IsDataAvailable(metadata))                
                {
                    pCancelBookmarkCreation = true;
                    OutUnitId.Set(context, UnitId.Get(context));
                    //if (!base._useQueryNotificationFor3DWorkflows)
                    //{
                    //    if (!String.IsNullOrEmpty(metadata.DataMonitoringEventId))
                    //    {
                    //        WaitEventManager.TryRemoveEvent(metadata.DataMonitoringEventId);
                    //    }
                    //}
                }
                else
                {
                    BookmarkMetadata.Set(context, metadata.ToString());
                    if (base._useQueryNotificationFor3DWorkflows)
                    {
                        _waitSetDataWrapper.InitiateWaitForData(context.WorkflowInstanceId, BookmarkName,
                            metadata, _waitHandlersRepository.Get("WorkQueueElement"));
                    }
                    else
                    {
                        _waitSetDataWrapper.InitiateWaitForDataEvent(context.WorkflowInstanceId, BookmarkName,
                           metadata, _waitHandlersRepository.Get("WorkQueueElement"));
                    }
                }
        }
            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Processing UnitId: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), BookmarkName));

        }

        protected override void OnAddToWorklist(NativeActivityContext context, Guid instanceId)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Enter: Processing UnitId: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), BookmarkName));

            var bookmarkManager = context.GetExtension<IQWorklistManager>();
            string bookmarkDesc = BookmarkDesc.Get(context);
            bookmarkDesc = String.IsNullOrEmpty(bookmarkDesc) ? String.Empty : bookmarkDesc.Trim();
            bookmarkManager.AddWorklistItem(BookmarkName, IQBookmarkType.DBMonitor, bookmarkDesc, BookmarkMetadata.Get(context));

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Processing UnitId: [{1}], Context Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), BookmarkName));
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
                    Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), bookmark.Name));
            OutUnitId.Set(context, UnitId.Get(context));
            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Finished Processing UnitId: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), bookmark.Name));

        }

        #endregion

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddImplementationVariable(BookmarkMetadata);
        }
    }
}
