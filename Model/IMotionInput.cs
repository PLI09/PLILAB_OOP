namespace Model
{
    /// <summary>
    /// Интерфейс для элементов управления вводом параметров движения
    /// </summary>
    public interface IMotionInput
    {
        /// <summary>
        /// Проверка корректности ввода
        /// </summary>
        bool ValidateInput();

        /// <summary>
        /// Получение объекта движения с рассчитанными параметрами
        /// </summary>
        MotionBase GetMotion();
    }
}
