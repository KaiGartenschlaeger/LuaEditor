using LuaEditor.Objetcts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LuaEditor.Dialogs.Controls
{
    public partial class EditorTabsControl : UserControl
    {
        #region Fields

        private EditorSettings _editorSettings;
        private EditorTheme _theme;

        #endregion

        #region Constructor

        public EditorTabsControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Control events

        private void tabControl_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void tabControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                for (int tabIndex = 0; tabIndex < tabControl.TabCount; tabIndex++)
                {
                    Rectangle tabRect = tabControl.GetTabRect(tabIndex);
                    if (tabRect.Contains(e.Location))
                    {
                        GetByIndex(tabIndex).Close();
                    }
                }
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var editor = Current;

            if (editor != null)
                editor.Focus();

            if (TabChanged != null)
                TabChanged(this, EventArgs.Empty);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Ändert das Theme für alle aktiven Editor Instanzen.
        /// </summary>
        public void SetTheme(EditorTheme theme)
        {
            if (theme == null)
                throw new ArgumentNullException(nameof(theme));

            _theme = theme;

            foreach (var editor in Editors)
            {
                editor.SetTheme(theme);
            }
        }

        /// <summary>
        /// Wechselt zum nächsten Tab
        /// </summary>
        public void NextTab()
        {
            int index = tabControl.SelectedIndex;
            if (index < tabControl.TabCount)
                index++;
            else
                index = 0;

            tabControl.SelectedIndex = index;
        }

        /// <summary>
        /// Wechselt zum vorherigen Tab
        /// </summary>
        public void PreviousTab()
        {
            int index = tabControl.SelectedIndex;
            if (index > 0)
                index--;
            else
                index = tabControl.TabCount - 1;

            tabControl.SelectedIndex = index;
        }

        /// <summary>
        /// Entfernt alle aktiven Tabs
        /// </summary>
        public void Clear()
        {
            tabControl.TabPages.Clear();
        }

        /// <summary>
        /// Liefert die Editor Instanz zum angegebenen Projekt Eintrag.
        /// </summary>
        public ScintillaWrapperControl GetByEntry(ProjectEntry entry)
        {
            foreach (var editor in Editors)
            {
                if (editor.Entry.Id == entry.Id)
                    return editor;
            }

            return null;
        }

        /// <summary>
        /// Liefert den Editor mit dem angebenen Index.
        /// </summary>
        public ScintillaWrapperControl GetByIndex(int index)
        {
            if (index < 0 || index + 1 > tabControl.TabCount)
                return null;

            return (ScintillaWrapperControl)tabControl.TabPages[index].Controls[0];
        }

        /// <summary>
        /// Liefert den Tab Index anhand des Projekt Eintrags.
        /// </summary>
        public int GetIndexByEntry(ProjectEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            for (int i = 0; i < tabControl.TabCount; i++)
            {
                var editor = GetByIndex(i);
                if (editor.Entry.Id == entry.Id)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Fügt einen neuen Tab für den angegebenen Projekt Eintrag hinzu.
        /// </summary>
        public void Add(ProjectEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            int index = GetIndexByEntry(entry);
            if (index == -1)
            {
                // create a new tab for the given entry
                TabPage page = new TabPage(entry.Location);
                page.Padding = new Padding(0);

                ScintillaWrapperControl scintilla = new ScintillaWrapperControl(entry, _theme);

                scintilla.EditorFont = _editorSettings.EditorFont;
                scintilla.Tag = page;
                scintilla.Dock = DockStyle.Fill;

                scintilla.OnTextChange += scintilla_OnTextChange;
                scintilla.OnSelectionChange += scintilla_PositionChanged;
                scintilla.OnClose += scintilla_OnClose;

                scintilla.Width = page.ClientSize.Width;
                scintilla.Height = page.ClientSize.Height;

                page.Controls.Add(scintilla);

                tabControl.TabPages.Add(page);
                tabControl.SelectTab(page);
            }
            else
            {
                tabControl.SelectedIndex = index;
            }
        }

        #endregion

        #region Editor events

        private void scintilla_PositionChanged(object sender, ScintillaPosEventArgs e)
        {
            if (EditorPositonChanged != null)
                EditorPositonChanged(this, e);
        }

        private void scintilla_OnTextChange(object sender, EventArgs e)
        {
            ScintillaWrapperControl control = (ScintillaWrapperControl)sender;
            TabPage page = (TabPage)control.Tag;

            page.Text = control.Entry.Location;
            if (control.HasChanges)
            {
                page.Text += "*";
            }

            if (EditorTextChanged != null)
                EditorTextChanged(this, EventArgs.Empty);
        }

        private void scintilla_OnClose(object sender, EventArgs e)
        {
            ScintillaWrapperControl control = (ScintillaWrapperControl)sender;
            TabPage page = (TabPage)control.Tag;

            tabControl.TabPages.Remove(page);
        }

        #endregion

        #region Events

        /// <summary>
        /// Wird aufgerufen, wenn der aktive Tab sich ändert.
        /// </summary>
        public event EventHandler TabChanged;

        /// <summary>
        /// Wird aufgerufen, wenn die Cursor Position im Editor sich ändert.
        /// </summary>
        public event EventHandler<ScintillaPosEventArgs> EditorPositonChanged;

        /// <summary>
        /// Wird aufgerufen, wenn der Text im Editor geändert wird.
        /// </summary>
        public event EventHandler EditorTextChanged;

        #endregion

        #region Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        /// <summary>
        /// Liefert den aktiven Editor
        /// </summary>
        public ScintillaWrapperControl Current
        {
            get
            {
                if (tabControl.SelectedIndex != -1)
                    return (ScintillaWrapperControl)tabControl.SelectedTab.Controls[0];

                return null;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        /// <summary>
        /// Liefert einen Wert der angibt, ob ein Editor aktiv ist.
        /// </summary>
        public bool EditorOpen
        {
            get { return tabControl.SelectedIndex != -1; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        /// <summary>
        /// Liefert ein iterierbares Objekt der Editor Instanzen
        /// </summary>
        public IEnumerable<ScintillaWrapperControl> Editors
        {
            get
            {
                foreach (TabPage page in tabControl.TabPages)
                {
                    yield return (ScintillaWrapperControl)page.Controls[0];
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        /// <summary>
        /// Liefert die Anzahl an aktiven Instanzen.
        /// </summary>
        public int Count
        {
            get { return tabControl.TabCount; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        /// <summary>
        /// Liefert den Index des aktiven Tabs oder ändert diesen.
        /// </summary>
        public int SelectedIndex
        {
            get { return tabControl.SelectedIndex; }
            set { tabControl.SelectedIndex = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        /// <summary>
        /// Liefert die aktuellen Editor Einstellungen oder legt diese fest.
        /// </summary>
        public EditorSettings Settings
        {
            get { return _editorSettings; }
            set { _editorSettings = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        /// <summary>
        /// Liefert das aktuelle Theme
        /// </summary>
        public EditorTheme CurrentTheme
        {
            get { return _theme; }
        }

        #endregion        
    }
}