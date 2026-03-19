using System.Xml.Serialization;

namespace Model
{
    [XmlInclude(typeof(UniformMotion))]
    [XmlInclude(typeof(UniformlyAcceleratedMotion))]
    [XmlInclude(typeof(OscillatoryMotion))]

    /// <summary>
    /// Класс с базовыми параметрами движения
    /// </summary>
    public abstract class MotionBase
    { 
        /// <summary>
        /// Тип движения
        /// </summary>
        public virtual string Name => "Тип движения";
        
        /// <summary>
        /// Рассчитанная координата
        /// </summary>
        public virtual double Coordinate => GetPosition();

        /// <summary>
        /// Начальная координата
        /// </summary>
        private double _initialPosition;

        /// <summary>
        /// Время
        /// </summary>
        private double _time;
        
        /// <summary>
        /// Скорость 
        /// </summary>
        private double _speed;

        /// <summary>
        /// Конструктор базового класса
        /// </summary>
        /// <param name="initialPosition">Начальная координата</param>
        /// <param name="time">Время</param>
        /// <param name="speed">Скорость</param>
        protected MotionBase(double initialPosition, double time, double speed)
        {
            InitialPosition = initialPosition;
            Time = time;
            Speed = speed;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        protected MotionBase() { }

        /// <summary>
        /// Свойство для начальной координаты
        /// </summary>
        public double InitialPosition
        {
            get => _initialPosition;
            set
            {
                 CheckingForNegative(value);
                _initialPosition = value;
            }
        }

        /// <summary>
        /// Свойство для времени
        /// </summary>
        public double Time
        {
            get => _time;
            set
            {
                CheckingForNegative(value);
                _time = value;
            }
        }

        /// <summary>
        /// Свойство для скорости
        /// </summary>
        public double Speed
        {
            get => _speed;
            set
            {
                CheckingForNegative(value);
                _speed = value;
            }
        }

        /// <summary>
        /// Метод для проверки на NaN и Infinity
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="IncorrectArgumentException">Значение должно быть
        /// конечным числом</exception>
        protected void CheckingForNegative(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new IncorrectArgumentException
                    ("Значение должно быть конечным числом");
        }

        /// <summary>
        /// Вывод базовой информации
        /// </summary>
        /// <returns></returns>
        public virtual string GetInfo()
        {
            return $"Начальная координата Xo={InitialPosition} м\n" +
                   $"Скорость V={Speed} м/с\nВремя t={Time} c";
        }

        /// <summary>
        /// Абстрактный метод для расчета координаты
        /// </summary>
        /// <returns></returns>
        public abstract double GetPosition();

        /// <summary>
        /// Возвращает название дополнительных параметров движения
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetExtraColumnNames()
        {
            yield break;
        }

        /// <summary>
        /// Возвращает значения дополнительных параметров движения
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<double> GetExtraColumnValues()
        {
            yield break;
        }

     // public virtual IEnumerable<object> GetTableRowData()
     // {
     //     yield return Name;
     //     yield return Time;
     //     yield return Coordinate;
     //     yield return Speed;
     //
     //     foreach (var value in GetExtraColumnValues())
     //         yield return value;
     // }
    }
}