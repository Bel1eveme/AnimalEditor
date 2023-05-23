namespace AnimalEditor.View
{
    partial class OpenForm
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
            filePathTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            OpenButton = new Button();
            ChooseFileButton = new Button();
            FormatTextBox = new TextBox();
            ArchieveTextBox = new TextBox();
            SuspendLayout();
            // 
            // filePathTextBox
            // 
            filePathTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            filePathTextBox.Location = new Point(194, 15);
            filePathTextBox.Name = "filePathTextBox";
            filePathTextBox.ReadOnly = true;
            filePathTextBox.Size = new Size(218, 34);
            filePathTextBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 60);
            label2.Name = "label2";
            label2.Size = new Size(152, 28);
            label2.TabIndex = 3;
            label2.Text = "Serialize format:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 97);
            label3.Name = "label3";
            label3.Size = new Size(172, 28);
            label3.TabIndex = 4;
            label3.Text = "Archiving settings:";
            // 
            // OpenButton
            // 
            OpenButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            OpenButton.Location = new Point(124, 136);
            OpenButton.Name = "OpenButton";
            OpenButton.Size = new Size(154, 38);
            OpenButton.TabIndex = 6;
            OpenButton.Text = "Open";
            OpenButton.UseVisualStyleBackColor = true;
            OpenButton.Click += OpenButton_Click;
            // 
            // ChooseFileButton
            // 
            ChooseFileButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ChooseFileButton.Location = new Point(12, 12);
            ChooseFileButton.Name = "ChooseFileButton";
            ChooseFileButton.Size = new Size(156, 37);
            ChooseFileButton.TabIndex = 9;
            ChooseFileButton.Text = "Choose file:";
            ChooseFileButton.UseVisualStyleBackColor = true;
            ChooseFileButton.Click += ChooseFileButton_Click;
            // 
            // FormatTextBox
            // 
            FormatTextBox.Enabled = false;
            FormatTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormatTextBox.Location = new Point(194, 57);
            FormatTextBox.Name = "FormatTextBox";
            FormatTextBox.ReadOnly = true;
            FormatTextBox.Size = new Size(218, 34);
            FormatTextBox.TabIndex = 10;
            // 
            // ArchieveTextBox
            // 
            ArchieveTextBox.Enabled = false;
            ArchieveTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ArchieveTextBox.Location = new Point(194, 97);
            ArchieveTextBox.Name = "ArchieveTextBox";
            ArchieveTextBox.ReadOnly = true;
            ArchieveTextBox.Size = new Size(218, 34);
            ArchieveTextBox.TabIndex = 11;
            // 
            // OpenForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(421, 189);
            Controls.Add(ArchieveTextBox);
            Controls.Add(FormatTextBox);
            Controls.Add(ChooseFileButton);
            Controls.Add(OpenButton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(filePathTextBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "OpenForm";
            Text = "Open";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox filePathTextBox;
        private Label label2;
        private Label label3;
        private Button OpenButton;
        private Button ChooseFileButton;
        private TextBox FormatTextBox;
        private TextBox ArchieveTextBox;
    }
}