using System.Globalization;
using Model;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Элемент управления для ввода параметров равномерного движения
    /// </summary>
    public partial class UniformMotionControl : UserControl, IMotionInput
    {
        //TODO: RSDN+
        /// <summary>
        /// Валидатор для поля «Начальная координата»
        /// </summary>
        private readonly ValidateMotion _validateInitial = 
            new() { AllowNegative = true };

        //TODO: RSDN+
        /// <summary>
        /// Валидатор для поля «Скорость»
        /// </summary>
        private readonly ValidateMotion _validateSpeed = new();

        //TODO: RSDN+
        /// <summary>
        /// Валидатор для поля «Время»
        /// </summary>
        private readonly ValidateMotion _validateTime = new();

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public UniformMotionControl()
        {
            InitializeComponent();
            InitialCoordinateTextBoxUM.TextChanged += 
                _validateInitial.TextBox_TextChanged;
            SpeedTextBoxUM.TextChanged += _validateSpeed.TextBox_TextChanged;
            TimeTextBoxUM.TextChanged += _validateTime.TextBox_TextChanged;
        }

        /// <summary>
        /// Создаёт и возвращает объект с параметрами из полей ввода
        /// </summary>
        /// <returns></returns>
        public MotionBase GetMotion() => new UniformMotion(
            ParseDouble(SpeedTextBoxUM.Text),
            ParseDouble(InitialCoordinateTextBoxUM.Text),
            ParseDouble(TimeTextBoxUM.Text));

        /// <summary>
        /// Проверяет, прошли ли все поля ввода валидацию
        /// </summary>
        /// <returns></returns>
        public bool ValidateInput() =>
            _validateInitial.IsValid && _validateSpeed.IsValid 
            && _validateTime.IsValid;

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
