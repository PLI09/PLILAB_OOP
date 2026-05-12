using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Model;

namespace ModelTests;

[TestFixture]
public class IncorrectArgumentExceptionTests
{
    /// <summary>
    /// Проверка создания исключения с сообщением
    /// </summary>
    /// <param name="message">Текст сообщения</param>
    [TestCase("Invalid value", TestName = "Сообщение: Неверное значение")]
    [TestCase("Parameter error", TestName = "Сообщение: Ошибка параметра")]
    [TestCase("", TestName = "Обработка пустого сообщения")]
    public void Constructor_WithMessage_SetsMessage(
        string message)
    {
        var exception = new IncorrectArgumentException(
            message);
        Assert.That(exception.Message,
            Is.EqualTo(message));
    }

    /// <summary>
    /// Проверка, что исключение поддерживает сериализацию
    /// </summary>
    [TestCase(TestName = "Проверка сериализации")]
    public void Class_IsMarkedAsSerializable()
    {
        var type = typeof(IncorrectArgumentException);
        Assert.That(
            type.GetCustomAttributes(
                typeof(SerializableAttribute),
                true).Length, Is.GreaterThan(0));
    }
}
