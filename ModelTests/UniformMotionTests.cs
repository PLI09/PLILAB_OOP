using Model;
namespace ModelTests;

[TestFixture]
public class UniformMotionTests : MotionBaseTests
{
    [TestCase(TestName = "Проверка свойства Name")]
    public void Name_ReturnsExpectedValue()
    {
        const string expectedName = "Равномерное движение";
        var motion = new UniformMotion();
        Assert.That(motion.Name, Is.EqualTo(expectedName));
    }

    [TestCase(0, 0, 0, 0, TestName = "Проверка нулевых значений")]
    [TestCase(10, 5, 2, 20, TestName = "Проверка обычных значений")]
    [TestCase(-5, 3, 4, 7, TestName = "Проверка с отрицательной начальной координатой")]
    [TestCase(0, 10, 0.5, 5, TestName = "Проверка с дробной скоростью")]
    public void GetPosition_CalculatesCorrectly(
        double x0, double t, double v, double expected)
    {
        var motion = new UniformMotion(v, x0, t);
        Assert.That(motion.GetPosition(), Is.EqualTo(expected).Within(1e-10));
    }
}