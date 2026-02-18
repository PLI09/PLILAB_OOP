using LB2;

namespace Lab2
{
    /// <summary>
    /// Выполнение основной части программы
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Лист из 7 человек");
            Console.WriteLine();

            List <PersonBase> personList = new List <PersonBase>();

            Random random = new Random();

            for (int i = 0; i < 7; i++)
            {
                Gender[] genderList = 
                { 
                    Gender.Male, Gender.Female 
                };

                Gender genderRandom = genderList
                    [random.Next(genderList.Length)];

                PersonBase randomPerson = random.Next(2) == 0
                    ? Adult.GetRandomAdult(genderRandom)
                    : Child.GetRandomChild();
                personList.Add(randomPerson);
            }

            Stop();

            PrintList(personList);

            Stop();

            Console.WriteLine("Тип четвертого человека из списка:");

            if (personList[3] is Adult)
            {
                Console.WriteLine(Adult.GetAdult());
            }
            if (personList[3] is Child)
            {
                Console.WriteLine(Child.GetChild());
            }
        }

        /// <summary>
        /// Метод для вывода списка людей
        /// </summary>
        /// <param name="personList">Список людей</param>
        /// <exception cref="Exception">Список людей пуст</exception>
        private static void PrintList(List <PersonBase> personList)
        {
            if (personList == null)
            {
                throw new Exception("Список людей пуст!");
            }
            else
            {
                for (int i = 0; i < personList.Count; i++)
                {
                    Console.WriteLine(personList[i].GetInfo());
                }
            }
        }

        /// <summary>
        /// Метод для ожидания действия пользователя  
        /// </summary>
        private static void Stop()
        {
            Console.WriteLine("Нажми любую клавишу");
            Console.ReadKey();
        }
    }
}