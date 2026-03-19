namespace Model
{
    /// <summary>
    /// Класс для равноускоренного дивжения
    /// </summary>
    public class UniformlyAcceleratedMotion : MotionBase
    {
        /// <summary>
        /// Название движения
        /// </summary>
        public override string Name => "Равноускренное движение";

        /// <summary>
        /// Рассчитаная координата
        /// </summary>
        public override double Coordinate => GetPosition();

        /// <summary>
        /// Ускорение
        /// </summary>
        private double _acceleration;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public UniformlyAcceleratedMotion() : this(1, 1, 1, 1) { }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="acceleration"></param>
        /// <param name="initialPosition"></param>
        /// <param name="time"></param>
        /// <param name="speed"></param>
        public UniformlyAcceleratedMotion(double acceleration,
            double initialPosition, double time, double speed)
            : base(initialPosition, time, speed)
        {
            Acceleration = acceleration;
        }

        /// <summary>
        /// Свойство для проверки ускорения
        /// </summary>
        public override double Acceleration
        {
            get
            {
                return _acceleration;
            }
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new ArgumentException
                        ("Значение должно быть конечным числом");
                }
                _acceleration = value;
            }
        }

        /// <summary>
        /// Метод для вывода информации о параметрах
        /// </summary>
        /// <returns>Информация о параметрах</returns>
        public override string GetInfo()
        {
            return $"{base.GetInfo()}\nУскорение a={Acceleration} м/с^2";
        }

        /// <summary>
        /// Метод для расчета координаты при равноускоренном движении
        /// </summary>
        /// <returns>координата</returns>
        public override double GetPosition()
        {
            return InitialPosition + Speed * Time +
                0.5 * Acceleration * Time * Time;
        }
    }
}
