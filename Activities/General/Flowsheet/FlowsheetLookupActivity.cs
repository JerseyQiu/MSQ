using System;
using System.Activities;
using IdeaBlade.Persistence;
using IdeaBlade.Persistence.Rdb;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Designer;
using Microsoft.VisualBasic.Activities;

namespace Impac.Mosaiq.IQ.Activities.General.Flowsheet
{
    #region Enumerations
    /// <summary>
    /// Determines whether to pull the earliest or latest record for this patient.
    /// </summary>
    [Serializable]
    public enum FlowsheetLookupMethod
    {
        /// <summary>Pulls the first observation for the selected definition</summary>
        Earliest = 1,

        /// <summary>Pulls the most recent observation for the selected definition</summary>
        Latest = 2,

        /// <summary>Pulls the observation associated with the passed in OBR_ID</summary>
        ByObrId = 3
    }
    #endregion

    /// <summary>
    /// This activity takes as input an ObsDef reference and looks up either the earliest or latest
    /// value from the given patient's observations.
    /// </summary>
    [FlowsheetLookupActivity_DisplayName]
    [Flowsheet_Category]
    [GeneralCharting_ActivityGroup]
    public class FlowsheetLookupActivity : MosaiqCodeActivity
    {
        #region Static Constructor
        static FlowsheetLookupActivity()
        {
            //Guarantees a new script will have this import.  Important so that the code created in the constructor
            //has the appropriate reference already in the list.
            ImportsUtil.AddImportReference(typeof(FlowsheetLookupMethod));
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Ctor
        /// </summary>
        public FlowsheetLookupActivity()
        {
            //Initialize the lookup method
            var lookupMethodExpr = new VisualBasicValue<FlowsheetLookupMethod>("FlowsheetLookupMethod.Earliest");
            LookupMethod = new InArgument<FlowsheetLookupMethod>(lookupMethodExpr);
        }
        #endregion

        #region Private Fields (Constants)
        private const string OBR_ID_QUERY =
            @"select OBS.* from Observe OBS
	        inner join ObsReq OBR on (OBS.OBR_SET_ID = OBR.OBR_SET_ID)
            where OBS.Pat_Id1 = {0} and OBS.obd_id = {1} and OBR.OBR_ID = {2}";

        private const string EARLIEST_QUERY =
            @"select top 1 OBS.* from Observe OBS
	        inner join ObsReq OBR on (OBS.OBR_SET_ID = OBR.OBR_SET_ID)
            where OBS.Pat_Id1 = {0} and OBS.obd_id = {1} and OBR.Version = 0 and OBR.Status_Enum <> 1
            order by Obs_DtTm asc";

        private const string LATEST_QUERY =
            @"select top 1 OBS.* from Observe OBS
	        inner join ObsReq OBR on (OBS.OBR_SET_ID = OBR.OBR_SET_ID)
            where OBS.Pat_Id1 = {0} and OBS.obd_id = {1} and OBR.Version = 0 and OBR.Status_Enum <> 1
            order by Obs_DtTm desc";
        #endregion

        #region Properties (Input Parameters)
        /// <summary>
        ///  The Obd_Guid of the data/table item being looked up
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FlowsheetLookupActivity_ObdGuid_DisplayName]
        [FlowsheetLookupActivity_ObdGuid_Description]
        public InArgument<Guid> ObdGuid { get; set; }

        /// <summary>
        /// The patient for whom the value is being looked up.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FlowsheetLookupActivity_PatId1_DisplayName]
        [FlowsheetLookupActivity_PatId1_Description]
        public InArgument<int> PatId1 { get; set; }

        /// <summary>
        /// The lookup method which will be used
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FlowsheetLookupActivity_LookupMethod_DisplayName]
        [FlowsheetLookupActivity_LookupMethod_Description]
        public InArgument<FlowsheetLookupMethod> LookupMethod { get; set; }

