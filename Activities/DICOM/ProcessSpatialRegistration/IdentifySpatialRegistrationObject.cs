using System;
using System.Activities;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Impac.Mosaiq.Dicom.DataServicesLayerInterface;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;

namespace Impac.Mosaiq.IQ.Activities.DICOM.ProcessSpatialRegistration
{
    /// <summary> </summary>
    [IdentifySpatialRegistrationObject_DisplayName]
    [DICOM_ActivityGroup]
    [SpatialRegistration_Category]
    public class IdentifySpatialRegistrationObject : MosaiqCodeActivity
    {
        /// <summary> </summary>
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private IProcessSpatialRegistration _processSpatialRegistrationObject;

        /// <summary>  Input Unit Id to perform work on </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [UnitId_DisplayName]
        [UnitId_Description]
        public InArgument<string> UnitId { get; set; }

        /// <summary> Spatial Registration Object Type identifier </summary>
        [OutputParameterCategory]
        public OutArgument<string> SpatialRegistrationObjectType { get; set; }
        /// <summary> Internal Patient Id </summary>
        [OutputParameterCategory]
        public OutArgument<int> PatientID { get; set; }
        /// <summary> Manufacturer </summary>
        [OutputParameterCategory]
        public OutArgument<string> Manufacturer { get; set; }
        /// <summary> Model Name </summary>
        [OutputParameterCategory]
        public OutArgument<string> Model { get; set; }
        /// <summary> Software Version </summary>
        [OutputParameterCategory]
        public OutArgument<string> SoftwareVersion { get; set; }
        /// <summary> Modality </summary>
        [OutputParameterCategory]
        public OutArgument<string> Modality { get; set; }

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
            string localStatus = "FAILED";
            ActivityStatus.Set(context, localStatus);            
            string sSpatialRegistrationObjectType = "";
            int nPatientID = 0;
            string sManfacturer = "";
            string sModel = "";
            string sSoftwareVersion = "";
            string sModality = "";
            string _dcmInstanceId = UnitId.Get(context);
            int nDcmInstanceId = Convert.ToInt32(_dcmInstanceId);

            context.GetExtension<CompositionContainer>().SatisfyImportsOnce(this);
            try
            {
                _processSpatialRegistrationObject.RetrieveSpatialRegistrationDetails(nDcmInstanceId, ref nPatientID, ref sManfacturer, ref sModel, ref sSoftwareVersion, ref sModality, ref sSpatialRegistrationObjectType);
                if (nPatientID > 0)
                {
                    localStatus = "SUCCESS";
                }
                SpatialRegistrationObjectType.Set(context, sSpatialRegistrationObjectType);
                PatientID.Set(context, nPatientID);
                Manufacturer.Set(context, sManfacturer);
                Model.Set(context, sModel);
                SoftwareVersion.Set(context, sSoftwareVersion);
                Modality.Set(context, sModality);
                ActivityStatus.Set(context, localStatus);
            }
            catch (Exception e)
            {
                ActivityStatus.Set(context, localStatus);                
            }
        }
    }
}
