using Model;

namespace Lab4
{
    //TODO: refactor
    /// <summary>
    /// Интерфейс для расчета координаты
    /// </summary>
    public interface IMove
    {
        /// <summary>
        /// Параметры движения
        /// </summary>
        public MotionBase MovementParameters { get; }

    }
}
