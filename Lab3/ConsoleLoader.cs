using System;
using System.Net.Http.Headers;
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
                    if (typeMove != 1 && typeMove != 2 && typeMove != 3)
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
                catch (FormatException)
                {
                    Console.WriteLine("Введено некорректное значение");
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
                            while (true)
                            {
                                try
                                {
                                    motionAccelerated.Acceleration =
                                    Convert.ToDouble(Console.ReadLine());
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine
                                    ("Введено некорректное значение");
                                }
                            }
                        }),
                        "ускорения"));
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
                            while (true)
                            {
                                try
                                {
                                    motionOscillatory.Frequency =
                                        Convert.ToDouble(Console.ReadLine());
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine
                                    ("Введено некорректное значение");
                                }
                            }
                        }),
                        "частоты"));
                        ActionMove(actionListOscillatory);
                        return motionOscillatory;
                    }
            }
            return new UniformMotion();
        }

        /// <summary>
        /// Метод для считывания базовых параметров
        /// </summary>
        /// <param name="parameters">Параметры движения</param>
        /// <returns>Базовые параметры</returns>
        public static List<(Action, string)> 
            ReadBaseParameters(MotionBase parameters) 
        {
            var actionList = new List<(Action, string)>
            {
                (new Action(
                    () =>
                    {
                        while (true)
                        {
                            try
                            {
                                parameters.InitialPosition = 
                                Convert.ToDouble(Console.ReadLine());
                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine
                                ("Введено некорректное значение");
                            }
                        }

                    }),
                    "начальной координаты"),
                (new Action(
                    () =>
                    {
                        while (true)
                        {
                            try
                            {
                                parameters.Time =
                                Convert.ToDouble(Console.ReadLine());
                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine
                                ("Введено некорректное значение");
                            }
                        }

                    }),
                    "времени"),
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
                Console.WriteLine($"Введите значение {action.Item2}");
                while (true) 
                {
                    try
                    {
                        action.Item1.Invoke();
                        break;
                    }
                    catch (IncorrectArgumentException exception)
                    {
                        Console.WriteLine (exception.Message);
                    }
                }
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