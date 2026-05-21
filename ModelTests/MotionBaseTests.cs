using Model;
namespace ModelTests;

/// <summary>
/// Тесты для абстрактного класса MotionBase
/// </summary>
[TestFixture]
public class MotionBaseTests
{
    /// <summary>
    /// Вспомогательный класс для тестирования абстракции
    /// </summary>
    private class TestMotion : MotionBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр с заданными параметрами
        /// </summary>
        /// <param name="x0">Начальная координата</param>
        /// <param name="t">Время</param>
        /// <param name="v">Скорость</param>
        public TestMotion(double x0, double t, double v)
            : base(x0, t, v) { }
        
        /// <summary>
        /// Возвращает текущую координату тела по формуле 
        /// равномерного движения
        /// </summary>
        /// <returns>Координата тела</returns>
        public override double GetPosition() =>
            InitialPosition + Speed * Time;
    }

    private static class MotionFactory
    {
        public static MotionBase GetMotion(int motionNumber, 
            double initialPosition, 
            double time, 
            double speed, 
            double value = 1)
        {
            switch (motionNumber)
            {
                case 0:
                {
                    return new OscillatoryMotion(value, initialPosition, time, speed);
                }
                case 1:
                {
                    return new UniformlyAcceleratedMotion(value, initialPosition, time, speed);
                }
                case 2:
                default:
                {
                    return new UniformMotion(initialPosition, time, speed);
                }
            }
        }
    }

    /// <summary>
    /// Проверка свойства Name по умолчанию
    /// </summary>
    [TestCase(TestName = "Проверка типа движения")]
    public void Name_Default_ReturnsBaseValue()
    {
        var motion = new TestMotion(0, 0, 0);
        Assert.That(motion.Name,
            Is.EqualTo("Тип движения"));
    }

    /// <summary>
    /// Проверка свойства Coordinate
    /// </summary>
    [TestCase(TestName = "Проверка координаты")]
    public void Coordinate_CallsGetPosition()
    {
        var motion = new TestMotion(10, 2, 3);
        Assert.That(motion.Coordinate,
            Is.EqualTo(motion.GetPosition()));
    }

    /// <summary>
    /// Проверка метода GetInfo
    /// </summary>
    [TestCase(TestName = "Проверка метода GetInfo")]
    public void GetInfo_ReturnsFormattedString()
    {
        var motion = new TestMotion(5, 10, 2);
        var info = motion.GetInfo();
        Assert.That(info, Does.Contain("Xo=5 м"));
        Assert.That(info, Does.Contain("V=2 м/с"));
        Assert.That(info, Does.Contain("t=10 c"));
    }

    /// <summary>
    /// Проверка валидации с корректными данными
    /// </summary>
    /// <param name="x0">Координата</param>
    /// <param name="t">Время</param>
    /// <param name="v">Скорость</param>
    [TestCase(0, 0, 0, TestName = "Валидация нулевых значений")]
    [TestCase(100, 50, 10, TestName = "Валидация больших значений")]
    [TestCase(-10, 5, 0.5, TestName = "Валидация отрицательных значений")]
    public void ValidateParameters_ValidData_ReturnsEmpty(
        double x0, double t, double v)
    {
        var motion = new TestMotion(x0, t, v);
        Assert.That(motion.ValidateParameters(),
            Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// Проверка валидации с отрицательным временем
    /// </summary>
    [TestCase(TestName = " Проверка на отрицательное время")]
    public void ValidateParameters_NegativeTime_ReturnsError()
    {
        var motion = new TestMotion(0, -1, 1);
        var result = motion.ValidateParameters();
        Assert.That(result, Does.Contain(
            "Время не может быть отрицательным"));
    }

    /// <summary>
    /// Проверка валидации с отрицательной скоростью
    /// </summary>
    [TestCase(TestName = " Проверка на отрицательную скорость")]
    public void ValidateParameters_NegativeSpeed_ReturnsError()
    {
        var motion = new TestMotion(0, 1, -1);
        var result = motion.ValidateParameters();
        Assert.That(result, Does.Contain(
            "Скорость не может быть отрицательной"));
    }

    /// <summary>
    /// Проверка валидации с NaN в координате
    /// </summary>
    [TestCase(TestName = "Проверка на Nan значения координаты")]
    public void ValidateParameters_NaNPosition_ReturnsError()
    {
        var motion = new TestMotion(
            double.NaN, 1, 1);
        var result = motion.ValidateParameters();
        Assert.That(result, Does.Contain(
            "Начальная координата содержит некорректное"));
    }

    /// <summary>
    /// Проверка валидации с Infinity во времени
    /// </summary>
    [TestCase(TestName = "Проверка на Infinity значения времени")]
    public void ValidateParameters_InfinityTime_ReturnsError()
    {
        var motion = new TestMotion(
            0, double.PositiveInfinity, 1);
        var result = motion.ValidateParameters();
        Assert.That(result, Does.Contain(
            "Время содержит некорректное значение"));
    }

    /// <summary>
    /// Проверка атрибутов сериализации на MotionBase
    /// </summary>
    [TestCase(TestName = "Проверка всех типов движений в сохраненном файле")]
    public void MotionBase_HasXmlIncludeAttributes()
    {
        var type = typeof(MotionBase);
        var attributes = type.GetCustomAttributes(
            typeof(System.Xml.Serialization.XmlIncludeAttribute),
            true);
        Assert.That(attributes.Length, Is.GreaterThan(0),
            "MotionBase should have XmlInclude attributes");
    }

    /// <summary>
    /// Проверка, что все типы движения имеют атрибут XmlInclude
    /// </summary>
    [TestCase(TestName = "Проверить то, что новый тип движения был " +
        "добавлен в список для сохранения")]
    public void MotionBase_IncludesAllDerivedTypes()
    {
        var type = typeof(MotionBase);
        var attributes = type.GetCustomAttributes(
            typeof(System.Xml.Serialization.XmlIncludeAttribute),
            true) as System.Xml.Serialization.XmlIncludeAttribute[];

        var types = new System.Collections.Generic.List<Type>();
        foreach (var attr in attributes)
        {
            types.Add(attr.Type);
        }

        Assert.That(types, Does.Contain(typeof(UniformMotion)));
        Assert.That(types, Does.Contain(
            typeof(UniformlyAcceleratedMotion)));
        Assert.That(types, Does.Contain(
            typeof(OscillatoryMotion)));
    }


    /// <summary>
    /// Проверка конструктора с параметрами при присваиваении значений
    /// </summary>
    [TestCase(0, TestName = "Проверка конструктора OscillatoryMotion с параметрами при" +
        " присваиваении значений")]
    [TestCase(1, TestName = "Проверка конструктора UniformlyAcceleratedMotion с параметрами при" +
        " присваиваении значений")]
    [TestCase(2, TestName = "Проверка конструктора UniformMotion с параметрами при" +
        " присваиваении значений")]
    public void ParameterConstructor_AssignsValuesCorrectly(int motionNumber)
    {
        const int initialPosion = 100;
        const int time = 5;
        const int speed = 8;

        MotionBase motion = 
            MotionFactory.GetMotion(motionNumber, initialPosion, time, speed);

        Assert.That(motion.InitialPosition, Is.EqualTo(100));
        Assert.That(motion.Time, Is.EqualTo(5));
        Assert.That(motion.Speed, Is.EqualTo(8));
    }
}