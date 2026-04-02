using System.Globalization;
using Model;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Элемент управления для ввода параметров колебательного движения
    /// </summary>
    public partial class OscillatoryMotionControl : UserControl, IMotionInput
    {
        //TODO: RSDN+
        /// <summary>
        /// Валидатор для поля «Начальная координата»
        /// </summary>
        private readonly DoubleValidator _validateInitial = 
            new() { AllowNegative = true };

        //TODO: RSDN+
        /// <summary>
        /// Валидатор для поля «Частота колебаний»
        /// </summary>
        private readonly DoubleValidator _validateFrequency = 
            new() { AllowZero = false };

        //TODO: RSDN+
        /// <summary>
        /// Валидатор для поля «Скорость»
        /// </summary>
        private readonly DoubleValidator _validateSpeed = new();

        //TODO: RSDN+
        /// <summary>
        /// Валидатор для поля «Время»
        /// </summary>
        private readonly DoubleValidator _validateTime = new();

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public OscillatoryMotionControl()
        {
            InitializeComponent();
            InitialCoordinateTextBoxOM.TextChanged += 
                _validateInitial.TextBox_TextChanged;
            SpeedTextBoxOM.TextChanged += _validateSpeed.TextBox_TextChanged;
            TimeTextBoxOM.TextChanged += _validateTime.TextBox_TextChanged;
            FrequencyTextBoxOM.TextChanged += 
                _validateFrequency.TextBox_TextChanged;
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
            _validateInitial.IsValid && _validateSpeed.IsValid && 
            _validateTime.IsValid && _validateFrequency.IsValid;

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

