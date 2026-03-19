namespace Model
{
    /// <summary>
    /// Класс исключения
    /// </summary>
    public class IncorrectArgumentException : Exception
    {
        /// <summary>
        /// Конструктор класса для обработки исключений
        /// </summary>
        /// <param name="message">Сообщение об исключении</param>
        public IncorrectArgumentException(string message) : base(message) { }
    }
}
