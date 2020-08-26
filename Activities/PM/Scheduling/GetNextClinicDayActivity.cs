using System;
using System.Activities;
using Impac.Mosaiq.Core.Globals;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.PM.Scheduling
{
    /// <summary> Returns the next day the clinic is open. </summary>
    [GetNextClinicDay_DisplayName]
    [Scheduling_Category]
    [PracticeManagement_ActivityGroup]
    public class GetNextClinicDayActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        ///<summary>Sets the date from which to begin the search.
        ///</summary>
        [InputParameterCategory]
        [GetNextClinicDayActivity_SearchStartDate_Description]
        [GetNextClinicDayActivity_SearchStartDate_DisplayName]
        public InArgument<DateTime?> SearchStartDate { get; set; }
        #endregion
        
        #region Properties (Output Parameters)
        /// <summary> Returns the DateTime of the next day the clinic is open </summary>
        [OutputParameterCategory]
        [GetNextClinicDayActivity_NextClinicDate_Description]
        [GetNextClinicDayActivity_NextClinicDate_DisplayName]
        public OutArgument<DateTime> NextClinicDay { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Finds the next day with clinic hours for the current department.  Searches 30 days into the future
        /// and then throws an InvalidOperationException.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            ClarionDateTime cDate = null;
            bool nextClinicDayFound = false;
            DateTime? searchStartDate = SearchStartDate.Get(context);

            //Determine whether to begin with a date provided or today's date.
            DateTime startDate = searchStartDate.HasValue 
                ? searchStartDate.Value 
                : DateTime.Today;

            for (int i = 0; i <= 60; i++)
            {
                cDate = startDate.AddDays(i);

                //Must be a clinic date and not a holiday.
                if (CallClarion.CallIsClinicDay(cDate.Date, Global.Inst_ID) &&
                    !CallClarion.CallIsHoliday(cDate.Date, Global.Inst_ID))
                {
                    nextClinicDayFound = true;
                    break;
                }
            }

            if (!nextClinicDayFound)
                throw new InvalidOperationException(Strings.GetNextClinicDayActivity_NextClinicDayNotFound);

            NextClinicDay.Set(context, cDate);
        }
        #endregion
    }
}