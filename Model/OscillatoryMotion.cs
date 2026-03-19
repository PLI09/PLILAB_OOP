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
        /// Частота
        /// </summary>
        private double _frequency;

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
        public double Frequency
        {
            get => _frequency;
            set
            {
                CheckingForNegative(value);
                _frequency = value;
            }
        }

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
            return InitialPosition + (Frequency / Speed) * Math.Sin(Frequency * Time);
        }

        /// <summary>
        /// Метод для возвращения названия колонки "Частота"
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<string> GetExtraColumnNames()
        {
            yield return "Частота (Гц)";
        }

        /// <summary>
        /// Метод для возвращения значения колонки "Частота"
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<double> GetExtraColumnValues()
        {
            yield return Frequency;
        }
    }
}
