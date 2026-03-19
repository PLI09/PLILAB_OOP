using Model;

namespace Lab4
{
    /// <summary>
    /// Класс добавления рассчитаной координаты
    /// </summary>
    public class AddedCalculationMotion : EventArgs
    {
        /// <summary>
        /// Параметры движения
        /// </summary>
        public MotionBase MovementParameters { get; }

        /// <summary>
        /// Добавление рассчитаной координаты
        /// </summary>
        public AddedCalculationMotion(MotionBase move)
        {
            MovementParameters = move;
        }
    }
}
