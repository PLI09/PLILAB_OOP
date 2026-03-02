namespace Model
{
    /// <summary>
    /// Интерфейс для всех типов движения
    /// </summary>
    public interface IMotionable
    {
        /// <summary>
        /// Метод для вычисления координаты в заданный момент времени
        /// </summary>
        /// <param name="time">время</param>
        /// <returns>Координата</returns>
        double GetPosition();
    }
}
