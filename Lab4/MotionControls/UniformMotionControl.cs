using Model;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Форма для равномерного движения
    /// </summary>
    public partial class UniformMotionControl : UserControl, IMove
    {
        /// <summary>
        /// Валидатор для поля начальной координаты
        /// </summary>
        private ValidateMotionControl valInitial;

        /// <summary>
        /// Валидатор для поля скорости
        /// </summary>
        private ValidateMotionControl valSpeed;

        /// <summary>
        /// Валидатор для поля времени
        /// </summary>
        private ValidateMotionControl valTime;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public UniformMotionControl()
        {
            InitializeComponent();

            valInitial = new ValidateMotionControl { AllowNegative = true };
            valSpeed = new ValidateMotionControl();
            valTime = new ValidateMotionControl();

            InitialCoordinateTextBoxUM.TextChanged 
                += valInitial.TextBox_TextChanged;
            SpeedTextBoxUM.TextChanged 
                += valSpeed.TextBox_TextChanged;
            TimeTextBoxUM.TextChanged += valTime.TextBox_TextChanged;
        }

        /// <summary>
        /// Создание экземпляра класса колебательного движения 
        /// </summary>
        public MotionBase MovementParameters
        {
            get
            {
                double initial = double.Parse
                    (InitialCoordinateTextBoxUM.Text.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);
                double speed = double.Parse
                    (SpeedTextBoxUM.Text.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);
                double time = double.Parse
                    (TimeTextBoxUM.Text.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);

                return new UniformMotion(speed, initial, time);
            }
        }

        /// <summary>
        /// Проверка ввода во всех полях
        /// </summary>
        /// <returns></returns>
        public bool ValidateInput()
        {
            return valInitial.IsValid && valSpeed.IsValid && valTime.IsValid;
        }
    }
}
