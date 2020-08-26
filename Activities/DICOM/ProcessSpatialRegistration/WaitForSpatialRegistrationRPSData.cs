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
using Impac.Mosaiq.Dicom.DataBaseMonitor;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessSpatialRegistration
{
    /// <summary> </summary>
    [WaitForSpatialRegistrationRPSDataActivity_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class WaitForSpatialRegistrationRPSData : MosaiqBookmarkActivity<string>
    {
        private readonly Logger _logger = DefaultFactory.Instance().CreateLogger(typeof(WaitForSpatialRegistrationRPSData).Name);

        private Variable<string> BookmarkMetadata { get; set; }

        private readonly string _activityBookmarkName = "SRO_WaitForRPS_" + Guid.NewGuid();

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
        private IProcessSpatialRegistration _processSpatialRegistrationObject;

        /// <summary>
        /// Allows one to create the relevant Workflow application and associate
        /// the necessary handlers.
        /// </summary>
        [Import]
        private IBookmarkedDataWaitHandlersRepository _waitHandlersRepository;


        /// <summary>SOP Instance UID of the RPS object to wait for </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [RPSUid_DisplayName]
        [RPSUid_Description]
        public InArgument<string> RPSUid { get; set; }

        /// <summary>DCMInstance Id of the RPS instance</summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [OutRPSUnitId_DisplayName]
        [OutRPSUnitId_Description]
        public OutArgument<string> OutRPSUnitId { get; set; }


        ///<summary>
        /// CTor
        ///</summary>
        public WaitForSpatialRegistrationRPSData()
        {
            BookmarkMetadata = new Variable<string> { Name = "BookmarkMetadata" };
            base.BookmarkName = typeof(WaitForSpatialRegistrationRPSData).Name + Guid.NewGuid();
        }

        #region Overrides of MosaiqBookmarkActivity<int>

        protected override void BeforeDoWork(NativeActivityContext context)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            _logger.Log(EventSeverity.Debug, method,
                string.Format("[{0}] Processing RPSUid: [{1}], Bookmark name: [{2}], Extension Recorded Id:[{3}]",
                Thread.CurrentThread.ManagedThreadId,
                RPSUid.Get(context), BookmarkName, _processSpatialRegistrationObject.ConstructedBy));
        }

        protected override void OnBeforeCreateBookmark(NativeActivityContext context, out bool pCancelBookmarkCreation)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;

            pCancelBookmarkCreation = false;
            _logger.Log(EventSeverity.Debug, method,
                string.Format("[{0}] Enter: Processing RPSUid: [{1}], Bookmark name: [{2}], Extension Recorded Id:[{3}]",
                Thread.CurrentThread.ManagedThreadId,
                RPSUid.Get(context), BookmarkName, _processSpatialRegistrationObject.ConstructedBy));

            var metadata = _processSpatialRegistrationObject.BuildMonitorMetadataForRPSData(RPSUid.Get(context));

            //
            // If the dataset we are planning to wait is already available
            // skip the bookmark creation and proceed to next activity.
            //
            if (_waitDataWrapper.IsDataAvailable(metadata))
            {
                pCancelBookmarkCreation = true;
                var dcmInstanceId = _processSpatialRegistrationObject.GetRPSDcmInstanceId(RPSUid.Get(context));
                OutRPSUnitId.Set(context, dcmInstanceId);

                _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] IsDataAvailable(): Processing RPSUid: [{1}], Bookmark name: [{2}], RPS  already available : Id [{3}]",
                    Thread.CurrentThread.ManagedThreadId, RPSUid.Get(context), BookmarkName, dcmInstanceId));

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
                    _waitDataWrapper.InitiateWaitForData(context.WorkflowInstanceId, BookmarkName,
                         metadata, _waitHandlersRepository.Get("WorkQueueElement"));
                }
                else
                {
                    _waitDataWrapper.InitiateWaitForDataEvent(context.WorkflowInstanceId, BookmarkName,
                         metadata, _waitHandlersRepository.Get("WorkQueueElement"));
                }
            }

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Processing RPSUid: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, RPSUid.Get(context), BookmarkName));

        }

        protected override void OnAddToWorklist(NativeActivityContext context, Guid instanceId)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Enter: Processing RPSUid: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, RPSUid.Get(context), BookmarkName));

            var bookmarkManager = context.GetExtension<IQWorklistManager>();
            string bookmarkDesc = BookmarkDesc.Get(context);
            bookmarkDesc = String.IsNullOrEmpty(bookmarkDesc) ? String.Empty : bookmarkDesc.Trim();
            bookmarkManager.AddWorklistItem(BookmarkName, IQBookmarkType.DBMonitor, bookmarkDesc, BookmarkMetadata.Get(context));

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Processing RPSUid: [{1}], Context Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, RPSUid.Get(context), BookmarkName));
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
            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Enter: Finished Processing RPSUid: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, RPSUid.Get(context), bookmark.Name));

            var dcmInstanceId = _processSpatialRegistrationObject.GetRPSDcmInstanceId(RPSUid.Get(context));
            OutRPSUnitId.Set(context, dcmInstanceId);

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Finished Processing RPSUid: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, RPSUid.Get(context), bookmark.Name));

        }

        #endregion

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddImplementationVariable(BookmarkMetadata);
        }
    }
}
