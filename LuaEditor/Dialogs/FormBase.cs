using LuaEditor.Objetcts;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LuaEditor.Dialogs
{
    public class FormBase : Form
    {
        #region Fields

        private EditorSettings _settings;

        #endregion

        #region Contructor

        public FormBase()
        {

        }

        public FormBase(EditorSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            _settings = settings;
        }

        #endregion

        #region Methods

        public void Center(Form parentForm)
        {
            if (StartPosition != FormStartPosition.Manual)
                return;

            Location = new Point(
                parentForm.Left + parentForm.Width / 2 - Width / 2,
                parentForm.Top + parentForm.Height / 2 - Height / 2);
        }

        public void Close(DialogResult result)
        {
            DialogResult = result;
            Close();
        }

        private int CalculateLeftPosition()
        {
            int left = Left;

            if (WindowState == FormWindowState.Maximized)
                left = RestoreBounds.Left;

            if (Owner != null)
            {
                return left - Owner.Left;
            }

            return left;
        }

        private int CalculateTopPosition()
        {
            int top = Top;

            if (WindowState == FormWindowState.Maximized)
                top = RestoreBounds.Top;

            if (Owner != null)
            {
                return top - Owner.Top;
            }

            return top;
        }

        /// <summary>
        /// Speichert die aktuelle Größe und Position des Fensters in den Einstellungen.
        /// </summary>
        public void SavePosition(EditorSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            
            var rectNormal = new Rectangle(
                CalculateLeftPosition(),
                CalculateTopPosition(),
                Width,
                Height);

            if (WindowState == FormWindowState.Maximized)
            {
                rectNormal = new Rectangle(
                    x: CalculateLeftPosition(),
                    y: CalculateTopPosition(),
                    width: RestoreBounds.Width,
                    height: RestoreBounds.Height);
            }

            if (!settings.FormSettings.ContainsKey(Name))
            {
                settings.FormSettings.Add(Name, new EditorFormSettings()
                {
                    Bounds = new Rectangle(
                        x: CalculateLeftPosition(),
                        y: CalculateTopPosition(),
                        width: Bounds.Width,
                        height: Bounds.Height),
                    Maximized = WindowState == FormWindowState.Maximized,
                    IsAbsolutePos = Owner == null
                });
            }
            else
            {
                settings.FormSettings[Name].Bounds = rectNormal;
                settings.FormSettings[Name].Maximized = WindowState == FormWindowState.Maximized;
                settings.FormSettings[Name].IsAbsolutePos = Owner == null;
            }
        }

        /// <summary>
        /// Stellt die Position und Größe des Fensters anhand der übergebenen Einstellungen wieder her.
        /// </summary>
        public void RestorePosition(EditorSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (Settings.FormSettings.ContainsKey(Name))
            {
                EditorFormSettings s = Settings.FormSettings[Name];

                if (s.IsAbsolutePos)
                {
                    Left = s.Bounds.Left;
                    Top = s.Bounds.Top;
                }
                else
                {
                    if (Owner != null)
                    {
                        Left = Owner.Left + s.Bounds.Left;
                        Top = Owner.Top + s.Bounds.Top;
                    }
                    else
                    {
                        // Kein übergeordnetes Fenster vorhanden, aber die Koordinaten wurden so gespeichert.
                        // Fallback: Am Bildschirm zentrieren
                        CenterToScreen();
                    }
                }

                if (FormBorderStyle == FormBorderStyle.Sizable || FormBorderStyle == FormBorderStyle.SizableToolWindow)
                {
                    Width = s.Bounds.Width;
                    Height = s.Bounds.Height;

                    if (MaximizeBox)
                    {
                        if (s.Maximized)
                            WindowState = FormWindowState.Maximized;
                    }
                }
            }
            else
            {
                if (StartPosition == FormStartPosition.Manual)
                {
                    CenterToParent();
                }
            }
        }

        #endregion

        #region Form events

        protected override void OnLoad(EventArgs e)
        {
            if (Settings != null)
                RestorePosition(Settings);

            base.OnLoad(e);
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Settings != null)
                SavePosition(Settings);

            base.OnClosing(e);
        }

        #endregion

        #region Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EditorSettings Settings
        {
            get
            {
                if (_settings == null && Owner != null)
                {
                    // Wenn die Einstellungen nicht übergeben wurden, so müssen diese aus dem übergeordneten Fenster kommen.
                    FormBase fb = Owner as FormBase;

                    _settings = fb.Settings;
                }

                return _settings;
            }
        }

        #endregion
    }
}