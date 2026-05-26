using Model;

namespace ModelTests;
/// <summary>
/// Тесты для класса MotionBase
/// </summary>
[TestFixture]
public class MotionBaseTests
{
    /// <summary>
    /// Вспомогательный класс для тестирования
    /// </summary>
    private class TestMotion : MotionBase
    {
        /// <summary>
        /// Инициализация нового экземпляра с заданными параметрами
        /// </summary>
        /// <param name="initialPosition">Начальная координата</param>
        /// <param name="time">Время</param>
        /// <param name="speed">Скорость</param>
        public TestMotion(double initialPosition, double time, double speed) 
            : base(initialPosition, time, speed) { }

        /// <summary>
        /// Возвращает текущую координату тела ппо формуле
        /// </summary>
        /// <returns>Координата тела</returns>
        public override double GetPosition() => 
            InitialPosition + Speed * Time;
    }
    protected static void AssertBaseProperties(
    MotionBase motion,
    double expectedInitialPosition,
    double expectedTime,
    double expectedSpeed)
    {
        Assert.That(motion.InitialPosition, Is.EqualTo(expectedInitialPosition));
        Assert.That(motion.Time, Is.EqualTo(expectedTime));
        Assert.That(motion.Speed, Is.EqualTo(expectedSpeed));
    }
    /// <summary>
    /// Проверка свойств базового класса по умолчанию
    /// </summary>
    /// <param name="motion">Экземпляр MotionBase для проверки</param>
    protected static void AssertBasePropertiesHaveDefaults(MotionBase motion)
    {
        const double defaultInitialPosition = 1;
        const double defaultTime = 1;
        const double defaultSpeed = 1;
        AssertBaseProperties(motion, defaultInitialPosition, defaultTime, defaultSpeed);
    }

    /// <summary>
    /// Проверка валидации параметра на некорректные значения
    /// </summary>
    /// <param name="motion">Экземпляр MotionBase для проверки</param>
    /// <param name="parameterName">Название параметра </param>
    /// <param name="expectedErrorFragment">Сообщение об ошибке</param>
    protected static void AssertParameterValidation(
        MotionBase motion,
        string parameterName,
        string expectedErrorFragment)
    {
        var result = motion.ValidateParameters();
        
        Assert.That(result, Is.Not.Empty,
            $"Ожидается ошибка валидации для {parameterName}");
        Assert.That(result, Does.Contain(expectedErrorFragment),
            $"Сообщение должно содержать '{expectedErrorFragment}'");
    }

    /// <summary>
    /// Проверяет, что свойство Name возвращает базовое значение
    /// "Тип движения"
    /// </summary>
    [TestCase(TestName = "Проверка типа движения")]
    public void Name_Default_ReturnsBaseValue()
    {
        const string expectedName = "Тип движения";
        var motion = new TestMotion(0, 0, 0);
        
        Assert.That(motion.Name, Is.EqualTo(expectedName));
    }

    /// <summary>
    /// Проверяет, что свойство Coordinate вызывает метод GetPosition
    /// </summary>
    [TestCase(TestName = "Проверка координаты")]
    public void Coordinate_CallsGetPosition()
    {
        const double initialPosition = 10, time = 2, speed = 3;
        var motion = new TestMotion(initialPosition, time, speed);
        
        Assert.That(motion.Coordinate, Is.EqualTo(motion.GetPosition()));
    }

    /// <summary>
    /// Проверяет, что метод GetInfo возвращает отформатированную строку
    /// </summary>
    [TestCase(TestName = "Проверка метода GetInfo")]
    public void GetInfo_ReturnsFormattedString()
    {
        const double initialPosition = 5, time = 10, speed = 2;
        var motion = new TestMotion(initialPosition, time, speed);
        var info = motion.GetInfo();

        Assert.That(info, Does.Contain($"Xo={initialPosition} м"));
        Assert.That(info, Does.Contain($"V={speed} м/с"));
        Assert.That(info, Does.Contain($"time={time} c"));
    }

