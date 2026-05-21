using Model;
using System.Globalization;

namespace ModelTests
{
    /// <summary>
    /// Тесты для UniformlyAcceleratedMotion
    /// </summary>
    [TestFixture]
    public class UniformlyAcceleratedMotionTests
    {
        //TODO: refactor+
        /// <summary>
        /// Культура для тестов, требующих специфичного формата (ru-RU)
        /// </summary>
        private static readonly CultureInfo _testCulture =
            new CultureInfo("ru-RU");

        /// <summary>
        /// Устанавливает культуру ru-RU для текущего потока.
        /// Должен вызываться в начале каждого теста, зависящего от формата
        /// </summary>
        private void SetTestCulture() =>
            Thread.CurrentThread.CurrentCulture = _testCulture;

        /// <summary>
        /// Тестовое значение ускорения (м/с²) 9.8
        /// </summary>
        private const double TestAcceleration = 9.8;

        /// <summary>
        /// Проверка расчета с нулевым ускорением
        /// </summary>
        [TestCase(TestName = "Проверка расчета с нулевым ускорением")]
        public void GetPosition_ZeroAcceleration_EqualsUniformMotion()
        {
            SetTestCulture();
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
            SetTestCulture();
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
            SetTestCulture();
            var motion = new UniformlyAcceleratedMotion();
            motion.Acceleration = TestAcceleration;
            Assert.That(motion.Acceleration, Is.EqualTo(TestAcceleration));
        }

        /// <summary>
        /// Проверка валидации с некорректными числовыми 
        /// значениями в ускорении
        /// </summary>
        /// <param name="acceleration">Тестовое значение ускорения</param>
        /// <param name="description">Описание случая для отчёта</param>
        /// <param name="checkValueInMessage">
        /// Если true — дополнительно проверяет, что сообщение содержит 
        /// строковое представление значения
        /// </param>
        [TestCase(double.NaN, "NaN", true,
            TestName = "Проверка валидации с NaN в ускорении")]
        [TestCase(double.PositiveInfinity, "+Infinity", false,
            TestName = "Проверка валидации с +Infinity в ускорении")]
        [TestCase(double.NegativeInfinity, "-Infinity", false,
            TestName = "Проверка валидации с -Infinity в ускорении")]
        public void ValidateParameters_NonFiniteAcceleration_ReturnsError(
            double acceleration,
            string description,
            bool checkValueInMessage = false)
        {
            SetTestCulture();
            var motion = new UniformlyAcceleratedMotion(
                acceleration, 0, 1, 1);
            var result = motion.ValidateParameters();
            Assert.That(result, Does.Contain(
                "Ускорение содержит некорректное значение"),
                $"Ошибка не найдена для случая: {description}");
            if (checkValueInMessage)
            {
                Assert.That(result, Does.Contain(
                    acceleration.ToString(CultureInfo.CurrentCulture)),
                    $"Сообщение должно содержать '{acceleration}' " +
                    $"для случая: {description}");
            }
        }

        /// <summary>
        /// Проверка Coordinate свойства
        /// </summary>
        [TestCase(TestName = "Проверка Coordinate свойства")]
        public void Coordinate_ReturnsGetPositionResult()
        {
            SetTestCulture();
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
            SetTestCulture();
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
            SetTestCulture();
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
            SetTestCulture();
            var motion = new UniformlyAcceleratedMotion();
            Assert.That(motion.Name, Is.EqualTo("Равноускоренное движение"));
        }

        /// <summary>
        /// Проверка валидации с некорректным временем из базового класса
        /// </summary>
        [TestCase(TestName = "Проверка валидации с некорректным" +
            " временем из базового класса")]
        public void ValidateParameters_InvalidTime_ReturnsBaseError()
        {
            SetTestCulture();
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
            SetTestCulture();
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
            SetTestCulture();
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
            SetTestCulture();
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
            SetTestCulture();
            var motion = new UniformlyAcceleratedMotion(3, 7, 2, 4);
            var expected = 7 + 4 * 2 + 0.5 * 3 * 4;
            Assert.That(motion.Coordinate, Is.EqualTo(21).Within(1e-10));
            Assert.That(motion.Coordinate, Is.EqualTo(motion.GetPosition()));
        }
    }
}