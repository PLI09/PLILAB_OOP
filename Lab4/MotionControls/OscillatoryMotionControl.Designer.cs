namespace Lab4.MotionControls
{
    partial class OscillatoryMotionControl
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
            InitialCoordinateLabelOM = new Label();
            TimeLabelOM = new Label();
            SpeedLabelOM = new Label();
            FrequencyLabelOM = new Label();
            InitialCoordinateTextBoxOM = new TextBox();
            TimeTextBoxOM = new TextBox();
            SpeedTextBoxOM = new TextBox();
            FrequencyTextBoxOM = new TextBox();
            SuspendLayout();
            // 
            // InitialCoordinateLabelOM
            // 
            InitialCoordinateLabelOM.AutoSize = true;
            InitialCoordinateLabelOM.Location = new Point(30, 58);
            InitialCoordinateLabelOM.Name = "InitialCoordinateLabelOM";
            InitialCoordinateLabelOM.Size = new Size(171, 20);
            InitialCoordinateLabelOM.TabIndex = 0;
            InitialCoordinateLabelOM.Text = "Начальная координата";
            // 
            // TimeLabelOM
            // 
            TimeLabelOM.AutoSize = true;
            TimeLabelOM.Location = new Point(30, 102);
            TimeLabelOM.Name = "TimeLabelOM";
            TimeLabelOM.Size = new Size(54, 20);
            TimeLabelOM.TabIndex = 1;
            TimeLabelOM.Text = "Время";
            // 
            // SpeedLabelOM
            // 
            SpeedLabelOM.AutoSize = true;
            SpeedLabelOM.Location = new Point(30, 151);
            SpeedLabelOM.Name = "SpeedLabelOM";
            SpeedLabelOM.Size = new Size(73, 20);
            SpeedLabelOM.TabIndex = 2;
            SpeedLabelOM.Text = "Скорость";
            // 
            // FrequencyLabelOM
            // 
            FrequencyLabelOM.AutoSize = true;
            FrequencyLabelOM.Location = new Point(30, 198);
            FrequencyLabelOM.Name = "FrequencyLabelOM";
            FrequencyLabelOM.Size = new Size(63, 20);
            FrequencyLabelOM.TabIndex = 3;
            FrequencyLabelOM.Text = "Частота";
            // 
            // InitialCoordinateTextBoxOM
            // 
            InitialCoordinateTextBoxOM.Location = new Point(228, 53);
            InitialCoordinateTextBoxOM.Name = "InitialCoordinateTextBoxOM";
            InitialCoordinateTextBoxOM.Size = new Size(125, 27);
            InitialCoordinateTextBoxOM.TabIndex = 4;
            // 
            // TimeTextBoxOM
            // 
            TimeTextBoxOM.Location = new Point(228, 99);
            TimeTextBoxOM.Name = "TimeTextBoxOM";
            TimeTextBoxOM.Size = new Size(125, 27);
            TimeTextBoxOM.TabIndex = 5;
            // 
            // SpeedTextBoxOM
            // 
            SpeedTextBoxOM.Location = new Point(228, 148);
            SpeedTextBoxOM.Name = "SpeedTextBoxOM";
            SpeedTextBoxOM.Size = new Size(125, 27);
            SpeedTextBoxOM.TabIndex = 6;
            // 
            // FrequencyTextBoxOM
            // 
            FrequencyTextBoxOM.Location = new Point(228, 195);
            FrequencyTextBoxOM.Name = "FrequencyTextBoxOM";
            FrequencyTextBoxOM.Size = new Size(125, 27);
            FrequencyTextBoxOM.TabIndex = 7;
            // 
            // OscillatoryMotionControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(FrequencyTextBoxOM);
            Controls.Add(SpeedTextBoxOM);
            Controls.Add(TimeTextBoxOM);
            Controls.Add(InitialCoordinateTextBoxOM);
            Controls.Add(FrequencyLabelOM);
            Controls.Add(SpeedLabelOM);
            Controls.Add(TimeLabelOM);
            Controls.Add(InitialCoordinateLabelOM);
            Name = "OscillatoryMotionControl";
            Size = new Size(375, 284);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label InitialCoordinateLabelOM;
        private Label TimeLabelOM;
        private Label SpeedLabelOM;
        private Label FrequencyLabelOM;
        private TextBox InitialCoordinateTextBoxOM;
        private TextBox TimeTextBoxOM;
        private TextBox SpeedTextBoxOM;
        private TextBox FrequencyTextBoxOM;
    }
}