using System.Globalization;
using Model;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Элемент управления для ввода параметров равномерного движения
    /// </summary>
    public partial class UniformMotionControl : UserControl, IMotionInput
    {
        /// <summary>
        /// Валидатор для поля «Начальная координата»
        /// </summary>
        private readonly ValidateMotionControl _valInitial = 
            new() { AllowNegative = true };

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
        public UniformMotionControl()
        {
            InitializeComponent();
            InitialCoordinateTextBoxUM.TextChanged += 
                _valInitial.TextBox_TextChanged;
            SpeedTextBoxUM.TextChanged += _valSpeed.TextBox_TextChanged;
            TimeTextBoxUM.TextChanged += _valTime.TextBox_TextChanged;
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
            _valInitial.IsValid && _valSpeed.IsValid && _valTime.IsValid;

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
