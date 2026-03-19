namespace Model
{
    /// <summary>
    /// Класс для исключений
    /// </summary>
    [Serializable]
    public class IncorrectArgumentException : Exception
    {
        /// <summary>
        /// Экземпляр класса
        /// </summary>
        /// <param name="message">Сробщение об ошибке</param>
        public IncorrectArgumentException(string message) : base(message) { }
    }
}
