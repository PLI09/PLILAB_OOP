using System.Globalization;
using Model;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Форма для равноускоренного движения
    /// </summary>
    public partial class AcceleratedMotionControl : UserControl, IMotionInput
    {
        private ValidateMotionControl valInitial;
        private ValidateMotionControl valSpeed;
        private ValidateMotionControl valTime;
        private ValidateMotionControl valAccel;

        public AcceleratedMotionControl()
        {
            InitializeComponent();

            valInitial = new ValidateMotionControl { AllowNegative = true };
            valAccel = new ValidateMotionControl { AllowNegative = true };
            valSpeed = new ValidateMotionControl();
            valTime = new ValidateMotionControl();

            InitialCoordinateTextBoxAM.TextChanged += valInitial.TextBox_TextChanged;
            SpeedTextBoxAM.TextChanged += valSpeed.TextBox_TextChanged;
            TimeTextBoxAM.TextChanged += valTime.TextBox_TextChanged;
            AccelerateTextBoxAM.TextChanged += valAccel.TextBox_TextChanged;
        }

        /// <summary>
        /// Реализация метода интерфейса: возвращает объект движения
        /// </summary>
        public MotionBase GetMotion()  //
        {
            double initial = ParseDouble(InitialCoordinateTextBoxAM.Text);
            double speed = ParseDouble(SpeedTextBoxAM.Text);
            double time = ParseDouble(TimeTextBoxAM.Text);
            double accel = ParseDouble(AccelerateTextBoxAM.Text);

            return new UniformlyAcceleratedMotion(accel, initial, time, speed);
        }

        /// <summary>
        /// Проверка ввода во всех полях
        /// </summary>
        public bool ValidateInput()
        {
            return valInitial.IsValid && valSpeed.IsValid && valTime.IsValid && valAccel.IsValid;
        }

        private double ParseDouble(string text)
        {
            return double.Parse(text.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
    }
}

