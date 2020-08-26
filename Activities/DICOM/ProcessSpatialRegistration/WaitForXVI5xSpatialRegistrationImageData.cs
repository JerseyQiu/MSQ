using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
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
    [WaitForXVI5xSpatialRegistrationImageDataActivity_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class WaitForXVI5xSpatialRegistrationImageData : MosaiqBookmarkActivity<string>
    {
        private readonly Logger _logger = DefaultFactory.Instance().CreateLogger(typeof(WaitForXVI5xSpatialRegistrationImageData).Name);

        private Variable<string> BookmarkMetadata { get; set; }

        private readonly string _activityBookmarkName = "SRO_WaitForXVIImages_" + Guid.NewGuid();

        /// <summary>
        /// This class needs to be thread-safe and should only deal with
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

        /// <summary> The SOP Instance UID values for the referenced planning images </summary>
        [RequiredArgument]
        [InputParameterCategory]
        public InArgument<IList<string>> PlanningImageSopInstanceUidValues { get; set; }

        /// <summary> The SOP Instance UID values for the referenced verification images </summary>
        [RequiredArgument]
        [InputParameterCategory]
        public InArgument<IList<string>> VerificationImageSopInstanceUidValues { get; set; }

        /// <summary>Unit Id that work was performed on </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<string> OutUnitId { get; set; }

        /// <summary>The image record id values for the referenced verifcation images </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<IList<int>> VerificationImageIds { get; set; }

        /// <summary>The image record id values for the referenced planning images </summary>
        [RequiredArgument]
        [OutputParameterCategory]
        public OutArgument<IList<int>> PlanningImageIds { get; set; }

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
        public WaitForXVI5xSpatialRegistrationImageData()
        {
            BookmarkMetadata = new Variable<string> { Name = "BookmarkMetadata" };
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
                string.Format("[{0}] Processing SRO UnitId: [{1}], Bookmark name: [{2}], Extension Recorded Id:[{3}]",
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
                string.Format("[{0}] Enter: Processing SRO UnitId: [{1}], Bookmark name: [{2}], Extension Recorded Id:[{3}]",
                Thread.CurrentThread.ManagedThreadId,
                UnitId.Get(context), BookmarkName, _processSpatialRegistrationObject.ConstructedBy));

            var planningSopInstanceUids = PlanningImageSopInstanceUidValues.Get(context);
            var verificationSopInstanceUids = VerificationImageSopInstanceUidValues.Get(context);

            // we want to wait for both planning and verification instances
            //IList<string> instanceUidsToWaitFor =
            //        planningSopInstanceUids.Concat(verificationSopInstanceUids).ToList();

            string verForUid = string.Empty;            
            if (!base._useQueryNotificationFor3DWorkflows)
            {
                verForUid = _processSpatialRegistrationObject.GetLocalizationFrameOfRefFromXVISRO(Int32.Parse(UnitId.Get(context)));                
            }
            var metadata = _processSpatialRegistrationObject.BuildMonitorMetadataForXVI5xReferencedDicomData(verForUid, verificationSopInstanceUids);
            //
            // If the dataset we are planning to wait for is already available
            // skip the bookmark creation and proceed to next activity.
            //
            if (_waitSetDataWrapper.IsDataAvailable(metadata))
            {
                pCancelBookmarkCreation = true;

                // get the Image IDs for the Verification and Planning images
                IList<int> verifImageIds = new List<int>();
                _processSpatialRegistrationObject.GetReferencedImageIdsFromSOPInstanceUIDs(Int32.Parse(UnitId.Get(context)),
                                    VerificationImageSopInstanceUidValues.Get(context), ref verifImageIds);

                IList<int> planningImageIds = new List<int>();
                _processSpatialRegistrationObject.GetReferencedImageIdsFromSOPInstanceUIDs(Int32.Parse(UnitId.Get(context)),
                                    PlanningImageSopInstanceUidValues.Get(context), ref planningImageIds);

                // (NB for an XVI 5x SRO we only expect a single Planning image & a single Verification image
                if (verifImageIds.Count != 1 || planningImageIds.Count != 1)
                {
                    _logger.Log(EventSeverity.Error, method, string.Format("[{0}] Processing UnitId: [{1}], Verification and/or Planning Image Ids were not correctly found", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context)));
                    ActivityStatus.Set(context, "FAILED");
                }
                else
                {
                     ActivityStatus.Set(context, "SUCCESS");
                }
               
                foreach (int imageId in verifImageIds)
                {
                    _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing SRO UnitId: [{1}], Found Verification Image ID: [{2}]", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), imageId.ToString()));
                }

                foreach (int imageId in planningImageIds)
                {
                    _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing SRO UnitId: [{1}], Found Planning Image ID: [{2}]", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), imageId.ToString()));
                }

                PlanningImageIds.Set(context, planningImageIds);                
                VerificationImageIds.Set(context, verifImageIds);
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

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Processing SRO UnitId: [{1}], Bookmark name: [{2}]",
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

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Enter: Processing SRO UnitId: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), BookmarkName));

            var bookmarkManager = context.GetExtension<IQWorklistManager>();
            string bookmarkDesc = BookmarkDesc.Get(context);
            bookmarkDesc = String.IsNullOrEmpty(bookmarkDesc) ? String.Empty : bookmarkDesc.Trim();
            bookmarkManager.AddWorklistItem(BookmarkName, IQBookmarkType.DBMonitor, bookmarkDesc, BookmarkMetadata.Get(context));

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Processing SRO UnitId: [{1}], Context Bookmark name: [{2}]",
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
            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Enter: Finished Processing SRO UnitId: [{1}], Bookmark name: [{2}]",
                    Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), bookmark.Name));

            // get the Image IDs for the Verification and Planning images
            IList<int> verifImageIds = new List<int>();
            _processSpatialRegistrationObject.GetReferencedImageIdsFromSOPInstanceUIDs(Int32.Parse(UnitId.Get(context)),
                                VerificationImageSopInstanceUidValues.Get(context), ref verifImageIds);

            IList<int> planningImageIds = new List<int>();
            _processSpatialRegistrationObject.GetReferencedImageIdsFromSOPInstanceUIDs(Int32.Parse(UnitId.Get(context)),
                                PlanningImageSopInstanceUidValues.Get(context), ref planningImageIds);

            // (NB for an XVI 5x SRO we only expect a single Planning image & a single Verificationn image
            if (verifImageIds.Count != 1 || planningImageIds.Count != 1)
            {
                _logger.Log(EventSeverity.Error, method, string.Format("[{0}] Processing SRO UnitId: [{1}], Verification and/or Planning Image Ids were not correctly found", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context)));
                ActivityStatus.Set(context, "FAILED");
            }
            else
            {
                ActivityStatus.Set(context, "SUCCESS");
            }

            foreach (int imageId in verifImageIds)
            {
                _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing SRO UnitId: [{1}], Found Verification Image ID: [{2}]", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), imageId.ToString()));
            }

            foreach (int imageId in planningImageIds)
            {
                _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Processing SRO UnitId: [{1}], Found Planning Image ID: [{2}]", Thread.CurrentThread.ManagedThreadId, UnitId.Get(context), imageId.ToString()));
            }

            PlanningImageIds.Set(context, planningImageIds);
            VerificationImageIds.Set(context, verifImageIds);
            OutUnitId.Set(context, UnitId.Get(context));

            _logger.Log(EventSeverity.Debug, method, string.Format("[{0}] Exit: Finished Processing SRO UnitId: [{1}], Bookmark name: [{2}]",
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
