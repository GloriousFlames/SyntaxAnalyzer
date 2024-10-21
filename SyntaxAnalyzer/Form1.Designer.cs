namespace SyntaxAnalyzer
{
    partial class MainAnalyzer
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
            btnSyntax = new Button();
            btnSemantic = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            tbSemantic = new TextBox();
            btnInfo = new Button();
            rtbSyntax = new RichTextBox();
            rtbInput = new RichTextBox();
            SuspendLayout();
            // 
            // btnSyntax
            // 
            btnSyntax.AutoSize = true;
            btnSyntax.Font = new Font("Segoe UI Light", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSyntax.Location = new Point(14, 85);
            btnSyntax.Margin = new Padding(3, 4, 3, 4);
            btnSyntax.Name = "btnSyntax";
            btnSyntax.Size = new Size(250, 31);
            btnSyntax.TabIndex = 0;
            btnSyntax.Text = "Синтаксический анализ";
            btnSyntax.UseVisualStyleBackColor = true;
            btnSyntax.Click += ButtonSyntax_Click;
            // 
            // btnSemantic
            // 
            btnSemantic.AutoSize = true;
            btnSemantic.Enabled = false;
            btnSemantic.Font = new Font("Segoe UI Light", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSemantic.Location = new Point(322, 85);
            btnSemantic.Margin = new Padding(3, 4, 3, 4);
            btnSemantic.Name = "btnSemantic";
            btnSemantic.Size = new Size(250, 31);
            btnSemantic.TabIndex = 1;
            btnSemantic.Text = "Семантический анализ";
            btnSemantic.UseVisualStyleBackColor = true;
            btnSemantic.Click += ButtonSemantic_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Light", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(14, 23);
            label1.Name = "label1";
            label1.Size = new Size(208, 21);
            label1.TabIndex = 3;
            label1.Text = "Введите строку для анализа:";
            // 
            // label2
            // 
            label2.Location = new Point(14, 147);
            label2.Name = "label2";
            label2.Size = new Size(558, 21);
            label2.TabIndex = 4;
            label2.Text = "Результат синтаксического анализа:";
            // 
            // label3
            // 
            label3.Location = new Point(12, 363);
            label3.Name = "label3";
            label3.Size = new Size(558, 21);
            label3.TabIndex = 6;
            label3.Text = "Результат семантического анализа:";
            // 
            // tbSemantic
            // 
            tbSemantic.Location = new Point(14, 387);
            tbSemantic.Multiline = true;
            tbSemantic.Name = "tbSemantic";
            tbSemantic.ReadOnly = true;
            tbSemantic.ScrollBars = ScrollBars.Vertical;
            tbSemantic.Size = new Size(558, 150);
            tbSemantic.TabIndex = 7;
            // 
            // btnInfo
            // 
            btnInfo.Location = new Point(477, 5);
            btnInfo.Name = "btnInfo";
            btnInfo.Size = new Size(95, 29);
            btnInfo.TabIndex = 8;
            btnInfo.Text = "О задании";
            btnInfo.UseVisualStyleBackColor = true;
            btnInfo.Click += btnInfo_Click;
            // 
            // rtbSyntax
            // 
            rtbSyntax.Location = new Point(14, 171);
            rtbSyntax.Multiline = true;
            rtbSyntax.Name = "tbSyntax";
            rtbSyntax.ReadOnly = true;
            rtbSyntax.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbSyntax.Size = new Size(558, 150);
            rtbSyntax.TabIndex = 5;
            // 
            // rtbInput
            // 
            rtbInput.Location = new Point(14, 47);
            rtbInput.Multiline = false;
            rtbInput.Name = "rtbInput";
            rtbInput.Size = new Size(556, 31);
            rtbInput.TabIndex = 9;
            rtbInput.Text = "";
            // 
            // MainAnalyzer
            // 
            AutoScaleDimensions = new SizeF(8F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 561);
            Controls.Add(rtbInput);
            Controls.Add(btnInfo);
            Controls.Add(tbSemantic);
            Controls.Add(label3);
            Controls.Add(rtbSyntax);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSemantic);
            Controls.Add(btnSyntax);
            Font = new Font("Segoe UI Light", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainAnalyzer";
            Text = "Синтаксический анализатор";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSyntax;
        private Button btnSemantic;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox tbSemantic;
        private Button btnInfo;
        private RichTextBox rtbSyntax;
        private RichTextBox rtbInput;
    }
}
