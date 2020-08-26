using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Activities;
using System.Collections.Generic;
using System.Globalization;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using IdeaBlade.Persistence;
using Impac.Mosaiq.Core.Defs.Constants;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.Core.Defs.Enumerations;
using Impac.Mosaiq.UI.InputTemplates.DateTimeTemplates.InputDateRange;
using Impac.Mosaiq.Widgets.MedOnc.LabResults;

namespace Impac.Mosaiq.IQ.Activities.General.Escribe
{
	/// <summary>
	/// Implements the flowsheet merge fields - Labs, Vitals and Assessments
	/// </summary>
	[GeneralCharting_ActivityGroup]
	[Escribe_Category]
	[FlowsheetMergeFieldActivity_DisplayName]
	public class FlowsheetMergeFieldActivity : MosaiqCodeActivity
	{
		/// <summary>
		/// A list of string values configured by ESI that could be stored in Observe.Obs_String 
		/// to indicate that the actual imported value is stored in a Notes table record
		/// </summary>
		private static string [] _esiNoteIndicators;

		/// <summary>
		/// The default ADO timeout of 30 seconds is too small in some cases, so it is increased to 120s.
		/// This value can be overridden with an Impac.ini key.
		/// </summary>
		private static int _queryTimeout = 120;

        #region Properties (Input Parameters)
		/// <summary></summary>
		[InputParameterCategory]
		[RequiredArgument]
		[FlowsheetMergeFieldActivity_PatId1_DisplayName]
		[FlowsheetMergeFieldActivity_PatId1_Description]
		public InArgument<int> PatId1 { get; set; }

		/// <summary>
		/// An ordered list of ObdGuids that need to be included in the result.
		/// The list includes, major and minor headings, tables(enums), data items and functions
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[FlowsheetMergeFieldActivity_SelectedItems_DisplayName]
		[FlowsheetMergeFieldActivity_SelectedItems_Description]
		public InArgument<FlowsheetSelection> SelectedItems { get; set; }

		/// <summary>
		/// Values filter: 1 - all, 2 - abnormal only, 3 - critical only
		/// For Assessments: 1- all, 2- all marked 'required', 3 - all with ranges 'outside of range'
		/// TODO: handle enums better
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[FlowsheetMergeFieldActivity_Values_DisplayName]
		[FlowsheetMergeFieldActivity_Values_Description]
		public InArgument<IQEnum> Values { get; set; }

        /// <summary>
        /// Hide if blank: 0 - don't hide, 1 - rows (data items) that have no data, 2 - rows & folders
        /// </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [FlowsheetMergeFieldActivity_HideIfBlank_DisplayName]
        [FlowsheetMergeFieldActivity_HideIfBlank_Description]
        public InArgument<IQEnum> HideIfBlank { get; set; }

		/// <summary>
        /// Output format: Flowsheet = 1, Table = 2, List = 3, Paragraph = 4, FlowsheetNoLines = 5, TableNoLines = 6, ListNoHeader = 7, ParagraphNoHeader = 8, TableNoHeader = 9, TableNoLinesNoHeader = 10
		/// TODO: handle enums better
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[FlowsheetMergeFieldActivity_OutputFormat_DisplayName]
		[FlowsheetMergeFieldActivity_OutputFormat_Description]
		public InArgument<IQEnum> OutputFormat { get; set; }

		/// <summary>
		/// Display the record status info: 1 - no info, 2 - initials and date, 3 - full name, designation and date
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[FlowsheetMergeFieldActivity_StatusInfo_DisplayName]
		[FlowsheetMergeFieldActivity_StatusInfo_Description]
		public InArgument<IQEnum> StatusInfo { get; set; }

		/// <summary>
		/// The display format for 'table' items (ObsDef.Type == 5)
		/// 1 - Label, 2 - Description, 3 - Label and description
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[FlowsheetMergeFieldActivity_TableItemsFormat_DisplayName]
		[FlowsheetMergeFieldActivity_TableItemsFormat_Description]
		public InArgument<IQEnum> TableItemsFormat { get; set; }

