using System;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

using Impac.Mosaiq.Core.Toolbox;

namespace Impac.Mosaiq.IQ.Activities.General.Escribe
{
	/// <summary>
	/// This class is be used by the common merge field output formatter and the flowsheet merge fields
	/// </summary>
	class HtmlCell
	{
		/// <summary> Cell text alignment </summary>
		public enum Alignment { Left, Center, Right }

        /// <summary>Cell Format</summary>
        private static StringBuilder FORMATCELL = new StringBuilder(256);

		/// <summary>
		/// Convert a string to an HTML table cell (&lt;td&gt;), with a specified set of options. If
		/// the contents of the cell are blank, use a non-breaking space because most browsers will
		/// mess up borders if the cell is empty.
		/// </summary>
		public static string FormatCell(StringBuilder result, string contents, bool bold, 
			Alignment alignment = Alignment.Left, string background = null, int colSpan = 0, int indent = 0, bool strikethrough = false)
		{
			contents = Htmlize(contents);
			if (indent > 0)
				contents = String.Concat(Enumerable.Repeat("&nbsp;", indent)) + contents;
            
			result.Append("<td");
			if (colSpan > 1)
				result.Append(" colspan=" + colSpan);
			if (String.IsNullOrEmpty(background))
			{
				result.Append(" style='padding:0in 5.4pt 0in 5.4pt'>");
			}
			else
			{
				result.Append(" style='padding:0in 5.4pt 0in 5.4pt; background:");
				result.Append(background);
				result.Append("'>");
			}

			string align = alignment.ToString().ToLower();
			result.Append("<p align=");
			result.Append(align);
			result.Append(" style='text-align:");
			result.Append(align);
			result.Append("'>");

            if (strikethrough)
                result.Append("<span style=\"text-decoration:line-through\">");
			if (bold)
				result.Append("<b>");
			result.Append(contents);
			if (bold)
				result.Append("</b>");
            if (strikethrough)                
                result.Append("</span>");

			result.Append("</p></td>");

			return result.ToString();
		}

        public static string FormatCell2(string contents, bool bold,
           Alignment alignment = Alignment.Left, string background = null, int colSpan = 0, int indent = 0, bool strikethrough = false)
        {
            contents = Htmlize(contents);
            if (indent > 0)
                contents = String.Concat(Enumerable.Repeat("&nbsp;", indent)) + contents;

            FORMATCELL.Clear();

            FORMATCELL.Append("<td");
            if (colSpan > 1)
                FORMATCELL.Append(" colspan=" + colSpan);
            if (String.IsNullOrEmpty(background))
            {
                FORMATCELL.Append(" style='padding:0in 5.4pt 0in 5.4pt'>");
            }
            else
            {
                FORMATCELL.Append(" style='padding:0in 5.4pt 0in 5.4pt; background:");
                FORMATCELL.Append(background);
                FORMATCELL.Append("'>");
            }

            string align = alignment.ToString().ToLower();
            FORMATCELL.Append("<p align=");
            FORMATCELL.Append(align);
            FORMATCELL.Append(" style='text-align:");
            FORMATCELL.Append(align);
            FORMATCELL.Append("'>");

            if (strikethrough)
                FORMATCELL.Append("<span style=\"text-decoration:line-through\">");
            if (bold)
                FORMATCELL.Append("<b>");
            FORMATCELL.Append(contents);
            if (bold)
                FORMATCELL.Append("</b>");
            if (strikethrough)
                FORMATCELL.Append("</span>");

            FORMATCELL.Append("</p></td>");

            return FORMATCELL.ToString();
        }

		/// <summary>
		/// Converts an RTF string to plain text
		/// </summary>
		/// <param name="contents"></param>
		/// <returns></returns>
		public static string ToPlainText(string contents)
		{
			if (String.IsNullOrEmpty(contents))
				return String.Empty;
			if (!contents.StartsWith("{\\rtf1"))
				return contents;

			if (!RtfUtil.Rtf2Text(contents, out contents))
				return String.Empty;
			return contents;
		}

		/// <summary>
		/// Convert a string to its HTML equivalent by replacing a minimal set of special characters
		/// with html tags. If the string is in RTF format it is first converted to plain text
		/// </summary>
		/// <param name="contents">Plain text to update</param>
		/// <returns> HTMLized text </returns>
		private static string Htmlize(string contents)
		{
			contents = ToPlainText(contents);
			contents = contents.TrimEnd();
            if (contents == String.Empty)
                return "&nbsp;";

			contents = contents
					.Replace("&", "&amp;")
					.Replace("\"", "&quot;")
					.Replace("'", "&#39;")
					.Replace("<", "&lt;")
					.Replace(">", "&gt;");
            Regex rgx = new Regex("\r\n|\r|\n");
            contents = rgx.Replace(contents, "<br>");
            return contents;
		}
	}
}
