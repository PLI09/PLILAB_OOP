using System;
using System.Reflection;
using Model;

namespace LAB_1
{
    /// <summary>
    /// Основной класс программы.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        private static void Main()
        {
            // 3.a Создание двух списков персон по три человека
            PersonList personList1 = new PersonList();
            PersonList personList2 = new PersonList();

            Person persona1 = new Person(
                "Дмитрий", "Иванов", 18, Gender.Male);
            Person persona2 = new Person(
                "Анастасия", "Крылова", 27, Gender.Female);
            Person persona3 = new Person(
                "Юрий", "Таскаев", 54, Gender.Male);

            Person persona4 = new Person(
                "Виктор", "Семенов", 87, Gender.Male);
            Person persona5 = new Person(
                "Людмила", "Сергеева", 42, Gender.Female);
            Person persona6 = new Person(
                "Дарья", "Смольянинова", 34, Gender.Female);

            personList1.AddPerson(persona1);
            personList1.AddPerson(persona2);
            personList1.AddPerson(persona3);

            personList2.AddPerson(persona4);
            personList2.AddPerson(persona5);
            personList2.AddPerson(persona6);

            // 3.b Вывод содержимого каждого списка на экран
            Console.WriteLine($"Список №1:\n" +
                $"{personList1.GetInfo()}");
            Console.WriteLine($"Список №2:\n" +
                $"{personList2.GetInfo()}");
            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey(true);

            // 3.c Добавление нового человека в первый список
            Person persona7 = new Person(
                "Светлана", "Петрова", 65, Gender.Female);
            personList1.AddPerson(persona7);
            Console.WriteLine($"В список №1 добавлен новый человек.\n" +
                $"Список №1:\n{personList1.GetInfo()}");
            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey(true);

            // 3.d Копирование второго человека из первого списка в конец
            // второго
            Console.WriteLine("Во второй список добавлен второй человек из" +
                " первого списка:");
            personList2.AddPerson(personList1.GetPersonAtIndex(1));
            Console.WriteLine($"Список №1:" +
                $"\n{personList1.GetInfo()}");
            Console.WriteLine($"Список №2:" +
                $"\n{personList2.GetInfo()}");
            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey(true);

            // 3.e Удаление второго человека из первого списка
            personList1.RemovePersonAtIndex(1);
            Console.WriteLine("В первом списке удален второй человек:");
            Console.WriteLine($"Список №1:" +
                $"\n{personList1.GetInfo()}");
            Console.WriteLine($"Список №2:" +
                $"\n{personList2.GetInfo()}");
            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey(true);

            // 3.f Очистка второго списка
            personList2.RemovePerson();
            Console.WriteLine("Второй список очищен");
            Console.WriteLine($"Список №1:" +
                $"\n{personList1.GetInfo()}");
            Console.WriteLine($"Список №2:" +
                $"\n{personList2.GetInfo()}");

            // 4 Чтение персоны с клавиатуры и вывод персоны на экран
            Person personConsole = ConsolePerson.ReadPersonFromConsole();
            Console.WriteLine(personConsole.GetInfo());

            // 5 Генерация рандомной персоны
            Person randomPerson = PersonGenerate.GenerateRandomPerson();
            personList1.AddPerson(randomPerson);
            Console.WriteLine(
                $"Список №1 после добавление рандомной персоны:\n" +
                $"{personList1.GetInfo()}");
            Console.ReadKey(true);
        }
    }
}
