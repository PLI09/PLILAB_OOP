using Model;
using System.Globalization;

namespace ModelTests
{
    /// <summary>
    /// Дополнительные тесты для UniformlyAcceleratedMotion
    /// </summary>
    [TestFixture]
    public class UniformlyAcceleratedMotionTests
    {
        //TODO: refactor
        [SetUp]
        public void SetUp()
        {
            Thread.CurrentThread.CurrentCulture =
                new CultureInfo("ru-RU");
        }

        /// <summary>
        /// Проверка расчета с нулевым ускорением
        /// </summary>
        [TestCase(TestName = "Проверка расчета с нулевым ускорением")]
        public void GetPosition_ZeroAcceleration_EqualsUniformMotion()
        {
            var accelerated = new UniformlyAcceleratedMotion(
                0, 10, 5, 3);
            var uniform = new UniformMotion(3, 10, 5);
            Assert.That(accelerated.GetPosition(),
                Is.EqualTo(uniform.GetPosition()).Within(1e-10));
        }

        /// <summary>
        /// Проверка расчета с отрицательным ускорением
        /// </summary>
        [TestCase(TestName = "Проверка расчета с отрицательным ускорением")]
        public void GetPosition_NegativeAcceleration_Decelerates()
        {
            var motion = new UniformlyAcceleratedMotion(
                -2, 0, 3, 10);
            var position = motion.GetPosition();
            Assert.That(position, Is.EqualTo(21).Within(1e-10));
        }

        /// <summary>
        /// Проверка свойства Acceleration
        /// </summary>
        [TestCase(TestName = "Проверка свойства Acceleration")]
        public void Acceleration_Property_GetSet()
        {
            var motion = new UniformlyAcceleratedMotion();
            motion.Acceleration = 9.8;
            Assert.That(motion.Acceleration, Is.EqualTo(9.8));
        }

        /// <summary>
        /// Проверка валидации с Infinity в ускорении
        /// </summary>
        [TestCase(TestName = "Проверка валидации с Infinity в ускорении")]
        public void ValidateParameters_InfinityAcceleration_ReturnsError()
        {
            var motion = new UniformlyAcceleratedMotion(
                double.PositiveInfinity, 0, 1, 1);
            var result = motion.ValidateParameters();
            Assert.That(result, Does.Contain(
                "Ускорение содержит некорректное значение"));
        }

        /// <summary>
        /// Проверка валидации с NegativeInfinity в ускорении
        /// </summary>
        [TestCase(TestName = "Проверка валидации с NegativeInfinity" +
            " в ускорении")]
        public void ValidateParameters_NegInfinityAcceleration_ReturnsError()
        {
            var motion = new UniformlyAcceleratedMotion(
                double.NegativeInfinity, 0, 1, 1);
            var result = motion.ValidateParameters();
            Assert.That(result, Does.Contain(
                "Ускорение содержит некорректное значение"));
        }

        /// <summary>
        /// Проверка Coordinate свойства
        /// </summary>
        [TestCase(TestName = "Проверка Coordinate свойства")]
        public void Coordinate_ReturnsGetPositionResult()
        {
            var motion = new UniformlyAcceleratedMotion(
                2, 5, 3, 4);
            Assert.That(motion.Coordinate,
                Is.EqualTo(motion.GetPosition()));
        }

        /// <summary>
        /// Проверка конструктора по умолчанию
        /// </summary>
        [TestCase(TestName = "Проверка конструктора по умолчанию")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            var motion = new UniformlyAcceleratedMotion();
            Assert.That(motion.InitialPosition, Is.EqualTo(1));
            Assert.That(motion.Time, Is.EqualTo(1));
            Assert.That(motion.Speed, Is.EqualTo(1));
            Assert.That(motion.Acceleration, Is.EqualTo(1));
        }

