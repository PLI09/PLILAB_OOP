using Model;

namespace ModelTests
{
    /// <summary>
    /// Класс для тестирования класса UniformMotion
    /// </summary>
    [TestFixture]
    public class UniformMotionTests : MotionTestsBase<UniformMotion>
    {
        /// <summary>
        /// Создание экземпляра движения с заданными параметрами
        /// </summary>
        /// <param name="initialPosition">Начальная координата</param>
        /// <param name="time">Время</param>
        /// <param name="speed">Скорость</param>
        /// <param name="specificParam">Специфичный параметр</param>
        /// <returns>Экземпляр класса UniformMotion</returns>
        protected override UniformMotion CreateMotion(
            double initialPosition, double time,
            double speed, double specificParam)
                => new UniformMotion(initialPosition, time, speed);

        /// <summary>
        /// Создание экземпляра движения через конструктор по умолчанию
        /// </summary>
        /// <returns>Экземпляр с типом движения "Равномерное"</returns>
        protected override UniformMotion CreateDefaultMotion()
            => new UniformMotion();

        /// <summary>
        /// Ожидаемое значение свойства Name для данного типа движения
        /// </summary>
        protected override string ExpectedName => "Равномерное движение";

        /// <summary>
        /// Проверка значения свойства для специфичного параметра
        /// </summary>
        /// <param name="motion">Тип движения</param>
        /// <param name="expectedValue">Специфический параметр</param>
        protected override void AssertSpecificProperty(
            UniformMotion motion, double expectedValue)
        {
            Assert.Pass();
        }

        /// <summary>
        /// Проверка, что GetInfo не содержит 
        /// информацию об ускорении или частоте
        /// </summary>
        /// <param name="motion">Тип движения</param>
        protected override void AssertGetInfoContainsSpecific(
            UniformMotion motion)
        {
            var info = motion.GetInfo();
            Assert.That(info,
                Does.Not.Contain("Ускорение").And.Not.Contain("Частота"));
        }

        /// <summary>
        /// Реализация проверки, что недопустимые значения 
        /// специфических параметров выбрасывают исключение
        /// </summary>
        /// <param name="motion">Тип движения</param>
        protected override void AssertInvalidSpecificProperty(
            UniformMotion motion)
        {
            Assert.Pass();
        }

        /// <summary>
        /// Проверка корректности расчёта GetPosition с известными значениями
        /// </summary>
        /// <param name="motion">Тип движения</param>
        /// <param name="expected">Значенеи</param>
        protected override void AssertGetPositionCalculatesCorrectly(
            UniformMotion motion, double expected)
                => Assert.That(motion.GetPosition(),
                    Is.EqualTo(expected).Within(1e-10));
    }
}