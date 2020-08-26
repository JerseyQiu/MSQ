using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;

namespace Impac.Mosaiq.IQ.Activities.General.Careplan
{
    /// <summary>
    /// This form opens a browse that lists the intent prompt values available for selection.
    /// </summary>
    public partial class IntentPromptSelectorForm : XtraForm
    {
        #region Constructors

        /// <summary>
        /// Parameterless Ctor
        /// </summary>
        public IntentPromptSelectorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ctor with persistence manager.
        /// </summary>
        public IntentPromptSelectorForm(ImpacPersistenceManager pManager)
        {
            InitializeComponent();
            _pm = pManager;
        }

        #endregion

        #region Private Fields

        /// <summary>The PM used to load the prompt values</summary>
        private readonly ImpacPersistenceManager _pm;

        /// <summary>The prompt values that will be displayed in the form</summary>
        private EntityList<Prompt> _promptValues;

        #endregion

        #region Public Properties

        /// <summary>
        /// The selected Prompt entity
        /// </summary>
        public Prompt SelectedItem { get; set; }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles form Loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntentPromptSelectorForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                //pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();

                var query = new ImpacRdbQuery(typeof (Prompt));
                query.AddClause(PromptDataRow.PGroupEntityColumn, EntityQueryOp.EQ, "MEDA");
                query.AddOrderBy(PromptDataRow.TextEntityColumn, ListSortDirection.Ascending);
                _promptValues = _pm.GetEntities<Prompt>(query);
                bsPropmt.DataSource = _promptValues;
            }
        }

        /// <summary>
        /// Handles form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntentPromptSelectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SelectedItem = listBoxControlPromptValues.SelectedItem as Prompt;
        }

        /// <summary>
        /// Handles "Select" button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}