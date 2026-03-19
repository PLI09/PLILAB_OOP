using Model;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Форма для колебательного движения
    /// </summary>
    public partial class OscillatoryMotionControl : UserControl, IMove
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
        /// Валидатор для поля частоты
        /// </summary>
        private ValidateMotionControl valFrequency;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public OscillatoryMotionControl()
        {
            InitializeComponent();

            valInitial = new ValidateMotionControl { AllowNegative = true };   
            valFrequency = new ValidateMotionControl { AllowNegative = false,
                AllowZero = false }; 
            valSpeed = new ValidateMotionControl();                        
            valTime = new ValidateMotionControl();        

            InitialCoordinateTextBoxOM.TextChanged += 
                valInitial.TextBox_TextChanged;
            SpeedTextBoxOM.TextChanged += 
                valSpeed.TextBox_TextChanged;
            TimeTextBoxOM.TextChanged += 
                valTime.TextBox_TextChanged;
            FrequencyTextBoxOM.TextChanged += 
                valFrequency.TextBox_TextChanged;
        }

        /// <summary>
        /// Создание экземпляра класса колебательного движения 
        /// </summary>
        public MotionBase MovementParameters
        {
            get
            {
                double initial = double.Parse
                    (InitialCoordinateTextBoxOM.Text.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);
                double speed = double.Parse
                    (SpeedTextBoxOM.Text.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);
                double time = double.Parse
                    (TimeTextBoxOM.Text.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);
                double freq = double.Parse
                    (FrequencyTextBoxOM.Text.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);

                return new OscillatoryMotion(freq, initial, time, speed);
            }
        }

        /// <summary>
        /// Проверка ввода во всех полях
        /// </summary>
        /// <returns></returns>
        public bool ValidateInput()
        {
            return valInitial.IsValid && valSpeed.IsValid 
                && valTime.IsValid && valFrequency.IsValid;
        }
    }
}

