using System;
using System.Activities;
using System.Collections.Generic;
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

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessSpatialRegistration
{
    /// <summary> </summary>
    [WaitForSpatialRegistrationStructureSetDataActivity_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class WaitForSpatialRegistrationStructureSetData : MosaiqBookmarkActivity<string>
    {
        private const string SpatialRegistrationSopClass = "1.2.840.10008.5.1.4.1.1.66.1";
        private readonly Logger _logger = DefaultFactory.Instance().CreateLogger(typeof(WaitForSpatialRegistrationStructureSetData).Name);

        private Variable<string> BookmarkMetadata { get; set; }

        private readonly string _activityBookmarkName = "SRO_" + Guid.NewGuid();        

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
        [Import]
        private IProcessSpatialRegistration _processSpatialRegistrationObject;        

        /// <summary>
        /// Allows one to create the relevant Workflow application and associate
        /// the necessary handlers.
        /// </summary>
        [Import]
        private IBookmarkedDataWaitHandlersRepository _waitHandlersRepository;

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

        /// <summary>  DCM Instance Ids of the received structure sets </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [OutStructureSetIds_DisplayName]
        [OutStructureSetIds_Description]
        public OutArgument<IList<string>> OutStructureSetIds { get; set; }        

        /// <summary>
        /// constructor
        /// </summary>
        public WaitForSpatialRegistrationStructureSetData()
        {
            BookmarkMetadata = new Variable<string> { Name = "BookmarkMetadata" };
            base.BookmarkName = typeof(WaitForSpatialRegistrationStructureSetData).Name + Guid.NewGuid();
        }

        #region Overrides of MosaiqBookmarkActivity<int>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void BeforeDoWork(NativeActivityContext context)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;
            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);

            _logger.Log(EventSeverity.Debug, method,
                string.Format("[{0}] Processing UnitId: [{1}], Bookmark name: [{2}], Extension Recorded Id:[{3}]",
                Thread.CurrentThread.ManagedThreadId,
                UnitId.Get(context), BookmarkName, _processSpatialRegistrationObject.ConstructedBy));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pCancelBookmarkCreation"></param>
        protected override void OnBeforeCreateBookmark(NativeActivityContext context, out bool pCancelBookmarkCreation)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod().Name;

            pCancelBookmarkCreation = false;
            _logger.Log(EventSeverity.Debug, method,
                string.Format("[{0}] Enter: Processing UnitId: [{1}], Bookmark name: [{2}], Extension Recorded Id:[{3}]",
                Thread.CurrentThread.ManagedThreadId,
                UnitId.Get(context), BookmarkName, _processSpatialRegistrationObject.ConstructedBy));

            string verificationForUid = _processSpatialRegistrationObject.GetReferencedFrameOfReferenceInfo(Int32.Parse(UnitId.Get(context)));

            var metadata = _processSpatialRegistrationObject.BuildMonitorMetadataForStructureSetData(verificationForUid);

            //
            // If the dataset we are planning to wait is already available
            // skip the bookmark creation and proceed to next activity.
            //
            if (_waitDataWrapper.IsDataAvailable(metadata))
            {                
                pCancelBookmarkCreation = true;
                string referencedSiteId = _processSpatialRegistrationObject.GetReferencedSiteId(Int32.Parse(UnitId.Get(context)));
                if (referencedSiteId != "")
                {
                    //get DcmInstanceId of planning structure set. The verification FrameofRefUid is 
                    //passed as an argument but is redundant. (confusing also)
                    var tmpList = _processSpatialRegistrationObject.GetReferenceStructureSetDcmInstances(verificationForUid, referencedSiteId, Int32.Parse(UnitId.Get(context)));

                    foreach (string rtssId in tmpList)
                    {
                        _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing UnitId: [{1}], Found structure set id: [{2}]",
                        Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), rtssId));
                    }
                    OutStructureSetIds.Set(context, tmpList);

                    OutUnitId.Set(context, UnitId.Get(context));

                    //if (!base._useQueryNotificationFor3DWorkflows)
                    //{
                    //    if (!String.IsNullOrEmpty(metadata.DataMonitoringEventId))
                    //    {
                    //        WaitEventManager.TryRemoveEvent(metadata.DataMonitoringEventId);
                    //    }
                    //}
                }                                                                                                
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
            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Processing UnitId: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), BookmarkName));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="instanceId"></param>
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

            string referencedSiteId = _processSpatialRegistrationObject.GetReferencedSiteId(Int32.Parse(UnitId.Get(context)));
            var refFrameOfRefUid = _processSpatialRegistrationObject.GetReferencedFrameOfReferenceInfo(Int32.Parse(UnitId.Get(context)));

            if (referencedSiteId != "")
            {
                var tmpList = _processSpatialRegistrationObject.GetReferenceStructureSetDcmInstances(refFrameOfRefUid, referencedSiteId, Int32.Parse(UnitId.Get(context)));

                foreach (string rtssId in tmpList)
                {
                    _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing UnitId: [{1}], Found structure set id: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), rtssId));
                }
                OutStructureSetIds.Set(context, tmpList);

                OutUnitId.Set(context, UnitId.Get(context));
                _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Finished Processing UnitId: [{1}], Bookmark name: [{2}]",
                        Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), bookmark.Name));
            }           
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddImplementationVariable(BookmarkMetadata);
        }       
    }
}
