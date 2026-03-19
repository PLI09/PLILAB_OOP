using Model;
namespace Lab4
{
    //TODO: refactor
    /// <summary>
    /// Класс генерации рандомного движения
    /// </summary>
    public class RandomMove
    {
        /// <summary>
        /// Минимальное число для генерации
        /// </summary>
        private const double _minRandomValue = 0.0001;

        /// <summary>
        /// Максимальное число для генерации
        /// </summary>
        private const double _maxRandomValue = 1000000000.0;

        /// <summary>
        /// Генерация рандомного параметра
        /// </summary>
        /// <returns></returns>
        private static double GenerateRandomValue()
        {
            double minRandomValue = _minRandomValue;
            double maxRandomValue = _maxRandomValue;
            Random random = new Random();
            return random.NextDouble()
                * (maxRandomValue - minRandomValue) + minRandomValue;
        }
        /// <summary>
        /// Генерация рандомного движения
        /// </summary>
        /// <returns>Движение</returns>
        public static MotionBase GetRandomMove()
        {
            Random random = new Random();
            switch (random.Next(3))
            {
                case 0:
                {
                    return new UniformMotion
                        (GenerateRandomValue(),
                        GenerateRandomValue(),
                        GenerateRandomValue());
                }
                case 1:
                {
                    return new UniformlyAcceleratedMotion
                        (GenerateRandomValue(),
                        GenerateRandomValue(),
                        GenerateRandomValue(),
                        GenerateRandomValue());
                }
                case 2:
                {
                    return new OscillatoryMotion
                        (GenerateRandomValue(),
                        GenerateRandomValue(),
                        GenerateRandomValue(),
                        GenerateRandomValue());
                }
            }
            return new UniformMotion();
        }
    }
}