        /// <summary>
        /// The primary key of the ObsReq record from which we will look for a value.
        /// </summary>
        [InputParameterCategory]
        [FlowsheetLookupActivity_ObrId_DisplayName]
        [FlowsheetLookupActivity_ObrId_Description]
        public InArgument<int> ObrId { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary>
        /// The date and time the observation value was recorded.
        /// </summary>
        [OutputParameterCategory]
        [FlowsheetLookupActivity_ObsDtTm_DisplayName]
        [FlowsheetLookupActivity_ObsDtTm_Description]
        public OutArgument<DateTime?> ObsDtTm { get; set; }

        /// <summary>
        /// The number of days since this observation was taken.
        /// </summary>
        [OutputParameterCategory]
        [FlowsheetLookupActivity_AgeInDays_DisplayName]
        [FlowsheetLookupActivity_AgeInDays_Description]
        public OutArgument<double?> AgeInDays { get; set; }

        /// <summary>
        /// The number of hours since this observation was taken.
        /// </summary>
        [OutputParameterCategory]
        [FlowsheetLookupActivity_AgeInHours_DisplayName]
        [FlowsheetLookupActivity_AgeInHours_Description]
        public OutArgument<double?> AgeInHours { get; set; }

        /// <summary>
        /// Returns a value determining if a result was found.
        /// </summary>
        [OutputParameterCategory]
        [FlowsheetLookupActivity_ObsResultFound_DisplayName]
        [FlowsheetLookupActivity_ObsResultFound_Description]
        public OutArgument<bool> ObsResultFound { get; set; }

        /// <summary>
        /// The return type of the observation.
        /// </summary>
        [OutputParameterCategory]
        [FlowsheetLookupActivity_ObsDataValueFormat_DisplayName]
        [FlowsheetLookupActivity_ObsDataValueFormat_Description]
        public OutArgument<ObsDefDataFormat> ObsDataValueFormat { get; set; }

        /// <summary>
        /// The observation value looked up if it is a numeric type.  Null otherwise
        /// </summary>
        [OutputParameterCategory]
        [FlowsheetLookupActivity_ObsNumericValue_DisplayName]
        [FlowsheetLookupActivity_ObsNumericValue_Description]
        public OutArgument<double?> ObsNumericValue { get; set; }

        /// <summary>
        /// The observation value looked up if it is a string type.  Null otherwise
        /// </summary>
        [OutputParameterCategory]
        [FlowsheetLookupActivity_ObsStringValue_DisplayName]
        [FlowsheetLookupActivity_ObsStringValue_Description]
        public OutArgument<string> ObsStringValue { get; set; }

        /// <summary>
        /// The observation value looked up if the data type is either a date or a time.  Null otherwise
        /// </summary>
        [OutputParameterCategory]
        [FlowsheetLookupActivity_ObsDateTimeValue_DisplayName]
        [FlowsheetLookupActivity_ObsDateTimeValue_Description]
        public OutArgument<DateTime?> ObsDateTimeValue { get; set; }

        /// <summary>
        /// The observation value looked up if the data type is a true/false (checkbox) value.  Null otherwise.
        /// </summary>
        [OutputParameterCategory]
        [FlowsheetLookupActivity_ObsCheckboxValue_DisplayName]
        [FlowsheetLookupActivity_ObsCheckboxValue_Description]
        public OutArgument<bool?> ObsCheckboxValue { get; set; }

        /// <summary>
        /// The observation value looked up if the data type is a true/false (checkbox) value.  Null otherwise.
        /// </summary>
        [OutputParameterCategory]
        [FlowsheetLookupActivity_ObsChoiceValue_DisplayName]
        [FlowsheetLookupActivity_ObsChoiceValue_Description]
        public OutArgument<Guid?> ObsChoiceValue { get; set; }

		/// <summary>
		/// The primary key of the ObsReq record from which we will look for a value.
		/// </summary>
		[OutputParameterCategory]
		[FlowsheetLookupActivity_ObrId_DisplayName]
		[FlowsheetLookupActivity_ObrId_Description]
		public OutArgument<int?> ObrIdValue { get; set; }

        #endregion

        #region Overriden Methods
        /// <summary>
        /// Execute the activity and look up the flowsheet value.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override void DoWork(CodeActivityContext context)
        {
            //Get Variables
            Guid obdGuid = ObdGuid.Get(context);
            int? patId1 = PatId1.Expression != null ? PatId1.Get(context) : (int?) null;
            int? obrId = ObrId.Expression != null ? ObrId.Get(context) : (int?) null;
            FlowsheetLookupMethod lookupMethod = LookupMethod.Expression != null
                                               ? LookupMethod.Get(context)
                                               : FlowsheetLookupMethod.Earliest;
            
            //Lookup entities
            ObsDef obsDefEntity = ObsDef.GetEntityByObdGuid(obdGuid, PM);
            ObsDataValueFormat.Set(context, obsDefEntity.ObsDefDataFormat);

            string queryString;
            switch (lookupMethod)
            {
                case FlowsheetLookupMethod.Earliest:
                    queryString = String.Format(EARLIEST_QUERY, patId1.GetValueOrDefault(0),
                                          obsDefEntity.OBD_ID);
                    break;

                case FlowsheetLookupMethod.Latest:
                    queryString = String.Format(LATEST_QUERY, patId1.GetValueOrDefault(0),
                                          obsDefEntity.OBD_ID);
                    break;


                case FlowsheetLookupMethod.ByObrId:
                    queryString = String.Format(OBR_ID_QUERY, patId1.GetValueOrDefault(0),
                                          obsDefEntity.OBD_ID, obrId.GetValueOrDefault(0));
                    break;
                default:
                    throw new InvalidOperationException("Invalid Lookup Operation");
            }

            var pQuery = new PassthruRdbQuery(typeof(Observe), queryString);
            var result = PM.GetEntity<Observe>(pQuery, QueryStrategy.DataSourceOnly);

            //If we didn't find any observations
            if (result == result.NullEntity)
            {
                ObsResultFound.Set(context, false);
                return;
            }

            //Assign output information.
            ObsResultFound.Set(context, true);
            ObsDtTm.Set(context, result.ObsReqTip.Obs_DtTm);
			ObrIdValue.Set(context, result.OBR_SET_ID);

            if (result.ObsReqTip.Obs_DtTm != null)
            {
                // Recalculating for hours since don't want to mess with day calculation
                TimeSpan span = DateTime.Now - result.ObsReqTip.Obs_DtTm.Value;
                AgeInDays.Set(context, span.TotalDays);
                AgeInHours.Set(context, span.TotalHours);
            }

            //Always populate ObsString.  Lab interface may populate this even if the ObsDef type is something
            //different.
            ObsStringValue.Set(context, result.Obs_String);
            if (result.ObsDefEntityOnObsChoice != null && !result.ObsDefEntityOnObsChoice.IsNullEntity)
                ObsChoiceValue.Set(context, result.ObsDefEntityOnObsChoice.OBD_GUID);
            else
                ObsChoiceValue.Set(context, null);

            //Set the appropriate output value.
            switch (result.ObsDefEntity.ObsDefDataFormat)
            {
                case ObsDefDataFormat.CheckBox:
                    ObsCheckboxValue.Set(context, (result.Obs_Float == 0) ? false : true);
                    break;

                case ObsDefDataFormat.Date:
                    ObsDateTimeValue.Set(context, ClarionConversions.DateToDateTime(result.Obs_Float));
                    break;

                case ObsDefDataFormat.Time:
                    ObsDateTimeValue.Set(context, ClarionConversions.TimeToDateTime(result.Obs_Float));
                    break;

                case ObsDefDataFormat.Numeric:
                    ObsNumericValue.Set(context, result.Obs_Float);
                    break;

                case ObsDefDataFormat.Choice:
                case ObsDefDataFormat.String:
				case ObsDefDataFormat.Memo:
                    //Do nothing.  Already string and choice already set set above the switch statement
                    break;

                default:
                    throw new InvalidOperationException(
                        String.Format(Strings.Error_InvalidObsDefDataFormat,
                            result.ObsDefEntity.ObsDefDataFormat));
            }
        }
        #endregion
    }
}
