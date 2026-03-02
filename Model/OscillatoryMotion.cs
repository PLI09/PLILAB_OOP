using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Класс для коллебательного движения
    /// </summary>
    public class OscillatoryMotion : MotionBase
    {
        /// <summary>
        /// Частота
        /// </summary>
        private double _frequency;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public OscillatoryMotion() : this (1, 1, 1, 1) { }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="frequency">частота</param>
        /// <param name="initialPosition">начальная координата</param>
        /// <param name="time">время</param>
        protected OscillatoryMotion(double frequency, 
            double initialPosition, double time, double speed)
            : base(initialPosition, time,speed)
        {
            Frequency = frequency;
        }

        /// <summary>
        /// Cвойство для проверки корректности частоты
        /// </summary>
        public double Frequency
        {
            get
            {
                return _frequency;
            }
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new IncorrectArgumentException(
                        "Частота должна быть конечным числом");
                }
                if (value < 0)
                {
                    throw new IncorrectArgumentException(
                        "Частота не может быть отрицательной");
                }
                _frequency = value;
            }
        }

        /// <summary>
        /// Метод для вывода информации о параметрах движения
        /// </summary>
        /// <returns>Пареметры движения</returns>
        public override string GetInfo()
        {
            return $"{base.GetInfo()}\nЧастота w={Frequency} рад/c";
        }

        /// <summary>
        /// Метод для расчета координаты для колебательного движения
        /// </summary>
        /// <param name="time">время</param>
        /// <returns>координата для колебательного движения</returns>
        public override double GetPosition()
        {

            return InitialPosition + (Frequency/Speed) * 
                Math.Sin(Frequency * Time);
        }
    }
}
