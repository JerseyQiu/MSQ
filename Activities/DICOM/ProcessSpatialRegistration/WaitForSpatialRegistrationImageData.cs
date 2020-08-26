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
    [WaitForSpatialRegistrationImageDataActivity_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class WaitForSpatialRegistrationImageData : MosaiqBookmarkActivity<string>
    {
        private const string SpatialRegistrationSopClass = "1.2.840.10008.5.1.4.1.1.66.1";
        private readonly Logger _logger = DefaultFactory.Instance().CreateLogger(typeof(WaitForSpatialRegistrationImageData).Name);

        private Variable<string> BookmarkMetadata { get; set; }

        private readonly string _activityBookmarkName = "SRO_" + Guid.NewGuid();

        /// <summary>
        /// This class need to be thread-safe and should only deal with
        /// Behaviors.
        /// </summary>
        [Import]
        private IAsyncWrapperBookmarkedWaitSetData<string> _waitSetDataWrapper;

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

        /// <summary> Spatial Registration Object Type identifier </summary>
        [RequiredArgument]
        [InputParameterCategory]
        public InArgument<string> SpatialRegistrationObjectType { get; set; }

        /// <summary> The SOP Instance UID values for the referenced planning images </summary>
        [RequiredArgument]
        [InputParameterCategory]
        public InArgument<IList<string>> PlanningImageSopInstanceValues { get; set; }

        /// <summary> The SOP Instance UID values for the referenced verification images </summary>
        [RequiredArgument]
        [InputParameterCategory]
        public InArgument<IList<string>> VerificationImageSopInstanceValues { get; set; }     

        /// <summary> Input Unit Id that work was performed on </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<string> OutUnitId { get; set; }

        /// <summary> The image record id values for the referenced planning images </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<IList<int>> VerificationImageIds { get; set; }
        /// <summary>  Result of the Activity </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        [ActivityStatus_DisplayName]
        [ActivityStatus_Description]
        public OutArgument<string> ActivityStatus { get; set; }

        ///<summary>
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string BookmarkName
        {
            get { return _activityBookmarkName; }
        }

        ///<summary>
        ///</summary>
        public WaitForSpatialRegistrationImageData()
        {
            BookmarkMetadata = new Variable<string> {Name = "BookmarkMetadata"};
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

            var registrationType = SpatialRegistrationObjectType.Get(context);
            var planningSopInstanceUids = PlanningImageSopInstanceValues.Get(context);
            switch (registrationType)
            {
                case "2D":
                    _processSpatialRegistrationObject.GetReferenced2DImageInfo(Int32.Parse(UnitId.Get(context)), planningSopInstanceUids);
                    break;
                case "3D":
                    _processSpatialRegistrationObject.GetReferenced3DImageInfo(Int32.Parse(UnitId.Get(context)), planningSopInstanceUids);
                    break;
            }

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

            var registrationType = SpatialRegistrationObjectType.Get(context);
            var planningSopInstanceUids = PlanningImageSopInstanceValues.Get(context);

            switch (registrationType)
            {
                case "2D":
                    var ref2DImageInfo = _processSpatialRegistrationObject.GetReferenced2DImageInfo(Int32.Parse(UnitId.Get(context)), planningSopInstanceUids);
                    foreach (var tuple in ref2DImageInfo)
                    {
                        foreach (string refSopInstanceUid in tuple.Item2)
                        {
                            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing UnitId: [{1}], Looking for IMG with SOP Instance: [{2}]", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), refSopInstanceUid));
                        }

                        var metadata = _processSpatialRegistrationObject.BuildMonitorMetadataForReferencedImageData(tuple.Item1, tuple.Item2);
                        IList<int> imageIds = new List<int>();

                        //
                        // If the dataset we are planning to wait is already available
                        // skip the bookmark creation and proceed to next activity.
                        //
                        if (_waitSetDataWrapper.IsDataAvailable(metadata))
                        {
                            pCancelBookmarkCreation = true;
                            _processSpatialRegistrationObject.GetReferencedImageIds(Int32.Parse(UnitId.Get(context)), VerificationImageSopInstanceValues.Get(context), ref imageIds);
                            if (imageIds.Count > 0)
                            {
                                foreach (int image_id in imageIds)
                                {
                                    _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing UnitId: [{1}], Found Image ID: [{2}]", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), image_id.ToString()));
                                }
                            }
                            if (imageIds.Count == 0)
                            {
                                _logger.Log(EventSeverity.Error, method, string.Format("[{0}] Processing UnitId: [{1}], Image Id list was not set correctly", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context)));
                                ActivityStatus.Set(context, "FAILED");
                            }
                            else
                            {
                                ActivityStatus.Set(context, "SUCCESS");
                            }
                            VerificationImageIds.Set(context, imageIds);
                            OutUnitId.Set(context, UnitId.Get(context));
                        }
                        else
                        {
                            // Perform manual processing to see which images are currently available
                            // This needs to handle 1-N cases
                            _processSpatialRegistrationObject.GetReferencedImageIds(Int32.Parse(UnitId.Get(context)), VerificationImageSopInstanceValues.Get(context), ref imageIds);
                            // based on that result set, set the bookmark if we are still missing some data
                            if (imageIds.Count < VerificationImageSopInstanceValues.Get(context).Count)
                            {
                                BookmarkMetadata.Set(context, metadata.ToString());
                                _waitSetDataWrapper.InitiateWaitForData(context.WorkflowInstanceId, BookmarkName,
                                                                        metadata,
                                                                        _waitHandlersRepository.Get("WorkQueueElement"));
                            }
                        }
                    }
                    break;
                case "3D":
                    var ref3DImageInfo = _processSpatialRegistrationObject.GetReferenced3DImageInfo(Int32.Parse(UnitId.Get(context)), planningSopInstanceUids);
                    int i = 0;
                    foreach (var tuple in ref3DImageInfo)
                    {
                        if (tuple.Item2.Count == 0)
                            continue;

                        foreach (string refSopInstanceUid in tuple.Item3)
                        {
                            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing UnitId: [{1}], Looking for DCM SOP Instance: [{2}]", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), refSopInstanceUid));
                        }

                        string verForUid = string.Empty;                        
                        if (!base._useQueryNotificationFor3DWorkflows)
                        {
                            verForUid = _processSpatialRegistrationObject.GetReferencedFrameOfReferenceInfo(Int32.Parse(UnitId.Get(context)));                            
                        }
                        var metadata = _processSpatialRegistrationObject.BuildMonitorMetadataForReferencedDicomData(tuple.Item1, tuple.Item2[i], verForUid, tuple.Item3);
                        //
                        // If the dataset we are planning to wait is already available
                        // skip the bookmark creation and proceed to next activity.
                        //
                        if (_waitSetDataWrapper.IsDataAvailable(metadata))
                        {
                            pCancelBookmarkCreation = true;
                            IList<int> imageIds = new List<int>();
                            _processSpatialRegistrationObject.GetReferencedImageIdsFromSOPInstanceUIDs(Int32.Parse(UnitId.Get(context)), VerificationImageSopInstanceValues.Get(context), ref imageIds);
                            foreach (int image_id in imageIds)
                            {
                                _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing UnitId: [{1}], Found Image ID: [{2}]", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), image_id.ToString()));
                            }
                            if (imageIds.Count == 0)
                            {
                                _logger.Log(EventSeverity.Error, method, string.Format("[{0}] Processing UnitId: [{1}], Image Id list was not set correctly", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context)));
                                ActivityStatus.Set(context, "FAILED");
                            }
                            else
                            {
                                ActivityStatus.Set(context, "SUCCESS");
                            }
                            VerificationImageIds.Set(context, imageIds);
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
                        i++;
                    }
                    break;
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
            var registrationType = SpatialRegistrationObjectType.Get(context);
            if (registrationType.Equals("2D"))
            {
                IList<int> imageIds = new List<int>();
                _processSpatialRegistrationObject.GetReferencedImageIds(Int32.Parse(UnitId.Get(context)), VerificationImageSopInstanceValues.Get(context), ref imageIds);
                foreach (int image_id in imageIds)
                {
                    _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing UnitId: [{1}], Found Image ID: [{2}]", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), image_id.ToString()));
                }
                if (imageIds.Count == 0)
                {
                    _logger.Log(EventSeverity.Error, method, string.Format("[{0}] Processing UnitId: [{1}], Image Id list was not set correctly", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context)));
                    ActivityStatus.Set(context, "FAILED");
                }
                else
                {
                    ActivityStatus.Set(context, "SUCCESS");
                }
                VerificationImageIds.Set(context, imageIds);
                OutUnitId.Set(context, UnitId.Get(context));
                ActivityStatus.Set(context, "SUCCESS");
            }
            else if (registrationType.Equals("3D"))
            {
                IList<int> imageIds = new List<int>();
                _processSpatialRegistrationObject.GetReferencedImageIdsFromSOPInstanceUIDs(Int32.Parse(UnitId.Get(context)), VerificationImageSopInstanceValues.Get(context), ref imageIds);
                foreach (int image_id in imageIds)
                {
                    _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing UnitId: [{1}], Found Image ID: [{2}]", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), image_id.ToString()));
                }
                if (imageIds.Count == 0)
                {
                    _logger.Log(EventSeverity.Error, method, string.Format("[{0}] Processing UnitId: [{1}], Image Id list was not set correctly", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context)));
                    ActivityStatus.Set(context, "FAILED");
                }
                else
                {
                    ActivityStatus.Set(context, "SUCCESS");
                }
                VerificationImageIds.Set(context, imageIds);
                OutUnitId.Set(context, UnitId.Get(context));
            }
            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Finished Processing UnitId: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), bookmark.Name));
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
