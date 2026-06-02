using Model;

namespace ModelTests
{
    /// <summary>
    /// Класс для тестирования класса UniformlyAcceleratedMotion
    /// </summary>
    [TestFixture]
    public class UniformlyAcceleratedMotionTests :
        MotionTestsBase<UniformlyAcceleratedMotion>
    {
        /// <summary>
        /// Создание экземпляра движения с заданными параметрами
        /// </summary>
        /// <param name="acceleration">Ускорение</param>
        /// <param name="initialPosition">Начальная координата</param>
        /// <param name="time">Время</param>
        /// <param name="speed">Скорость</param>
        /// <returns>Экземпляр класса UniformlyAcceleratedMotion</returns>
        protected override UniformlyAcceleratedMotion CreateMotion(
            double initialPosition, double time,
            double speed, double acceleration)
                => new UniformlyAcceleratedMotion(
                    acceleration, initialPosition, time, speed);

        /// <summary>
        /// Создание экземпляра движения через конструктор по умолчанию
        /// </summary>
        /// <returns>Экземпляр с типом движения "Равноускоренное"</returns>
        protected override UniformlyAcceleratedMotion CreateDefaultMotion()
            => new UniformlyAcceleratedMotion();

        /// <summary>
        /// Ожидаемое значение свойства Name для данного типа движения
        /// </summary>
        protected override string ExpectedName => "Равноускоренное движение";

        /// <summary>
        /// Проверка значения свойства Acceleration
        /// </summary>
        /// <param name="motion">Тип движения</param>
        /// <param name="acceleration">Ускорение</param>
        protected override void AssertSpecificProperty(
            UniformlyAcceleratedMotion motion, double acceleration)
                => Assert.That(motion.Acceleration,
                    Is.EqualTo(acceleration));

        /// <summary>
        /// Проверка, что GetInfo содержит информацию об ускорении
        /// </summary>
        /// <param name="motion">Тип движения</param>
        protected override void AssertGetInfoContainsSpecific(
            UniformlyAcceleratedMotion motion)
        {
            var info = motion.GetInfo();
            Assert.That(info, Does.Contain(
                $"Ускорение a={motion.Acceleration} м/с²"));
        }

        /// <summary>
        /// Проверка, что недопустимые значения 
        /// ускорения выбрасывают исключение
        /// </summary>
        /// <param name="motion">Тип движения</param>
        protected override void AssertInvalidSpecificProperty(
            UniformlyAcceleratedMotion motion)
        {
            Assert.Throws<IncorrectArgumentException>(()
                => motion.Acceleration = double.NaN);
            Assert.Throws<IncorrectArgumentException>(()
                => motion.Acceleration = double.PositiveInfinity);
            Assert.DoesNotThrow(()
                => motion.Acceleration = -5);
        }

        /// <summary>
        /// Проверка корректности расчёта GetPosition с известными значениями
        /// </summary>
        /// <param name="motion">Тип движения</param>
        /// <param name="expected">Значение</param>
        protected override void AssertGetPositionCalculatesCorrectly(
            UniformlyAcceleratedMotion motion, double expected)
                => Assert.That(motion.GetPosition(),
                    Is.EqualTo(expected).Within(1e-10));

        /// <summary>
        /// Расчет координаты для равноускоренного движения
        /// </summary>
        /// <param name="motion"></param>
        /// <returns>Рассчитанная координата</returns>
        protected override double ComputeExpectedPositionForKnownValues(
            UniformlyAcceleratedMotion motion)
        {
            // Для a=π, x0=10, t=0.5, v=2
            return 10 + 2 * 0.5 + 0.5 * Math.PI * 0.25;
        }
    }
}