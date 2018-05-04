namespace LuaEditor.Dialogs.Controls
{
    partial class ScintillaWrapperControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniEdit_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mniEdit_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEdit_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEdit_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuEditor
            // 
            this.contextMenuEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniEdit_Cut,
            this.mniEdit_Copy,
            this.mniEdit_Paste,
            this.toolStripMenuItem1,
            this.mniEdit_Clear});
            this.contextMenuEditor.Name = "contextMenuEditor";
            this.contextMenuEditor.Size = new System.Drawing.Size(153, 120);
            this.contextMenuEditor.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuEditor_Opening);
            // 
            // mniEdit_Paste
            // 
            this.mniEdit_Paste.Image = global::LuaEditor.Properties.Resources.page_white_paste;
            this.mniEdit_Paste.Name = "mniEdit_Paste";
            this.mniEdit_Paste.Size = new System.Drawing.Size(152, 22);
            this.mniEdit_Paste.Text = "Einfügen";
            this.mniEdit_Paste.Click += new System.EventHandler(this.mniPaste_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // mniEdit_Clear
            // 
            this.mniEdit_Clear.Name = "mniEdit_Clear";
            this.mniEdit_Clear.Size = new System.Drawing.Size(152, 22);
            this.mniEdit_Clear.Text = "Leeren";
            this.mniEdit_Clear.Click += new System.EventHandler(this.mniClear_Click);
            // 
            // mniEdit_Cut
            // 
            this.mniEdit_Cut.Image = global::LuaEditor.Properties.Resources.cut;
            this.mniEdit_Cut.Name = "mniEdit_Cut";
            this.mniEdit_Cut.Size = new System.Drawing.Size(152, 22);
            this.mniEdit_Cut.Text = "Ausschneiden";
            this.mniEdit_Cut.Click += new System.EventHandler(this.mniCut_Click);
            // 
            // mniEdit_Copy
            // 
            this.mniEdit_Copy.Image = global::LuaEditor.Properties.Resources.clipboard_empty;
            this.mniEdit_Copy.Name = "mniEdit_Copy";
            this.mniEdit_Copy.Size = new System.Drawing.Size(152, 22);
            this.mniEdit_Copy.Text = "Kopieren";
            this.mniEdit_Copy.Click += new System.EventHandler(this.mniCopy_Click);
            // 
            // ScintillaWrapperControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ScintillaWrapperControl";
            this.Size = new System.Drawing.Size(990, 511);
            this.Load += new System.EventHandler(this.LuaEditControl_Load);
            this.contextMenuEditor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuEditor;
        private System.Windows.Forms.ToolStripMenuItem mniEdit_Cut;
        private System.Windows.Forms.ToolStripMenuItem mniEdit_Copy;
        private System.Windows.Forms.ToolStripMenuItem mniEdit_Paste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mniEdit_Clear;
    }
}
