namespace Lab4
{
    partial class CalculateMotion
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
            TypeMoveLabel = new Label();
            TypeMoveComboBox = new ComboBox();
            СalculateButton = new Button();
#if DEBUG
            RandomButton = new Button();
#endif
            SuspendLayout();
            // 
            // TypeMoveLabel
            // 
            TypeMoveLabel.AutoSize = true;
            TypeMoveLabel.Location = new Point(43, 57);
            TypeMoveLabel.Name = "TypeMoveLabel";
            TypeMoveLabel.Size = new Size(109, 20);
            TypeMoveLabel.TabIndex = 0;
            TypeMoveLabel.Text = "Тип движения";
            // 
            // TypeMoveComboBox
            // 
            TypeMoveComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TypeMoveComboBox.FormattingEnabled = true;
            TypeMoveComboBox.Location = new Point(178, 55);
            TypeMoveComboBox.Name = "TypeMoveComboBox";
            TypeMoveComboBox.Size = new Size(247, 28);
            TypeMoveComboBox.TabIndex = 1;
            TypeMoveComboBox.SelectedIndexChanged += TypeMoveComboBox_SelectedIndexChanged;
            // 
            // СalculateButton
            // 
            СalculateButton.Location = new Point(29, 355);
            СalculateButton.Name = "СalculateButton";
            СalculateButton.Size = new Size(183, 29);
            СalculateButton.TabIndex = 2;
            СalculateButton.Text = "Рассчитать параметры";
            СalculateButton.UseVisualStyleBackColor = true;
            СalculateButton.Click += СalculateButton_Click;
            // 
            // RandomButton
            // 
#if DEBUG
            RandomButton.Location = new Point(242, 355);
            RandomButton.Name = "RandomButton";
            RandomButton.Size = new Size(183, 29);
            RandomButton.TabIndex = 3;
            RandomButton.Text = "Рандомные параметры";
            RandomButton.UseVisualStyleBackColor = true;
            RandomButton.Click += RandomButton_Click;
#endif
            // 
            // CalculateMotion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(463, 420);
#if DEBUG
            Controls.Add(RandomButton);
#endif
            Controls.Add(СalculateButton);
            Controls.Add(TypeMoveComboBox);
            Controls.Add(TypeMoveLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CalculateMotion";
            Text = "Расчет координаты";
            ResumeLayout(false);
            PerformLayout();
        }

#endregion

        private Label TypeMoveLabel;
        private ComboBox TypeMoveComboBox;
        private Button СalculateButton;
#if DEBUG
        private Button RandomButton;
#endif
    }
}