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
    [QclExistsActivity_DisplayName]
    [PracticeManagement_ActivityGroup]
    public class QclExistsActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary>
        /// The primary key of the patient used for the QCL search.
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_PatId1_Description]
        [Patient_DisplayName]
        public InArgument<int?> PatId1 { get; set; }

        /// <summary>
        /// The Prompt.TSK_Id of the Checklist Task used for the QCL search.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CreateQclActivity_TskId_Description]
        [CreateQclActivity_TskId_DisplayName]
        public InArgument<int> TskId { get; set; }

        /// <summary>
        /// The Staff.Staff_Id of the responsible staff/location used for the QCL search..
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CreateQclActivity_StaffIdResponsible_Description]
        [CreateQclActivity_StaffIdResponsible_DisplayName]
        public InArgument<int> StaffIdResponsible { get; set; }

        /// <summary>
        /// The Staff.Staff_Id of the requesting staff/location used for the QCL search..
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_StaffIdRequesting_Description]
        [CreateQclActivity_StaffIdRequesting_DisplayName]
        public InArgument<int?> StaffIdRequesting { get; set; }

        /// <summary>
        /// The due date used for the QCL search.
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_DueDate_Description]
        [CreateQclActivity_DueDate_DisplayName]
        public InArgument<DateTime?> DueDate { get; set; }

        /// <summary>
        /// The instruction used for the QCL search.
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_Comment_Description]
        [CreateQclActivity_Comment_DisplayName]
        public InArgument<string> Comment { get; set; }

        /// <summary>
        /// The instruction used for the QCL search.
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_Instructions_Description]
        [CreateQclActivity_Instructions_DisplayName]
        public InArgument<string> Instructions { get; set; }


        /// <summary>
        /// The Pro_Id of the code group used for the QCL search.
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_CGroup_Description]
        [CreateQclActivity_CGroup_DisplayName]
        public InArgument<int?> CGroup { get; set; }

        /// <summary>
        /// The Prs_Id of the code used for the QCL search.
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_PrsId_Description]
        [CreateQclActivity_PrsId_DisplayName]
        public InArgument<int?> PrsId { get; set; }

        /// <summary>
        /// Indicates whether or not to look for QCL's that are completed.  If the value is null, both completed and 
        /// non-completed QCL's will constitute a match.
        /// </summary>
        [InputParameterCategory]
        [QclExistsActivity_Completed_DisplayName]
        [QclExistsActivity_Completed_Description]
        public InArgument<bool?> Completed { get; set; }

        #endregion

        #region Properties (Output Parameters)
        /// <summary>
        /// The primary key of the newly created QCL item
        /// </summary>
        [OutputParameterCategory]
        [QclExistsActivity_QclExists_Description]
        [QclExistsActivity_QclExists_DisplayName]
        public OutArgument<bool> QclExists { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Checks for QCL's with attributes matching the parameters passed in.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            var mgr = new QclManager{PM = PM};
            bool qclExists = mgr.CheckQclExists(PatId1.Get(context),
                TskId.Get(context),
                StaffIdResponsible.Get(context),
                StaffIdRequesting.Get(context),
                DueDate.Get(context),
                CGroup.Get(context),
                PrsId.Get(context),
                Instructions.Get(context),
                Comment.Get(context),
                Completed.Get(context));

            QclExists.Set(context, qclExists);
        }
        #endregion
    }
}