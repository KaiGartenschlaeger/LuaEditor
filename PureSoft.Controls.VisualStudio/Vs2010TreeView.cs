using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PureSoft.Controls.VisualStudio
{
    public class Vs2010TreeView : TreeView
    {
        #region Fields

        private bool _elv;
        private bool _enableExplorerTheme = true;

        #endregion

        #region Constructor

        public Vs2010TreeView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        #endregion

        #region Win Proc

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 15 && !_elv && _enableExplorerTheme)
            {
                SetWindowTheme(Handle, "explorer", null);

                SendMessage(Handle, TVM_SETEXTENDEDSTYLE, TVS_EX_DOUBLEBUFFER, TVS_EX_DOUBLEBUFFER);
                SendMessage(Handle, TVM_SETEXTENDEDSTYLE, TVS_EX_AUTOHSCROLL, TVS_EX_AUTOHSCROLL);
                SendMessage(Handle, TVM_SETEXTENDEDSTYLE, TVS_EX_FADEINOUTEXPANDOS, TVS_EX_FADEINOUTEXPANDOS);

                _elv = true;
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Win API

        private const int TV_FIRST = 0x1100;
        private const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;
        private const int TVM_GETEXTENDEDSTYLE = TV_FIRST + 45;
        private const int TVM_SETAUTOSCROLLINFO = TV_FIRST + 59;
        private const int TVS_EX_AUTOHSCROLL = 0x0020;
        private const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;
        private const int GWL_STYLE = -16;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        #endregion
    }
}