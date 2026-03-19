using Model;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Форма для равноускоренного движения
    /// </summary>
    public partial class AcceleratedMotionControl : UserControl, IMove
    {
        //TODO: RSDN
        /// <summary>
        /// Валидатор для поля начальной координаты
        /// </summary>
        private ValidateMotionControl valInitial;

        /// <summary>
        /// Валидатор для поля скорости
        /// </summary>
        private ValidateMotionControl valSpeed;

        /// <summary>
        /// Валидатор для поля время
        /// </summary>
        private ValidateMotionControl valTime;

        /// <summary>
        /// Валидатор для поля ускорение
        /// </summary>
        private ValidateMotionControl valAccel;

        /// <summary>
        /// Конструтор формы
        /// </summary>
        public AcceleratedMotionControl()
        {
            InitializeComponent();

            valInitial = new ValidateMotionControl { AllowNegative = true };
            valAccel = new ValidateMotionControl { AllowNegative = true };
            valSpeed = new ValidateMotionControl(); 
            valTime = new ValidateMotionControl();

            InitialCoordinateTextBoxAM.TextChanged += 
                valInitial.TextBox_TextChanged;
            SpeedTextBoxAM.TextChanged += 
                valSpeed.TextBox_TextChanged;
            TimeTextBoxAM.TextChanged +=
                valTime.TextBox_TextChanged;
            AccelerateTextBoxAM.TextChanged += 
                valAccel.TextBox_TextChanged;
        }

        /// <summary>
        /// Создание экземпляра класса равноускоренного движения 
        /// </summary>
        public MotionBase MovementParameters
        {
            get
            {
                double initial = double.Parse
                    (InitialCoordinateTextBoxAM.Text.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);
                double speed = double.Parse
                    (SpeedTextBoxAM.Text.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);
                double time = double.Parse
                    (TimeTextBoxAM.Text.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);
                double accel = double.Parse
                    (AccelerateTextBoxAM.Text.Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture);

                return new UniformlyAcceleratedMotion(
                    accel, initial, time, speed);
            }
        }

        /// <summary>
        /// Проверка ввода во всех полях
        /// </summary>
        /// <returns></returns>
        public bool ValidateInput()
        {
            return valInitial.IsValid && valSpeed.IsValid 
                && valTime.IsValid && valAccel.IsValid;
        }
    }
    
}

