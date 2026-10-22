namespace loader_test
{
    partial class ValidationTestForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbFile = new TextBox();
            btnFile = new Button();
            tbResult = new TextBox();
            btnCheck = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // tbFile
            // 
            tbFile.Location = new Point(12, 30);
            tbFile.Name = "tbFile";
            tbFile.ReadOnly = true;
            tbFile.Size = new Size(413, 23);
            tbFile.TabIndex = 0;
            // 
            // btnFile
            // 
            btnFile.Location = new Point(431, 29);
            btnFile.Name = "btnFile";
            btnFile.Size = new Size(75, 23);
            btnFile.TabIndex = 1;
            btnFile.Text = "...";
            btnFile.UseVisualStyleBackColor = true;
            btnFile.Click += btnFile_Click;
            // 
            // tbResult
            // 
            tbResult.Location = new Point(12, 89);
            tbResult.MaxLength = 1000000;
            tbResult.Multiline = true;
            tbResult.Name = "tbResult";
            tbResult.ReadOnly = true;
            tbResult.ScrollBars = ScrollBars.Vertical;
            tbResult.Size = new Size(494, 667);
            tbResult.TabIndex = 3;
            // 
            // btnCheck
            // 
            btnCheck.Location = new Point(212, 59);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(75, 23);
            btnCheck.TabIndex = 2;
            btnCheck.Text = "Проверить";
            btnCheck.UseVisualStyleBackColor = true;
            btnCheck.Click += btnCheck_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 4;
            label1.Text = "Файл";
            // 
            // ValidationTestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(518, 768);
            Controls.Add(label1);
            Controls.Add(btnCheck);
            Controls.Add(tbResult);
            Controls.Add(btnFile);
            Controls.Add(tbFile);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "ValidationTestForm";
            Text = "Проверка формата";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbFile;
        private Button btnFile;
        private TextBox tbResult;
        private Button btnCheck;
        private Label label1;
    }
}
