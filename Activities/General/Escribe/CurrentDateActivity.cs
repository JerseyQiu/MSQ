using System;
using System.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.General.Escribe
{
	/// <summary>
	/// </summary>
	[GeneralCharting_ActivityGroup]
	[Escribe_Category]
	[CurrentDateActivity_DisplayName]
	public class CurrentDateActivity : MosaiqCodeActivity
	{
		#region Properties (Input Parameters)
		/// <summary>
		/// 
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CurrentDateActivity_DateFormat_DisplayName]
		[CurrentDateActivity_DateFormat_Description]
		public InArgument<int> DateFormat { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CurrentDateActivity_AlternateSeparators_DisplayName]
		[CurrentDateActivity_AlternateSeparators_Description]
		public InArgument<int> AlternateSeparators { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CurrentDateActivity_Capitalization_DisplayName]
		[CurrentDateActivity_Capitalization_Description]
		public InArgument<int> Capitalization { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CurrentDateActivity_PrependDayOfWeek_DisplayName]
		[CurrentDateActivity_PrependDayOfWeek_Description]
		public InArgument<int> PrependDayOfWeek { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CurrentDateActivity_KeepLeadingSpaces_DisplayName]
		[CurrentDateActivity_KeepLeadingSpaces_Description]
		public InArgument<bool> KeepLeadingSpaces { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CurrentDateActivity_BlankIfNoDate_DisplayName]
		[CurrentDateActivity_BlankIfNoDate_Description]
		public InArgument<bool> BlankIfNoDate { get; set; }
		#endregion

		#region Properties (Output Parameters)
		/// <summary> Standard parameter of a eScribe IQ script. Not used by this activity</summary>
		[OutputParameterCategory]
		[CurrentDateActivity_Html_DisplayName]
		[CurrentDateActivity_Html_Description]
		public OutArgument<string> Html { get; set; }

		/// <summary> </summary>
		[OutputParameterCategory]
		[CurrentDateActivity_PlainText_DisplayName]
		[CurrentDateActivity_PlainText_Description]
		public OutArgument<string> PlainText { get; set; }
		#endregion

		#region Overrides of MosaiqCodeActivity
		/// <summary>
		/// Format the current date giving the user the same options as the legacy eScribe merge fields wizard
		/// </summary>
		protected override void DoWork(CodeActivityContext context)
		{
			string result = DateTime.Now.ToShortDateString();
			switch (DateFormat.Get(context))
			{
				case 1:
					result = DateTime.Now.ToShortDateString();
					break;
				case 2:
					result = DateTime.Now.ToLongDateString();
					break;
				case 3:
					result = DateTime.Now.ToString("M/dd/yy");
					break;
				case 4:
					result = DateTime.Now.ToString("MM/dd/yy");
					break;
				case 5:
					result = DateTime.Now.ToString("MM/dd/yyyy");
					break;
				case 6:
					result = DateTime.Now.ToString("MMM dd, yyyy");
					break;
				case 7:
					result = DateTime.Now.ToString("MMMM dd, yyyy");
					break;
				case 8:
					result = DateTime.Now.ToString("dd/MM/yy");
					break;
				case 9:
					result = DateTime.Now.ToString("dd/MM/yyyy");
					break;
				case 10:
					result = DateTime.Now.ToString("dd MMM yy");
					break;
				case 11:
					result = DateTime.Now.ToString("dd MMM yyyy");
					break;
				case 12:
					result = DateTime.Now.ToString("yy/MM/dd");
					break;
				case 13:
					result = DateTime.Now.ToString("yyyy/MM/dd");
					break;
				case 14:
					result = DateTime.Now.ToString("yyMMdd");
					break;
				case 15:
					result = DateTime.Now.ToString("yyyyMMdd");
					break;
				case 16:
					result = DateTime.Now.ToString("MM/yy");
					break;
				case 17:
					result = DateTime.Now.ToString("MM/yyyy");
					break;
				case 18:
					result = DateTime.Now.ToString("yy/MM");
					break;
				case 19:
					result = DateTime.Now.ToString("yyyy/MM");
					break;
			}

			switch (AlternateSeparators.Get(context))
			{
				case 1:
					// do nothing
					break;
				case 2:
					// do nothing
					break;
				case 3:
					result = result.Replace('/', '.');
					break;
				case 4:
					result = result.Replace('/', '-');
					break;
				case 5:
					result = result.Replace('/', ' ');
					break;
				case 6:
					result = result.Replace('/', ' ');
					break;
			}

			string shortDayOfWeek = DateTime.Now.ToString("ddd");
			string longDayOfWeek = DateTime.Now.ToString("dddd");

			switch (Capitalization.Get(context))
			{
				case 1:
					// do nothing
					break;
				case 2:
					result = result.ToUpper();
					shortDayOfWeek = shortDayOfWeek.ToUpper();
					longDayOfWeek = longDayOfWeek.ToUpper();
					break;
				case 3:
					result = result.ToLower();
					shortDayOfWeek = shortDayOfWeek.ToLower();
					longDayOfWeek = longDayOfWeek.ToLower();
					break;
				case 4:
					// capitalize first letter
					break;
			}

			// It looks like the .Net 'control panel long format' includes the day of the week, while Clarion's 
			// function does not. For that reason we need to remove it first and then add it as necessary
			int pos = result.IndexOf(longDayOfWeek);
			if (pos >= 0)
				result = result.Remove(pos, longDayOfWeek.Length + 1).Trim();
			pos = result.IndexOf(shortDayOfWeek);
			if (pos >= 0)
				result = result.Remove(pos, shortDayOfWeek.Length + 1).Trim();

			switch (PrependDayOfWeek.Get(context))
			{
				case 1:
					break;
				case 2:
					result = String.Format("{0}, {1}", longDayOfWeek, result);
					break;
				case 3:
					result = String.Format("{0}, {1}", shortDayOfWeek, result);
					break;
			}

			PlainText.Set(context, result);
			Html.Set(context, null);
		}
		#endregion
	}
}
