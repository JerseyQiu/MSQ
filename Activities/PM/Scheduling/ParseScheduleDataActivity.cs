using System;
using System.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.PM.Scheduling
{
	/// <summary> Returns the next day the clinic is open. </summary>
	[ParseScheduleDataActivity_DisplayName]
	[Scheduling_Category]
	[PracticeManagement_ActivityGroup]
	public class ParseScheduleDataActivity : MosaiqCodeActivity
	{
		#region Properties (Input Parameters)
		///<summary>Sets the date from which to begin the search.
		///</summary>
		[InputParameterCategory]
		[ParseScheduleDataActivity_ScheduleData_Description]
		[ParseScheduleDataActivity_ScheduleData_DisplayName]
		public InArgument<string> ScheduleData { get; set; }
		#endregion

		#region Properties (Output Parameters)
		/// <summary> Returns the DateTime of the next day the clinic is open </summary>
		[OutputParameterCategory]
		[ParseScheduleDataActivity_PatId1_Description]
		[ParseScheduleDataActivity_PatId1_DisplayName]
		public OutArgument<int?> PatId1 { get; set; }

		[OutputParameterCategory]
		[ParseScheduleDataActivity_AppDtTm_Description]
		[ParseScheduleDataActivity_AppDtTm_DisplayName]
		public OutArgument<DateTime> AppDtTm { get; set; }

		[OutputParameterCategory]
		[ParseScheduleDataActivity_Activity_Description]
		[ParseScheduleDataActivity_Activity_DisplayName]
		public OutArgument<string> Activity { get; set; }

		[OutputParameterCategory]
		[ParseScheduleDataActivity_StaffId_Description]
		[ParseScheduleDataActivity_StaffId_DisplayName]
		public OutArgument<int?> StaffId { get; set; }

		[OutputParameterCategory]
		[ParseScheduleDataActivity_Private_Description]
		[ParseScheduleDataActivity_Private_DisplayName]
		public OutArgument<short> Private { get; set; }

		[OutputParameterCategory]
		[ParseScheduleDataActivity_CreateId_Description]
		[ParseScheduleDataActivity_CreateId_DisplayName]
		public OutArgument<int?> CreateId { get; set; }

		[OutputParameterCategory]
		[ParseScheduleDataActivity_CreateDtTm_Description]
		[ParseScheduleDataActivity_CreateDtTm_DisplayName]
		public OutArgument<DateTime?> CreateDtTm { get; set; }
		#endregion

		#region Overriden Methods
		/// <summary>
		/// Finds the next day with clinic hours for the current department.  Searches 30 days into the future
		/// and then throws an InvalidOperationException.
		/// </summary>
		/// <param name="context"></param>
		protected override void DoWork(CodeActivityContext context)
		{
			var scheduleData = ScheduleData.Get(context);
			var pairs = scheduleData.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var pair in pairs)
			{
				var tuple = pair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
				if (tuple.Length != 2)
					continue;

				var name = tuple[0].Trim().ToLower();
				var value = tuple[1].Trim();

				switch (name)
				{
					case "pat_id1":
						PatId1.Set(context, Int32.Parse(value));
						break;
					case "appt_dttm":
						AppDtTm.Set(context, DateTime.Parse(value));
						break;
					case "activity":
						Activity.Set(context, value);
						break;
					case "staff_id":
						StaffId.Set(context, Int32.Parse(value));
						break;
					case "private": // TODO: should we call the IsAppointmentPrivate logic from the Schedule BOM entity?
						Private.Set(context, Int16.Parse(value) != 0);
						break;
					case "create_id":
						CreateId.Set(context, Int32.Parse(value));
						break;
					case "create_dttm":
						CreateDtTm.Set(context, DateTime.Parse(value));
						break;
				}
			}
		}
		#endregion
	}
}