		/// <summary>
		/// The display format for the data items captions
		/// 1 - Label, 2 - Description, 3 - Label and description
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[FlowsheetMergeFieldActivity_DataItemsFormat_DisplayName]
		[FlowsheetMergeFieldActivity_DataItemsFormat_Description]
		public InArgument<IQEnum> DataItemsFormat { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[FlowsheetMergeFieldActivity_IncludeInstructions_DisplayName]
		[FlowsheetMergeFieldActivity_IncludeInstructions_Description]
		public InArgument<bool> IncludeInstructions { get; set; }

		/// <summary>
		/// Can be a standard date range or a custom one
        /// The custom ranges are: 1 - most recent, 2 - since last encounter, 3 - Last N records, 4 - Last N Records per Item
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[FlowsheetMergeFieldActivity_DateRange_DisplayName]
		[FlowsheetMergeFieldActivity_DateRange_Description]
		public InArgument<IQDateRangeVar> DateRange { get; set; }

        /// <summary>
        /// Number of records to retrieve
        /// Input valid only for Custom Date Ranges "Last N Records" and "Last N Records per Item"
        /// </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [FlowsheetMergeFieldActivity_NumberOfRecords_DisplayName]
        [FlowsheetMergeFieldActivity_NumberOfRecords_Description]
        public InArgument<IQIntegerVar> NumberOfRecords { get; set; }

        /// <summary>
        /// Show Record Date next to data item value
        /// Only applicable in List and Paragraph OutputFormats
        /// </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [FlowsheetMergeFieldActivity_ShowDate_DisplayName]
        [FlowsheetMergeFieldActivity_ShowDate_Description]
        public InArgument<IQEnum> ShowDate { get; set; }

		/// <summary>
		/// List of staff IDs to filter on. Can be empty or in state 'None' or 'All'
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[FlowsheetMergeFieldActivity_Staff_DisplayName]
		[FlowsheetMergeFieldActivity_Staff_Description]
		public InArgument<IQIntegerVar> Staff { get; set; }

        /// <summary>
        /// Custom delimiter for pharagraph format
        /// </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [FlowsheetMergeFieldActivity_ParagraphDelimiter_DisplayName]
        [FlowsheetMergeFieldActivity_ParagraphDelimiter_Description]
        public InArgument<string> ParagraphDelimiter { get; set; }

        /// <summary>
        /// ObsReq set ids to display specific data (used for displaying user defined data for diagnosis)
        /// </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [FlowsheetMergeFieldActivity_ObrSetIds_DisplayName]
        [FlowsheetMergeFieldActivity_ObrSetIds_Description]
        public InArgument<List<int>> ObrSetIds { get; set; }

        /// <summary> </summary>
        [InputParameterCategory]
        [FlowsheetMergeFieldActivity_ShowOrder_DisplayName]
        [FlowsheetMergeFieldActivity_ShowOrder_Description]
        public InArgument<bool> ShowOrder { get; set; }

        [InputParameterCategory]
        [FlowsheetMergeFieldActivity_ShowVoided_DisplayName]
        [FlowsheetMergeFieldActivity_ShowVoided_Description]
        public InArgument<bool> ShowVoided { get; set; }
        #endregion Properties (Input Parameters)

        #region Properties (Output Parameters)
        /// <summary> If the output format is 'flowsheet' or 'table' it is stored here in html format </summary>
		[OutputParameterCategory]
		[FlowsheetMergeFieldActivity_Html_DisplayName]
		[FlowsheetMergeFieldActivity_Html_Description]
		public OutArgument<string> Html { get; set; }

		/// <summary> If the output format is 'list' or 'paragraph' it is stored here as plain text</summary>
		[OutputParameterCategory]
		[FlowsheetMergeFieldActivity_PlainText_DisplayName]
		[FlowsheetMergeFieldActivity_PlainText_Description]
		public OutArgument<string> PlainText { get; set; }

        /// <summary> List of ObsReq values  </summary>
        [OutputParameterCategory]
        [FlowsheetMergeFieldActivity_ObsReqsByDate_DisplayName]
        [FlowsheetMergeFieldActivity_ObsReqsByDate_Description]
        public OutArgument<object> ObsReqsByDate { get; set; }
		#endregion

		private MergeFieldUtility.FlowsheetOutputFormats _outputFormat;

		#region Overrides of MosaiqCodeActivity
		/// <summary>
		/// </summary>
		protected override void DoWork(CodeActivityContext context)
		{
			PlainText.Set(context, null);
			Html.Set(context, null);
			FlowsheetSelection selectedItems = SelectedItems.Get(context);
			if (selectedItems == null)
			{
                PlainText.Set(context, MergeFieldUtility.NO_DATA);
				return;
			}

			var noteIndicators = Mosaiq.Core.Toolbox.FileUtilities.INIFile.ImpacINIFile.IniReadStringValue(
				"Global", "EsiNoteIndicators", "See note");
			_esiNoteIndicators = !String.IsNullOrEmpty(noteIndicators)
			                     	? noteIndicators.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
			                     	: new string[0];

			var timeout = Mosaiq.Core.Toolbox.FileUtilities.INIFile.ImpacINIFile.IniReadIntValue(
				"Escribe", "FlowsheetQueryTimeout", 0);
			if (timeout > 0)
				_queryTimeout = timeout;

			var pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
			// Read the ObsDef items for the requested ObdGuids
			var obsDefs = ReadObsDefs(pm, selectedItems);
			var obdIds = obsDefs.Select(o => o.OBD_ID).ToList();

			var query = new ImpacRdbQuery(typeof(ObsDef));
            var linkedObsDefs = GetObsDefEntities(pm, query, ObsDefDataRow.Link_OBD_IDEntityColumn, obdIds, MergeFieldUtility.QUERY_SEGMENT_LENGTH);

			// Read the observations for the ObsDef items, filtered by PatId1 and Date
            bool showVoided = ShowVoided.Get(context);
			var observations = ReadObservations(pm, obdIds, linkedObsDefs, 
		            PatId1.Get(context), DateRange.Get(context), Staff.Get(context), ObrSetIds.Get(context), showVoided);

			// Handle ESI-imported historical entries
			var args = observations.Select(
				entity => new Tuple<int, int?, ObsReq>(entity.OBX_ID, entity.OBD_ID, entity.ObsReqEntityOnObrSet)).ToList();
			var obxIdsToKeep = LabResults.RemoveHistoricalEntries(args);
			int idx = 0;
			while (idx < observations.Count)
			{
				var entity = observations[idx];
				if (obxIdsToKeep.ContainsKey(entity.OBX_ID))
				{
					entity.Corrected = obxIdsToKeep[entity.OBX_ID];
					idx++;
				}
				else
				{
					observations.Remove(entity);
				}
			}

			// get the ordered list of obsreq timestamps
            List<ObsReq> observationRecords = observations
				.Where(item => item.ObsReqEntityOnObrSet.Obs_DtTm.HasValue && item.ObsReqEntityOnObrSet.Obs_DtTm > SqlDateTime.MinValue.Value)
				.Select(item => item.ObsReqEntityOnObrSet)
				.Distinct()
				.OrderBy(item => item.Obs_DtTm)
				.ToList();

			int lastNPerItem = 0;
            // handle the 'most recent' date range option - use the most recent ObsReq 
            if (DateRange.Get(context).SelectedElement.ElementType == IQVarElementType.Custom)
                switch ((MergeFieldUtility.CustomDateRange)DateRange.Get(context).SelectedElement.CustomValue.Key)
                {
                    case MergeFieldUtility.CustomDateRange.MostRecent:
                        observationRecords = MergeFieldUtility.TrimPreviousRecords(observationRecords, 1);
                        break;
                    case MergeFieldUtility.CustomDateRange.LastN:
                        observationRecords = MergeFieldUtility.TrimPreviousRecords(observationRecords, NumberOfRecords.Get(context).SelectedElement.Value);
                        break;
                    case MergeFieldUtility.CustomDateRange.LastNPerItem:
                        observationRecords.Sort(ObsReq.CompareObsReqByDtTm); //sort here so unneeded records can be discarded in GenerateTable
                        lastNPerItem = NumberOfRecords.Get(context).SelectedElement.Value;
                        break;
                    default:
                        //Do nothing for CustomDateRange.SinceLastEncounter
                        break;
                }

            // process the observations and generate a table with all the data            
			List<List<TableItem>> dataTable = GenerateTable(context, pm, obsDefs, linkedObsDefs, observationRecords, observations, lastNPerItem);
            var observationDates = observationRecords.Select(item => item.Obs_DtTm ?? SqlDateTime.MinValue.Value).ToList(); //get dates after GenerateTable because observationRecords is trimmed for CustomDateRange.LastNPerItem

			_outputFormat = (MergeFieldUtility.FlowsheetOutputFormats)OutputFormat.Get(context).Key;
			var dataItemsFormat = (MergeFieldUtility.DataItemsFormats) DataItemsFormat.Get(context).Key;

			// handle the 'include instructions' parameter - if we need to include instructions we need the View_ID 
			// in order to be able to query for the actual ObsDef items that contain the instructions
			int viewId = 0;
			bool includeInstructions = IncludeInstructions.Get(context);
			if (includeInstructions)
			{
				ObsDef view = ObsDef.GetEntityByObdGuid(selectedItems.ViewGuid, pm);
				viewId = view.View_ID;
			}

			// Create a list of ObsDefInfo items
			var obsDefInfoList = new List<ObsDefInfo>();
			foreach (var obsDef in obsDefs)
			{
				var obsDefInfo = new ObsDefInfo {ItemType = obsDef.Type, DataFlags = obsDef.ObsDefDataFormat};

				// display the 'Other Labs' function as a special item
				if (obsDef.OBD_GUID == MergeFieldUtility.OtherLabsItemGuid)
					obsDefInfo.ItemType = MergeFieldUtility.OBD_TYPE_NONE;

				// item label
				string label;
				switch (dataItemsFormat)
				{
					case MergeFieldUtility.DataItemsFormats.Description:
						label = obsDef.Description.Trim();
						break;
					case MergeFieldUtility.DataItemsFormats.LabelAndDescription:
						label = String.Format("{0} - {1}", obsDef.Label.Trim(), obsDef.Description.Trim());
						break;
					case MergeFieldUtility.DataItemsFormats.EscribeDescription:
						label = (obsDef.eScribeDescription.Trim().Length > 0
												 ? obsDef.eScribeDescription.Trim()
												 : obsDef.Description.Trim());
						break;
					default:
						label = obsDef.Label.Trim();
						break;
				}

				// append the units of measure if they exist
				if (obsDef.ObsDefEntityOnUnits != null && !String.IsNullOrEmpty(obsDef.ObsDefEntityOnUnits.Label))
					label += String.Format(" ({0})", obsDef.ObsDefEntityOnUnits.Label.Trim());

				obsDefInfo.Label = label;

				// instructions
				if (includeInstructions && MergeFieldUtility.IsHeadingItem(obsDef.Type))
					obsDefInfo.Instructions = ObsDef.GetInstructionsByViewIDHeaderID(viewId, obsDef.OBD_ID, pm).Trim();

				obsDefInfoList.Add(obsDefInfo);
			}

            // if Show Order is set to true, show the order info linked to this obsreq entry (via the Observe_Order table.)
            bool showOrder = ShowOrder.Get(context);
            if (showOrder && dataTable.Count > 0)   // no need to add order info if there is no data for this query result
                AddOrderInfo(observationRecords, obsDefInfoList, dataTable);

            // if the status info is required extend the collections generated so far (dataTable, itemTypes and 
            // itemLabels) with the necessary elements to add additional rows of data containing the status info
            int statusInfo = StatusInfo.Get(context).Key;
			AddStatusInfo((MergeFieldUtility.StatusInfoFormat) statusInfo, observationRecords, obsDefInfoList, dataTable);

            // convert the data table to the required output format
            string result = String.Empty;
            bool isHtml = true;
            bool showDate = (MergeFieldUtility.ShowDateFormat)ShowDate.Get(context).Key == MergeFieldUtility.ShowDateFormat.ShowShortDate;

			switch (_outputFormat)
            {
                case MergeFieldUtility.FlowsheetOutputFormats.TableNoHeader:
                case MergeFieldUtility.FlowsheetOutputFormats.TableNoLinesNoHeader:
                //showHeader = false;
                case MergeFieldUtility.FlowsheetOutputFormats.Table:
                case MergeFieldUtility.FlowsheetOutputFormats.TableNoLines:
                //invert = true;            
                    result = GenerateHtml(obsDefInfoList, observationDates, dataTable);
                    break;
                case MergeFieldUtility.FlowsheetOutputFormats.Flowsheet:
                case MergeFieldUtility.FlowsheetOutputFormats.FlowsheetNoLines:
                    // Defect 8705, do not show date when configuring it to not show the date 
                    result = GenerateHtml(obsDefInfoList, (showDate) ? observationDates : null, dataTable);
                    break;
                case MergeFieldUtility.FlowsheetOutputFormats.List:
                case MergeFieldUtility.FlowsheetOutputFormats.ListNoHeader:
					result = obsDefInfoList.Count > 0
            		         	? GenerateList(obsDefInfoList, dataTable, (showDate) ? observationDates : null)
            		         	: MergeFieldUtility.NO_DATA;
                    isHtml = false;
                    break;
                case MergeFieldUtility.FlowsheetOutputFormats.Paragraph:
                case MergeFieldUtility.FlowsheetOutputFormats.ParagraphNoHeader:
                    string delimiter = ParagraphDelimiter.Get(context);
					result = obsDefInfoList.Count > 0
            		         	? GenerateParagraph(obsDefInfoList, dataTable,
													(showDate) ? observationDates : null, delimiter)
            		         	: MergeFieldUtility.NO_DATA;
                    isHtml = false;
                    break;
            }

			if (isHtml)
				Html.Set(context, result);
			else
				PlainText.Set(context, result);

            //Dictionary<DateTime, ObsReq> reqStats = observationRecords.ToDictionary(itm => itm.Obs_DtTm ?? SqlDateTime.MinValue.Value, itm => itm);
            ObsReqsByDate.Set(context, observationRecords); //return list of ObsReq dates with status values
		}
		#endregion

		#region Read the data from ObsDef, ObsReq and Observe
		/// <summary>
		/// Read the ObsDef items for the requested ObdGuids
		/// </summary>
		private static List<ObsDef> ReadObsDefs(ImpacPersistenceManager pm, FlowsheetSelection selectedItems)
		{
			var query = new ImpacRdbQuery(typeof (ObsDef));
			var result = new List<ObsDef>();
			var obdGuids = selectedItems.ItemGuids;

            var obsDefEntities = GetObsDefEntities(pm, query, ObsDefDataRow.OBD_GUIDEntityColumn, obdGuids, MergeFieldUtility.QUERY_SEGMENT_LENGTH_GUID);
			result.AddRange(obsDefEntities);

			// sort the items in the same order of IDs as in the obdIds list
			var sortedObsDefs = obdGuids
				.Select(guid => result.Find(o => o.OBD_GUID == guid))
				.Where(o => o != null) // DR 45006: If the script has references to non existing (i.e. deleted) data items
				.ToList(); // the Select() operator will generate null items for them - those need to be filtered out

			// check if the have the 'Other Labs' function
			if (obdGuids.IndexOf(MergeFieldUtility.OtherLabsItemGuid) != -1)
			{
				// get all data items that belong to the current view - the Other Labs folder should not 
				// display them even if the user unselected them in the Flowsheet Item Selector window
				ObsDef view = ObsDef.GetEntityByObdGuid(selectedItems.ViewGuid, pm);

				query.Clear();
				query.AddClause(ObsDefDataRow.TypeEntityColumn, EntityQueryOp.EQ, 12);
				query.AddClause(ObsDefDataRow.View_IDEntityColumn, EntityQueryOp.EQ, view.View_ID);
				var ids = pm.GetEntities<ObsDef>(query).Select(o => o.Item_ID).ToList();

                var obsDefs = GetObsDefEntities(pm, query, ObsDefDataRow.OBD_IDEntityColumn, ids, MergeFieldUtility.QUERY_SEGMENT_LENGTH);
				var viewItemsObdIds = obsDefs.Select(od => od.OBD_ID).ToList();
                
				// get all data items marked as 'Labs'
				query.Clear();
				query.AddClause(ObsDefDataRow.DataItemTypeEntityColumn, EntityQueryOp.EQ, (short)StdDefs.DataItemType.Lab);
				var otherLabsDefs = pm.GetEntities<ObsDef>(query);
 				var otherLabsObdIds = otherLabsDefs.Select(ol => ol.OBD_ID).ToList();
				
				foreach (var obsDef in otherLabsDefs)
				{
					// filter out items that are present in the view
					if (viewItemsObdIds.IndexOf(obsDef.OBD_ID) >= 0)
						continue;

					// filter out items that are linked to items present in the view
					if (obsDef.Link_OBD_ID.HasValue)
					{
						var linkObdId = obsDef.Link_OBD_ID.Value;
						if (viewItemsObdIds.IndexOf(linkObdId) >= 0 || otherLabsObdIds.IndexOf(linkObdId) >= 0)
							continue;
					}
					sortedObsDefs.Add(obsDef);
				}
			}
			return sortedObsDefs;
		}

		/// <summary>
		/// Read the observations for the ObsDef items, filtered by PatId1 and Date
		/// </summary>
		private static EntityList<Observe> ReadObservations(ImpacPersistenceManager pm, IEnumerable<int> obdIds, IEnumerable<ObsDef> linkedObsDefs, 
			int patId1, IQDateRangeVar dateRange, IQIntegerVar staff, List<int> obrSetIds, bool showVoided)
		{
			// Reading the observations with a combined Observe query and an ObsReq subquery generates a very inefficient SQL statement,
			// so instead of that we use two separate queries

			// if specific OBR_SET_ID value is given display only the records for this set, otherwise get the list of applicable OBR_Set_IDs
			if (obrSetIds == null || obrSetIds.Count == 0)
				obrSetIds = ReadObsReqs(pm, patId1, dateRange, staff, showVoided)
					.Select(obr => obr.OBR_Set_ID ?? 0)
					.ToList();

			// combine the requested OBD_IDs with any others linked to them
			var allObdIds = new List<int>();
			allObdIds.AddRange(obdIds);
			allObdIds.AddRange(linkedObsDefs.Select(o => o.OBD_ID).ToList());
			allObdIds = allObdIds.Distinct().ToList();

			int segments = allObdIds.Count / MergeFieldUtility.QUERY_SEGMENT_LENGTH;
			int remaining = allObdIds.Count % MergeFieldUtility.QUERY_SEGMENT_LENGTH;

			var query = new ImpacRdbQuery(typeof (Observe));
			var result = new EntityList<Observe>();

			for (int i = 0; i < segments; i++)
				result.AddRange(ReadObservationsSegment(pm, query, 
					allObdIds.GetRange(i * MergeFieldUtility.QUERY_SEGMENT_LENGTH, MergeFieldUtility.QUERY_SEGMENT_LENGTH), 
					patId1, obrSetIds));

			if (remaining > 0)
				result.AddRange(ReadObservationsSegment(pm, query, 
					allObdIds.GetRange(segments * MergeFieldUtility.QUERY_SEGMENT_LENGTH, remaining), 
					patId1, obrSetIds));

			return result;
		}

		private static IEnumerable<ObsReq> ReadObsReqs(ImpacPersistenceManager pm, int patId1, IQDateRangeVar dateRange, IQIntegerVar staff, bool showVoided)
		{
			var query = new ImpacRdbQuery(typeof(ObsReq));
			query.AddClause(ObsReqDataRow.VersionEntityColumn, EntityQueryOp.EQ, 0);
			query.AddClause(ObsReqDataRow.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);

			// exclude the voided items
			if (!showVoided)
				query.AddClause(ObsReqDataRow.Status_EnumEntityColumn, EntityQueryOp.NE, (byte)BOM.Entities.Defs.StatusEnum.Void);

			// If a list of staff ids was provided, add the staff filter
			if (staff.State == IQVarState.Selected && staff.SelectedValuesAll.Count > 0)
				query.AddClause(ObsReqDataRow.Edit_IDEntityColumn, EntityQueryOp.In, staff.SelectedValuesAll);

			// add the date filter
			if (dateRange.SelectedElement.ElementType == IQVarElementType.Custom)
			{
				if ((MergeFieldUtility.CustomDateRange) dateRange.SelectedElement.CustomValue.Key == MergeFieldUtility.CustomDateRange.SinceLastEncounter)
				{
					// handle 'since last encounter'
					Patient patient = Patient.GetEntityByID(patId1);
					if (!patient.IsNullEntity && patient.LastPatientVisitDate.HasValue && patient.LastPatientVisitDate > DateTime.MinValue)
						query.AddClause(ObsReqDataRow.Obs_DtTmEntityColumn, EntityQueryOp.GT, patient.LastPatientVisitDate);
				}
				//((CustomDateRange)dateRange.SelectedElement.CustomValue.Key == CustomDateRange.MostRecent), CustomDateRange.LastN, CustomDateRange.LastNPerItem handled later
			}
			else if (dateRange.SelectedElement.ElementType == IQVarElementType.Standard 
				&& dateRange.SelectedElement.RangeType != (int)InputDateRangeControlType.All)
			{
				DateTime? startDate = dateRange.SelectedElement.Value.StartDate;
				if (startDate.HasValue && startDate > SqlDateTime.MinValue.Value)
					query.AddClause(ObsReqDataRow.Obs_DtTmEntityColumn, EntityQueryOp.GE, startDate);

				DateTime? endDate = dateRange.SelectedElement.Value.EndDate;
				if (endDate.HasValue && endDate < SqlDateTime.MaxValue.Value)
					query.AddClause(ObsReqDataRow.Obs_DtTmEntityColumn, EntityQueryOp.LE, endDate);
			}

			var obsReqs = pm.GetEntities<ObsReq>(query);
			return obsReqs.Where(obr => obr.OBR_Set_ID.HasValue).ToList();
		}

		private static IEnumerable<Observe> ReadObservationsSegment(ImpacPersistenceManager pm, ImpacRdbQuery query, 
			IEnumerable<int> obdIds, int patId1, List<int> obrSetIds)
		{
			int segments = obrSetIds.Count / MergeFieldUtility.QUERY_SEGMENT_LENGTH;
			int remaining = obrSetIds.Count % MergeFieldUtility.QUERY_SEGMENT_LENGTH;

			var result = new EntityList<Observe>();

			for (int i = 0; i < segments; i++)
			{
				query.Clear();
				query.AddClause(ObserveDataRow.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);
				query.AddClause(ObserveDataRow.OBD_IDEntityColumn, EntityQueryOp.In, obdIds);
				query.AddClause(ObserveDataRow.OBR_SET_IDEntityColumn, EntityQueryOp.In,
					obrSetIds.GetRange(i * MergeFieldUtility.QUERY_SEGMENT_LENGTH, MergeFieldUtility.QUERY_SEGMENT_LENGTH));

				if (_queryTimeout > 0)
					query.CommandTimeout = _queryTimeout;
				result.AddRange(pm.GetEntities<Observe>(query));
			}

			if (remaining > 0)
			{
				query.Clear();
				query.AddClause(ObserveDataRow.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);
				query.AddClause(ObserveDataRow.OBD_IDEntityColumn, EntityQueryOp.In, obdIds);
				query.AddClause(ObserveDataRow.OBR_SET_IDEntityColumn, EntityQueryOp.In,
					obrSetIds.GetRange(segments * MergeFieldUtility.QUERY_SEGMENT_LENGTH, remaining));

				if (_queryTimeout > 0)
					query.CommandTimeout = _queryTimeout;
				result.AddRange(pm.GetEntities<Observe>(query));
			}

			return result;
		}

		private static IEnumerable<ObsDef> GetObsDefEntities<T>(ImpacPersistenceManager pm, ImpacRdbQuery query,
            EntityColumn entityColumn, List<T> ids, int lengthForOneQuery)
		{
			var result = new List<ObsDef>();

			// It looks like the query expression is limited to 8000 chars. When the list of ids is long 
			// we may hit that limit, which results in an exception from Ideablade. For that reason
			// we split the list into 50-item long segments and handle each of them separately

            //Defect 10317, 'Always use 50 to split' is part of the performance issue. Use 100 for GUID, and 300 for INT. 300 is based on function of ImpacPersistenceManager.GetEntitiesMatchingListValues

            int segments = ids.Count / lengthForOneQuery;
            int remaining = ids.Count % lengthForOneQuery;

			for (int i = 0; i < segments; i++)
			{
				query.Clear();
				query.AddClause(entityColumn, EntityQueryOp.In,
                                ids.GetRange(i * lengthForOneQuery, lengthForOneQuery));
				var obsDefs = pm.GetEntities<ObsDef>(query);
				result.AddRange(obsDefs);
			}

			if (remaining > 0)
			{
				query.Clear();
				query.AddClause(entityColumn, EntityQueryOp.In,
                                ids.GetRange(segments * lengthForOneQuery, remaining));
				var obsDefs = pm.GetEntities<ObsDef>(query);
				result.AddRange(obsDefs);
			}
			return result;
		}
		#endregion

		#region Generate a table structure containing the data
		class TableItem
		{
			public string Value { get; set; }
			public string WarningIndicator { get; set; }
			public bool RightJustify { get; set; }
		    public string ReportedByPatient { get; set; }

		    public bool IsNormal()
			{
				return String.IsNullOrEmpty(WarningIndicator);
			}

			public bool IsWarning()
			{
				return WarningIndicator == ObsDef.Indicator_WarningLow || WarningIndicator == ObsDef.Indicator_WarningHigh;
			}

			public bool IsPanic()
			{
				return WarningIndicator == ObsDef.Indicator_PanicLow || WarningIndicator == ObsDef.Indicator_PanicHigh;
			}

			public bool IsOutOfRange()
			{
				return WarningIndicator == ObsDef.Indicator_OutOfRange_Low || WarningIndicator == ObsDef.Indicator_OutOfRange_High;
			}

		    public override string ToString()
			{
				return Value;
			}

		}

		/// <summary>
		/// Generate a table having separate rows for each ObsDef item and separate columns for each observation date.
		/// The observation items are converted to table items filling in the appropriate cells. Not all cells are filled
		/// </summary>
		private List<List<TableItem>> GenerateTable(CodeActivityContext context, ImpacPersistenceManager pm, 
			IList<ObsDef> obsDefItems, IEnumerable<ObsDef> linkedObsDefItems, 
			IList<ObsReq> observationRecords, IEnumerable<Observe> observationItems, int lastNPerItem)
		{
			// read some additional input arguments
            var hideBlankRows = (MergeFieldUtility.HideFormats)HideIfBlank.Get(context).Key != MergeFieldUtility.HideFormats.DoNotHide;
            var hideBlankFolders = (MergeFieldUtility.HideFormats)HideIfBlank.Get(context).Key
                                   == MergeFieldUtility.HideFormats.HideRowsAndFolders;
            var valuesFilter = (MergeFieldUtility.ValuesFilter)Values.Get(context).Key;
            var choiceItemFormat = (MergeFieldUtility.TableItemsFormats)TableItemsFormat.Get(context).Key;

			// create a map to store the linked items to each of the requested ObsDef items
			var linkedObdIdsMap = new Dictionary<int, List<int>>();
			foreach (ObsDef obd in obsDefItems)
			{
				int oid = obd.OBD_ID;
				var linkedIds = linkedObsDefItems
					.Where(o => o.Link_OBD_ID == oid)
					.Select(o => o.OBD_ID)
					.Distinct()
					.ToList();
				linkedIds.Add(oid); // add the original item to the list
				linkedObdIdsMap[oid] = linkedIds;
			}

			if (hideBlankRows)
				RemoveEmptyObsDefs(obsDefItems, linkedObdIdsMap, observationItems);

			var table = new List<List<TableItem>>();
			foreach (var obsDef in obsDefItems)
			{
				var newRow = new List<TableItem>();
				table.Add(newRow);

				// Get a list of OBD_IDs that consists of the current item plus the items linked to it
				var obsDefObdIds = linkedObdIdsMap[obsDef.OBD_ID];

                // Defect 10317, LINQ uses deferred execution. Unless you call .ToList(), the results of a query would never be stored anywhere. Instead, it reiterates teh query every time you iterate the results.				
				var obsDefObservations = observationItems
							.Where(obs => obs.OBD_ID.HasValue && obsDefObdIds.IndexOf(obs.OBD_ID.Value) != -1).ToList();

                int obsReqCntr = 0;
				foreach (var obsReq in observationRecords)
				{
					var obsDefType = (MedDefs.ObdType) obsDef.Type;
					// The function items don't have Observe records
					if (obsDefType == MedDefs.ObdType.Function)
					{
						newRow.Add(CreateFunctionItem(obsDef, obsReq, pm));
                        obsReqCntr++; //increment number of records added to table
						continue;
					}

                    // find if there is an observation for this ObsDef item (or one of its linked items)  
                    // Defect 3207 - match the ObsReq by OBR_SET_ID, not ObsReq timestamp
					int obrSetId = obsReq.OBR_Set_ID.HasValue ? obsReq.OBR_Set_ID.Value : 0;
					var observation = obsDefObservations
							.Where(obs => obs.OBR_SET_ID == obrSetId)
							.FirstOrDefault();

					// If there is no Observe record for this data item and obsReq, put a null element in the table
					if (observation == default(Observe))
					{
						newRow.Add(null);
						continue;
					}

					TableItem newItem = null;
					if (obsDefType == MedDefs.ObdType.ItemTable)
					{
						newItem = new TableItem
						{
							WarningIndicator = observation.WarningIndicator
						};

						switch (choiceItemFormat)
						{
                            case MergeFieldUtility.TableItemsFormats.Label:
								newItem.Value = observation.ObsDefEntityOnObsChoice.Label;
								break;
                            case MergeFieldUtility.TableItemsFormats.Description:
								newItem.Value = observation.ObsDefEntityOnObsChoice.Description;
								break;
                            case MergeFieldUtility.TableItemsFormats.LabelAndDescription:
								newItem.Value = String.Format("{0}{1}{2}",
															 observation.ObsDefEntityOnObsChoice.Label,
                                                             MergeFieldUtility.LABELVALUE_LABELDESCRIPTION_SEPARATOR,
															 observation.ObsDefEntityOnObsChoice.Description);
								break;
                            case MergeFieldUtility.TableItemsFormats.EscribeDescription:
						        newItem.Value = (observation.ObsDefEntityOnObsChoice.eScribeDescription.Trim().Length > 0
						                                 ? observation.ObsDefEntityOnObsChoice.eScribeDescription.Trim()
						                                 : observation.ObsDefEntityOnObsChoice.Description.Trim());
						        break;
						}
                        if (obsReq.HasPatientReportedResults)
                            newItem.ReportedByPatient = StringConstants.INDICATOR_REPORTED_BY_PATIENT;


                    }
					else if (obsDefType == MedDefs.ObdType.ItemData)
					{
						newItem = CreateDataItem(obsDef, obsReq, observation, valuesFilter);
					}
					newRow.Add(newItem);
                    obsReqCntr++; //increment number of records added to table
				}

                if (lastNPerItem > 0) //if limiting to N per item
                {
                    //since only want latest N to real table, overwrite/backfill remainder with nulls
                    int idx;
                    //advance index until
                    // 1) through entire row
                    // 2) number of ObsReqs still existing is more than those required OR row entry is null anyway
                    for (idx = 0; idx < newRow.Count && obsReqCntr > lastNPerItem; idx++)
                        if (newRow[idx] != null)
                        {
                            newRow[idx] = null;
                            obsReqCntr--;
                        }

                    //track number of ObsReq required to meet minimum N, keep aggregate track of highest number of ObsReq required (conversely, lowest number of ObsReq to remove)
                    for (; idx < newRow.Count && obsReqCntr <= lastNPerItem; idx++)
                        if (newRow[idx] != null)
                            break; //break at first non-null data that is retained
                }
			}

            if (lastNPerItem > 0) //if limiting to N per item
            {
                //cut out excess ObsReq entries and their corresponding data items
                for (int obsReqIdx = observationRecords.Count - 1; obsReqIdx >= 0; obsReqIdx--)
                {
                    if (!ColumnIsUsed(table, obsReqIdx))
                    {
                        //cut out any excess ObsReq entries
                        observationRecords.RemoveAt(obsReqIdx);

                        //cut out corresponding excess table columns
                        foreach (List<TableItem> row in table)
                            row.RemoveAt(obsReqIdx);
                    }
                }
            }

            if (hideBlankRows)
                RemoveEmptyItems(obsDefItems, table, 0);
            if (hideBlankFolders)
                RemoveEmptyHeadings(obsDefItems, table);

			int otherLabsIdx = -1;
			for (int i = 0; i < obsDefItems.Count; i++)
			{
				var ol = obsDefItems[i];
                if (ol.OBD_GUID == MergeFieldUtility.OtherLabsItemGuid)
				{
					otherLabsIdx = i;
					break;
				}
			}
			if (otherLabsIdx != -1)
				RemoveEmptyItems(obsDefItems, table, otherLabsIdx);

			// Finally, merge columns that have the same Obs_DtTm timestamps. That could happen with imported values.
			// The function RemoveHistoricalEntries() would merge multiple values for the same data item ('corrections')
			// but it would not merge different ObsReq records (that have the same Obs_DtTm timestamps) if they contain
			// values for different data items
			int col = 0;
			while (col < observationRecords.Count - 1)
			{
				// skip the manually created records
				if (!observationRecords[col].Created_By_Interface || !observationRecords[col + 1].Created_By_Interface)
				{
					col++;
					continue;
				}

				// check if the timestamp of the current row is the same as the next row's timestamp
				DateTime? currentTimestamp = observationRecords[col].Obs_DtTm;
				DateTime? nextTimestamp = observationRecords[col + 1].Obs_DtTm;
				if (!currentTimestamp.HasValue || !nextTimestamp.HasValue)
				{
					col++;
					continue;
				}
				if (currentTimestamp.Value != nextTimestamp.Value)
				{
					col++;
					continue;
				}

				foreach (List<TableItem> line in table)
				{
					if (line[col + 1] != null)
						line[col] = line[col + 1];

					line.RemoveAt(col + 1);
				}
				observationRecords.RemoveAt(col + 1);
			}

			return table;
		}

		/// <summary>
		/// Filter out the passed in list of ObsDef items removing the items of type ItemData or ItemTable for which there are no observations
		/// </summary>
		private static void RemoveEmptyObsDefs(IList<ObsDef> obsDefItems, Dictionary<int, List<int>> linkedObdIdsMap, IEnumerable<Observe> observationItems)
		{
			// get the distinct list of OBD_IDs for the passed in observations
			var observationObdIds = observationItems
				.Select(obs => obs.OBD_ID.HasValue ? obs.OBD_ID.Value : 0)
				.Distinct();

			for (int i = obsDefItems.Count - 1; i >= 0; i--)
			{
				var obsDef = obsDefItems[i];
				var obsDefType = (MedDefs.ObdType)obsDef.Type;
				if (obsDefType != MedDefs.ObdType.ItemData && obsDefType != MedDefs.ObdType.ItemTable)
					continue;

				var obdIds = linkedObdIdsMap[obsDef.OBD_ID];
				var noDataForItem = obdIds.Intersect(observationObdIds).Count() <= 0;
				if (noDataForItem)
					obsDefItems.RemoveAt(i);
			}
		}

		private static TableItem CreateFunctionItem(ObsDef obsDef, ObsReq obsReq, ImpacPersistenceManager pm)
		{
            if (!obsDef.Prog_ID.HasValue)
                return null;

			string value = String.Empty;
            switch ((MedDefs.ObdTypeFunctionProgIds) obsDef.Prog_ID.Value)
			{
				// supported functions:
				// 2000 - Initials - use obr.Edit_ID
                // 2001 - Full Name - use obr.Edit_ID's name and designation + date
				// 2016 - Notes - use  obr.Note_ID
				// 2017 - Approved By initials - use obr.Sanct_ID
                case MedDefs.ObdTypeFunctionProgIds.Initials:
                    value = obsReq.StaffEntityOnEdit.Initials;
                    break;
                case MedDefs.ObdTypeFunctionProgIds.NameAndSuffix:
                    value = obsReq.StaffEntityOnEdit.FullNameSuffix + (obsReq.Edit_DtTm.HasValue ? " (" + obsReq.Edit_DtTm.Value.ToShortDateString() + ")" : string.Empty);
                    break;
                case MedDefs.ObdTypeFunctionProgIds.Notes:
                    if (obsReq.Note_ID.HasValue)
                    {
                        var key = new PrimaryKey(typeof(Notes), obsReq.Note_ID.Value);
                        var note = pm.GetEntity<Notes>(key);
                        if (!note.IsNullEntity && !note.NotesText.IsNullEntity)
                            if (!String.IsNullOrEmpty(note.NotesText.Notes))
                                value = HtmlCell.ToPlainText(note.NotesText.Notes.Trim());
                    }
                    break;
                case MedDefs.ObdTypeFunctionProgIds.SanctInitials:
                    value = obsReq.StaffEntityOnSanct.Initials;
                    break;
			}

			return !String.IsNullOrEmpty(value)
			               ? new TableItem {Value = value.Trim()}
			               : null;
		}

		private static TableItem CreateDataItem(ObsDef obsDefItem, ObsReq obsReqItem, Observe observation, MergeFieldUtility.ValuesFilter valuesFilter)
		{
			var result = new TableItem
			             	{
			             		WarningIndicator = observation.WarningIndicator
			             	};

			bool includeItem = false;
			switch (valuesFilter)
			{
                case MergeFieldUtility.ValuesFilter.All:
					includeItem = true;
					break;
                case MergeFieldUtility.ValuesFilter.Abnormal:
					includeItem = !result.IsNormal();
					break;
                case MergeFieldUtility.ValuesFilter.Critical:
					includeItem = result.IsPanic() || result.IsOutOfRange();
					break;
                case MergeFieldUtility.ValuesFilter.OutOfRange:
					includeItem = result.IsOutOfRange();
					break;
                case MergeFieldUtility.ValuesFilter.Required:
					includeItem = (obsDefItem.Item_Data_Flags & (short) MedDefs.ItemData.RightJustify) != 0;
					break;
			}
			if (!includeItem)
				return null;

		    if (obsReqItem.HasPatientReportedResults)
                result.ReportedByPatient = StringConstants.INDICATOR_REPORTED_BY_PATIENT;

			// handle ESI imported values
			if (obsReqItem.Created_By_Interface)
			{
				string val = observation.Obs_String;
                double tmp;
				if (_esiNoteIndicators.Any(i => i.Equals(observation.Obs_String, StringComparison.InvariantCultureIgnoreCase)))
				{
					// handle ESI values stored as Notes
					if (observation.Note_ID.HasValue && observation.Note_ID.Value != 0)
					{
						var noteEntity = NotesText.GetEntityByID(observation.Note_ID.Value);
						if (!noteEntity.IsNullEntity)
							val = noteEntity.GetUnformattedNotesText();
					}
				}
                else if (obsDefItem.ObsDefDataFormat == ObsDefDataFormat.Numeric && double.TryParse(observation.Obs_String, out tmp))
                {
                    //convert and display value as mL/s (stored in DB as mL/min)
                    if (observation.IsOrLinkedCreatinineClearance && observation.HasOrLinkedCrClUnitsMilliLitersPerSecond)
                        tmp /= 60;

                    int digits;
                    if (BOM.Entities.Mo.BomMoUtils.GetDecimalDigitsCount(obsDefItem.Item_Data_Format, out digits))
                        val = string.Format("{0:N" + digits + "}", tmp);
                    else
                        val = tmp.ToString(CultureInfo.InvariantCulture);
                }

			    if (observation.Corrected)
					result.Value = "[C] " + val;
				else
					result.Value = val;

				return result;
			}
            switch (obsDefItem.ObsDefDataFormat)
			{
				case ObsDefDataFormat.Numeric:
					int digits;

                    //Defect-2966: Use Converted_Obs_Float_Nullable instead of Obs_Float for numbers so CrCl values are converted correctly, as in flowsheet
                    if (!observation.Converted_Obs_Float_Nullable.HasValue)
                        return null;

                    if (BOM.Entities.Mo.BomMoUtils.GetDecimalDigitsCount(obsDefItem.Item_Data_Format, out digits))
			        {
                        //DR-57288: TruncateTrailingZeros, same as flowsheet and UDDF display format
			            result.Value = observation.Converted_Obs_Float_Nullable.Value.ToString("0." + new string('#', digits));
			        }
			        else
			        {
			            result.Value = observation.Converted_Obs_Float_Nullable.Value.ToString(CultureInfo.InvariantCulture);
			        }
					result.RightJustify = true;
					break;
				case ObsDefDataFormat.String:
                        result.Value = observation.Obs_String;
                        result.RightJustify = (obsDefItem.Item_Data_Flags
                                               & (short)MedDefs.ItemData.RightJustify) != 0;
			        break;
				case ObsDefDataFormat.CheckBox:
					result.Value = (Convert.ToInt32(observation.Obs_Float) == 1 ? Strings.Yes : String.Empty);
					break;
				case ObsDefDataFormat.Memo:
					result.Value = observation.Obs_String;
					break;
				case ObsDefDataFormat.Date:
                    var date = observation.Obs_Date;
					result.Value = date.HasValue ? date.Value.ToShortDateString() : string.Empty;
					break;
				case ObsDefDataFormat.Time:
					var time = observation.Obs_Time;
                    result.Value = time.HasValue ? time.Value.ToShortTimeString() : string.Empty;
					break;
			}
			return result;
		}

        /// <summary>
        /// Remove the data items that have no observation records
        /// </summary>
        private static void RemoveEmptyItems(IList<ObsDef> obsDefItems, IList<List<TableItem>> dataTable, int startIdx)
        {
            if (startIdx < 0) startIdx = 0;
            for (int currIdx = dataTable.Count - 1; currIdx >= startIdx; currIdx--)
            {
				if (MergeFieldUtility.IsHeadingItemOrOtherLabs(obsDefItems[currIdx]))
                    continue;

                if (!dataTable[currIdx].Exists(itm => itm != null))
                {
                    obsDefItems.RemoveAt(currIdx);
                    dataTable.RemoveAt(currIdx);
                }
            }
        }

		/// <summary>
        /// Remove the headings that don't have any data items below them
        /// Assumes RemoveEmptyItems has been called previous to this call
        /// </summary>
        private static void RemoveEmptyHeadings(IList<ObsDef> obsDefItems, List<List<TableItem>> dataTable)
        {
            for (int currIdx = 0; currIdx < dataTable.Count; currIdx++)
            {
				if (MergeFieldUtility.IsHeadingItemOrOtherLabs(obsDefItems[currIdx])
					&& (currIdx + 1 >= dataTable.Count || MergeFieldUtility.IsHeadingItemOrOtherLabs(obsDefItems[currIdx + 1])))
                {
                    obsDefItems.RemoveAt(currIdx);
                    dataTable.RemoveAt(currIdx--); //decrement to counteract increment on loop
                }
            }
        }

        /// <summary>
        /// Add order description and date if this obs req has order entity linked to it (via the Observe_Orders table)
        /// </summary>
        /// <param name="observationRecords"></param>
        /// <param name="obsDefInfoList"></param>
        /// <param name="dataTable"></param>
        private static void AddOrderInfo(IEnumerable<ObsReq> observationRecords, IList<ObsDefInfo> obsDefInfoList, List<List<TableItem>> dataTable)
        {
            // prepare data to be added
            var orderDescList = new List<TableItem>();
            var orderDateList = new List<TableItem>();

            foreach (ObsReq obsreq in observationRecords)
            {
                var orderDesc = new TableItem();
                var orderDate = new TableItem();
                if (obsreq.Observe_OrdersEntities != null && obsreq.Observe_OrdersEntities.Count > 0)
                {
					var orderEntity = obsreq.Observe_OrdersEntities[0].OrdersEntityOnOrcSet;
					orderDesc.Value = orderEntity.OrderSentenceMergeField;
					if (orderEntity.Start_DtTm.HasValue)
						orderDate.Value = orderEntity.Start_DtTm.Value.ToShortDateString();
                }

                orderDescList.Add(orderDesc);
                orderDateList.Add(orderDate);
            }

            // order sentence
            obsDefInfoList.Add(new ObsDefInfo
            {
                ItemType = (short)MedDefs.ObdType.ItemData, // treat it like a data item
                Label = Strings.FlowsheetMergeFieldActivity_OrderDescription,
                DataFlags = ObsDefDataFormat.String
            });
            dataTable.Add(orderDescList);

            // order start date
            obsDefInfoList.Add(new ObsDefInfo
            {
                ItemType = (short)MedDefs.ObdType.ItemData, // treat it like a data item
                Label = Strings.FlowsheetMergeFieldActivity_OrderDate,
                DataFlags = ObsDefDataFormat.Date
            });
            dataTable.Add(orderDateList);
        }

		// Add additional rows to the items table containing the status info
		private static void AddStatusInfo(MergeFieldUtility.StatusInfoFormat format, IEnumerable<ObsReq> observationRecords, IList<ObsDefInfo> obsDefInfoList, 
            List<List<TableItem>> dataTable)
		{
            if (format == MergeFieldUtility.StatusInfoFormat.NoInfo)
				return;

			// status
			obsDefInfoList.Add(new ObsDefInfo
			                   	{
			                   		ItemType = MergeFieldUtility.OBD_TYPE_NONE,
			                   		Label = Strings.FlowsheetMergeFieldActivity_Status,
			                   		DataFlags = ObsDefDataFormat.String
			                   	});
			var statuses = observationRecords
				.Select(item => new TableItem { Value = item.Status })
				.ToList();
			dataTable.Add(statuses);

			// statused by
			string label = (format == MergeFieldUtility.StatusInfoFormat.Initials
				? Strings.FlowsheetMergeFieldActivity_StatusBy_Initials
				: Strings.FlowsheetMergeFieldActivity_StatusBy_FullName);
			obsDefInfoList.Add(new ObsDefInfo
			{
				ItemType = MergeFieldUtility.OBD_TYPE_NONE,
				Label = label,
				DataFlags = ObsDefDataFormat.String
			});
			var names = observationRecords
				.Select(item =>
						new TableItem
						{
							Value = item.Sanct_ID.HasValue
										? (format == MergeFieldUtility.StatusInfoFormat.Initials ? item.StaffEntityOnSanct.Initials : item.StaffEntityOnSanct.FullNameSuffix)
										: String.Empty
						})
				.ToList();
			dataTable.Add(names);

			// status date
			obsDefInfoList.Add(new ObsDefInfo
			{
				ItemType = MergeFieldUtility.OBD_TYPE_NONE,
				Label = Strings.FlowsheetMergeFieldActivity_StatusDate,
				DataFlags = ObsDefDataFormat.Date
			});
			var dates = observationRecords
				.Select(item => new TableItem { Value = item.Sanct_DtTm.HasValue ? item.Sanct_DtTm.Value.ToShortDateString() : String.Empty })
				.ToList();
			dataTable.Add(dates);
		}

        #region Utility Methods for LastNItemsPerRecord
        private static bool ColumnIsUsed(IEnumerable<List<TableItem>> tbl, int colIdx)
        {
            ////Pre-LINQ conversion:
            //bool valueExists = false;
            //foreach (List<TableItem> row in tbl)
            //    valueExists |= (row[colIdx] != null);
            //return valueExists;
            return tbl.Aggregate(false, (current, row) => current | (row[colIdx] != null));
        }
        #endregion Utility Methods for LastNItemsPerRecord
        #endregion

        #region Generate the output - HTML or plain text
		private string GenerateParagraph(IList<ObsDefInfo> obsDefInfoList, IList<List<TableItem>> listData, IList<DateTime> dates, string delimiter)
        {
            var result = new StringBuilder();
            if (dates != null && dates.Count == 1)//listData.Count > 0 && listData[0].Count == 1)
            {
                result.Append(Strings.FlowsheetMergeFieldActivity_SingleRecordParagraphListDateHeader + MergeFieldUtility.LABELVALUE_LABELDESCRIPTION_SEPARATOR
                              + dates[0].ToShortDateString() + " " + dates[0].ToShortTimeString());
                dates = null; //nullify dates so not added per-item
            }

			for (int i = 0; i < obsDefInfoList.Count; i++)
            {
                if (result.Length > 0)
                    result.Append(delimiter);
				AppendRowString(obsDefInfoList[i], listData[i], dates, result);
            }
            //result.AppendLine();
            return result.ToString();
        }

		private string GenerateList(IList<ObsDefInfo> obsDefInfoList, IList<List<TableItem>> listData, IList<DateTime> dates)
        {
            var result = new StringBuilder();
            if (dates != null && dates.Count == 1)//listData.Count > 0 && listData[0].Count == 1)
            {
                result.Append(Strings.FlowsheetMergeFieldActivity_SingleRecordParagraphListDateHeader + MergeFieldUtility.LABELVALUE_LABELDESCRIPTION_SEPARATOR
                              + dates[0].ToShortDateString() + " " + dates[0].ToShortTimeString());
                dates = null; //nullify dates so not added per-item
            }

			for (int i = 0; i < obsDefInfoList.Count; i++)
			{
                if (result.Length > 0)
                    result.AppendLine();
				AppendRowString(obsDefInfoList[i], listData[i], dates, result);
            }
            return result.ToString();
        }

        /// <summary> </summary>
		private void AppendRowString(ObsDefInfo obsDefInfo, IList<TableItem> row, IList<DateTime> dates, StringBuilder result)
        {
			bool noHeaders = _outputFormat == MergeFieldUtility.FlowsheetOutputFormats.ListNoHeader
				|| _outputFormat == MergeFieldUtility.FlowsheetOutputFormats.ParagraphNoHeader;

			if (MergeFieldUtility.IsHeadingItem(obsDefInfo.ItemType))
            {
				result.Append(obsDefInfo.Label);
				if (!String.IsNullOrEmpty(obsDefInfo.Instructions))
					result.AppendFormat(" ({0}: {1})", Strings.Instructions, obsDefInfo.Instructions);
            }
            else
            {
                if (!noHeaders)
					result.AppendFormat("{0}{1}", obsDefInfo.Label, MergeFieldUtility.LABELVALUE_LABELDESCRIPTION_SEPARATOR);

                Debug.Assert(dates == null || dates.Count == row.Count);
                var line = new StringBuilder();
                int idx = 0;
                foreach (var item in row)
                {
                    if (item != null)
                    {
                        if (line.Length > 0)
                            line.Append(", ");
						string itemValue = noHeaders && obsDefInfo.DataFlags == ObsDefDataFormat.CheckBox
							? obsDefInfo.Label
							: item.Value;

                        //Add indicator
                        if (!string.IsNullOrWhiteSpace(item.WarningIndicator))
                            itemValue += " " + item.WarningIndicator;
                        //Add date
                        if (dates != null)
                            itemValue += " " + MergeFieldUtility.DATE_BRACKET_PRE
                                         + dates[idx].ToShortDateString() + " " + dates[idx].ToShortTimeString()
                                         + MergeFieldUtility.DATE_BRACKET_POST;


                        // Add Reported by patient
                        if (!string.IsNullOrWhiteSpace(item.ReportedByPatient))
                            itemValue += " " + item.ReportedByPatient;

                        line.Append(itemValue.Trim());
                    }

                    idx++;
                }
                result.Append(line.ToString());
            }
        }

		private string GenerateHtml(IList<ObsDefInfo> obsDefInfoList, IList<DateTime> listDates, IList<List<TableItem>> listData)
		{
			bool invert = _outputFormat != MergeFieldUtility.FlowsheetOutputFormats.Flowsheet
						  && _outputFormat != MergeFieldUtility.FlowsheetOutputFormats.FlowsheetNoLines;
			bool showLines = _outputFormat == MergeFieldUtility.FlowsheetOutputFormats.Flowsheet
							 || _outputFormat == MergeFieldUtility.FlowsheetOutputFormats.Table
							 || _outputFormat == MergeFieldUtility.FlowsheetOutputFormats.TableNoHeader;
			bool showHeader = _outputFormat != MergeFieldUtility.FlowsheetOutputFormats.TableNoHeader
							  && _outputFormat != MergeFieldUtility.FlowsheetOutputFormats.TableNoLinesNoHeader;

			var result = new StringBuilder();
			result.AppendFormat("<table border={0} cellspacing=0 cellpadding=0 style='border-collapse:collapse;border:none'>",
			                    showLines ? "1" : "0");
			if (!invert)
				Table(result, obsDefInfoList, listDates, listData, showHeader);
			else
				TableI(result, obsDefInfoList, listDates, listData, showHeader);
			result.Append("</table>");
			return result.ToString();
		}

		/// <summary>
        /// Generate a html table in flowsheet format (the rows are data items, the columns are timestamps)
        /// </summary>
        private static void Table(StringBuilder result, IList<ObsDefInfo> obsDefInfoList, IEnumerable<DateTime> listDates, IList<List<TableItem>> listData, bool showHeader)
        {
            if (showHeader && listDates != null)
            {
                var row1 = new List<string>();
                var row2 = new List<string>();
                foreach (DateTime date in listDates)
                {
                    row1.Add(date.ToShortDateString());
                    row2.Add(date.ToShortTimeString());
                }
            	CreateHeaderRows(result, row1, null, row2,
            	                 Strings.FlowsheetMergeFieldActivity_Date, String.Empty,
            	                 Strings.FlowsheetMergeFieldActivity_Time);
            }

            // remaining rows
			for (int i = 0; i < obsDefInfoList.Count; i++)
            {
                result.Append("<tr>");

				ObsDefInfo obsDefInfo = obsDefInfoList[i];

				if (MergeFieldUtility.IsHeadingItem(obsDefInfo.ItemType) || MergeFieldUtility.IsSpecialItem(obsDefInfo.ItemType))
                    result.Append(HtmlCell.FormatCell2(obsDefInfo.Label, true, background: "#D9D9D9"));
                else
                    result.Append(HtmlCell.FormatCell2(obsDefInfo.Label, false, indent: 2));

                List<TableItem> data = listData[i];
                // Check if the current item is a major or minor heading and has instructions
				if (MergeFieldUtility.IsHeadingItem(obsDefInfo.ItemType) && !String.IsNullOrEmpty(obsDefInfo.Instructions) && data.Count > 0)
                    result.Append(HtmlCell.FormatCell2(obsDefInfo.Instructions, false, colSpan: data.Count));
                else
                    foreach (TableItem item in data)
                        ProcessItem(item, result);
                result.Append("</tr>");
            }
        }

        /// <summary>
        /// Generate a html table in table format (the columns are timestamps, the rows are data items)
        /// </summary>
        private static void TableI(StringBuilder result, IList<ObsDefInfo> obsDefInfoList, IList<DateTime> listDates, IList<List<TableItem>> listData, bool showHeader)
        {
            if (showHeader)
            {
            	bool haveInstructions = obsDefInfoList.Where(o => !String.IsNullOrEmpty(o.Instructions)).Count() > 0;
                var row1 = new List<string>();
				var row15 = haveInstructions ? new List<string>() : null;
                var row2 = new List<string>();
                string currentHeader = String.Empty;
				string currentInstructions = String.Empty;
                bool currentHeaderAdded = false;
				foreach (ObsDefInfo obsDefInfo in obsDefInfoList)
				{
					if (MergeFieldUtility.IsHeadingItem(obsDefInfo.ItemType) || MergeFieldUtility.IsSpecialItem(obsDefInfo.ItemType))
					{
						if (!String.IsNullOrEmpty(currentHeader) && !currentHeaderAdded)
						{
							row1.Add(currentHeader);
							if (haveInstructions)
								row15.Add(currentInstructions);
							row2.Add(String.Empty);
						}
						currentHeader = obsDefInfo.Label;
						currentInstructions = obsDefInfo.Instructions;
						currentHeaderAdded = false;
					}
					else
					{
						row1.Add(currentHeader);
						if (haveInstructions)
							row15.Add(currentInstructions);
						row2.Add(obsDefInfo.Label);
						currentHeaderAdded = true;
					}
				}
                if (!String.IsNullOrEmpty(currentHeader) && !currentHeaderAdded)
                {
                    row1.Add(currentHeader);
					if (haveInstructions)
						row15.Add(currentInstructions);
                    row2.Add(String.Empty);
                }
                CreateHeaderRows(result, row1, row15, row2, 
					Strings.FlowsheetMergeFieldActivity_Heading, Strings.Instructions, Strings.FlowsheetMergeFieldActivity_TestName);
            }

            // remaining rows
            for (int rowIdx = 0; rowIdx < listDates.Count; rowIdx++)
            {
                result.Append("<tr>");

                DateTime date = listDates[rowIdx];
                result.Append(HtmlCell.FormatCell2(date.ToShortDateString() + " " + date.ToShortTimeString(), true));

                // If there are no items in a heading we need to add an empty cell before switching
                // to the next header. 
                // addedItemsForCurrentHeader is initialized to true so that we don't do that for the first heading
                bool addedItemsForCurrentHeading = true;
				for (int colIdx = 0; colIdx < obsDefInfoList.Count; colIdx++)
                {
					ObsDefInfo obsDefInfo = obsDefInfoList[colIdx];

					if (MergeFieldUtility.IsHeadingItem(obsDefInfo.ItemType) || MergeFieldUtility.IsSpecialItem(obsDefInfo.ItemType))
                    {
                        // switching to the next heading
                        if (!addedItemsForCurrentHeading)
                            HtmlCell.FormatCell(result, String.Empty, false);

                        addedItemsForCurrentHeading = false;
                        continue;
                    }

                    addedItemsForCurrentHeading = true;
                    //if (IsHeadingItem(listItemTypes[colIdx]))
                    //	continue;
                    TableItem item = listData[colIdx][rowIdx];
                    ProcessItem(item, result);
                }

                result.Append("</tr>");
            }
        }

		private static void CreateHeaderRows(StringBuilder result,
			IList<string> row1, IList<string> row15, IEnumerable<string> row2, 
			string row1Header, string row15Header, string row2Header)
		{
			var colSpans = new List<int>();
			string prev = row1.Count > 0 ? row1[0] : String.Empty;
			int span = 0;
			foreach (string curr in row1)
			{
				if (curr == prev)
				{
					span++;
				}
				else
				{
					colSpans.Add(span);
					span = 1;
					prev = curr;
				}
			}
			if (span > 0)
				colSpans.Add(span);

			// row 1
			result.Append("<tr>");
			HtmlCell.FormatCell(result, row1Header, true);
			int pos = 0;
			foreach (int t in colSpans)
			{
				HtmlCell.FormatCell(result, row1[pos], true, HtmlCell.Alignment.Center, colSpan: t);
				pos += t;
			}
			result.Append("</tr>");

			// row 1.5 (optional)
			if (row15 != null)
			{
				result.Append("<tr>");
				HtmlCell.FormatCell(result, row15Header, true);
				pos = 0;
				foreach (int t in colSpans)
				{
					HtmlCell.FormatCell(result, row15[pos], false, HtmlCell.Alignment.Center, colSpan: t);
					pos += t;
				}
				result.Append("</tr>");
			}

			// row 2
			result.Append("<tr>");
			HtmlCell.FormatCell(result, row2Header, true);
			foreach (string s in row2)
				HtmlCell.FormatCell(result, s, true);
			result.Append("</tr>");

		}

		private static void ProcessItem(TableItem item, StringBuilder result)
		{
			HtmlCell.Alignment align = HtmlCell.Alignment.Left;
			string background = String.Empty;

			string value = item == null ? String.Empty : item.Value;
			if (item != null)
			{
				align = item.RightJustify ? HtmlCell.Alignment.Right : HtmlCell.Alignment.Left;
				if (item.IsWarning())
					background = "yellow";
				else if (item.IsPanic())
					background = "red";
				else if (item.IsOutOfRange())
					background = "lightgrey";

				if (!String.IsNullOrEmpty(item.WarningIndicator))
					value += " " + item.WarningIndicator;

                // Add Reported by patient
                if (!string.IsNullOrWhiteSpace(item.ReportedByPatient))
                    value += " " + item.ReportedByPatient;


			}
            result.Append(HtmlCell.FormatCell2(value, false, align, background));
		}
		#endregion
	}

	class ObsDefInfo
	{
		public short ItemType { get; set; }
		public string Label { get; set; }
		public ObsDefDataFormat DataFlags { get; set; }
		public string Instructions { get; set; }
	}
}
