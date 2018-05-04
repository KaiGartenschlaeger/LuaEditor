using PureSoft.Controls.VisualStudio.Renderer;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LuaEditor.Dialogs.Controls
{
    public partial class ConsoleControl : UserControl
    {
        #region Constructor

        public ConsoleControl()
        {
            InitializeComponent();

            contextMenuConsole.Renderer = new Vs2010MenuStripRenderer();
        }

        #endregion

        #region Win API

        [DllImport("user32.dll")]
        private static extern bool HideCaret(IntPtr hWnd);

        #endregion

        #region Control events

        private void tbxConsole_TextChanged(object sender, EventArgs e)
        {
            //HideCaret(tbxConsole.Handle);
        }

        private void tbxConsole_Enter(object sender, EventArgs e)
        {
            //HideCaret(tbxConsole.Handle);
        }

        private void contextMenuConsole_Opening(object sender, CancelEventArgs e)
        {
            mniConsole_Clear.Enabled = tbxConsole.Text.Length > 0;
        }

        private void mniConsole_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #endregion

        #region Methods

        public void AppendLine(string line)
        {
            tbxConsole.AppendText(line + Environment.NewLine);
        }

        public void SetText(string message)
        {
            tbxConsole.Text = message;
        }

        public void Clear()
        {
            tbxConsole.Clear();
        }

        public Font ConsoleFont
        {
            get { return tbxConsole.Font; }
            set { tbxConsole.Font = value; }
        }

        #endregion
    }
}