using Model;
using NUnit.Framework;
using System.Globalization;
using System.Threading;

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
    /// Объединённый тест валидации ускорения на некорректные значения
    /// </summary>
    [TestCase(double.NaN, "NaN", true, TestName = "Проверка ускорения на NaN")]
    [TestCase(double.PositiveInfinity, "+Infinity", false, TestName = "Проверка ускорения на +Infinity")]
    [TestCase(double.NegativeInfinity, "-Infinity", false, TestName = "Проверка ускорения на -Infinity")]
    public void ValidateParameters_NonFiniteAcceleration_ReturnsError(
        double acceleration, string description, bool checkValueInMessage)
    {
        SetTestCulture();
        const string expectedError = "Ускорение содержит некорректное значение";

        var motion = new UniformlyAcceleratedMotion(acceleration, 0, 1, 1);
        AssertAccelerationValidation(motion, expectedError, description);

        if (checkValueInMessage)
        {
            Assert.That(motion.ValidateParameters(),
                Does.Contain(acceleration.ToString(CultureInfo.CurrentCulture)),
                $"Сообщение должно содержать '{acceleration}'");
        }
    }

    [TestCase(TestName = "Проверка конструктора по умолчанию")]
    public void DefaultConstructor_SetsDefaultValues()
    {
        SetTestCulture();
        const double defaultAcceleration = 1.0;

        var motion = new UniformlyAcceleratedMotion();
        AssertBasePropertiesHaveDefaults(motion);
        Assert.That(motion.Acceleration, Is.EqualTo(defaultAcceleration));
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
    /// Объединённый тест: некорректные базовые параметры не должны вызывать ошибки ускорения
    /// </summary>
    [TestCase(double.NaN, null, "время", TestName = "Проверка времени")]
    [TestCase(null, double.PositiveInfinity, "скорость", TestName = "Проверка скорости")]
    public void ValidateParameters_InvalidBaseParam_DoesNotAffectAccelerationCheck(
        double? invalidTime, double? invalidSpeed, string paramName)
    {
        SetTestCulture();
        const double acceleration = 1, x0 = 0, defaultValue = 1;
        var t = invalidTime ?? defaultValue;
        var v = invalidSpeed ?? defaultValue;

        var motion = new UniformlyAcceleratedMotion(acceleration, x0, t, v);
        var result = motion.ValidateParameters();

        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Does.Not.Contain("Ускорение"),
            $"Ошибка не должна упоминать ускорение при некорректном {paramName}");
    }

    [TestCase(TestName = "Проверка успешной валидации корректных параметров")]
    public void ValidateParameters_ValidParameters_ReturnsEmptyString()
    {
        SetTestCulture();
        const double acceleration = 9.8, x0 = 0, t = 10, v = 5;

        var motion = new UniformlyAcceleratedMotion(acceleration, x0, t, v);
        Assert.That(motion.ValidateParameters(), Is.Empty);
    }

    [TestCase(TestName = "Проверка GetPosition с нулевым временем")]
    public void GetPosition_ZeroTime_ReturnsInitialPosition()
    {
        SetTestCulture();
        const double acceleration = 100, x0 = 42, t = 0, v = 10;

        var motion = new UniformlyAcceleratedMotion(acceleration, x0, t, v);
        Assert.That(motion.GetPosition(), Is.EqualTo(x0).Within(1e-10));
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