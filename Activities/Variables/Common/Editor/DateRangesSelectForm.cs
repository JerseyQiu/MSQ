using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Impac.Mosaiq.IQ.Common.Variable;

namespace Impac.Mosaiq.IQ.Activities.Variables.Common.Editor
{
	/// <summary> This form allows the user select and reorder the available date ranges for IQVarDateRangeActivity </summary>
    public partial class DateRangesSelectForm : XtraForm
	{
		class DateRangeItem
		{
			public int RangeType;

			public override string ToString()
			{
				return IQDateRangeElement.GetRangeDisplayName(RangeType);
			}
		}

        #region Constructor
        /// <summary> Default ctor </summary>
        public DateRangesSelectForm()
        {
            InitializeComponent();
        }
        #endregion


		/// <summary> The currently selected date ranges </summary>
		public List<int> Value { get; set; }

        #region Event Handlers

        private void DateRangesSelectForm_Load(object sender, EventArgs e)
        {
			// add the currently selected date ranges to the list
			foreach (var selectedItem in Value)
				listDateRanges.Items.Add(new DateRangeItem { RangeType = selectedItem }, true);

			// add the remaining available ranges
			foreach (var item in IQDateRangeElement.GetAvailableRanges())
				if (Value.IndexOf(item) == -1)
					listDateRanges.Items.Add(new DateRangeItem {RangeType = item}, false);
        }

        private void barButtonOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void barButtonCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void PredefinedStatesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ValidateChildren();

            if (DialogResult != DialogResult.OK)
                return;

			Value.Clear();
			// return only the selected date ranges
			for (int i = 0; i < listDateRanges.Items.Count; i++)
			{
				var item = (DateRangeItem) listDateRanges.Items[i];
				bool selected = listDateRanges.GetItemChecked(i);
				if (selected)
					Value.Add(item.RangeType);
			}
        }

		private void barButtonMoveUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (listDateRanges.SelectedIndex < 1)
				return;
			MoveSelectedItem(listDateRanges.SelectedIndex - 1);
		}

		private void barButtonMoveDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (listDateRanges.SelectedIndex < 0 || listDateRanges.SelectedIndex == listDateRanges.Items.Count - 1)
				return;
			MoveSelectedItem(listDateRanges.SelectedIndex + 1);
		}

		private void MoveSelectedItem(int index)
		{
			object item = listDateRanges.SelectedItem;
			bool isChecked = listDateRanges.GetItemChecked(listDateRanges.SelectedIndex);
			listDateRanges.Items.Remove(item);
			listDateRanges.Items.Insert(index, item);
			listDateRanges.SetItemChecked(index, isChecked);
			listDateRanges.SelectedIndex = index;
		}

        #endregion

        #region Overriden Methods

        /// <summary> Add special handling for enter and escape key presses </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    DialogResult = DialogResult.OK;
                    e.Handled = true;
                    Close();
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
