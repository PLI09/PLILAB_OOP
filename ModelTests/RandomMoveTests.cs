using Model;

namespace ModelTests;

/// <summary>
/// Тесты для класса RandomMove
/// </summary>
[TestFixture]
public class RandomMoveTests
{
    /// <summary>
    /// Проверка, что метод возвращает не-null объект
    /// </summary>
    [TestCase(TestName = "Проверка, что метод возвращает не-null объект")]
    public void GetRandomMove_ReturnsNonNull()
    {
        var result = RandomMove.GetRandomMove();
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Проверка, что возвращается один из типов движения
    /// </summary>
    [TestCase(TestName = "Проверка, что возвращается один из" +
        " типов движения")]
    public void GetRandomMove_ReturnsValidMotionType()
    {
        var result = RandomMove.GetRandomMove();
        var isValidType = result is UniformMotion ||
            result is UniformlyAcceleratedMotion ||
            result is OscillatoryMotion;
        Assert.That(isValidType, Is.True,
            $"Unexpected type: {result.GetType().Name}");
    }

    /// <summary>
    /// Проверка, что сгенерированное движение валидно
    /// </summary>
    [TestCase(TestName = "Проверка, что сгенерированное движение валидно")]
    public void GetRandomMove_GeneratedMotionIsValid()
    {
        var result = RandomMove.GetRandomMove();
        var error = result.ValidateParameters();
        Assert.That(string.IsNullOrEmpty(error),
            Is.True, () => $"Error: {error}");
    }

    /// <summary>
    /// Проверка, что генерируются разные типы движений
    /// </summary>
    [TestCase(TestName = "Проверка, что генерируются разные типы движений")]
    public void GetRandomMove_GeneratesDifferentTypes()
    {
        var types = new System.Collections.Generic.HashSet<string>();
        for (int i = 0; i < 100; i++)
        {
            var move = RandomMove.GetRandomMove();
            types.Add(move.GetType().Name);
        }
        Assert.That(types.Count, Is.GreaterThanOrEqualTo(2),
            "Ожидается 2 различных типа движения");
    }

    /// <summary>
    /// Проверка, что параметры сгенерированного движения 
    /// находятся в допустимом диапазоне
    /// </summary>
    [TestCase(TestName = "Проверка, что параметры сгенерированного " +
        "движения находятся в допустимом диапазоне")]
    public void GetRandomMove_ParametersInRange()
    {
        var result = RandomMove.GetRandomMove();
        Assert.That(result.InitialPosition,
            Is.GreaterThanOrEqualTo(0.0001));
        Assert.That(result.InitialPosition,
            Is.LessThanOrEqualTo(1000.0));
        Assert.That(result.Time,
            Is.GreaterThanOrEqualTo(0.0001));
        Assert.That(result.Time, Is.LessThanOrEqualTo(1000.0));
        Assert.That(result.Speed,
            Is.GreaterThanOrEqualTo(0.0001));
        Assert.That(result.Speed, Is.LessThanOrEqualTo(1000.0));
    }
}
