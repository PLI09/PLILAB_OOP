using Model;
using NUnit.Framework;
using System.Globalization;
using static ModelTests.MotionBaseTests;

namespace ModelTests;

[TestFixture]
public class UniformlyAcceleratedMotionTests : MotionBaseTests
{
    private static readonly CultureInfo _testCulture = new CultureInfo("ru-RU");
    private void SetTestCulture() => Thread.CurrentThread.CurrentCulture = _testCulture;

    /// <summary>
    /// Универсальная проверка валидации ускорения
    /// </summary>
    private static void AssertAccelerationValidation(
        UniformlyAcceleratedMotion motion, string expectedErrorFragment, string description)
    {
        var result = motion.ValidateParameters();
        Assert.That(result, Does.Contain(expectedErrorFragment),
            $"Ошибка не найдена: {description}");
    }

    [TestCase(TestName = "Проверка расчета с нулевым ускорением")]
    public void GetPosition_ZeroAcceleration_EqualsUniformMotion()
    {
        SetTestCulture();
        const double acceleration = 0, x0 = 10, t = 5, v = 3;

        var accelerated = new UniformlyAcceleratedMotion(acceleration, x0, t, v);
        var uniform = new UniformMotion(v, x0, t);

        Assert.That(accelerated.GetPosition(), Is.EqualTo(uniform.GetPosition()).Within(1e-10));
    }

    [TestCase(TestName = "Проверка расчета с отрицательным ускорением")]
    public void GetPosition_NegativeAcceleration_Decelerates()
    {
        SetTestCulture();
        const double acceleration = -2, x0 = 0, t = 3, v = 10;
        const double expected = 21;

        var motion = new UniformlyAcceleratedMotion(acceleration, x0, t, v);
        Assert.That(motion.GetPosition(), Is.EqualTo(expected).Within(1e-10));
    }

    [TestCase(TestName = "Проверка свойства Acceleration")]
    public void Acceleration_Property_GetSet()
    {
        SetTestCulture();
        const double testAcceleration = 9.8;

        var motion = new UniformlyAcceleratedMotion();
        motion.Acceleration = testAcceleration;
        Assert.That(motion.Acceleration, Is.EqualTo(testAcceleration));
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
        [TestCase(1, TestName = "Проверка конструктора " +
            "UniformlyAcceleratedMotion с параметрами при " +
            "присваиваении значений")]
        public void DefaultConstructor_SetsDefaultValues(int motionNumber)
        {
            const int initialPosition = 100;
            const int time = 5;
            const int speed = 8;
            const int acceleration = 1;

            var motion = (UniformlyAcceleratedMotion)MotionFactory.GetMotion(
                motionNumber, initialPosition, time, speed, acceleration);

            Assert.That(motion.Acceleration, Is.EqualTo(1));
        }

    [TestCase(TestName = "Проверка GetInfo содержит всю информацию")]
    public void GetInfo_ContainsAllInfo()
    {
        SetTestCulture();
        const double acceleration = 2, x0 = 10, t = 5, v = 3;

        var motion = new UniformlyAcceleratedMotion(acceleration, x0, t, v);
        var info = motion.GetInfo();

        Assert.That(info, Does.Contain($"Начальная координата Xo={x0} м"));
        Assert.That(info, Does.Contain($"Скорость V={v} м/с"));
        Assert.That(info, Does.Contain($"Время t={t} c"));
        Assert.That(info, Does.Contain("Ускорение"));
    }

    [TestCase(TestName = "Проверка свойства Name")]
    public void Name_ReturnsCorrectValue()
    {
        SetTestCulture();
        const string expectedName = "Равноускоренное движение";

        var motion = new UniformlyAcceleratedMotion();
        Assert.That(motion.Name, Is.EqualTo(expectedName));
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

    [TestCase(TestName = "Проверка, что Coordinate использует GetPosition")]
    public void Coordinate_Property_UsesGetPosition()
    {
        SetTestCulture();
        const double acceleration = 3, x0 = 7, t = 2, v = 4;
        const double expected = 7 + 4 * 2 + 0.5 * 3 * 4;

        var motion = new UniformlyAcceleratedMotion(acceleration, x0, t, v);
        Assert.That(motion.Coordinate, Is.EqualTo(expected).Within(1e-10));
        Assert.That(motion.Coordinate, Is.EqualTo(motion.GetPosition()));
    }
}