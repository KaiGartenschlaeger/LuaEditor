using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LuaEditor.Dialogs
{
    public partial class FormSettings : FormBase
    {
        #region Constructor

        public FormSettings()
        {
            InitializeComponent();
        }

        #endregion

        #region Helper

        private void ChangeFont(Label fontControl)
        {
            using (FontDialog dialog = new FontDialog())
            {
                dialog.Font = fontControl.Font;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    fontControl.Font = dialog.Font;
                }
            }
        }

        #endregion

        #region Control events

        private void btnChangeLuaInterpreterPath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Ausführen (Anwendung)";

                if (!string.IsNullOrEmpty(tbxStartApplcationPath.Text))
                    dialog.InitialDirectory = Path.GetDirectoryName(tbxStartApplcationPath.Text);

                dialog.Filter = "Anwendung|*.exe";
                dialog.FileName = tbxStartApplcationPath.Text;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    tbxStartApplcationPath.Text = dialog.FileName;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnEditorFont_Click(object sender, EventArgs e)
        {
            ChangeFont(lblEditorFont);
        }

        private void btnConsoleFont_Click(object sender, EventArgs e)
        {
            ChangeFont(lblConsoleFont);
        }

        #endregion

        #region Properties

        public string ApplicationPath
        {
            get { return tbxStartApplcationPath.Text; }
            set { tbxStartApplcationPath.Text = value; }
        }

        public string ApplicationArguments
        {
            get { return tbxStartApplicationArguments.Text; }
            set { tbxStartApplicationArguments.Text = value; }
        }

        public bool HideApplicationWindow
        {
            get { return chbHideWindow.Checked; }
            set { chbHideWindow.Checked = value; }
        }

        public bool RedirectOutput
        {
            get { return chbRedirectOutput.Checked; }
            set { chbRedirectOutput.Checked = value; }
        }

        public Font EditorFont
        {
            get { return lblEditorFont.Font; }
            set { lblEditorFont.Font = value; }
        }

        public Font ConsoleFont
        {
            get { return lblConsoleFont.Font; }
            set { lblConsoleFont.Font = value; }
        }

        #endregion
    }
}