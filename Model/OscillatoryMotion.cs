namespace Model
{
    /// <summary>
    /// Класс для колебательного движения
    /// </summary>
    public class OscillatoryMotion : MotionBase
    {
        /// <summary>
        /// Название движения
        /// </summary>
        public override string Name => "Колебательное движение";
        
        /// <summary>
        /// Расчитанная координата
        /// </summary>
        public override double Coordinate => GetPosition();

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public OscillatoryMotion() : this(1, 1, 1, 1) { }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="frequency">частота</param>
        /// <param name="initialPosition">начальная координата</param>
        /// <param name="time">время</param>
        /// <param name="speed">скорость</param>
        public OscillatoryMotion(double frequency,
            double initialPosition, double time, double speed)
            : base(initialPosition, time, speed)
        {
            Frequency = frequency;
        }

        /// <summary>
        /// Свойство для частоты
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// Метод для вывод информации о частоте
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return $"{base.GetInfo()}\nЧастота ω={Frequency} рад/с";
        }

        /// <summary>
        /// Расчет координаты для колебательного движения
        /// </summary>
        /// <returns></returns>
        public override double GetPosition()
        {
            return InitialPosition + (Frequency / Speed) * 
                Math.Sin(Frequency * Time);
        }

        /// <summary>
        /// Валидация для колебательного движения
        /// </summary>
        /// <returns>Сообщение об ошибке</returns>
        public override string ValidateParameters()
        {
            var baseError = base.ValidateParameters();
            if (!string.IsNullOrEmpty(baseError))
            {
                return baseError;
            }

            if (double.IsNaN(Frequency) || double.IsInfinity(Frequency)) 
            {
                return $"Частота содержит некорректное значение: " +
                    $"{Frequency}";
            }

            if (Frequency < 0) 
            {
                return $"Частота не может быть отрицательной: {Frequency}";
            }
            return string.Empty;
        }
    }
}
