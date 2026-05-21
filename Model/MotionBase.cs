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
        public double InitialPosition { get; set; }

        /// <summary>
        /// Свойство для времени
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Свойство для скорости
        /// </summary>
        public double Speed { get; set; }

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
        /// Валидация введенных данных
        /// </summary>
        /// <returns></returns>
        public virtual string ValidateParameters()
        {
            //TODO: validation
            if (double.IsNaN(InitialPosition) || 
                double.IsInfinity(InitialPosition))
            {
                return $"Начальная координата содержит некорректное" +
                    $" значение: {InitialPosition}";
            }

            if (double.IsNaN(Time) || double.IsInfinity(Time))
            {
                return $"Время содержит некорректное значение: {Time}";
            }

            if (double.IsNaN(Speed) || double.IsInfinity(Speed))
            {
                return $"Скорость содержит некорректное значение: {Speed}";
            }

            if (Time < 0)
            {
                return $"Время не может быть отрицательным: {Time}";
            }

            if (Speed < 0)
            {
                return $"Скорость не может быть отрицательной: {Speed}";
            }

            return string.Empty;
        }
    }
}

 