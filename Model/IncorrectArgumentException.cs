namespace Model
{
    /// <summary>
    /// Класс исключения
    /// </summary>
    public class IncorrectArgumentException : Exception
    {
        public IncorrectArgumentException(string message) : base(message) { }
    }
}
