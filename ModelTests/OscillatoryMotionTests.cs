using Model;

namespace ModelTests
{
    /// <summary>
    /// Класс для тестирования класса OscillatoryMotion
    /// </summary>
    [TestFixture]
    public class OscillatoryMotionTests : MotionTestsBase<OscillatoryMotion>
    {
        /// <summary>
        /// Создание экземпляра движения с заданными параметрами
        /// </summary>
        /// <param name="initialPosition">Начальная координата</param>
        /// <param name="time">Время</param>
        /// <param name="speed">Скорость</param>
        /// <param name="frequency">Частота</param>
        /// <returns>Экземпляр класса</returns>
        protected override OscillatoryMotion CreateMotion(
            double initialPosition, double time,
            double speed, double frequency)
                => new OscillatoryMotion(frequency,
                    initialPosition, time, speed);

        /// <summary>
        /// Создание экземпляра движения через конструктор по умолчанию
        /// </summary>
        /// <returns>Экземпляр с типом движения "Колебательное"</returns>
        protected override OscillatoryMotion CreateDefaultMotion()
            => new OscillatoryMotion();

        /// <summary>
        /// Ожидаемое значение свойства Name для данного типа движения
        /// </summary>
        protected override string ExpectedName => "Колебательное движение";

        /// <summary>
        /// Проверка значения свойства Frequency
        /// </summary>
        /// <param name="motion">Тип движения</param>
        /// <param name="frequency">Частота</param>
        protected override void AssertSpecificProperty(
            OscillatoryMotion motion, double frequency)
                => Assert.That(motion.Frequency, Is.EqualTo(frequency));

        /// <summary>
        /// Проверка, что GetInfo содержит информацию о частоте
        /// </summary>
        /// <param name="motion">Тип движения</param>
        protected override void AssertGetInfoContainsSpecific(
            OscillatoryMotion motion)
        {
            var info = motion.GetInfo();
            Assert.That(info, Does.Contain(
                $"Частота ω={motion.Frequency} рад/с"));
        }

        /// <summary>
        /// Проверка, что недопустимые значения 
        /// частоты выбрасывают исключение
        /// </summary>
        /// <param name="motion">Тип движения</param>
        protected override void AssertInvalidSpecificProperty(
            OscillatoryMotion motion)
        {
            Assert.Throws<IncorrectArgumentException>(()
                => motion.Frequency = -1);
            Assert.Throws<IncorrectArgumentException>(()
                => motion.Frequency = double.NaN);
            Assert.Throws<IncorrectArgumentException>(()
                => motion.Frequency = double.PositiveInfinity);
        }

        /// <summary>
        /// Проверка корректности расчёта GetPosition с известными значениями
        /// </summary>
        /// <param name="motion">Тип движения</param>
        /// <param name="expected">Значение</param>
        protected override void AssertGetPositionCalculatesCorrectly(
            OscillatoryMotion motion, double expected)
                => Assert.That(motion.GetPosition(),
                    Is.EqualTo(expected).Within(1e-10));

        /// <summary>
        /// Расчет координаты для колебательного движения
        /// </summary>
        /// <param name="motion">Тип движения</param>
        /// <returns>Рассчитанная координата</returns>
        protected override double ComputeExpectedPositionForKnownValues(
            OscillatoryMotion motion)
        {
            // x0=10, t=0.5, v=2, ω=π
            return 10 + (Math.PI / 2) * Math.Sin(Math.PI * 0.5);
        }
    }
}