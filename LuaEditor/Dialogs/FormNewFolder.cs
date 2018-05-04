using LuaEditor.Helper;
using LuaEditor.Objetcts;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace LuaEditor.Dialogs
{
    public partial class FormNewFolder : FormBase
    {
        private ProjectEntry _parentFolder;

        public FormNewFolder(EditorSettings settings, ProjectEntry parentFolder)
            : base(settings)
        {
            InitializeComponent();

            if (parentFolder == null)
                throw new ArgumentNullException(nameof(parentFolder));

            _parentFolder = parentFolder;

            InitErrorProviderIconPosition(tbxFoldername);
        }

        private void InitErrorProviderIconPosition(params Control[] controls)
        {
            foreach (var control in controls)
            {
                errorProviderGeneral.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                errorProviderGeneral.SetIconPadding(control, 3);
            }
        }

        private void tbxFilename_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxFoldername.Text))
            {
                errorProviderGeneral.SetError(tbxFoldername, "Name kann nicht leer sein");
                e.Cancel = true;
            }
            else
            {
                string folderName = tbxFoldername.Text.Trim();
                if (FileHelper.IsValidFoldername(folderName))
                {
                    errorProviderGeneral.SetError(tbxFoldername, "Der Name enthält ungültige Zeichen");
                    e.Cancel = true;
                }
                else
                {
                    string path = Path.Combine(_parentFolder.Location, folderName);
                    if (Directory.Exists(path))
                    {
                        errorProviderGeneral.SetError(tbxFoldername, "Der Ordner existiert bereits");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
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

        public string Foldername
        {
            get { return tbxFoldername.Text.Trim(); }
        }
    }
}