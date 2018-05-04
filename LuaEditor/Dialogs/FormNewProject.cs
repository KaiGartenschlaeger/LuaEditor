using LuaEditor.Objetcts;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LuaEditor.Dialogs
{
    public partial class FormNewProject : FormBase
    {
        #region Constructor

        public FormNewProject()
        {
            InitializeComponent();
            InitErrorProviderIconPosition(tbxName, tbxRootDirectory);
        }

        #endregion

        #region Control events

        private void InitErrorProviderIconPosition(params Control[] controls)
        {
            foreach (var control in controls)
            {
                errorProviderGenerel.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                errorProviderGenerel.SetIconPadding(control, 3);
            }
        }

        private void FormNewProject_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                tbxRootDirectory.Text = Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments);
            }
        }

        private void btnChangeRootDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Projekt Verzeichnis:";
                dialog.SelectedPath = tbxRootDirectory.Text;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    tbxRootDirectory.Text = dialog.SelectedPath;
                }
            }
        }

        private void tbxName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxName.Text))
            {
                errorProviderGenerel.SetError(tbxName, "Projektname eingeben");
                e.Cancel = true;
            }
            else
            {
                errorProviderGenerel.SetError(tbxName, string.Empty);
            }
        }

        private void tbxRootDirectory_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxRootDirectory.Text))
            {
                errorProviderGenerel.SetError(tbxRootDirectory, "Projektverzeichnis auswählen");
                e.Cancel = true;
            }
            else
            {
                errorProviderGenerel.SetError(tbxRootDirectory, string.Empty);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region Properties

        public string ProjectName
        {
            get { return tbxName.Text.Trim(); }
        }

        public string ProjectRootDirectory
        {
            get { return tbxRootDirectory.Text.Trim(); }
        }

        #endregion
    }
}