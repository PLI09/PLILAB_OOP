using System.Xml.Serialization;
namespace Model
{
    /// <summary>
    /// Базовый класс
    /// </summary>
    [XmlInclude(typeof(UniformMotion))]
    [XmlInclude(typeof(UniformlyAcceleratedMotion))]
    [XmlInclude(typeof(OscillatoryMotion))]
    public abstract class MotionBase
    {
        /// <summary>
        /// Свойство для названия движения
        /// </summary>
        public virtual string Name => "Тип движения";

        /// <summary>
        /// Свойство для расчитанной координаты
        /// </summary>
        public virtual double Coordinate => GetPosition();

        /// <summary>
        /// Ускорение (для равноускоренного движения)
        /// </summary>
        public virtual double Acceleration { get; set; } = 0;

        /// <summary>
        /// Частота (для колебательного движения)
        /// </summary>
        public virtual double Frequency { get; set; } = 0;

        /// <summary>
        /// Начальная координата
        /// </summary>
        private double _initialPosition;

        /// <summary>
        /// Время
        /// </summary>
        private double _time;

        /// <summary>
        /// Скорость
        /// </summary>
        private double _speed;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="initialPosition">начальная координата</param>
        /// <param name="time">время</param>
        /// <param name="speed">скорость</param>
        public MotionBase(double initialPosition,
            double time, double speed)
        {
            InitialPosition = initialPosition;
            Time = time;
            Speed = speed;
        }

        /// <summary>
        /// Cвойство для начальной координаты
        /// </summary>
        public double InitialPosition
        {
            get
            {
                return _initialPosition;
            }
            set
            {
                CheckingForNegative(value);
                _initialPosition = value;
            }
        }

        /// <summary>
        /// Метод для проверки базовых параметров
        /// </summary>
        /// <param name="value">Значение параметра</param>
        /// <exception cref="IncorrectArgumentException">Значение должно 
        /// быть конечным числом</exception>
        protected void CheckingForNegative(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new IncorrectArgumentException("Значение должно " +
                    "быть конечным числом");
            }
        }

        /// <summary>
        /// Свойство для времени
        /// </summary>
        public double Time
        {
            get
            {
                return _time;
            }
            set
            {
                CheckingForNegative(value);
                _time = value;
            }
        }

        /// <summary>
        /// Свойство для проверки скорости
        /// </summary>
        public double Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                CheckingForNegative(value);
                _speed = value;
            }
        }

        /// <summary>
        /// Метод для вывода информации о параметрах движения
        /// </summary>
        /// <returns>Информация о параметрах движения</returns>
        public virtual string GetInfo()
        {
            return $"Начальная координата Xo={InitialPosition} м\n" +
                   $"Скорость V={Speed} м/с\nВремя t={Time} c";
        }

        /// <summary>
        /// Абстрактный метод для расчета координаты движения
        /// </summary>
        /// <returns>Координата движения</returns>
        public abstract double GetPosition();
    }
}
