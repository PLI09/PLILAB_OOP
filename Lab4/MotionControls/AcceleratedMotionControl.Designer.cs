namespace Lab4.MotionControls
{
    partial class AcceleratedMotionControl
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
            InitialCoordinateLabelAM = new Label();
            SpeedLabelAM = new Label();
            TimeLabelAM = new Label();
            InitialCoordinateTextBoxAM = new TextBox();
            TimeTextBoxAM = new TextBox();
            SpeedTextBoxAM = new TextBox();
            AccelerateLabelAM = new Label();
            AccelerateTextBoxAM = new TextBox();
            SuspendLayout();
            // 
            // InitialCoordinateLabelAM
            // 
            InitialCoordinateLabelAM.AutoSize = true;
            InitialCoordinateLabelAM.Location = new Point(30, 91);
            InitialCoordinateLabelAM.Name = "InitialCoordinateLabelAM";
            InitialCoordinateLabelAM.Size = new Size(171, 20);
            InitialCoordinateLabelAM.TabIndex = 0;
            InitialCoordinateLabelAM.Text = "Начальная координата";
            // 
            // SpeedLabelAM
            // 
            SpeedLabelAM.AutoSize = true;
            SpeedLabelAM.Location = new Point(30, 181);
            SpeedLabelAM.Name = "SpeedLabelAM";
            SpeedLabelAM.Size = new Size(73, 20);
            SpeedLabelAM.TabIndex = 1;
            SpeedLabelAM.Text = "Скорость";
            // 
            // TimeLabelAM
            // 
            TimeLabelAM.AutoSize = true;
            TimeLabelAM.Location = new Point(30, 132);
            TimeLabelAM.Name = "TimeLabelAM";
            TimeLabelAM.Size = new Size(54, 20);
            TimeLabelAM.TabIndex = 2;
            TimeLabelAM.Text = "Время";
            // 
            // InitialCoordinateTextBoxAM
            // 
            InitialCoordinateTextBoxAM.Location = new Point(228, 86);
            InitialCoordinateTextBoxAM.Name = "InitialCoordinateTextBoxAM";
            InitialCoordinateTextBoxAM.Size = new Size(125, 27);
            InitialCoordinateTextBoxAM.TabIndex = 3;
            // 
            // TimeTextBoxAM
            // 
            TimeTextBoxAM.Location = new Point(228, 132);
            TimeTextBoxAM.Name = "TimeTextBoxAM";
            TimeTextBoxAM.Size = new Size(125, 27);
            TimeTextBoxAM.TabIndex = 4;
            // 
            // SpeedTextBoxAM
            // 
            SpeedTextBoxAM.Location = new Point(228, 181);
            SpeedTextBoxAM.Name = "SpeedTextBoxAM";
            SpeedTextBoxAM.Size = new Size(125, 27);
            SpeedTextBoxAM.TabIndex = 5;
            // 
            // AccelerateLabelAM
            // 
            AccelerateLabelAM.AutoSize = true;
            AccelerateLabelAM.Location = new Point(30, 231);
            AccelerateLabelAM.Name = "AccelerateLabelAM";
            AccelerateLabelAM.Size = new Size(84, 20);
            AccelerateLabelAM.TabIndex = 6;
            AccelerateLabelAM.Text = "Ускорение";
            // 
            // AccelerateTextBoxAM
            // 
            AccelerateTextBoxAM.Location = new Point(228, 228);
            AccelerateTextBoxAM.Name = "AccelerateTextBoxAM";
            AccelerateTextBoxAM.Size = new Size(125, 27);
            AccelerateTextBoxAM.TabIndex = 7;
            // 
            // AcceleratedMotionControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(AccelerateTextBoxAM);
            Controls.Add(AccelerateLabelAM);
            Controls.Add(SpeedTextBoxAM);
            Controls.Add(TimeTextBoxAM);
            Controls.Add(InitialCoordinateTextBoxAM);
            Controls.Add(TimeLabelAM);
            Controls.Add(SpeedLabelAM);
            Controls.Add(InitialCoordinateLabelAM);
            Name = "AcceleratedMotionControl";
            Size = new Size(375, 284);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label InitialCoordinateLabelAM;
        private Label SpeedLabelAM;
        private Label TimeLabelAM;
        private TextBox InitialCoordinateTextBoxAM;
        private TextBox TimeTextBoxAM;
        private TextBox SpeedTextBoxAM;
        private Label AccelerateLabelAM;
        private TextBox AccelerateTextBoxAM;
    }
}