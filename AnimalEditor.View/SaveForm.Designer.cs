namespace AnimalEditor.View
{
    partial class SaveForm
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
            FormatUpDown = new DomainUpDown();
            filePathTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            ArchiveUpDown = new DomainUpDown();
            SaveButton = new Button();
            ChoosePathButton = new Button();
            SuspendLayout();
            // 
            // FormatUpDown
            // 
            FormatUpDown.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormatUpDown.Location = new Point(203, 54);
            FormatUpDown.Name = "FormatUpDown";
            FormatUpDown.ReadOnly = true;
            FormatUpDown.Size = new Size(187, 34);
            FormatUpDown.TabIndex = 1;
            // 
            // filePathTextBox
            // 
            filePathTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            filePathTextBox.Location = new Point(203, 14);
            filePathTextBox.Name = "filePathTextBox";
            filePathTextBox.ReadOnly = true;
            filePathTextBox.Size = new Size(203, 34);
            filePathTextBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(25, 60);
            label2.Name = "label2";
            label2.Size = new Size(121, 28);
            label2.TabIndex = 3;
            label2.Text = "Save format:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(25, 98);
            label3.Name = "label3";
            label3.Size = new Size(172, 28);
            label3.TabIndex = 4;
            label3.Text = "Archiving settings:";
            // 
            // ArchiveUpDown
            // 
            ArchiveUpDown.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ArchiveUpDown.Location = new Point(203, 96);
            ArchiveUpDown.Name = "ArchiveUpDown";
            ArchiveUpDown.ReadOnly = true;
            ArchiveUpDown.Size = new Size(187, 34);
            ArchiveUpDown.TabIndex = 5;
            // 
            // SaveButton
            // 
            SaveButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            SaveButton.Location = new Point(124, 136);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(154, 38);
            SaveButton.TabIndex = 6;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += doButton_Click;
            // 
            // ChoosePathButton
            // 
            ChoosePathButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ChoosePathButton.Location = new Point(25, 12);
            ChoosePathButton.Name = "ChoosePathButton";
            ChoosePathButton.Size = new Size(156, 37);
            ChoosePathButton.TabIndex = 8;
            ChoosePathButton.Text = "Choose folder:";
            ChoosePathButton.UseVisualStyleBackColor = true;
            ChoosePathButton.Click += ChoosePathButton_Click;
            // 
            // SaveForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(421, 189);
            Controls.Add(ChoosePathButton);
            Controls.Add(SaveButton);
            Controls.Add(ArchiveUpDown);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(filePathTextBox);
            Controls.Add(FormatUpDown);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SaveForm";
            Text = "Save";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DomainUpDown FormatUpDown;
        private TextBox filePathTextBox;
        private Label label2;
        private Label label3;
        private DomainUpDown ArchiveUpDown;
        private Button SaveButton;
        private Button ChoosePathButton;
    }
}