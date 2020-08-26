using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Activities;
using System.Data;

using Impac.Mosaiq.Core.Defs.Enumerations;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.General.Escribe
{
	/// <summary>
	/// Converts a data table carying a merge field's data to html or plain text
	/// </summary>
	[GeneralCharting_ActivityGroup]
	[Escribe_Category]
	[MergeFieldFormatterActivity_DisplayName]
	public class MergeFieldFormatterActivity : MosaiqCodeActivity
	{
		#region Properties (Input Parameters)
		/// <summary> The merge field data passed in a DataTable instance</summary>
		[InputParameterCategory]
		[RequiredArgument]
		[MergeFieldFormatterActivity_FieldData_DisplayName]
		[MergeFieldFormatterActivity_FieldData_Description]
		public InArgument<DataTable> FieldData { get; set; }

		/// <summary> 
		/// The format of the output string: 1 - Table with header 2 - Table with no header, 3 - List, 4 - Paragraph, 
		/// 5 - table with no lines, 6 - table with no header and no lines
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[MergeFieldFormatterActivity_OutputFormat_DisplayName]
		[MergeFieldFormatterActivity_OutputFormat_Description]
		public InArgument<int> OutputFormat { get; set; }

		/// <summary>
		/// The list of columns to diplay
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[MergeFieldFormatterActivity_Columns_DisplayName]
		[MergeFieldFormatterActivity_Columns_Description]
		public InArgument<IList<IQEnum>> Columns { get; set; }
		#endregion

		#region Properties (Output Parameters)
		/// <summary> If the output format is 'table' it is stored here in html format</summary>
		[OutputParameterCategory]
		[MergeFieldFormatterActivity_Html_DisplayName]
		[MergeFieldFormatterActivity_Html_Description]
		public OutArgument<string> Html { get; set; }

		/// <summary> If the output format is 'list' or 'paragraph' it is stored here as plain text</summary>
		[OutputParameterCategory]
		[MergeFieldFormatterActivity_PlainText_DisplayName]
		[MergeFieldFormatterActivity_PlainText_Description]
		public OutArgument<string> PlainText { get; set; }
		#endregion

		#region Overrides of MosaiqCodeActivity
		/// <summary>
		/// </summary>
		protected override void DoWork(CodeActivityContext context)
		{
			PlainText.Set(context, null);
			Html.Set(context, null);

			DataTable dataTable = FieldData.Get(context);
			var format = (MergeFieldUtility.MergeFieldsOutputFormats) OutputFormat.Get(context);
			IList<IQEnum> columns = Columns.Get(context);

			if (dataTable.Columns.Count == 0)
			{
                PlainText.Set(context, MergeFieldUtility.NO_DATA);
				return;
			}

			// create a new table that contains only the user selected columns
			var filteredTable = FilterTable(dataTable, columns);
		    var statusList = GetStatusList(dataTable);

			string result;
			switch (format)
			{
                case MergeFieldUtility.MergeFieldsOutputFormats.Table:
                case MergeFieldUtility.MergeFieldsOutputFormats.TableNoLines:
                case MergeFieldUtility.MergeFieldsOutputFormats.TableNoHeader:
                case MergeFieldUtility.MergeFieldsOutputFormats.TableNoLinesNoHeader:
                    bool showHeader = format == MergeFieldUtility.MergeFieldsOutputFormats.Table || format == MergeFieldUtility.MergeFieldsOutputFormats.TableNoLines;
                    bool showLines = format == MergeFieldUtility.MergeFieldsOutputFormats.Table || format == MergeFieldUtility.MergeFieldsOutputFormats.TableNoHeader;
					if (filteredTable.Rows.Count == 0 && !showHeader)
					{
                        PlainText.Set(context, MergeFieldUtility.NO_DATA);
					}
					else
					{
						result = ConvertToHtml(filteredTable, showHeader, showLines, statusList);
						Html.Set(context, result);
					}
					break;
                case MergeFieldUtility.MergeFieldsOutputFormats.List:
                    // use html format for the same look and feel (line spacing)
                    result = ConvertToHtmlParagraph(filteredTable, " ", "<br>", statusList);
                    Html.Set(context, result);
					break;
                case MergeFieldUtility.MergeFieldsOutputFormats.Paragraph:
                    if (statusList == null || statusList.Count <= 0)
                    {
                        result = ConvertToParagraph(filteredTable, " ", "; ");
                        PlainText.Set(context, result);
                    }
                    else
                    {
                        result = ConvertToHtmlParagraph(filteredTable, " ", "; ", statusList);
                        Html.Set(context, result);                        
                    }
					break;
			}
		}
		#endregion

		#region private methods
		/// <summary>
		/// Create a new data table that only has the columns selected by the user
		/// </summary>
		/// <param name="dataTable">The passed in data table containing all avaiable columns</param>
		/// <param name="columns">The ordered list of columns selected by the user</param>
		/// <returns></returns>
		private static DataTable FilterTable(DataTable dataTable, IList<IQEnum> columns)
		{
			var filteredTable = new DataTable();
			// create the columns of the new table in the order specified by the user
			foreach (var column in columns)
			{
				// use the strings in column.Value for the column names, because that is 
				// what is displayed on the Preferences UI. The column names in the provided 
				// data table are actually indexes converted to strings and are used 
				// to match the columns in the data table and the user selected columns
				string dataTableColumnName = Convert.ToString(column.Key);
				string newColumnName = column.Value;
				DataColumn dataColumn = dataTable.Columns[dataTableColumnName];
				filteredTable.Columns.Add(newColumnName, dataColumn.DataType);
			}

			// create the rows of the new table and fill them with the data from the data table
			foreach (DataRow row in dataTable.Rows)
			{
				var newRow = filteredTable.NewRow();
				for (int i = 0; i < columns.Count; i++ )
				{
					string colName = Convert.ToString(columns[i].Key);
					newRow[i] = row[colName];
				}
				filteredTable.Rows.Add(newRow);
			}
			return filteredTable;
		}

        /// <summary>
        /// if there are a special formats defined for rows, save it 
        /// </summary>
        /// <param name="dataTable">a list that has true/false on voided row numbers</param>
        /// <returns></returns>
        private static List<Int32> GetStatusList(DataTable dataTable)
	    {
            if (!dataTable.Columns.Contains(Strings.Status_Enum))
                return new List<Int32>();

            return (from DataRow row in dataTable.Rows
                    select (Int32)(row[Strings.Status_Enum])).ToList();
	    }

		/// <summary>
		/// Generate a string that contains the data from the table in plain text format. The individual items from
		/// each row are separated with the provided columns separator, and the individual data rows are separated
		/// with the provided row separator
		/// </summary>
		private static string ConvertToParagraph(DataTable table, string columnSeparator, string rowSeparator)
		{
			if (table.Rows.Count == 0)
                return MergeFieldUtility.NO_DATA;

			var result = new StringBuilder();
			foreach (DataRow row in table.Rows)
			{
				string line = GetLine(table.Columns, row, columnSeparator);
				// if the line is empty - skip it
				if (String.IsNullOrEmpty(line))
					continue;
				if (result.Length > 0)
					result.Append(rowSeparator);
				result.Append(line);
			}
            return result.Length > 0 ? result.ToString() : MergeFieldUtility.NO_DATA;
		}

        /// <summary>
        /// Generate a paragraph string that contains the data from the table in html format. 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnSeparator"></param>
        /// <param name="rowSeparator"></param>
        /// <param name="statusList"></param>
        /// <returns></returns>
        private static string ConvertToHtmlParagraph(DataTable table, string columnSeparator, string rowSeparator, List<int> statusList)
        {
			const string NO_DATA_HTML = "<span>&nbsp;&nbsp;</span>";
			// when there is no data return the HTML equivalent of a couple of spaces
            if (table.Rows.Count == 0)
				return NO_DATA_HTML;

            var result = new StringBuilder();
            int rowNum = 0;

            foreach (DataRow row in table.Rows)
            {
                string line = GetLine(table.Columns, row, columnSeparator);
                // if the line is empty - skip it
                if (!String.IsNullOrEmpty(line))
                {
                    if (result.Length > 0)
                        result.Append(rowSeparator);

                    if (statusList != null && statusList.Count > 0 && statusList[rowNum] == 1)
                        line = string.Format("<span style=\"text-decoration:line-through\">{0}</span>", line);       // apply strikethrough format

                    result.Append(line);
                }
                rowNum++;
            }

            //return result.Length > 0 ? string.Format("<p>{0}</p>", result) : MergeFieldUtility.NO_DATA;
			return result.Length > 0 ? result.ToString() : NO_DATA_HTML;            
        }

		private static string GetLine(DataColumnCollection columns, DataRow row, string columnSeparator)
		{
			var line = new StringBuilder();
			for (int i = 0; i < row.ItemArray.Length; i++ )
			{
				string cellData = CellData(row.ItemArray[i], columns[i].DataType);
				// if the current cell has no data - skip it
				if (String.IsNullOrEmpty(cellData))
					continue;
				if (line.Length > 0)
					line.Append(columnSeparator);
				line.Append(cellData);
			}
			return line.ToString().Trim();
		}

		/// <summary>
		/// Generate a string that contains the data from the table in html format
		/// </summary>
		/// <returns></returns>
		private static string ConvertToHtml(DataTable table, bool showHeaderRow, bool showLines, List<int> statusList)
		{
			var result = new StringBuilder();

			result.AppendFormat("<table border={0} cellpadding=1 cellspacing=0 style='border-collapse:collapse;border:none'>", 
				showLines ? "1" : "0");
			if (showHeaderRow)
			{
				result.Append("<thead style='font-weight: bold'><tr>");
				foreach (DataColumn item in table.Columns)
					HtmlCell.FormatCell(result, item.ColumnName, true);
				result.Append("</tr></thead>");
			}

			result.Append("<tbody>");

		    int rowNum = 0;
			foreach (DataRow row in table.Rows)
			{
                // if this row has status 'voided', apply strikethrough format
			    bool markVoided = false;
                if (statusList != null && statusList.Count > 0)
			        markVoided = (statusList[rowNum] == 1);      // status 1 = voided

				result.Append("<tr>");
				for (int i = 0; i < row.ItemArray.Length; i++)
					HtmlCell.FormatCell(result, CellData(row.ItemArray[i], table.Columns[i].DataType), false,
					                    ShouldRightJustify(table.Columns[i].DataType)
					                    	? HtmlCell.Alignment.Right
					                    	: HtmlCell.Alignment.Left,
                                        null, 0, 0, markVoided);

				result.Append("</tr>");
			    rowNum++;
			}
			result.Append("</tbody></table>");
			return result.ToString();
		}

		/// <summary>
		/// Convert a data table item to string, with special handling for boolean and date time types
		/// </summary>
		/// <param name="data"></param>
		/// <param name="dataType"></param>
		/// <returns></returns>
		private static string CellData(object data, Type dataType)
		{
			string str = data.ToString();
			if (dataType == typeof(bool) && (data is bool))
			{
				str = (bool) data ? Strings.Boolean_True : Strings.Boolean_False;
			}
			else if (dataType == typeof(DateTime) && (data is DateTime))
			{
				str = DateTime.Parse(data.ToString()).ToShortDateString();
			}
			return str.Trim();
		}

		/// <summary> Return true if this is a numeric or date/time field, all of which should be right justified. </summary>
		private static bool ShouldRightJustify(Type t)
		{
			if (t == typeof(Int32) || t == typeof(Int16) || t == typeof(Int64) || t == typeof(float)
			|| t == typeof(double) || t == typeof(decimal) || t == typeof(byte) || t == typeof(DateTime))
				return true;

			return false;
		}
		#endregion
	}
}
