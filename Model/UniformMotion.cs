using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    /// <summary>
    /// Класс равномерное движение
    /// </summary>
    public class UniformMotion : MotionBase
    {

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public UniformMotion() : this(1, 1, 1) { }

        /// <summary>
        /// Конструктор класса 
        /// </summary>
        /// <param name="initialPosition">Начальная координата</param>
        /// <param name="speed">Скорость</param>
        public UniformMotion(double speed, double initialPosition, 
            double time) : base(initialPosition, time, speed) {}

        
        /// <summary>
        /// Метод для вычисления координаты для равномерного движения
        /// </summary>
        /// <param name="time">Время</param>
        /// <returns>Координата для равномерного движения</returns>
        public override double GetPosition()
        {         
            return InitialPosition + Speed * Time;
        }
    }
}
