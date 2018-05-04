namespace LuaEditor.Dialogs
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnChangeLuaInterpreterPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxStartApplcationPath = new System.Windows.Forms.TextBox();
            this.lblEditorFont = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEditorFont = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxStartApplicationArguments = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbHideWindow = new System.Windows.Forms.CheckBox();
            this.chbRedirectOutput = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblConsoleFont = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConsoleFont = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(532, 401);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(446, 401);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 28);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnChangeLuaInterpreterPath
            // 
            this.btnChangeLuaInterpreterPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeLuaInterpreterPath.Location = new System.Drawing.Point(538, 47);
            this.btnChangeLuaInterpreterPath.Name = "btnChangeLuaInterpreterPath";
            this.btnChangeLuaInterpreterPath.Size = new System.Drawing.Size(37, 20);
            this.btnChangeLuaInterpreterPath.TabIndex = 8;
            this.btnChangeLuaInterpreterPath.Text = "...";
            this.btnChangeLuaInterpreterPath.UseVisualStyleBackColor = true;
            this.btnChangeLuaInterpreterPath.Click += new System.EventHandler(this.btnChangeLuaInterpreterPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Anwendung";
            // 
            // tbxStartApplcationPath
            // 
            this.tbxStartApplcationPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxStartApplcationPath.Location = new System.Drawing.Point(16, 47);
            this.tbxStartApplcationPath.Name = "tbxStartApplcationPath";
            this.tbxStartApplcationPath.ReadOnly = true;
            this.tbxStartApplcationPath.Size = new System.Drawing.Size(516, 20);
            this.tbxStartApplcationPath.TabIndex = 6;
            // 
            // lblEditorFont
            // 
            this.lblEditorFont.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEditorFont.Location = new System.Drawing.Point(16, 48);
            this.lblEditorFont.Name = "lblEditorFont";
            this.lblEditorFont.Size = new System.Drawing.Size(510, 30);
            this.lblEditorFont.TabIndex = 9;
            this.lblEditorFont.Text = "{Font}";
            this.lblEditorFont.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Editor";
            // 
            // btnEditorFont
            // 
            this.btnEditorFont.Location = new System.Drawing.Point(532, 58);
            this.btnEditorFont.Name = "btnEditorFont";
            this.btnEditorFont.Size = new System.Drawing.Size(43, 20);
            this.btnEditorFont.TabIndex = 8;
            this.btnEditorFont.Text = "...";
            this.btnEditorFont.UseVisualStyleBackColor = true;
            this.btnEditorFont.Click += new System.EventHandler(this.btnEditorFont_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(241, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Platzhalter: {startElementPath}, {projectRootPath}";
            // 
            // tbxStartApplicationArguments
            // 
            this.tbxStartApplicationArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxStartApplicationArguments.Location = new System.Drawing.Point(16, 95);
            this.tbxStartApplicationArguments.Name = "tbxStartApplicationArguments";
            this.tbxStartApplicationArguments.Size = new System.Drawing.Size(559, 20);
            this.tbxStartApplicationArguments.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Parameter";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chbHideWindow);
            this.groupBox1.Controls.Add(this.chbRedirectOutput);
            this.groupBox1.Controls.Add(this.tbxStartApplcationPath);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnChangeLuaInterpreterPath);
            this.groupBox1.Controls.Add(this.tbxStartApplicationArguments);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 186);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ausführen";
            // 
            // chbHideWindow
            // 
            this.chbHideWindow.AutoSize = true;
            this.chbHideWindow.Location = new System.Drawing.Point(16, 155);
            this.chbHideWindow.Name = "chbHideWindow";
            this.chbHideWindow.Size = new System.Drawing.Size(117, 17);
            this.chbHideWindow.TabIndex = 11;
            this.chbHideWindow.Text = "Fenster verstecken";
            this.chbHideWindow.UseVisualStyleBackColor = true;
            // 
            // chbRedirectOutput
            // 
            this.chbRedirectOutput.AutoSize = true;
            this.chbRedirectOutput.Location = new System.Drawing.Point(156, 155);
            this.chbRedirectOutput.Name = "chbRedirectOutput";
            this.chbRedirectOutput.Size = new System.Drawing.Size(169, 17);
            this.chbRedirectOutput.TabIndex = 11;
            this.chbRedirectOutput.Text = "Ausgabe auf Konsole umleiten";
            this.chbRedirectOutput.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblConsoleFont);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblEditorFont);
            this.groupBox2.Controls.Add(this.btnConsoleFont);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnEditorFont);
            this.groupBox2.Location = new System.Drawing.Point(15, 212);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(597, 160);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Schriftart";
            // 
            // lblConsoleFont
            // 
            this.lblConsoleFont.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConsoleFont.Location = new System.Drawing.Point(16, 111);
            this.lblConsoleFont.Name = "lblConsoleFont";
            this.lblConsoleFont.Size = new System.Drawing.Size(510, 30);
            this.lblConsoleFont.TabIndex = 9;
            this.lblConsoleFont.Text = "{Font}";
            this.lblConsoleFont.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Konsole";
            // 
            // btnConsoleFont
            // 
            this.btnConsoleFont.Location = new System.Drawing.Point(532, 121);
            this.btnConsoleFont.Name = "btnConsoleFont";
            this.btnConsoleFont.Size = new System.Drawing.Size(43, 20);
            this.btnConsoleFont.TabIndex = 8;
            this.btnConsoleFont.Text = "...";
            this.btnConsoleFont.UseVisualStyleBackColor = true;
            this.btnConsoleFont.Click += new System.EventHandler(this.btnConsoleFont_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Ersetzen";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnChangeLuaInterpreterPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxStartApplcationPath;
        private System.Windows.Forms.Label lblEditorFont;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEditorFont;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxStartApplicationArguments;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbHideWindow;
        private System.Windows.Forms.CheckBox chbRedirectOutput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblConsoleFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConsoleFont;
    }
}