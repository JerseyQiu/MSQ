using System.Activities;
using Impac.Mosaiq.Business.PracticeManagement;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.PM.QCL
{
    /// <summary> This activity marks a QCL as completed. </summary>
    [CompleteQclActivity_DisplayName]
    [QualityChecklist_Category]
    [PracticeManagement_ActivityGroup]
    public class CompleteQclActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary>
        /// The primary key of the Chklist record to be marked complete.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CompleteQclActivity_ChkId_Description]
        [CompleteQclActivity_ChkId_DisplayName]
        public InArgument<int> ChkId { get; set; }

        /// <summary>
        /// The Staff.Staff_Id of the Completed staff/location.
        /// </summary>
        [InputParameterCategory]
        [CompleteQclActivity_StaffIdCompleted_Description]
        [CompleteQclActivity_StaffIdCompleted_DisplayName]
        public InArgument<int?> StaffIdCompleted { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Marks the looked up QCL record as complete.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {   
            var mgr = new QclManager {PM = PM};
            mgr.CompleteQcl(ChkId.Get(context), StaffIdCompleted.Get(context));
        }
        #endregion
    }
}