        /// <summary>
        /// Проверка GetInfo содержит всю базовую информацию
        /// </summary>
        [TestCase(TestName = "Проверка GetInfo содержит всю" +
            " базовую информацию")]
        public void GetInfo_ContainsAllInfo()
        {
            var motion = new UniformlyAcceleratedMotion(
                2, 10, 5, 3);
            var info = motion.GetInfo();
            Assert.That(info, Does.Contain(
                "Начальная координата Xo=10 м"));
            Assert.That(info, Does.Contain("Скорость V=3 м/с"));
            Assert.That(info, Does.Contain("Время t=5 c"));
            Assert.That(info, Does.Contain("Ускорение"));
        }

        /// <summary>
        /// Проверка свойства Name
        /// </summary>
        [TestCase(TestName = "Проверка свойства Name")]
        public void Name_ReturnsCorrectValue()
        {
            var motion = new UniformlyAcceleratedMotion();
            Assert.That(motion.Name, Is.EqualTo("Равноускоренное движение"));
        }

        /// <summary>
        /// Проверка валидации с NaN в ускорении
        /// </summary>
        [TestCase(TestName = "Проверка валидации с NaN в ускорении")]
        public void ValidateParameters_NaNAcceleration_ReturnsError()
        {
            var motion = new UniformlyAcceleratedMotion(
                double.NaN, 0, 1, 1);
            var result = motion.ValidateParameters();
            Assert.That(result, Does.Contain(
                "Ускорение содержит некорректное значение"));
            Assert.That(result, Does.Contain
                (double.NaN.ToString(CultureInfo.CurrentCulture)));
        }

        /// <summary>
        /// Проверка валидации с некорректным временем из базового класса
        /// </summary>
        [TestCase(TestName = "Проверка валидации с некорректным" +
            " временем из базового класса")]
        public void ValidateParameters_InvalidTime_ReturnsBaseError()
        {
            var motion = new UniformlyAcceleratedMotion(
                1, 0, double.NaN, 1);
            var result = motion.ValidateParameters();
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Does.Not.Contain("Ускорение"));
        }

        /// <summary>
        /// Проверка валидации с некорректной скоростью из базового класса
        /// </summary>
        [TestCase(TestName = "Проверка валидации с некорректной скоростью" +
            " из базового класса")]
        public void ValidateParameters_InvalidSpeed_ReturnsBaseError()
        {
            var motion = new UniformlyAcceleratedMotion(
                1, 0, 1, double.PositiveInfinity);
            var result = motion.ValidateParameters();
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Does.Not.Contain("Ускорение"));
        }

        /// <summary>
        /// Проверка успешной валидации корректных параметров
        /// </summary>
        [TestCase(TestName = "Проверка успешной валидации корректных" +
            " параметров")]
        public void ValidateParameters_ValidParameters_ReturnsEmptyString()
        {
            var motion = new UniformlyAcceleratedMotion(
                9.8, 0, 10, 5);
            var result = motion.ValidateParameters();
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// Проверка GetPosition с нулевым временем
        /// </summary>
        [TestCase(TestName = "Проверка GetPosition с нулевым временем")]
        public void GetPosition_ZeroTime_ReturnsInitialPosition()
        {
            var motion = new UniformlyAcceleratedMotion(
                100, 42, 0, 10);
            Assert.That(motion.GetPosition(), Is.EqualTo(42).Within(1e-10));
        }

        /// <summary>
        /// Проверка, что Coordinate использует GetPosition
        /// </summary>
        [TestCase(TestName = "Проверка, что Coordinate " +
            "использует GetPosition")]
        public void Coordinate_Property_UsesGetPosition()
        {
            var motion = new UniformlyAcceleratedMotion(3, 7, 2, 4);
            var expected = 7 + 4 * 2 + 0.5 * 3 * 4;
            Assert.That(motion.Coordinate, Is.EqualTo(21).Within(1e-10));
            Assert.That(motion.Coordinate, Is.EqualTo(motion.GetPosition()));
        }
    }
}