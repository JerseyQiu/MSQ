using System.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.PM.Demographics
{
    /// <summary>
    /// This activity retrieves the attending MD of the patient by department.
    /// </summary>
    [PracticeManagement_ActivityGroup]
    [Demographics_Category]
    [GetAttendingMdActivity_DisplayName]
    public class GetAttendingMdActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary> The patient for whom the Attending MD will be returned. </summary>
        [InputParameterCategory]
        [GetAttendingMdActivity_PatId1_Description]
        [Patient_DisplayName]
        public InArgument<int> PatId1 { get; set; }

        /// <summary> The department for which the attending MD is assigned to the patient. </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [GetAttendingMdActivity_InstId_Description]
        [Department_DisplayName]
        public InArgument<int> InstId { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary>
        /// The primary key the attending MD looked up.
        /// </summary>
        [OutputParameterCategory]
        [GetAttendingMdActivity_AttendingMdId_Description]
        [GetAttendingMdActivity_AttendingMdId_DisplayName]
        public OutArgument<int?> AttendingMdId { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Lookup the attending md </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            //var mgr = new QclManager();
            //int chkId = mgr.CreateQcl(PatId1.Get(context),
            //    ProId.Get(context),
            //    StaffIdResponsible.Get(context),
            //    StaffIdRequesting.Get(context),
            //    DueDate.Get(context),
            //    CGroup.Get(context),
            //    PrsId.Get(context),
            //    Comment.Get(context));

            //ChkId.Set(context, chkId);
        }
        #endregion
    }
}