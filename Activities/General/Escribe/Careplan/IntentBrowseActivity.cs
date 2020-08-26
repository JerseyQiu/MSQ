using System.Activities;
using System.Windows.Forms;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.General.Careplan
{
    /// <summary>
    /// This activity allows the user to select an intent value.
    /// </summary>
    [IntentBrowseActivity_DisplayName]
    [Careplan_Category]
    [GeneralCharting_ActivityGroup]
    public class IntentBrowseActivity : MosaiqUICodeActivity
    {
        #region Properties (Output Parameters)

        /// <summary>
        /// The Prompt.Pro_Id value of the selected prompt record
        /// </summary>
        [OutputParameterCategory]
        [IntentBrowseActivity_Prompt_Pro_Id_Description]
        public OutArgument<int> PromptProId { get; set; }

        /// <summary>
        /// The text value of the selected intent value.
        /// </summary>
        [OutputParameterCategory]
        [IntentBrowseActivity_SelectedText_Description]
        [IntentBrowseActivity_SelectedText_DisplayName]
        public OutArgument<string> SelectedText { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Opens the intent prompt browse and allows the user to select a prompt value.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            ImpacPersistenceManager pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
            var form = new IntentPromptSelectorForm(pm);

            if (form.ShowDialog() == DialogResult.OK)
            {
                PromptProId.Set(context, form.SelectedItem.Pro_ID);
                SelectedText.Set(context, form.SelectedItem.Text);
            }
        }
        #endregion
    }
}