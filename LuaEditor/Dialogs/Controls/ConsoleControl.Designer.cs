namespace LuaEditor.Dialogs.Controls
{
    partial class ConsoleControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbxConsole = new System.Windows.Forms.TextBox();
            this.contextMenuConsole = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniConsole_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolPaneLineControl1 = new PureSoft.Controls.VisualStudio.Vs2010ToolPaneHeader();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tbxConsole, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolPaneLineControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(811, 317);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tbxConsole
            // 
            this.tbxConsole.BackColor = System.Drawing.Color.Black;
            this.tbxConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxConsole.ContextMenuStrip = this.contextMenuConsole;
            this.tbxConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxConsole.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxConsole.ForeColor = System.Drawing.Color.White;
            this.tbxConsole.Location = new System.Drawing.Point(0, 22);
            this.tbxConsole.Margin = new System.Windows.Forms.Padding(0);
            this.tbxConsole.Multiline = true;
            this.tbxConsole.Name = "tbxConsole";
            this.tbxConsole.ReadOnly = true;
            this.tbxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxConsole.Size = new System.Drawing.Size(811, 295);
            this.tbxConsole.TabIndex = 1;
            this.tbxConsole.TextChanged += new System.EventHandler(this.tbxConsole_TextChanged);
            this.tbxConsole.Enter += new System.EventHandler(this.tbxConsole_Enter);
            // 
            // contextMenuConsole
            // 
            this.contextMenuConsole.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniConsole_Clear});
            this.contextMenuConsole.Name = "contextMenuConsole";
            this.contextMenuConsole.Size = new System.Drawing.Size(110, 26);
            this.contextMenuConsole.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuConsole_Opening);
            // 
            // mniConsole_Clear
            // 
            this.mniConsole_Clear.Name = "mniConsole_Clear";
            this.mniConsole_Clear.Size = new System.Drawing.Size(109, 22);
            this.mniConsole_Clear.Text = "Leeren";
            this.mniConsole_Clear.Click += new System.EventHandler(this.mniConsole_Clear_Click);
            // 
            // toolPaneLineControl1
            // 
            this.toolPaneLineControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.toolPaneLineControl1.Location = new System.Drawing.Point(0, 0);
            this.toolPaneLineControl1.Margin = new System.Windows.Forms.Padding(0);
            this.toolPaneLineControl1.Name = "toolPaneLineControl1";
            this.toolPaneLineControl1.Size = new System.Drawing.Size(811, 22);
            this.toolPaneLineControl1.TabIndex = 0;
            this.toolPaneLineControl1.Text = "Ausgabe";
            // 
            // ConsoleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConsoleControl";
            this.Size = new System.Drawing.Size(811, 317);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.contextMenuConsole.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private PureSoft.Controls.VisualStudio.Vs2010ToolPaneHeader toolPaneLineControl1;
        private System.Windows.Forms.TextBox tbxConsole;
        private System.Windows.Forms.ContextMenuStrip contextMenuConsole;
        private System.Windows.Forms.ToolStripMenuItem mniConsole_Clear;
    }
}
