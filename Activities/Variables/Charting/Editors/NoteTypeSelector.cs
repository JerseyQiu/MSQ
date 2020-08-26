using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;


namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors
{
    /// <summary>
    /// An editor that allows the user to select a note type
    /// </summary>
    public partial class NoteTypeSelector : XtraForm
    {
		class NoteType
		{
			public string Text { get; set; }
			public short Enum { get; set; }
		}

        #region Constructor and initialization
        /// <summary> Default ctor </summary>
        public NoteTypeSelector()
        {
            InitializeComponent();
        }

        private void NoteTypeSelector_Load(object sender, EventArgs e)
        {
            try
            {
				ImpacPersistenceManager pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
				var query = new ImpacRdbQuery(typeof(Prompt));
				query.AddClause(PromptDataRow.PGroupEntityColumn, EntityQueryOp.EQ, "#NT1");
				var noteTypes =
					pm.GetEntities<Prompt>(query).Select(ent => new NoteType { Text = ent.Text, Enum = ent.Enum }).ToList();

				foreach (var noteType in noteTypes)
					noteType.Text = IQNoteTypeVarConfig.FixNoteTypeName(noteType.Enum, noteType.Text);

				// sort the notes types by name
            	noteTypes.Sort((n1, n2) => n1.Text.CompareTo(n2.Text));

            	listNoteTypes.DisplayMember = "Text";
            	listNoteTypes.ValueMember = "Enum";
				listNoteTypes.DataSource = noteTypes;

            	listNoteTypes.SelectionMode = IsMultiSelect ? SelectionMode.MultiSimple : SelectionMode.One;
				if (!IsMultiSelect)
					checkAll.Enabled = false;
				listNoteTypes.SelectedIndex = -1;
				// Select the current value if there is one and if the mode is single-select
				if (!IsMultiSelect && Value != null && Value.Count > 0)
					for (int i = 0; i < listNoteTypes.ItemCount; i++)
					{
						var item = (NoteType) listNoteTypes.GetItem(i);
						if (item == null)
							continue;
						if (item.Enum != Value[0])
							continue;

						listNoteTypes.SelectedIndex = i;
						break;
					}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("NoteTypeSelector: " + ex);
            }
        }
        #endregion

        /// <summary>
        /// The note types to be displayed or returned
        /// </summary>
		public List<int> Value { get; set; }

		/// <summary>
		/// The mode in which the editor is open - single or multi select
		/// </summary>
		public bool IsMultiSelect { get; set; }


        #region Event Handlers
        private void barButtonItemOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			CloseDialog();
        }

        private void barButtonItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ValidateChildren();
            DialogResult = DialogResult.Cancel;
            Close();
        }

		private void checkAll_CheckedChanged(object sender, EventArgs e)
		{
			bool isChecked = checkAll.Checked;
			for (int i = 0; i < listNoteTypes.ItemCount; i++)
				listNoteTypes.SetSelected(i, isChecked);
		}
        #endregion

		private void CloseDialog()
		{
			Value = new List<int>();
			foreach (int idx in listNoteTypes.SelectedIndices)
			{
				var item = (NoteType) listNoteTypes.GetItem(idx);
				if (item != null)
					Value.Add(item.Enum);
			}

			ValidateChildren();
			DialogResult = DialogResult.OK;
			Close();
		}

        #region Overriden Methods
        /// <summary> Add special handling for enter and escape key presses </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
            		CloseDialog();
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    DialogResult = DialogResult.Cancel;
                    e.Handled = true;
                    Close();
                    break;
            }

            base.OnKeyDown(e);
        }
        #endregion
    }
}
