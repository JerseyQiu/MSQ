using System;
using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.PM.QCL
{
    //TODO:  This activity needs to return the primary key of the QCL item created.
    /// <summary>
    /// This activity opens the QCL Form and pre-populates it with the procedure and responsible staff
    /// assigned in the designer.
    /// </summary>
    [CreateQclWithFormActivity_DisplayName]
    [QualityChecklist_Category]
    [PracticeManagement_ActivityGroup]
    public class CreateQclWithFormActivity : MosaiqUICodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary>
        /// The Prompt.Pro_Id of the Checklist Procedure selected.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CreateQclWithFormActivity_ProId_Description]
        [CreateQclActivity_ProId_DisplayName]
        public InArgument<int> ProId { get; set; }

        /// <summary>
        /// The Staff_Id of the responsible staff.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [CreateQclWithFormActivity_StaffIdResponsible_Description]
        [CreateQclActivity_StaffIdResponsible_DisplayName]
        public InArgument<int> StaffIdResponsible { get; set; }

        /// <summary>
        /// The comment that will be set for the created QCL
        /// </summary>
        [InputParameterCategory]
        [CreateQclActivity_Comment_Description]
        [CreateQclActivity_Comment_DisplayName]
        public InArgument<string> Comment { get; set; }
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
        /// Opens the QCL Form.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            //Get Values off the arguments
            ImpacPersistenceManager pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
            int proId = ProId.Get(context);
            int staffIdResponsible = StaffIdResponsible.Get(context);
            string comment = Comment.Get(context);

            if (String.IsNullOrWhiteSpace(comment))
                comment = String.Empty;

            //Check if values exist in the database.
            var prompt = pm.GetEntity<Prompt>(new PrimaryKey(typeof(Prompt), proId));
            if (prompt.IsNullEntity)
                throw new InvalidOperationException("Cannot find QCL Procedure (Prompt) record with Pro_Id: " + proId);

            var staffResponsible = pm.GetEntity<Staff>(new PrimaryKey(typeof(Staff), staffIdResponsible));
            if (staffResponsible.IsNullEntity)
                throw new InvalidOperationException("Cannot find Responsible (Staff) record with Staff_Id: " + staffIdResponsible);

            int chkId = CallClarion.AddQclRecord(prompt.Text, comment, staffIdResponsible);
            ChkId.Set(context, chkId);
        }

        #endregion
    }
}