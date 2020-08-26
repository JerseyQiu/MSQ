using System.Activities;
using Impac.Mosaiq.Business.PracticeManagement;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.PM.QCL
{
    /// <summary>
    /// This activity deletes a QCL item from the database.  In reality, it updates the data to show that it is suppressed,
    /// which is what happens on the UI when deleting from the UI.
    /// </summary>
    [DeleteQclActivity_DisplayName]
    [QualityChecklist_Category]
    [PracticeManagement_ActivityGroup]
    public class DeleteQclActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary>
        /// The primary key of the QCL item being deleted
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [DeleteQclActivity_ChkId_Description]
        [DeleteQclActivity_ChkId_DisplayName]
        public InArgument<int> ChkId { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Marks a QCL Item as deleted.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            var mgr = new QclManager{PM = PM};
            mgr.DeleteQcl(ChkId.Get(context));
        }
        #endregion
    }
}