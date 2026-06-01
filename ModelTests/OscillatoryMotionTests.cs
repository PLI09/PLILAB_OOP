using Model;
using System.Globalization;
using static ModelTests.MotionBaseTests;


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

    /// <summary>
    /// Проверка расчета GetPosition с известными значениями
    /// </summary>
    [TestCase(TestName = "Проверка расчета координаты для" +
        " колебательного движения")]
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
        var motion1 = new OscillatoryMotion(10, 0, 1, 2);
        var motion2 = new OscillatoryMotion(10, 0, 1, 5);
        var position1 = motion1.GetPosition();
        var position2 = motion2.GetPosition();
        Assert.That(position1,
            Is.Not.EqualTo(position2));
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

    /// <summary>
    /// Проверка конструктора с параметрами при присваиваении значений
    /// </summary>
    [TestCase(0, TestName = "Проверка конструктора OscillatoryMotion с " +
        "параметрами при присваиваении значений")]
    public void ParameterConstructor_AssignsValuesCorrectly(int motionNumber)
    {
        const int initialPosition = 100;
        const int time = 5;
        const int speed = 8;
        const int frequency = 25;

        var motion = (OscillatoryMotion)MotionFactory.GetMotion(
            motionNumber, initialPosition, time, speed, frequency);

        Assert.That(motion.Frequency, Is.EqualTo(25));
    }
}