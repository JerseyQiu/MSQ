using Impac.Mosaiq.Business.PracticeManagement;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using System.Activities;

namespace Impac.Mosaiq.IQ.Activities.PM.QCL
{
    /// <summary>
    /// Attach an existing note to a Qcl reqcord
    /// </summary>
    /// <seealso cref="Impac.Mosaiq.IQ.Core.Framework.Activities.MosaiqCodeActivity" />
    [AttachNoteToQclActivity_DisplayName]
    [QualityChecklist_Category]
    [PracticeManagement_ActivityGroup]
    public class AttachNoteToQclActivity: MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary>
        /// The primary key of the Chklist record to be marked complete.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [AttachNoteToQclActivity_ChkId_Description]
        [AttachNoteToQclActivity_ChkId_DisplayName]
        public InArgument<int> ChkId { get; set; }

        /// <summary>
        /// The Staff.Staff_Id of the Completed staff/location.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [AttachNoteToQclActivity_NoteId_Description]
        [AttachNoteToQclActivity_NoteId_DisplayName]
        public InArgument<int> NoteId { get; set; }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Marks the looked up QCL record as complete.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            var mgr = new QclManager { PM = PM };
            mgr.AttachNoteToQcl(ChkId.Get(context), NoteId.Get(context));
        }
        #endregion Overridden Methods
    }
}
