using System.Globalization;
using Model;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Форма для колебательного движения
    /// </summary>
    public partial class OscillatoryMotionControl : UserControl, IMotionInput
    {
        private ValidateMotionControl valInitial;
        private ValidateMotionControl valSpeed;
        private ValidateMotionControl valTime;
        private ValidateMotionControl valFrequency;

        public OscillatoryMotionControl()
        {
            InitializeComponent();

            valInitial = new ValidateMotionControl { AllowNegative = true };
            valFrequency = new ValidateMotionControl { AllowNegative = false, AllowZero = false };
            valSpeed = new ValidateMotionControl();
            valTime = new ValidateMotionControl();

            InitialCoordinateTextBoxOM.TextChanged += valInitial.TextBox_TextChanged;
            SpeedTextBoxOM.TextChanged += valSpeed.TextBox_TextChanged;
            TimeTextBoxOM.TextChanged += valTime.TextBox_TextChanged;
            FrequencyTextBoxOM.TextChanged += valFrequency.TextBox_TextChanged;
        }

        /// <summary>
        /// Реализация метода интерфейса: возвращает объект движения
        /// </summary>
        public MotionBase GetMotion()
        {
            double initial = ParseDouble(InitialCoordinateTextBoxOM.Text);
            double speed = ParseDouble(SpeedTextBoxOM.Text);
            double time = ParseDouble(TimeTextBoxOM.Text);
            double freq = ParseDouble(FrequencyTextBoxOM.Text);

            return new OscillatoryMotion(freq, initial, time, speed);
        }

        /// <summary>
        /// Проверка ввода во всех полях
        /// </summary>
        public bool ValidateInput()
        {
            return valInitial.IsValid && valSpeed.IsValid && valTime.IsValid && valFrequency.IsValid;
        }

        private double ParseDouble(string text)
        {
            return double.Parse(text.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
    }
}

