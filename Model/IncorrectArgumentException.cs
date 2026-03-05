namespace Model
{
    /// <summary>
    /// Класс исключения
    /// </summary>
    public class IncorrectArgumentException : Exception
    {
        //TODO: XML
        public IncorrectArgumentException(string message) : base(message) { }
    }
}
