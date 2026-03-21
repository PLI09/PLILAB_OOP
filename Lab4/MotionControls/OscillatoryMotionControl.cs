using System.Globalization;
using Model;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Элемент управления для ввода параметров колебательного движения
    /// </summary>
    public partial class OscillatoryMotionControl : UserControl, IMotionInput
    {
        /// <summary>
        /// Валидатор для поля «Начальная координата»
        /// </summary>
        private readonly ValidateMotionControl _valInitial = 
            new() { AllowNegative = true };

        /// <summary>
        /// Валидатор для поля «Частота колебаний»
        /// </summary>
        private readonly ValidateMotionControl _valFrequency = 
            new() { AllowZero = false };

        /// <summary>
        /// Валидатор для поля «Скорость»
        /// </summary>
        private readonly ValidateMotionControl _valSpeed = new();

        /// <summary>
        /// Валидатор для поля «Время»
        /// </summary>
        private readonly ValidateMotionControl _valTime = new();

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public OscillatoryMotionControl()
        {
            InitializeComponent();
            InitialCoordinateTextBoxOM.TextChanged += 
                _valInitial.TextBox_TextChanged;
            SpeedTextBoxOM.TextChanged += _valSpeed.TextBox_TextChanged;
            TimeTextBoxOM.TextChanged += _valTime.TextBox_TextChanged;
            FrequencyTextBoxOM.TextChanged += 
                _valFrequency.TextBox_TextChanged;
        }

        /// <summary>
        /// Создаёт и возвращает объект с параметрами из полей ввода
        /// </summary>
        /// <returns>Новый экземпляр</returns>
        public MotionBase GetMotion() => new OscillatoryMotion(
            ParseDouble(FrequencyTextBoxOM.Text),
            ParseDouble(InitialCoordinateTextBoxOM.Text),
            ParseDouble(TimeTextBoxOM.Text),
            ParseDouble(SpeedTextBoxOM.Text));

        /// <summary>
        /// Проверяет, прошли ли все поля ввода валидацию
        /// </summary>
        /// <returns>true, если все четыре валидатора иначе false</returns>
        public bool ValidateInput() =>
            _valInitial.IsValid && _valSpeed.IsValid && 
            _valTime.IsValid && _valFrequency.IsValid;

        /// <summary>
        /// Преобразует строку в число типа double с поддержкой различных
        /// форматов
        /// </summary>
        /// <param name="text">Строка для преобразования</param>
        /// <returns>Числовое значение типа double</returns>
        private double ParseDouble(string text) =>
            double.Parse(text.Replace(',', '.'), 
                CultureInfo.InvariantCulture);
    }
}

