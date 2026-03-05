namespace Model
{
    /// <summary>
    /// Базовый класс для всех видов движений
    /// </summary>
    public abstract class MotionBase : IMotionable
    {
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
        protected MotionBase(double initialPosition, 
            double time, double speed) 
        {
            InitialPosition=initialPosition;
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
                //TODO: duplication
                if (double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new IncorrectArgumentException("Начальная " +
                        "координата должна быть конечным числом");
                }
                _initialPosition = value;
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
                //TODO: duplication
                if (value < 0) 
                {
                    throw new IncorrectArgumentException
                        ("Время не может быть отрицательным");
                }
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
                //TODO: duplication
                if (double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new IncorrectArgumentException
                        ("Скорость должна быть конечным числом");
                }
                //TODO: duplication
                if (value < 0)
                {
                    throw new IncorrectArgumentException(
                        "Скорость не может быть отрицательной");
                }
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
