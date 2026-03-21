namespace Lab4
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            toolStrip1 = new ToolStrip();
            FileToolStripDropDownButton = new ToolStripDropDownButton();
            LoadToolStripMenuItem = new ToolStripMenuItem();
            SaveToolStripMenuItem = new ToolStripMenuItem();
            CalculationDataGridView = new DataGridView();
            AddMovementButton = new Button();
            RemoveMovementButton = new Button();
            MoveCheckedListBox = new CheckedListBox();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CalculationDataGridView).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { FileToolStripDropDownButton });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(859, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // FileToolStripDropDownButton
            // 
            FileToolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            FileToolStripDropDownButton.DropDownItems.AddRange(new ToolStripItem[] { LoadToolStripMenuItem, SaveToolStripMenuItem });
            FileToolStripDropDownButton.Image = (Image)resources.GetObject("FileToolStripDropDownButton.Image");
            FileToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
            FileToolStripDropDownButton.Name = "FileToolStripDropDownButton";
            FileToolStripDropDownButton.Size = new Size(59, 24);
            FileToolStripDropDownButton.Text = "Файл";
            // 
            // LoadToolStripMenuItem
            // 
            LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
            LoadToolStripMenuItem.Size = new Size(192, 26);
            LoadToolStripMenuItem.Text = "Открыть";
            LoadToolStripMenuItem.Click += LoadToolStripMenuItemClick;
            // 
            // SaveToolStripMenuItem
            // 
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.Size = new Size(192, 26);
            SaveToolStripMenuItem.Text = "Сохранить как";
            SaveToolStripMenuItem.Click += SaveToolStripMenuItemClick;
            // 
            // CalculationDataGridView
            // 
            CalculationDataGridView.AllowUserToAddRows = false;
            CalculationDataGridView.AllowUserToDeleteRows = false;
            CalculationDataGridView.AllowUserToResizeRows = false;
            CalculationDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            CalculationDataGridView.BackgroundColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            CalculationDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            CalculationDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CalculationDataGridView.Location = new Point(12, 39);
            CalculationDataGridView.Name = "CalculationDataGridView";
            CalculationDataGridView.ReadOnly = true;
            CalculationDataGridView.RowHeadersVisible = false;
            CalculationDataGridView.RowHeadersWidth = 51;
            CalculationDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CalculationDataGridView.Size = new Size(833, 339);
            CalculationDataGridView.TabIndex = 1;
            // 
            // AddMovementButton
            // 
            AddMovementButton.Location = new Point(94, 409);
            AddMovementButton.Name = "AddMovementButton";
            AddMovementButton.Size = new Size(153, 29);
            AddMovementButton.TabIndex = 2;
            AddMovementButton.Text = "Рассчитать";
            AddMovementButton.UseVisualStyleBackColor = true;
            AddMovementButton.Click += AddMovementButton_Click;
            // 
            // RemoveMovementButton
            // 
            RemoveMovementButton.Location = new Point(315, 409);
            RemoveMovementButton.Name = "RemoveMovementButton";
            RemoveMovementButton.Size = new Size(153, 29);
            RemoveMovementButton.TabIndex = 3;
            RemoveMovementButton.Text = "Удалить запись";
            RemoveMovementButton.UseVisualStyleBackColor = true;
            RemoveMovementButton.Click += RemoveMovementButton_Click;
            // 
            // MoveCheckedListBox
            // 
            MoveCheckedListBox.FormattingEnabled = true;
            MoveCheckedListBox.Location = new Point(556, 392);
            MoveCheckedListBox.Name = "MoveCheckedListBox";
            MoveCheckedListBox.Size = new Size(237, 70);
            MoveCheckedListBox.TabIndex = 4;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(859, 474);
            Controls.Add(MoveCheckedListBox);
            Controls.Add(RemoveMovementButton);
            Controls.Add(AddMovementButton);
            Controls.Add(CalculationDataGridView);
            Controls.Add(toolStrip1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Калькулятор для расчета координаты тела";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CalculationDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripDropDownButton FileToolStripDropDownButton;
        private ToolStripMenuItem LoadToolStripMenuItem;
        private ToolStripMenuItem SaveToolStripMenuItem;
        private DataGridView CalculationDataGridView;
        private Button AddMovementButton;
        private Button RemoveMovementButton;
        private CheckedListBox MoveCheckedListBox;
        private DataGridViewTextBoxColumn Column1;
    }
}