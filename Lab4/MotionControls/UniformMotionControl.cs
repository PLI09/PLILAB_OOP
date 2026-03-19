using System;
using System.Globalization;
using Model;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Форма для равномерного движения
    /// </summary>
    public partial class UniformMotionControl : UserControl, IMotionInput
    {
        private ValidateMotionControl valInitial;
        private ValidateMotionControl valSpeed;
        private ValidateMotionControl valTime;

        public UniformMotionControl()
        {
            InitializeComponent();

            valInitial = new ValidateMotionControl { AllowNegative = true };
            valSpeed = new ValidateMotionControl();
            valTime = new ValidateMotionControl();

            InitialCoordinateTextBoxUM.TextChanged += valInitial.TextBox_TextChanged;
            SpeedTextBoxUM.TextChanged += valSpeed.TextBox_TextChanged;
            TimeTextBoxUM.TextChanged += valTime.TextBox_TextChanged;
        }

        /// <summary>
        /// ✅ Реализация метода интерфейса: возвращает объект движения
        /// </summary>
        public MotionBase GetMotion()  // ← Метод, не свойство!
        {
            double initial = ParseDouble(InitialCoordinateTextBoxUM.Text);
            double speed = ParseDouble(SpeedTextBoxUM.Text);
            double time = ParseDouble(TimeTextBoxUM.Text);

            return new UniformMotion(speed, initial, time);
        }

        /// <summary>
        /// Проверка ввода во всех полях
        /// </summary>
        public bool ValidateInput()
        {
            return valInitial.IsValid && valSpeed.IsValid && valTime.IsValid;
        }

        /// <summary>
        /// Вспомогательный метод для парсинга double с заменой запятой
        /// </summary>
        private double ParseDouble(string text)
        {
            return double.Parse(text.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
    }
}
