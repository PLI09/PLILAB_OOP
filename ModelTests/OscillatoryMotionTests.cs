using Model;
using System.Globalization;

namespace ModelTests;

/// <summary>
/// Дополнительные тесты для OscillatoryMotion
/// </summary>
[TestFixture]
public class OscillatoryMotionTests
{
    /// <summary>
    /// Устанавливает культуру ru-RU для текущего потока.
    /// Должен вызываться в начале каждого теста, зависящего от формата
    /// </summary>
    private void SetTestCulture() =>
        Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

    //TODO: refactor
    /// <summary>
    /// Тестовое значение частоты (Гц)
    /// </summary>
    private const double TestFrequency = 100;

    /// <summary>
    /// Проверка свойства Frequency
    /// </summary>
    [TestCase(TestName = "Проверка частоты")]
    public void Frequency_Property_GetSet()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion();
        motion.Frequency = TestFrequency;
        Assert.That(motion.Frequency, Is.EqualTo(TestFrequency));
    }

    /// <summary>
    /// Проверка валидации с Infinity и NaN в частоте
    /// </summary>
    /// <param name="infinityOrNaN">Infiniry или NaN</param>
    [TestCase(double.NaN,
        TestName = "Проверка частоты на Nan")]
    [TestCase(double.PositiveInfinity,
        TestName = "Проверка частоты на Infinity")]
    public void ValidateParameters_ReturnsError(double infinityOrNaN)
    {
        var motion = new OscillatoryMotion(
            infinityOrNaN, 0, 1, 1);
        var result = motion.ValidateParameters();
        Assert.That(result, Does.Contain(
            "Частота содержит некорректное значение"));
    }

    /// <summary>
    /// Проверка Coordinate свойства
    /// </summary>
    [TestCase(TestName = "Проверка расчета координаты")]
    public void Coordinate_ReturnsGetPositionResult()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(10, 5, 2, 3);
        Assert.That(motion.Coordinate,
            Is.EqualTo(motion.GetPosition()));
    }

    /// <summary>
    /// Проверка конструктора по умолчанию
    /// </summary>
    [TestCase(TestName = "Конструктор класса")]
    public void DefaultConstructor_SetsDefaultValues()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion();
        Assert.That(motion.InitialPosition, Is.EqualTo(1));
        Assert.That(motion.Time, Is.EqualTo(1));
        Assert.That(motion.Speed, Is.EqualTo(1));
        Assert.That(motion.Frequency, Is.EqualTo(1));
    }

    /// <summary>
    /// Проверка расчета при нулевом времени
    /// </summary>
    [TestCase(TestName = "Проверка расчета при нулевом времени")]
    public void GetPosition_ZeroTime_ReturnsInitialPosition()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(50, 10, 0, 5);
        Assert.That(motion.GetPosition(),
            Is.EqualTo(10).Within(1e-10));
    }

    /// <summary>
    /// Проверка GetInfo содержит информацию о частоте
    /// </summary>
    [TestCase(TestName = "Проверка наличия информации о частоте")]
    public void GetInfo_ContainsFrequencyInfo()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(50, 0, 1, 10);
        var info = motion.GetInfo();
        Assert.That(info, Does.Contain("Частота"));
        Assert.That(info, Does.Contain("рад/с"));
    }
    /// <summary>
    /// Проверка свойства Name
    /// </summary>
    [TestCase(TestName = "Проверка названия движения")]
    public void Name_ReturnsCorrectValue()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion();
        Assert.That(motion.Name, Is.EqualTo("Колебательное движение"));
    }

    /// <summary>
    /// Проверка валидации с отрицательной частотой
    /// </summary>
    [TestCase(TestName = "Проверка расчета с отрицательной частотой")]
    public void ValidateParameters_NegativeFrequency_ReturnsError()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(-5, 0, 1, 1);
        var result = motion.ValidateParameters();
        Assert.That(result, Does.Contain
            ("Частота не может быть отрицательной"));
        Assert.That(result, Does.Contain("-5"));
    }

    /// <summary>
    /// Проверка валидации с NegativeInfinity в частоте
    /// </summary>
    [TestCase(TestName = "Проверка частоты на минус бесконечность")]
    public void ValidateParameters_NegativeInfinityFrequency_ReturnsError()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(
            double.NegativeInfinity, 0, 1, 1);
        var result = motion.ValidateParameters();
        Assert.That(result, Does.Contain(
            "Частота содержит некорректное значение"));
    }

    /// <summary>
    /// Проверка успешной валидации корректных параметров
    /// </summary>
    [TestCase(TestName = "Проверка на корректные параметры")]
    public void ValidateParameters_ValidParameters_ReturnsEmptyString()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(10, 5, 2, 3);
        var result = motion.ValidateParameters();
        Assert.That(result, Is.Empty);
    }

    /// <summary>
    /// Проверка валидации с некорректным временем из базового класса
    /// </summary>
    [TestCase(TestName = "Проверка времени")]
    public void ValidateParameters_InvalidTimeFromBase_ReturnsBaseError()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(1, 0, double.NaN, 1);
        var result = motion.ValidateParameters();
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Does.Not.Contain("Частота"));
    }

    /// <summary>
    /// Проверка валидации с некорректной скоростью из базового класса
    /// </summary>
    [TestCase(TestName = "Проверка скорости")]
    public void ValidateParameters_InvalidSpeedFromBase_ReturnsBaseError()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(1, 0, 1, double.PositiveInfinity);
        var result = motion.ValidateParameters();
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Does.Not.Contain("Частота"));
    }

    /// <summary>
    /// Проверка расчета GetPosition с известными значениями
    /// </summary>
    [TestCase(TestName = "Проверка расчета координаты для" +
        " колебательного движения")]
    public void GetPosition_CalculatesCorrectly_WithKnownValues()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(
            Math.PI, 10, 0.5, 2);
        var expected = 10 + (Math.PI / 2) * Math.Sin(Math.PI * 0.5);
        Assert.That(motion.GetPosition(),
            Is.EqualTo(expected).Within(1e-10));
    }

    /// <summary>
    /// Проверка GetPosition при Frequency = 0
    /// </summary>
    [TestCase(TestName = "Проверка расчета при нулевом значении частоты")]
    public void GetPosition_ZeroFrequency_ReturnsInitialPosition()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(0, 42, 100, 5);
        Assert.That(motion.GetPosition(), Is.EqualTo(42).Within(1e-10));
    }

    /// <summary>
    /// Проверка, что деление на Speed работает корректно
    /// </summary>
    [TestCase(TestName = "Проверка корректности деления на скорость")]
    public void GetPosition_DivisionBySpeed_AffectsResult()
    {
        SetTestCulture();
        var motion1 = new OscillatoryMotion(10, 0, 1, 2); 
        var motion2 = new OscillatoryMotion(10, 0, 1, 5); 
        var position1 = motion1.GetPosition();
        var position2 = motion2.GetPosition();
        Assert.That(position1, 
            Is.Not.EqualTo(position2));
    }

    /// <summary>
    /// Проверка, что GetInfo содержит информацию из базового класса
    /// </summary>
    [TestCase(TestName = "Проверка наличия информации о параметрах" +
        " базового класса")]
    public void GetInfo_ContainsBaseInfo()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(50, 15, 3, 7);
        var info = motion.GetInfo();
        
        Assert.That(info, Does.Contain("Начальная координата"));
        Assert.That(info, Does.Contain("Xo=15"));
        Assert.That(info, Does.Contain("Скорость"));
        Assert.That(info, Does.Contain("Время"));
        Assert.That(info, Does.Contain("Частота ω=50"));
    }

    /// <summary>
    /// Проверка конструктора с параметрами при присваиваении значений
    /// </summary>
    [TestCase(TestName = "Проверка конструктора с параметрами при" +
        " присваиваении значений")]
    public void ParameterConstructor_AssignsValuesCorrectly()
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(25, 100, 5, 8);
        
        Assert.That(motion.Frequency, Is.EqualTo(25));
        Assert.That(motion.InitialPosition, Is.EqualTo(100));
        Assert.That(motion.Time, Is.EqualTo(5));
        Assert.That(motion.Speed, Is.EqualTo(8));
    }
}