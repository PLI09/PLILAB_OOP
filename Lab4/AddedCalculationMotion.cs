using Model;

namespace Lab4
{
    /// <summary>
    /// Аргументы события, передающие добавленное движение от формы ввода к
    /// основной форме
    /// </summary>
    public class AddedCalculationMotion : EventArgs
    {
        /// <summary>
        /// Получает объект движения, который был добавлен
        /// </summary>
        public MotionBase Motion { get; }

        /// <summary>
        ///  Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="motion">Объект движения</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, 
        /// если параметр motion равен null</exception>
        public AddedCalculationMotion(MotionBase motion)
        {
            Motion = motion ?? 
                throw new ArgumentNullException(nameof(motion));
        }
    }
}
