using System;
using System.Collections.Generic;

using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Core.Defs.Enumerations;
using Impac.Mosaiq.IQ.Common.Variable;

namespace Impac.Mosaiq.IQ.Activities.General.Escribe
{
    public static class MergeFieldUtility
    {
        #region Constants

// ReSharper disable InconsistentNaming
        public const int OBD_TYPE_NONE = 0;

        public const string DATE_BRACKET_PRE = "(";
        public const string DATE_BRACKET_POST = ")";
        public const string LABELVALUE_LABELDESCRIPTION_SEPARATOR = " - ";

        public const int QUERY_SEGMENT_LENGTH = 300;
        public const int QUERY_SEGMENT_LENGTH_GUID = 100;

		/// <summary> The returned string from a merge field when there is no data to return </summary>
		public const string NO_DATA = "  ";
// ReSharper restore InconsistentNaming

		public static readonly Guid OtherLabsItemGuid = Guid.Parse(FlowsheetSelection.OtherLabsGuidString);

        //Flowsheet specific
        public enum HideFormats { DoNotHide = 0, HideRows = 1, HideRowsAndFolders = 2 }
        public enum CustomDateRange { MostRecent = 1, SinceLastEncounter = 2, LastN = 3, LastNPerItem = 4 }
        public enum ValuesFilter { All = 1, Abnormal = 2, Critical = 3, OutOfRange = 4, Required = 5 }
        public enum TableItemsFormats { Label = 1, Description = 2, LabelAndDescription = 3, EscribeDescription = 4 }
        public enum DataItemsFormats { Label = 1, Description = 2, LabelAndDescription = 3, EscribeDescription = 4 }
        public enum StatusInfoFormat { NoInfo = 1, Initials = 2, FullName = 3 }
        public enum ShowDateFormat { DoNotShow = 0, ShowShortDate = 1 }

        //MergeFields
        /// <summary>
        /// Flowsheet specific output formats
        /// </summary>
        public enum FlowsheetOutputFormats { Flowsheet = 1, Table = 2, List = 3, Paragraph = 4, FlowsheetNoLines = 5, TableNoLines = 6, ListNoHeader = 7, ParagraphNoHeader = 8, TableNoHeader = 9, TableNoLinesNoHeader = 10 }
        /// <summary>
        /// Generic MergeFields output formats (kept separate to support templates calling MergeFieldFormatterActivity)
        /// </summary>
        public enum MergeFieldsOutputFormats { Table = 1, TableNoHeader = 2, List = 3, Paragraph = 4, TableNoLines = 5, TableNoLinesNoHeader = 6 }

        #endregion Constants

        #region Utility Methods

        //Assessment
        public static bool IsHeadingItem(short type)
        {
            return type == (short)MedDefs.ObdType.MajorHeading
                || type == (short)MedDefs.ObdType.MinorHeading;
        }

		public static bool IsHeadingItemOrOtherLabs(ObsDef item)
		{
			return IsHeadingItem(item.Type) || item.OBD_GUID == OtherLabsItemGuid;
		}

        public static bool IsSpecialItem(short type)
        {
            return type == OBD_TYPE_NONE;
        }

        public static List<ObsReq> TrimPreviousRecords(List<ObsReq> records, int numPreviousRecordsToRetain)
        {
            if (records.Count > numPreviousRecordsToRetain)
            {
                records.Sort(ObsReq.CompareObsReqByDtTm);
                records = records.GetRange(records.Count - numPreviousRecordsToRetain, numPreviousRecordsToRetain);
            }

            return records;
        }

        #endregion Utility Methods
    }
}
