namespace Model
{
    /// <summary>
    /// Класс для коллебательного движения
    /// </summary>
    public class OscillatoryMotion : MotionBase
    {
        /// <summary>
        /// Частота
        /// </summary>
        private double _frequency;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public OscillatoryMotion() : this (1, 1, 1, 1) { }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="frequency">частота</param>
        /// <param name="initialPosition">начальная координата</param>
        /// <param name="time">время</param>
        /// <param name="speed">скорость</param>
        protected OscillatoryMotion(double frequency, 
            double initialPosition, double time, double speed)
            : base(initialPosition, time, speed)
        {
            Frequency = frequency;
        }

        /// <summary>
        /// Cвойство для проверки корректности частоты
        /// </summary>
        public double Frequency
        {
            get
            {
                return _frequency;
            }
            set
            {
                //TODO: duplication+
                //TODO: duplication+
                CheckingForNegative(value);
                _frequency = value;
            }
        }

        /// <summary>
        /// Метод для вывода информации о параметрах движения
        /// </summary>
        /// <returns>Пареметры движения</returns>
        public override string GetInfo()
        {
            return $"{base.GetInfo()}\nЧастота w={Frequency} рад/c";
        }

        /// <summary>
        /// Метод для расчета координаты для колебательного движения
        /// </summary>
        /// <returns>координата для колебательного движения</returns>
        public override double GetPosition()
        {
            return InitialPosition + (Frequency/Speed) * 
                Math.Sin(Frequency * Time);
        }
    }
}
