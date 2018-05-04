using LuaEditor.Helper;
using LuaEditor.Objetcts;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace LuaEditor.Dialogs
{
    public partial class FormNewFile : FormBase
    {
        private ProjectEntry _parentFolder;
        private string _fileExtension;

        public FormNewFile(EditorSettings settings, ProjectEntry parentFolder, string fileExtension)
            : base(settings)
        {
            InitializeComponent();

            if (parentFolder == null)
                throw new ArgumentNullException(nameof(parentFolder));

            if (string.IsNullOrEmpty(fileExtension))
                throw new ArgumentNullException(nameof(fileExtension));
            if (fileExtension[0] != '.')
                throw new ArgumentException($"{nameof(fileExtension)} must start with an '.' character.");

            _parentFolder = parentFolder;
            _fileExtension = fileExtension;

            InitErrorProviderIconPosition(tbxFilename);
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
            if (string.IsNullOrWhiteSpace(tbxFilename.Text))
            {
                errorProviderGeneral.SetError(tbxFilename, "Dateiname eingeben");
                e.Cancel = true;
            }
            else
            {
                string filename = tbxFilename.Text.Trim();
                if (!filename.EndsWith(_fileExtension, StringComparison.OrdinalIgnoreCase))
                {
                    filename += _fileExtension;
                }

                if (FileHelper.IsValidFilename(filename))
                {
                    errorProviderGeneral.SetError(tbxFilename, "Ungültiger Dateiname");
                    e.Cancel = true;
                }
                else
                {
                    string filePath = Path.Combine(_parentFolder.Location, filename);
                    if (File.Exists(filePath))
                    {
                        errorProviderGeneral.SetError(tbxFilename, "Datei ist bereits vorhanden");
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

        public string Filename
        {
            get
            {
                string result = tbxFilename.Text.Trim();
                if (!result.EndsWith(_fileExtension, StringComparison.OrdinalIgnoreCase))
                {
                    result += _fileExtension;
                }

                return result;
            }
        }
    }
}