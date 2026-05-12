using Model;

namespace ModelTests;

/// <summary>
/// Тесты для класса UniformMotion
/// </summary>
[TestFixture]
public class UniformMotionTests
{
    /// <summary>
    /// Проверка свойства Name
    /// </summary>
    [TestCase(TestName = "Проверка свойства Name")]
    public void Name_ReturnsExpectedValue()
    {
        var motion = new UniformMotion();
        Assert.That(motion.Name,
            Is.EqualTo("Равномерное движение"));
    }

    /// <summary>
    /// Проверка расчёта координаты
    /// </summary>
    /// <param name="x0">Начальная координата</param>
    /// <param name="t">Время</param>
    /// <param name="v">Скорость</param>
    /// <param name="expected">Ожидаемый результат</param>

    [TestCase(0, 0, 0, 0, TestName = "Проверка нулевых значений")]
    [TestCase(10, 5, 2, 20, TestName = "Проверка обычных значений")]
    [TestCase(-5, 3, 4, 7, TestName = "Проверка с отрицательной" +
        " начальной координатой")]
    [TestCase(0, 10, 0.5, 5, TestName = "Проверка с дробной" +
        " скоростью")]
    public void GetPosition_CalculatesCorrectly(
        double x0, double t, double v, double expected)
    {
        var motion = new UniformMotion(v, x0, t);
        Assert.That(motion.GetPosition(),
            Is.EqualTo(expected).Within(1e-10));
    }

    /// <summary>
    /// Проверка свойства Coordinate
    /// </summary>
    [TestCase(TestName = "Проверка свойства Coordinate")]
    public void Coordinate_ReturnsGetPositionResult()
    {
        var motion = new UniformMotion(3, 2, 4);
        Assert.That(motion.Coordinate,
            Is.EqualTo(motion.GetPosition()));
    }

    /// <summary>
    /// Проверка конструктора по умолчанию
    /// </summary>
    [TestCase(TestName = "Проверка конструктора по умолчанию")]
    public void DefaultConstructor_SetsDefaultValues()
    {
        var motion = new UniformMotion();
        Assert.That(motion.InitialPosition, Is.EqualTo(1));
        Assert.That(motion.Time, Is.EqualTo(1));
        Assert.That(motion.Speed, Is.EqualTo(1));
    }
}