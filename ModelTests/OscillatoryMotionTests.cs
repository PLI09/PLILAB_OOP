using Model;
using System.Globalization;


namespace ModelTests;

/// <summary>
/// Тесты для класса OscillatoryMotion
/// </summary>
[TestFixture]
public class OscillatoryMotionTests : MotionBaseTests
{
   
    /// <summary>
    /// Утанавливает культуру ru-RU для корректного 
    /// форматирования чисел в тестах
    /// </summary>
    private void SetTestCulture() =>
        Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

    /// <summary>
    /// Проверка валидации частоты
    /// </summary>
    /// <param name="motion">Экземпляр OscillatoryMotion с 
    /// некорректной частотой</param>
    /// <param name="expectedErrorFragment">Ожидаемый фрагмент сообщения
    /// об ошибке</param>
    /// <param name="description">Описание тестового случая 
    /// для отладки</param>
    private static void AssertFrequencyValidation(
        OscillatoryMotion motion, string expectedErrorFragment, 
        string description)
    {
        var result = motion.ValidateParameters();
        
        Assert.That(result, Does.Contain(expectedErrorFragment),
            $"Ошибка не найдена: {description}");
    }

    /// <summary>
    /// Проверяет свойства Frequency
    /// </summary>
    [TestCase(TestName = "Проверка частоты")]
    public void Frequency_Property_GetSet()
    {
        SetTestCulture();
        const double testFrequency = 100;

        var motion = new OscillatoryMotion();
        motion.Frequency = testFrequency;
        
        Assert.That(motion.Frequency, Is.EqualTo(testFrequency));
    }

    /// <summary>
    /// Проверяет валидацию частоты на отрицательные и 
    /// некорректные значения (NaN, Infinity)
    /// </summary>
    /// <param name="invalidFrequency">Некорректное значение частоты</param>
    /// <param name="expectedError">Ожидаемое сообщение об ошибке</param>
    /// <param name="valueInMessage">Значение, которое должно быть
    /// в сообщении</param>
    /// <param name="description">Описание случая для отладки</param>
    [TestCase(-5, "Частота не может быть отрицательной", "-5",
        "отрицательное значение",
        TestName = "Проверка частоты на отрицательное")]
    [TestCase(double.NaN, "Частота содержит некорректное значение", null,
        "NaN", TestName = "Проверка частоты на NaN")]
    [TestCase(double.PositiveInfinity, 
        "Частота содержит некорректное значение", null,"+Infinity",
        TestName = "Проверка частоты на +Infinity")]
    [TestCase(double.NegativeInfinity, 
        "Частота содержит некорректное значение", null,"-Infinity", 
        TestName = "Проверка частоты на -Infinity")]
    public void ValidateParameters_InvalidFrequency_ReturnsError(
        double invalidFrequency, string expectedError,
        string? valueInMessage, string description)
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(invalidFrequency, 0, 1, 1);

        AssertFrequencyValidation(motion, expectedError, description);

