namespace Model
{
    /// <summary>
    /// Класс для равноускоренного движения
    /// </summary>
    public class UniformlyAcceleratedMotion : MotionBase
    {
        /// <summary>
        /// Название типа движения
        /// </summary>
        public override string Name => "Равноускоренное движение";

        /// <summary>
        /// Рассчитанная координата
        /// </summary>
        public override double Coordinate => GetPosition();

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public UniformlyAcceleratedMotion() : this(1, 1, 1, 1) { }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="acceleration">ускорение</param>
        /// <param name="initialPosition">начальная координата</param>
        /// <param name="time">время</param>
        /// <param name="speed">скорость</param>
        public UniformlyAcceleratedMotion(double acceleration,
            double initialPosition, double time, double speed)
            : base(initialPosition, time, speed)
        {
            Acceleration = acceleration;
        }

        /// <summary>
        /// Свойство для ускорения
        /// </summary>
        public double Acceleration{ get; set; }

        /// <summary>
        /// Метод для вывода информации о ускорении
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return $"{base.GetInfo()}\nУскорение a={Acceleration} м/с²";
        }

        /// <summary>
        /// Метод расчета координаты в равноускоренном движении
        /// </summary>
        /// <returns></returns>
        public override double GetPosition()
        {
            return InitialPosition + Speed * Time + 0.5 *
                Acceleration * Time * Time;
        }

        /// <summary>
        /// Валидация для равноускоренного движения
        /// </summary>
        /// <returns>Сообщение об ошибке</returns>
        public override string ValidateParameters()
        {
            var baseError = base.ValidateParameters();
            if (!string.IsNullOrEmpty(baseError))
            {
                return baseError;
            }
            if (double.IsNaN(Acceleration) || 
                double.IsInfinity(Acceleration))
            {
                return $"Ускорение содержит некорректное значение:" +
                    $" {Acceleration}";
            }

            return string.Empty;
        }
    }
}
