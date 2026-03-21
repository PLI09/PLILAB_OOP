namespace Lab4.MotionControls
{
    partial class UniformMotionControl
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
            InitialCoordinateLabelUM = new Label();
            TimeLabelUM = new Label();
            SpeedLabelUM = new Label();
            InitialCoordinateTextBoxUM = new TextBox();
            TimeTextBoxUM = new TextBox();
            SpeedTextBoxUM = new TextBox();
            SuspendLayout();
            // 
            // InitialCoordinateLabelUM
            // 
            InitialCoordinateLabelUM.AutoSize = true;
            InitialCoordinateLabelUM.Location = new Point(30, 67);
            InitialCoordinateLabelUM.Name = "InitialCoordinateLabelUM";
            InitialCoordinateLabelUM.Size = new Size(171, 20);
            InitialCoordinateLabelUM.TabIndex = 0;
            InitialCoordinateLabelUM.Text = "Начальная координата";
            // 
            // TimeLabelUM
            // 
            TimeLabelUM.AutoSize = true;
            TimeLabelUM.Location = new Point(30, 111);
            TimeLabelUM.Name = "TimeLabelUM";
            TimeLabelUM.Size = new Size(54, 20);
            TimeLabelUM.TabIndex = 1;
            TimeLabelUM.Text = "Время";
            // 
            // SpeedLabelUM
            // 
            SpeedLabelUM.AutoSize = true;
            SpeedLabelUM.Location = new Point(30, 157);
            SpeedLabelUM.Name = "SpeedLabelUM";
            SpeedLabelUM.Size = new Size(73, 20);
            SpeedLabelUM.TabIndex = 2;
            SpeedLabelUM.Text = "Скорость";
            // 
            // InitialCoordinateTextBoxUM
            // 
            InitialCoordinateTextBoxUM.Location = new Point(228, 62);
            InitialCoordinateTextBoxUM.Name = "InitialCoordinateTextBoxUM";
            InitialCoordinateTextBoxUM.Size = new Size(125, 27);
            InitialCoordinateTextBoxUM.TabIndex = 3;
            // 
            // TimeTextBoxUM
            // 
            TimeTextBoxUM.Location = new Point(228, 108);
            TimeTextBoxUM.Name = "TimeTextBoxUM";
            TimeTextBoxUM.Size = new Size(125, 27);
            TimeTextBoxUM.TabIndex = 4;
            // 
            // SpeedTextBoxUM
            // 
            SpeedTextBoxUM.Location = new Point(228, 157);
            SpeedTextBoxUM.Name = "SpeedTextBoxUM";
            SpeedTextBoxUM.Size = new Size(125, 27);
            SpeedTextBoxUM.TabIndex = 5;
            // 
            // UniformMotionControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(SpeedTextBoxUM);
            Controls.Add(TimeTextBoxUM);
            Controls.Add(InitialCoordinateTextBoxUM);
            Controls.Add(SpeedLabelUM);
            Controls.Add(TimeLabelUM);
            Controls.Add(InitialCoordinateLabelUM);
            Name = "UniformMotionControl";
            Size = new Size(375, 284);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label InitialCoordinateLabelUM;
        private Label TimeLabelUM;
        private Label SpeedLabelUM;
        private TextBox InitialCoordinateTextBoxUM;
        private TextBox TimeTextBoxUM;
        private TextBox SpeedTextBoxUM;
    }
}