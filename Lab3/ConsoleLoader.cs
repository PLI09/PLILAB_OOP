using Model;

namespace ConsoleLoader
{
    /// <summary>
    /// Класс, в котором выполняется основная часть программы
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Расчет координаты для различных видов " +
                "движения: равномерное, равноускоренное, колебательное");
            MotionBase move = ReadParameters();
            Console.WriteLine(move.GetInfo());
            ShowCoordinate(move);
            while (true)
            {
                Console.WriteLine("Если хотите закончить введите '0'");
                if (Console.ReadLine() == "0")
                {
                    break;
                }
                else
                {
                    MotionBase moveSecond = ReadParameters();
                    Console.WriteLine(moveSecond.GetInfo());
                    ShowCoordinate(moveSecond);
                }
            }
        }

        /// <summary>
        /// Считывание параметров с консоли
        /// </summary>
        /// <returns>Параметры движения</returns>
        public static MotionBase ReadParameters()
        {
            Console.WriteLine("Выберете тип движения:\n1 - Равномерное\n" +
                "2 - Равноускоренное\n3 - Колебательное\n");

            int typeMove;
            while (true)
            {
                try
                {
                    typeMove = Convert.ToInt32(Console.ReadLine());
                    //TODO: refactor+
                    if (typeMove < 1 || typeMove > 3)
                    {
                        throw new IncorrectArgumentException
                            ("Введите число от 1 до 3");
                    }
                    break;
                }
                catch (IncorrectArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            switch (typeMove)
            {
                case 1:
                {
                    UniformMotion motionUniform = new UniformMotion();
                    var actionList = ReadBaseParameters(motionUniform);
                    ActionMove(actionList);
                    return motionUniform;
                }
                case 2:
                    {
                        UniformlyAcceleratedMotion motionAccelerated = 
                            new UniformlyAcceleratedMotion();
                        var actionListAccelerated = 
                            ReadBaseParameters(motionAccelerated);

                        actionListAccelerated.Add((new Action(() =>
                        {
                            motionAccelerated.Acceleration = 
                            ReadValidatedDouble("Введите ускорение:");
                        }), "ускорения"));
                        ActionMove(actionListAccelerated);
                        return motionAccelerated;
                    }
                case 3:
                    {
                        OscillatoryMotion motionOscillatory = 
                            new OscillatoryMotion();
                        var actionListOscillatory = 
                            ReadBaseParameters(motionOscillatory);
                        actionListOscillatory.Add((new Action(() =>
                        {
                            motionOscillatory.Frequency = 
                            ReadValidatedDouble("Введите частоту:");
                        }), "частоты"));
                        ActionMove(actionListOscillatory);
                        return motionOscillatory;
                    }
                default:
                    {
                        return new UniformMotion();
                    }
            }
        }

        /// <summary>
        /// Метод для считывания базовых параметров
        /// </summary>
        /// <param name="parameters">Параметры движения</param>
        /// <returns>Базовые параметры</returns>
        public static List<(Action, string)> ReadBaseParameters
            (MotionBase parameters)
        {
            var actionList = new List<(Action, string)>
            {
                (new Action(() =>
                {
                    parameters.InitialPosition = ReadValidatedDouble
                    ("Введите начальную координату:");
                }), "начальной координаты"),

                (new Action(() =>
                {
                    parameters.Time = ReadValidatedDouble
                    ("Введите время:");
                }), "времени"),

                (new Action(() =>
                {
                    parameters.Speed = ReadValidatedDouble
                    ("Введите скорость:");
                }), "скорости"),
            };
            return actionList;
        }

        /// <summary>
        /// Заполнение параметров
        /// </summary>
        /// <param name="actionList">Лист с параметрами</param>
        public static void ActionMove (List<(Action, string)> actionList)
        {
            foreach (var action in actionList)
            {
                Console.WriteLine($"Значение {action.Item2}");
                while (true) 
                {
                    action.Item1.Invoke();
                    break;
                }
            }
        }

        /// <summary>
        /// Метод для проверки заполненных данных
        /// </summary>
        /// <param name="parametr">параметр</param>
        /// <returns>возвращаем параметр или сообщение</returns>
        public static double ReadValidatedDouble(string parametr)
        {
            while (true)
            {
                Console.WriteLine(parametr);                                
                string input = Console.ReadLine();

                if (double.TryParse(input, out double result))
                {
                    return result;
                }

                Console.WriteLine("Введите корректное число");
            }
        }

        /// <summary>
        /// Вывод на экран рассчитанной координаты
        /// </summary>
        /// <param name="motion">Параметры</param>
        public static void ShowCoordinate(MotionBase motion)
        {
            Console.WriteLine($"Значение рассчитаной координаты X=" +
                $"{motion.GetPosition()}");
        }
    }   
}