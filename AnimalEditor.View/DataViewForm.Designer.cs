namespace AnimalEditor.View
{
    partial class DataViewForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            objectsDataGridView = new DataGridView();
            classDomainUpDown = new DomainUpDown();
            addButton = new Button();
            editButton = new Button();
            deleteButton = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)objectsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // objectsDataGridView
            // 
            objectsDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            objectsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            objectsDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            objectsDataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            objectsDataGridView.Location = new Point(12, 63);
            objectsDataGridView.MultiSelect = false;
            objectsDataGridView.Name = "objectsDataGridView";
            objectsDataGridView.RowHeadersWidth = 51;
            objectsDataGridView.RowTemplate.Height = 29;
            objectsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            objectsDataGridView.ShowCellToolTips = false;
            objectsDataGridView.ShowEditingIcon = false;
            objectsDataGridView.Size = new Size(776, 388);
            objectsDataGridView.TabIndex = 0;
            // 
            // classDomainUpDown
            // 
            classDomainUpDown.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            classDomainUpDown.Location = new Point(384, 12);
            classDomainUpDown.Name = "classDomainUpDown";
            classDomainUpDown.Size = new Size(218, 39);
            classDomainUpDown.TabIndex = 1;
            classDomainUpDown.SelectedItemChanged += classDomainUpDown_SelectedItemChanged;
            // 
            // addButton
            // 
            addButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            addButton.Location = new Point(53, 457);
            addButton.Name = "addButton";
            addButton.Size = new Size(150, 50);
            addButton.TabIndex = 3;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += saveButton_Click;
            // 
            // editButton
            // 
            editButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            editButton.Location = new Point(319, 457);
            editButton.Name = "editButton";
            editButton.Size = new Size(146, 50);
            editButton.TabIndex = 4;
            editButton.Text = "Edit";
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += editButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            deleteButton.Location = new Point(540, 457);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(194, 50);
            deleteButton.TabIndex = 5;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(207, 14);
            label1.Name = "label1";
            label1.Size = new Size(155, 32);
            label1.TabIndex = 6;
            label1.Text = "Choose class:";
            // 
            // DataViewForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 563);
            Controls.Add(label1);
            Controls.Add(deleteButton);
            Controls.Add(editButton);
            Controls.Add(addButton);
            Controls.Add(classDomainUpDown);
            Controls.Add(objectsDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "DataViewForm";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)objectsDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView objectsDataGridView;
        private DomainUpDown classDomainUpDown;
        private Button addButton;
        private Button editButton;
        private Button deleteButton;
        private Label label1;
    }
}