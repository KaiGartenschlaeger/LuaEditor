using LuaEditor.Objetcts;
using LuaEditor.Properties;
using PureSoft.Controls.VisualStudio.Renderer;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LuaEditor.Dialogs.Controls
{
    public partial class ScintillaWrapperControl : UserControl
    {
        #region Constants

        public const string Lua_Keywords = "print and break do else elseif end false for function if in local global nil not or repeat return then true until while";

        #endregion

        #region Fields

        private readonly List<string> _autoCompletionList = new List<string>();
        private Scintilla _scintillaControl;
        private Font _editorFont;
        private ProjectEntry _entry;
        private EditorTheme _theme;
        private string _lastText;
        private FormSearch _formSearch;

        private IList<Range> _searchResults;
        private int _searchIndex;
        private string _lastSearchText;
        private bool _lastSearchMatchCase;
        private bool _lastSearchWasRegex;

        #endregion

        #region Constructor

        public ScintillaWrapperControl(ProjectEntry entry, EditorTheme theme)
        {
            InitializeComponent();

            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            if (theme == null)
                throw new ArgumentNullException(nameof(theme));

            _entry = entry;
            _theme = theme;
        }

        #endregion

        #region Control events

        private void LuaEditControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                _scintillaControl = new Scintilla();
                _scintillaControl.Dock = DockStyle.Fill;
                _scintillaControl.BorderStyle = BorderStyle.None;

                _scintillaControl.ContextMenuStrip = contextMenuEditor;
                _scintillaControl.ContextMenuStrip.Renderer = new Vs2010MenuStripRenderer();

                _scintillaControl.CharAdded += _scintillaControl_CharAdded;
                _scintillaControl.KeyPress += _scintillaControl_KeyPress;
                _scintillaControl.KeyDown += _scintillaControl_KeyDown;
                _scintillaControl.TextChanged += _scintillaControl_TextChanged;
                _scintillaControl.SelectionChanged += _scintillaControl_SelectionChanged;

                _scintillaControl.ConfigurationManager.Language = "Lua";

                //_scintillaControl.EndOfLine.IsVisible = true;
                //_scintillaControl.EndOfLine.Mode = EndOfLineMode.Crlf;

                //_scintillaControl.Whitespace.Mode = WhitespaceMode.VisibleAlways;

                InitGeneralSettings();
                InitSnippets();
                InitKeywords();
                InitCommands();

                if (_theme == null)
                    SetTheme(EditorTheme.Default);
                else
                    SetTheme(_theme);

                ReadFile();

                Controls.Add(_scintillaControl);
            }
        }

        #endregion

        #region Actions

        public bool Save()
        {
            // no changes so no save is required
            if (_lastText == _scintillaControl.Text)
                return false;

            try
            {
                File.WriteAllText(_entry.GetAbsolutePath(),
                    _scintillaControl.Text, Encoding.UTF8);

                _lastText = _scintillaControl.Text;

                if (OnTextChange != null)
                    OnTextChange(this, EventArgs.Empty);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei konnte nicht gespeichert werden:\n\n" + ex.Message, "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private bool ShowChangesLostMessage()
        {
            if (HasChanges)
            {
                DialogResult dr = MessageBox.Show("Die Änderungen wurden noch nicht gespeichert.\nMöchten Sie die Datei jetzt speichern?", "Warnung",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                switch (dr)
                {
                    case DialogResult.Yes:
                        if (!Save())
                        {
                            return false;
                        }
                        break;

                    case DialogResult.Cancel:
                        return true;
                }
            }

            return false;
        }

        public void Close()
        {
            if (!ShowChangesLostMessage())
            {
                if (OnClose != null)
                    OnClose(this, EventArgs.Empty);
            }
        }

        private void Search()
        {
            if (_formSearch == null || _formSearch.IsDisposed || _formSearch.Disposing)
            {
                _formSearch = new FormSearch();
                _formSearch.Center(ParentForm);
                _formSearch.OnSearch += _formSearch_OnSearch;
                _formSearch.FormClosed += _formSearch_FormClosed;

                _formSearch.Show(this);
            }
            else
            {
                _formSearch.Focus();
            }
        }

        private void FindNext()
        {
            if (_searchResults.Count == 0)
            {
                MessageBox.Show("Der Suchbegriff wurde nicht gefunden.", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (_searchResults != null)
                {
                    if (_searchIndex + 1 < _searchResults.Count)
                    {
                        _searchIndex++;
                    }
                    else
                    {
                        _searchIndex = 0;
                    }

                    _searchResults[_searchIndex].Select();
                }
            }
        }

        public void SetTheme(EditorTheme theme)
        {
            if (theme == null)
                throw new ArgumentNullException(nameof(theme));

            _theme = theme;

            if (_scintillaControl == null) // editor is not created yet
                return;

            _scintillaControl.Styles[(int)LuaLexerStyles.Default].Font = new Font(theme.FontName, theme.FontSize);

            _scintillaControl.Styles[(int)LuaLexerStyles.Default].BackColor = theme.DefaultBackgroundColor;
            _scintillaControl.Styles[(int)LuaLexerStyles.Default].ForeColor = theme.DefaultTextColor;

            // reset all styles to default 
            for (int i = 1; i <= _scintillaControl.Styles.Max.Index; i++)
            {
                _scintillaControl.Styles[i].BackColor = _scintillaControl.Styles[(int)LuaLexerStyles.Default].BackColor;
                _scintillaControl.Styles[i].ForeColor = _scintillaControl.Styles[(int)LuaLexerStyles.Default].ForeColor;
                _scintillaControl.Styles[i].Font = _scintillaControl.Styles[(int)LuaLexerStyles.Default].Font;
            }

            // comments
            SetLexerThemeStyle(theme, LuaLexerStyles.Comment, EditorThemeCategory.Comments, true);
            SetLexerThemeStyle(theme, LuaLexerStyles.CommentDoc, EditorThemeCategory.Comments, true);
            SetLexerThemeStyle(theme, LuaLexerStyles.CommentLine, EditorThemeCategory.Comments, true);

            // numbers
            SetLexerThemeStyle(theme, LuaLexerStyles.Numbers, EditorThemeCategory.Numbers);

            // strings
            SetLexerThemeStyle(theme, LuaLexerStyles.Strings, EditorThemeCategory.String);
            SetLexerThemeStyle(theme, LuaLexerStyles.StringsEOL, EditorThemeCategory.String);
            SetLexerThemeStyle(theme, LuaLexerStyles.Characters, EditorThemeCategory.String);

            // keywords
            SetLexerThemeStyle(theme, LuaLexerStyles.Words, EditorThemeCategory.Keywords);
            SetLexerThemeStyle(theme, LuaLexerStyles.Words2, EditorThemeCategory.Keywords);
            SetLexerThemeStyle(theme, LuaLexerStyles.Words3, EditorThemeCategory.Keywords);
            SetLexerThemeStyle(theme, LuaLexerStyles.Words4, EditorThemeCategory.Keywords);
            SetLexerThemeStyle(theme, LuaLexerStyles.Words5, EditorThemeCategory.Keywords);
            SetLexerThemeStyle(theme, LuaLexerStyles.Words6, EditorThemeCategory.Keywords);
            SetLexerThemeStyle(theme, LuaLexerStyles.Words7, EditorThemeCategory.Keywords);
            SetLexerThemeStyle(theme, LuaLexerStyles.Words8, EditorThemeCategory.Keywords);

            // operator
            SetLexerThemeStyle(theme, LuaLexerStyles.Operators, EditorThemeCategory.Operators);
            // identifier
            SetLexerThemeStyle(theme, LuaLexerStyles.Identifier, EditorThemeCategory.Identifier);
            // preprocessor
            SetLexerThemeStyle(theme, LuaLexerStyles.Preprocessor, EditorThemeCategory.PreProcessor);

            // line numbers
            _scintillaControl.Styles.LineNumber.BackColor = theme.GetBackgroundColor(EditorThemeCategory.LineNumbers);
            _scintillaControl.Styles.LineNumber.ForeColor = theme.GetTextColor(EditorThemeCategory.LineNumbers);

            // caret
            _scintillaControl.Caret.Color = theme.GetTextColor(EditorThemeCategory.Carret);

            // current line
            _scintillaControl.Caret.HighlightCurrentLine = true;
            _scintillaControl.Caret.CurrentLineBackgroundAlpha = 256;
            _scintillaControl.Caret.CurrentLineBackgroundColor = theme.GetBackgroundColor(EditorThemeCategory.CurrentLine);

            // selection
            _scintillaControl.Selection.BackColor = theme.GetBackgroundColor(EditorThemeCategory.Selection);
            _scintillaControl.Selection.BackColorUnfocused = theme.GetBackgroundColor(EditorThemeCategory.Selection);
            _scintillaControl.Selection.ForeColor = theme.GetTextColor(EditorThemeCategory.Selection);
            _scintillaControl.Selection.ForeColorUnfocused = theme.GetTextColor(EditorThemeCategory.Selection);
        }

        public int GetColumnIndex()
        {
            int position = _scintillaControl.CurrentPos;
            int columnIndex = _scintillaControl.GetColumn(position);

            return columnIndex;
        }

        public int GetLineIndex()
        {
            int position = _scintillaControl.CurrentPos;
            int lineIndex = _scintillaControl.Lines.FromPosition(position).Number;

            return lineIndex;
        }

        public char GetCurrentChar()
        {
            return _scintillaControl.CharAt(_scintillaControl.CurrentPos);
        }

        #endregion

        #region FormSearch events

        private void _formSearch_OnSearch(object sender, EventArgs e)
        {
            if (_lastSearchText == _formSearch.SearchText &&
                _lastSearchWasRegex == (_formSearch.RegularExpression != null) &&
                _lastSearchMatchCase == _formSearch.MatchCase)
            {
                FindNext();
                return;
            }

            _lastSearchText = _formSearch.SearchText;
            _lastSearchWasRegex = _formSearch.RegularExpression != null;
            _lastSearchMatchCase = _formSearch.MatchCase;

            if (_formSearch.RegularExpression != null)
            {
                _searchResults = _scintillaControl.FindReplace.FindAll(_formSearch.RegularExpression);
            }
            else
            {
                _searchResults = _scintillaControl.FindReplace.FindAll(_formSearch.SearchText, _formSearch.MatchCase ? SearchFlags.MatchCase : SearchFlags.Empty);
            }

            FindNext();
        }

        private void _formSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            _scintillaControl.Focus();
            _lastSearchText = null;
        }

        #endregion

        #region Scintilla events

        private void _scintillaControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                // find
                Search();
            }
            else if (e.KeyCode == Keys.F3)
            {
                // find next
                FindNext();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                // save
                Save();

                if (OnSave != null)
                    OnSave(this, EventArgs.Empty);
            }
        }

        private void _scintillaControl_TextChanged(object sender, EventArgs e)
        {
            if (OnTextChange != null)
            {
                OnTextChange(this, EventArgs.Empty);
            }
        }
        private void _scintillaControl_CharAdded(object sender, CharAddedEventArgs e)
        {
            if (_autoCompletionList.Count == 0)
            {
                return;
            }

            if (e.Ch == '(' || e.Ch == '{' || e.Ch == '\'' || e.Ch == '"')
            {
                char appendChar;
                switch (e.Ch)
                {
                    case '(':
                        appendChar = ')';
                        break;
                    case '{':
                        appendChar = '}';
                        break;
                    case '\'':
                        appendChar = '\'';
                        break;
                    case '"':
                        appendChar = '"';
                        break;

                    default:
                        return;
                }

                _scintillaControl.InsertText(_scintillaControl.CurrentPos, appendChar.ToString());
            }
            else
            {
                int pos = _scintillaControl.CurrentPos;
                string word = _scintillaControl.GetWordFromPosition(pos);

                if (word.Length >= 3)
                {
                    List<string> matches = new List<string>();
                    foreach (string current in _autoCompletionList)
                    {
                        if (current.Length - 2 > 3 && current.StartsWith(word, StringComparison.OrdinalIgnoreCase))
                        {
                            matches.Add(current);
                        }
                    }

                    if (matches.Count > 0)
                    {
                        matches.Sort();

                        _scintillaControl.AutoComplete.Show(0, matches);
                        _scintillaControl.AutoComplete.List = _autoCompletionList;
                    }
                }
            }
        }
        private void _scintillaControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            char nextChar = _scintillaControl.CharAt(_scintillaControl.CurrentPos);
            if (e.KeyChar == ')' && nextChar == ')')
            {
                e.Handled = true;
                _scintillaControl.CurrentPos += 1;
            }
        }
        private void _scintillaControl_SelectionChanged(object sender, EventArgs e)
        {
            if (OnSelectionChange != null)
            {
                OnSelectionChange(this, new ScintillaPosEventArgs(
                    GetColumnIndex(), GetLineIndex()));
            }
        }

        #endregion

        #region Menu events

        private void contextMenuEditor_Opening(object sender, CancelEventArgs e)
        {
            mniEdit_Clear.Enabled = _scintillaControl.Text.Length > 0;
            mniEdit_Copy.Enabled = _scintillaControl.Clipboard.CanCopy;
            mniEdit_Cut.Enabled = _scintillaControl.Clipboard.CanCut;
            mniEdit_Paste.Enabled = _scintillaControl.Clipboard.CanPaste;
        }

        private void mniCut_Click(object sender, EventArgs e)
        {
            _scintillaControl.Commands.Execute(BindableCommand.Cut);
        }

        private void mniCopy_Click(object sender, EventArgs e)
        {
            _scintillaControl.Commands.Execute(BindableCommand.Copy);
        }

        private void mniPaste_Click(object sender, EventArgs e)
        {
            _scintillaControl.Commands.Execute(BindableCommand.Paste);
        }

        private void mniClear_Click(object sender, EventArgs e)
        {
            _scintillaControl.Commands.Execute(BindableCommand.ClearAll);
        }

        #endregion

        #region Methods

        public void ShowSearchDialog()
        {
            Search();
        }

        #endregion

        #region Helper

        private void SetLexerThemeStyle(EditorTheme theme, LuaLexerStyles lexerStyleType, EditorThemeCategory category, bool italic = false)
        {
            _scintillaControl.Styles[(int)lexerStyleType].BackColor = theme.GetBackgroundColor(category);
            _scintillaControl.Styles[(int)lexerStyleType].ForeColor = theme.GetTextColor(category);
            _scintillaControl.Styles[(int)lexerStyleType].Italic = italic;
        }

        private void InitGeneralSettings()
        {
            // icons
            _scintillaControl.AutoComplete.RegisterImage(1, Encoding.ASCII.GetString(Resources.Xpm_Snippet));
            _scintillaControl.AutoComplete.RegisterImage(2, Encoding.ASCII.GetString(Resources.Xpm_Function));
            _scintillaControl.AutoComplete.RegisterImage(3, Encoding.ASCII.GetString(Resources.Xpm_Keyword));
            _scintillaControl.AutoComplete.RegisterImage(4, Encoding.ASCII.GetString(Resources.Xpm_Event));

            // indentation
            _scintillaControl.Indentation.UseTabs = false;
            _scintillaControl.Indentation.TabWidth = 4;
            _scintillaControl.Indentation.SmartIndentType = SmartIndent.Simple;

            // comments
            _scintillaControl.Lexing.LineCommentPrefix = "--";
            _scintillaControl.Lexing.StreamCommentPrefix = "[--";
            _scintillaControl.Lexing.StreamCommentSufix = "--]";

            // folding
            _scintillaControl.Folding.IsEnabled = false;
            _scintillaControl.Folding.MarkerScheme = FoldMarkerScheme.BoxPlusMinus;

            // margins
            for (int i = 0; i < _scintillaControl.Margins.Count; i++)
            {
                _scintillaControl.Margins[i].Width = 0;
            }

            _scintillaControl.Margins.Margin0.Width = 38; // numbers
            //sciEditor.Margins.Margin2.Width = 20; // folding
        }

        private void InitKeywords()
        {
            // snippets
            foreach (var snippet in _scintillaControl.Snippets.List)
            {
                _autoCompletionList.Add(snippet.Shortcut + "?1");
            }

            // keywords
            foreach (string kw in Lua_Keywords.Split(' '))
            {
                _autoCompletionList.Add(kw + "?3");
            }

            _scintillaControl.Lexing.Keywords[(int)LuaKeywordLists.Keywords] =
                Lua_Keywords + " " + string.Join(" ", _autoCompletionList.ToArray());
        }

        private void InitCommands()
        {
            // clear default commands
            _scintillaControl.Commands.RemoveBinding(Keys.H, Keys.Control);
            _scintillaControl.Commands.RemoveBinding(Keys.F, Keys.Control);
            _scintillaControl.Commands.RemoveBinding(Keys.G, Keys.Control);

            // custom

        }

        private void InitSnippets()
        {
            _scintillaControl.Snippets.List.Add(new Snippet("func",
                  "function $function_name$()" + Environment.NewLine
                + "    $end$" + Environment.NewLine
                + "end"));

            _scintillaControl.Snippets.List.Add(new Snippet("ifcond",
                  "if ($condition$) then" + Environment.NewLine
                + "    $end$" + Environment.NewLine
                + "end"));

            _scintillaControl.Snippets.List.Add(new Snippet("ifelsecond",
                  "if ($condition$) then" + Environment.NewLine
                + "    $end$" + Environment.NewLine
                + "else" + Environment.NewLine
                + "    " + Environment.NewLine
                + "end"));

            _scintillaControl.Snippets.List.Add(new Snippet("forloop",
                  "for i = $init$, $max$, $incremend$"
                + "do" + Environment.NewLine
                + "    " + Environment.NewLine
                + "end"));
        }

        private void ReadFile()
        {
            string path = _entry.GetAbsolutePath();
            string text = File.ReadAllText(path, Encoding.UTF8);

            _lastText = text;
            _scintillaControl.Text = text;
        }

        #endregion

        #region Events

        /// <summary>
        /// Wird ausgelöst, wenn der Text geändert wird.
        /// </summary>
        public event EventHandler OnTextChange;

        /// <summary>
        /// Wird aufgerufen, wenn das Control geschlossen wird.
        /// </summary>
        public event EventHandler OnClose;

        /// <summary>
        /// Wird aufgerufen, wenn der Text gespeichert wird.
        /// </summary>
        public event EventHandler OnSave;

        /// <summary>
        /// Wird aufgerufen, wenn die Auswahl geändert wurde.
        /// </summary>
        public event EventHandler<ScintillaPosEventArgs> OnSelectionChange;

        #endregion

        #region Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditorText
        {
            get { return _scintillaControl.Text; }
            set { _scintillaControl.Text = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font EditorFont
        {
            get { return _editorFont; }
            set { _editorFont = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasChanges
        {
            get { return (_lastText != _scintillaControl.Text); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProjectEntry Entry
        {
            get { return _entry; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Scintilla NativeControl
        {
            get { return _scintillaControl; }
        }

        #endregion
    }
}