    /// <summary>
    /// Проверяет валидацию корректных данных 
    /// </summary>
    /// <param name="initialPosition">Начальная координата</param>
    /// <param name="time">Время</param>
    /// <param name="speed">Скорость</param>
    [TestCase(0, 0, 0, TestName = "Валидация нулевых значений")]
    [TestCase(100, 50, 10, TestName = "Валидация больших значений")]
    [TestCase(-10, 5, 0.5, TestName = "Валидация отрицательных значений")]
    public void ValidateParameters_ValidData_ReturnsEmpty(
        double initialPosition, double time, double speed)
    {
        var motion = new TestMotion(initialPosition, time, speed);
        
        Assert.That(motion.ValidateParameters(), Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// Проверяет валидацию отрицательных значений времени и скорости
    /// </summary>
    /// <param name="negativeTime">Отрицательное время</param>
    /// <param name="negativeSpeed">>Отрицательная скорость</param>
    /// <param name="expectedError">сСообщение об ошибке</param>
    [TestCase(-1, null, "Время не может быть отрицательным",
        TestName = "Проверка на отрицательное время")]
    [TestCase(null, -1, "Скорость не может быть отрицательной",
        TestName = "Проверка на отрицательную скорость")]
    public void ValidateParameters_NegativeValue_ReturnsError(
        double? negativeTime, double? negativeSpeed, string expectedError)
    {
        const double initialPosition = 0, defaultValue = 1;
        var time = negativeTime ?? defaultValue;
        var speed = negativeSpeed ?? defaultValue;
        var motion = new TestMotion(initialPosition, time, speed);
        
        AssertParameterValidation(motion, negativeTime.HasValue 
            ? "время" : "скорость", expectedError);
    }

    /// <summary>
    /// Проверяет валидацию на NaN и Infinity для базовых параметров
    /// </summary>
    /// <param name="invalidInitinalPosition">Некорректная координата</param>
    /// <param name="invalidTime">Некорректное время</param>
    /// <param name="invalidSpeed">Некорректная скорость</param>
    /// <param name="expectedErrorFragment">Ожидаемый фрагмент ошибки</param>
    /// <param name="parameterName"></param>
    [TestCase(double.NaN, null, null, "Начальная координата содержит " +
        "некорректное значение","координата", 
        TestName = "Проверка координаты на NaN")]
    [TestCase(null, double.NaN, null, "Время содержит некорректное значение",
        "время", TestName = "Проверка времени на NaN")]
    [TestCase(null, null, double.NaN, "Скорость содержит " +
        "некорректное значение","скорость", 
        TestName = "Проверка скорости на NaN")]
    [TestCase(null, double.PositiveInfinity, null, "Время содержит " +
        "некорректное значение","время", 
        TestName = "Проверка времени на +Infinity")]
    [TestCase(null, double.NegativeInfinity, null, "Время содержит " +
        "некорректное значение","время", 
        TestName = "Проверка времени на -Infinity")]
    [TestCase(null, null, double.PositiveInfinity, "Скорость содержит " +
        "некорректное значение","скорость", 
        TestName = "Проверка скорости на +Infinity")]
    public void ValidateParameters_NonFiniteValue_ReturnsError(
        double? invalidInitinalPosition, double? invalidTime,
        double? invalidSpeed,string expectedErrorFragment, 
        string parameterName)
    {
        const double defaultValue = 1;
        var initialPosition = invalidInitinalPosition ?? defaultValue;
        var time = invalidTime ?? defaultValue;
        var v = invalidSpeed ?? defaultValue;
        var motion = new TestMotion(initialPosition, time, v);
        
        AssertParameterValidation
            (motion, parameterName, expectedErrorFragment);
    }

    /// <summary>
    /// Проверяет наличие атрибутов XmlInclude для сериализации
    /// производных типов
    /// </summary>
    [TestCase(TestName = "Проверка XmlInclude атрибутов")]
    public void MotionBase_HasXmlIncludeAttributes()
    {
        var type = typeof(MotionBase);
        var attributes = type.GetCustomAttributes(
            typeof(System.Xml.Serialization.XmlIncludeAttribute), true);

        Assert.That(attributes.Length, Is.GreaterThan(0));
    }

    /// <summary>
    /// Проверяет, что все производные типы добавлены в XmlInclude атрибуты
    /// </summary>
    [TestCase(TestName = "Проверка всех типов в XmlInclude")]
    public void MotionBase_IncludesAllDerivedTypes()
    {
        var type = typeof(MotionBase);
        var attributes = type.GetCustomAttributes(
            typeof(System.Xml.Serialization.XmlIncludeAttribute), true)
            as System.Xml.Serialization.XmlIncludeAttribute[];

        var types = new System.Collections.Generic.List<Type>();
        foreach (var attr in attributes)
            types.Add(attr.Type);

        Assert.That(types, Does.Contain(typeof(UniformMotion)));
        Assert.That(types, Does.Contain(typeof(UniformlyAcceleratedMotion)));
        Assert.That(types, Does.Contain(typeof(OscillatoryMotion)));
    }

}