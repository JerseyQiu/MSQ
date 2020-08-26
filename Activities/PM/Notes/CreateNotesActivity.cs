using Impac.Mosaiq.Business.PracticeManagement;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using System.Activities;

namespace Impac.Mosaiq.IQ.Activities.PM.Notes
{
    [Notes_Category]
    [CreateNoteActivity_DisplayName]
    [PracticeManagement_ActivityGroup]
    public class CreateNotesActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary>
        /// The primary key of the patient to whom we will assign this QCL Item too.
        /// </summary>
        [InputParameterCategory]
        [CreateNoteActivity_PatId1_Description]
        [Patient_DisplayName]
        public InArgument<int?> PatId1 { get; set; }

        /// <summary>
        /// The Prompt.Pro_Id of the Checklist Procedure selected.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CreateNoteActivity_NoteType_Description]
        [CreateNoteActivity_NoteType_DisplayName]
        public InArgument<short> NoteType { get; set; }

        /// <summary>
        /// The Staff.Staff_Id of the responsible staff/location.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CreateNoteActivity_Subject_Description]
        [CreateNoteActivity_Subject_DisplayName]
        public InArgument<string> Subject { get; set; }

        /// <summary>
        /// The Staff.Staff_Id of the requesting staff/location.
        /// </summary>
        [InputParameterCategory]
        [CreateNoteActivity_Note_Description]
        [CreateNoteActivity_Note_DisplayName]
        public InArgument<string> Note { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary>
        /// The primary key of the newly created QCL item
        /// </summary>
        [OutputParameterCategory]
        [CreateNoteActivity_NoteId_Description]
        [CreateNoteActivity_NoteId_DisplayName]
        public OutArgument<int> NoteId { get; set; }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Create the QCL record.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            var mgr = new NotesManager { PM = PM };
            int noteId = mgr.CreateNote(PatId1.Get(context),
                NoteType.Get(context),
                Subject.Get(context),
                Note.Get(context));

            NoteId.Set(context, noteId);
        }
        #endregion Overridden Methods
    }
}
