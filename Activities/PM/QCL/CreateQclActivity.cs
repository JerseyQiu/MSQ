using System;
using System.Activities;
using Impac.Mosaiq.Business.PracticeManagement;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.PM.QCL
{
    /// <summary>
    /// This activity silently adds a QCL to the database with no UI interaction.
    /// </summary>
    [QualityChecklist_Category]
    [CreateQclActivity_DisplayName]
    [PracticeManagement_ActivityGroup]
    public class CreateQclActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary>
        /// The primary key of the patient to whom we will assign this QCL Item too.
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_PatId1_Description]
        [Patient_DisplayName]
        public InArgument<int?> PatId1 { get; set; }

        /// <summary>
        /// The Prompt.Pro_Id of the Checklist Procedure selected.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CreateQclActivity_TskId_Description]
        [CreateQclActivity_TskId_DisplayName]
        public InArgument<int> TskId { get; set; }

        /// <summary>
        /// The Staff.Staff_Id of the responsible staff/location.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CreateQclActivity_StaffIdResponsible_Description]
        [CreateQclActivity_StaffIdResponsible_DisplayName]
        public InArgument<int> StaffIdResponsible { get; set; }

        /// <summary>
        /// The Staff.Staff_Id of the requesting staff/location.
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_StaffIdRequesting_Description]
        [CreateQclActivity_StaffIdRequesting_DisplayName]
        public InArgument<int?> StaffIdRequesting { get; set; }

        /// <summary>
        /// The due date that will be set for the created QCL
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_DueDate_Description]
        [CreateQclActivity_DueDate_DisplayName]
        public InArgument<DateTime?> DueDate { get; set; }

        /// <summary>
        /// The comment that will be set for the created QCL
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_Comment_Description]
        [CreateQclActivity_Comment_DisplayName]
        public InArgument<string> Comment { get; set; }

        /// <summary>
        /// The instruction that will be set for the created QCL
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_Instructions_Description]
        [CreateQclActivity_Instructions_DisplayName]
        public InArgument<string> Instructions { get; set; }


        /// <summary>
        /// The Pro_Id of the Code Group that will be set for the created QCL
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_CGroup_Description]
        [CreateQclActivity_CGroup_DisplayName]
        public InArgument<int?> CGroup { get; set; }

        /// <summary>
        /// The Prs_Id of the Code that will be set for the created QCL
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_PrsId_Description]
        [CreateQclActivity_PrsId_DisplayName]
        public InArgument<int?> PrsId { get; set; }

        #endregion

        #region Properties (Output Parameters)
        /// <summary>
        /// The primary key of the newly created QCL item
        /// </summary>
        [OutputParameterCategory]
        [CreateQclActivity_ChkId_Description]
        [CreateQclActivity_ChkId_DisplayName]
        public OutArgument<int> ChkId { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Create the QCL record.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            var mgr = new QclManager{PM = PM};
            int chkId = mgr.CreateQcl(PatId1.Get(context),
                TskId.Get(context),
                StaffIdResponsible.Get(context),
                StaffIdRequesting.Get(context),
                DueDate.Get(context),
                CGroup.Get(context),
                PrsId.Get(context),
                Instructions.Get(context),
                Comment.Get(context));

            ChkId.Set(context, chkId);
        }
        #endregion
    }
}