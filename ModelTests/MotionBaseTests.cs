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

    public static class MotionFactory
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
                        return new OscillatoryMotion(
                            value, initialPosition, time, speed);
                    }
                case 1:
                    {
                        return new UniformlyAcceleratedMotion(
                            value, initialPosition, time, speed);
                    }
                case 2:
                default:
                    {
                        return new UniformMotion(
                            initialPosition, time, speed);
                    }
            }
        }
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
    /// Проверка атрибутов сериализации на MotionBase
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

    /// <summary>
    /// Проверка конструктора с параметрами при присваиваении значений
    /// </summary>
    [TestCase(0, TestName = "Проверка конструктора OscillatoryMotion с " +
        "параметрами при присваиваении значений")]
    [TestCase(1, TestName = "Проверка конструктора " +
        "UniformlyAcceleratedMotion с параметрами при " +
        "присваиваении значений")]
    [TestCase(2, TestName = "Проверка конструктора UniformMotion с " +
        "параметрами при присваиваении значений")]
    public void ParameterConstructor_AssignsValuesCorrectly(int motionNumber)
    {
        const int initialPosition = 100;
        const int time = 5;
        const int speed = 8;

        MotionBase motion = MotionFactory.GetMotion(
            motionNumber, initialPosition, time, speed);

        Assert.That(motion.InitialPosition, Is.EqualTo(100));
        Assert.That(motion.Time, Is.EqualTo(5));
        Assert.That(motion.Speed, Is.EqualTo(8));
    }
}