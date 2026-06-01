using System.Xml.Serialization;

namespace Model
{
    [XmlInclude(typeof(UniformMotion))]
    [XmlInclude(typeof(UniformlyAcceleratedMotion))]
    [XmlInclude(typeof(OscillatoryMotion))]

    /// <summary>
    /// Класс с базовыми параметрами движения
    /// </summary>
    public abstract class MotionBase
    {
        /// <summary>
        /// Время
        /// </summary>
        private double _time;

        /// <summary>
        /// Скорость
        /// </summary>
        private double _speed;

        /// <summary>
        /// Начальная координата
        /// </summary>
        private double _initialPosition;

        /// <summary>
        /// Тип движения
        /// </summary>
        public virtual string Name => "Тип движения";

        /// <summary>
        /// Рассчитанная координата
        /// </summary>
        public virtual double Coordinate => GetPosition();

        /// <summary>
        /// Конструктор базового класса
        /// </summary>
        /// <param name="initialPosition">Начальная координата</param>
        /// <param name="time">Время</param>
        /// <param name="speed">Скорость</param>
        protected MotionBase(double initialPosition, 
            double time, double speed)
        {
            InitialPosition = initialPosition;
            Time = time;
            Speed = speed;
        }

        /// <summary>
        /// Свойство для начальной координаты
        /// </summary>
        public double InitialPosition 
        {
            get 
            {
                return _initialPosition;
            }
            set 
            {
                ValidateParameters(value);
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
                ValidateParameters(value);
                ValidateNegativeParameters(value);
                _time = value;
            } 
        }

        /// <summary>
        /// Свойство для скорости
        /// </summary>
        public double Speed 
        {
            get 
            { 
                return _speed;
            }
            set 
            {
                ValidateParameters(value);
                ValidateNegativeParameters(value);
                _speed = value;
            }
        }

        /// <summary>
        /// Вывод базовой информации
        /// </summary>
        /// <returns></returns>
        public virtual string GetInfo()
        {
            return $"Начальная координата Xo={InitialPosition} м\n" +
                   $"Скорость V={Speed} м/с\nВремя t={Time} c";
        }

        /// <summary>
        /// Абстрактный метод для расчета координаты
        /// </summary>
        /// <returns></returns>
        public abstract double GetPosition();

        /// <summary>
        /// Проверка параметров на NaN или Infinity
        /// </summary>
        /// <param name="parameter">Параметр, который 
        /// не должен быть NaN или Infinity</param>
        /// <exception cref="IncorrectArgumentException">
        /// Исключение</exception>
        public void ValidateParameters(double parameter)
        {
            if (double.IsNaN(parameter) || double.IsInfinity(parameter))
            {
                throw new IncorrectArgumentException(
                    $"Параметр содержит некорректное значение: {parameter}");
            }
        }

        /// <summary>
        /// Проверка параметров на отрицательное значение
        /// </summary>
        /// <param name="parameter">Параметр, который 
        /// не должен быть отрицательным</param>
        /// <exception cref="IncorrectArgumentException">
        /// Исключение</exception>
        protected void ValidateNegativeParameters(double parameter)
        {
            if (parameter < 0)
            {
                throw new IncorrectArgumentException(
                    $"Данный параметр не может быть отрицательным: " +
                    $"{parameter}");
            }
        }
    }
}

 