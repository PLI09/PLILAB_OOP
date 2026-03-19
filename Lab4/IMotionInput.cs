using Model;

namespace Lab4.MotionControls
{
    //TODO: refactor +
    /// <summary>
    /// Интерфейс для расчета координаты
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
