using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LuaEditor.Dialogs
{
    public partial class FormSearch : FormBase
    {
        #region Fields

        private Regex _regularExpression;

        #endregion

        #region Constructor

        public FormSearch()
        {
            InitializeComponent();
            InitErrorProviderIconPosition(tbxSearch);
        }

        #endregion

        #region Initialize methods

        private void InitErrorProviderIconPosition(params Control[] controls)
        {
            foreach (var control in controls)
            {
                errorProviderGeneral.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                errorProviderGeneral.SetIconPadding(control, 3);
            }
        }

        #endregion

        #region Control events

        private void tbxSearch_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxSearch.Text))
            {
                errorProviderGeneral.SetError(tbxSearch, "Suchbegriff kann nicht leer sein.");
                e.Cancel = true;
            }
            else
            {
                if (chbRegex.Checked)
                {
                    try
                    {
                        RegexOptions options = RegexOptions.IgnoreCase;
                        if (chbMatchCase.Checked)
                            options = RegexOptions.None;

                        _regularExpression = new Regex(tbxSearch.Text, options);

                        errorProviderGeneral.SetError(tbxSearch, string.Empty);
                    }
                    catch (ArgumentException ex)
                    {
                        errorProviderGeneral.SetError(tbxSearch, "Der Ausdruck ist ungültig:\n" + ex.Message);
                    }
                }
                else
                {
                    _regularExpression = null;
                    errorProviderGeneral.SetError(tbxSearch, string.Empty);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                if (OnSearch != null)
                {
                    OnSearch(this, EventArgs.Empty);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region Events

        public event EventHandler OnSearch;

        #endregion

        #region Properties

        [Browsable(true)]
        public string SearchText
        {
            get { return tbxSearch.Text; }
            set { tbxSearch.Text = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Regex RegularExpression
        {
            get { return _regularExpression; }
        }

        [Browsable(true)]
        public bool MatchCase
        {
            get { return chbMatchCase.Checked; }
            set { chbMatchCase.Checked = value; }
        }

        #endregion
    }
}