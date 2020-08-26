using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Impac.Mosaiq.Core.Toolbox.Forms;
using Padding = DevExpress.XtraLayout.Utils.Padding;

namespace Impac.Mosaiq.IQ.Activities.Tools.MsgBox
{
    /// <summary>
    /// This form is use in conjunction with the ListPromptActivity.  This form displays the
    /// options that the user can select from as defined in the ListPromptActivity.
    /// </summary>
    public partial class MultiOptionButtonForm : XtraForm
    {
        #region Constructor
        /// <summary>
        /// Default Ctor.
        /// </summary>
        public MultiOptionButtonForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Fields
        private readonly List<SimpleButton> _buttons = new List<SimpleButton>();
        #endregion

        #region Properties
        /// <summary>
        /// List of options which will appear in the form.
        /// </summary>
        public List<String> Choices { get; set; }

        /// <summary>
        /// Message which will appear above the drop down.
        /// </summary>
        public string Message 
        {
            get { return labelControlMessage.Text; }
            set { labelControlMessage.Text = value; }
        }

        /// <summary> Determines if the cancel button will be visible on the form. </summary>
        public bool ShowCancelButton { get; set; }

        /// <summary>
        /// The index of the selected item
        /// </summary>
        public int SelectedIndex { get; private set; }

        /// <summary>
        /// The text of the selected item.
        /// </summary>
        public string SelectedText { get; private set; }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Loads the Prompt Items into the list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiOptionButtonForm_Load(object sender, EventArgs e)
        {
            if (Choices != null && Choices.Count > 0)
            {
                foreach(string str in Choices)
                {
                    var button = new SimpleButton { Text = str, 
                        Font = new Font("Arial", 9F), 
                        Name = Guid.NewGuid().ToString(), 
                        DialogResult = DialogResult.OK};

                    button.Appearance.Options.UseFont = true;
                    button.Appearance.TextOptions.HAlignment = HorzAlignment.Near;

                    string strVal = str;
                    button.Click += (obj, args) =>
                                        {
                                            SelectedIndex = _buttons.IndexOf((SimpleButton)obj);
                                            SelectedText = strVal;
                                            Close();
                                        };
                    var item = new LayoutControlItem 
                    {Control = button, 
                        TextVisible = false, 
                        Name = Guid.NewGuid().ToString(),
                        Padding = new Padding(5)
                    };
                    layoutControlGroupButtons.AddItem(item);
                    _buttons.Add(button);
                }

                if(!ShowCancelButton)
                    layoutMain.HideItem(lciCancel);
            }
            
            BringToFront();
            Focus();
            WinFormsUtility.DoEvents();
        }

        /// <summary>
        /// Handles the layout event of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiOptionButtonForm_Layout(object sender, LayoutEventArgs e)
        {
            SetWindowSize();
        }

        /// <summary>
        /// Handle the cancel button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        #region Private Methods
        private void SetWindowSize()
        {
            int maxButtonWidth = 0;

            foreach(SimpleButton button in _buttons)
            {
                if (button.Width > maxButtonWidth)
                    maxButtonWidth = button.Width;
            }
            Size = new Size(maxButtonWidth + 50, Size.Height);

            var newHeight = 18 + 35 * (Choices.Count) + labelControlMessage.Height + 26;

            if (ShowCancelButton)
                newHeight += 35;
            
            Size = new Size(Size.Width, newHeight);
            MaximumSize = Size;
        }
        #endregion
    }
}