        if (valueInMessage != null)
        {
            Assert.That(motion.ValidateParameters(), Does.Contain(valueInMessage),
                $"Сообщение должно содержать '{valueInMessage}'");
        }
    }

    [TestCase(TestName = "Конструктор класса")]
    public void DefaultConstructor_SetsDefaultValues()
    {
        SetTestCulture();
        const double defaultFrequency = 1.0;

        var motion = new OscillatoryMotion();
        AssertBasePropertiesHaveDefaults(motion);
        Assert.That(motion.Frequency, Is.EqualTo(defaultFrequency));
    }

    [TestCase(TestName = "Проверка расчета при нулевом времени")]
    public void GetPosition_ZeroTime_ReturnsInitialPosition()
    {
        SetTestCulture();
        const double frequency = 50, x0 = 10, t = 0, v = 5;

        var motion = new OscillatoryMotion(frequency, x0, t, v);
        Assert.That(motion.GetPosition(), Is.EqualTo(x0).Within(1e-10));
    }

    [TestCase(TestName = "Проверка наличия информации о частоте")]
    public void GetInfo_ContainsFrequencyInfo()
    {
        SetTestCulture();
        const double frequency = 50, x0 = 0, t = 1, v = 10;

        var motion = new OscillatoryMotion(frequency, x0, t, v);
        var info = motion.GetInfo();

        Assert.That(info, Does.Contain("Частота"));
        Assert.That(info, Does.Contain("рад/с"));
    }

    [TestCase(TestName = "Проверка названия движения")]
    public void Name_ReturnsCorrectValue()
    {
        SetTestCulture();
        const string expectedName = "Колебательное движение";

        var motion = new OscillatoryMotion();
        Assert.That(motion.Name, Is.EqualTo(expectedName));
    }

    [TestCase(TestName = "Проверка на корректные параметры")]
    public void ValidateParameters_ValidParameters_ReturnsEmptyString()
    {
        SetTestCulture();
        const double frequency = 10, x0 = 5, t = 2, v = 3;

        var motion = new OscillatoryMotion(frequency, x0, t, v);
        Assert.That(motion.ValidateParameters(), Is.Empty);
    }

    /// <summary>
    /// Объединённый тест: некорректные базовые параметры не должны вызывать ошибки частоты
    /// </summary>
    [TestCase(double.NaN, null, "время", TestName = "Проверка времени")]
    [TestCase(null, double.PositiveInfinity, "скорость", TestName = "Проверка скорости")]
    public void ValidateParameters_InvalidBaseParam_DoesNotAffectFrequencyCheck(
        double? invalidTime, double? invalidSpeed, string paramName)
    {
        SetTestCulture();
        const double frequency = 1, x0 = 0, defaultValue = 1;
        var t = invalidTime ?? defaultValue;
        var v = invalidSpeed ?? defaultValue;

        var motion = new OscillatoryMotion(frequency, x0, t, v);
        var result = motion.ValidateParameters();

        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Does.Not.Contain("Частота"),
            $"Ошибка не должна упоминать частоту при некорректном {paramName}");
    }

    [TestCase(TestName = "Проверка расчета координаты для колебательного движения")]
    public void GetPosition_CalculatesCorrectly_WithKnownValues()
    {
        SetTestCulture();
        const double angularFrequency = Math.PI, x0 = 10, t = 0.5, v = 2;

        // Исправлено: const → var/явный тип
        double expected = 10 + (Math.PI / 2) * Math.Sin(Math.PI * 0.5);
        // или: var expected = 10 + (Math.PI / 2) * Math.Sin(Math.PI * 0.5);

        var motion = new OscillatoryMotion(angularFrequency, x0, t, v);
        Assert.That(motion.GetPosition(), Is.EqualTo(expected).Within(1e-10));
    }

    [TestCase(TestName = "Проверка расчета при нулевой частоте")]
    public void GetPosition_ZeroFrequency_ReturnsInitialPosition()
    {
        SetTestCulture();
        const double frequency = 0, x0 = 42, t = 100, v = 5;

        var motion = new OscillatoryMotion(frequency, x0, t, v);
        Assert.That(motion.GetPosition(), Is.EqualTo(x0).Within(1e-10));
    }

    [TestCase(TestName = "Проверка влияния скорости на результат")]
    public void GetPosition_DivisionBySpeed_AffectsResult()
    {
        SetTestCulture();
        const double frequency = 10, x0 = 0, t = 1, v1 = 2, v2 = 5;

        var motion1 = new OscillatoryMotion(frequency, x0, t, v1);
        var motion2 = new OscillatoryMotion(frequency, x0, t, v2);

        Assert.That(motion1.GetPosition(), Is.Not.EqualTo(motion2.GetPosition()));
    }

    [TestCase(TestName = "Проверка наличия информации о параметрах базового класса")]
    public void GetInfo_ContainsBaseInfo()
    {
        SetTestCulture();
        const double frequency = 50, x0 = 15, t = 3, v = 7;

        var motion = new OscillatoryMotion(frequency, x0, t, v);
        var info = motion.GetInfo();

        Assert.That(info, Does.Contain("Начальная координата"));
        Assert.That(info, Does.Contain($"Xo={x0}"));
        Assert.That(info, Does.Contain("Скорость"));
        Assert.That(info, Does.Contain("Время"));
        Assert.That(info, Does.Contain($"Частота ω={frequency}"));
    }

    [TestCase(25, 100, 5, 8, TestName = "Проверка конструктора с параметрами")]
    public void ParameterConstructor_AssignsValuesCorrectly(
        double frequency, double initialPosition, double time, double speed)
    {
        SetTestCulture();
        var motion = new OscillatoryMotion(frequency, initialPosition, time, speed);

        Assert.That(motion.Frequency, Is.EqualTo(frequency));
        Assert.That(motion.InitialPosition, Is.EqualTo(initialPosition));
        Assert.That(motion.Time, Is.EqualTo(time));
        Assert.That(motion.Speed, Is.EqualTo(speed));
    }
}