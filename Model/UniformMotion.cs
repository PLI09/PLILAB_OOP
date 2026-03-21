namespace Model
{
    /// <summary>
    /// Класс равномерное движение
    /// </summary>
    public class UniformMotion : MotionBase
    {
        /// <summary>
        /// Название движения
        /// </summary>
        public override string Name => "Равномерное движение";

        /// <summary>
        /// Рассчитаная координата
        /// </summary>
        public override double Coordinate => GetPosition();

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public UniformMotion() : this(1, 1, 1) { }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="initialPosition"></param>
        /// <param name="time"></param>
        public UniformMotion(double speed, double initialPosition,
            double time) : base(initialPosition, time, speed) { }

        /// <summary>
        /// Метод для вычисления координаты для равномерного движения
        /// </summary>
        /// <returns>Координата для равномерного движения</returns>
        public override double GetPosition()
        {
            return InitialPosition + Speed * Time;
        }
    }
}
