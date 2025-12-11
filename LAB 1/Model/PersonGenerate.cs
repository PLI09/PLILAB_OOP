using Model;
using System;

namespace Model
{
    /// <summary>
    /// Класс PersonGenerate для 
    /// создания рандомного человека.
    /// </summary>
    public class PersonGenerate
    {
        /// <summary>
        /// Метод создания рандомного человека.
        /// </summary>
        /// <returns>Объект класса Person.</returns>
        public static Person GenerateRandomPerson()
        {
            string[] maleName =
            {
                "Дмитрий", "Сергей", "Николай", "Кирилл", "Владимир", "Анатолий",
                "Степан", "Виктор", "Ярослав", "Денис",
            };

            string[] femaleName =
            {
                "Любовь", "Зинаида", "Людмила", "Светлана", "Дарья",
                "Яна", "Алина", "Ирина", "Виктория", "Мария",
            };

            string[] maleSurname =
            {
                "Иванов", "Петров", "Сидоров", "Светлаков", "Казначеев",
                "Светлогоров", "Склярук", "Шароян", "Данилов", "Бибиков",
            };

            string[] femaleSurname =
            {
                "Снеткова", "Рашитова", "Полякова", "Ячменева", "Баракова",
                "Телешева", "Пугачева", "Перфильева", "Непомнящих", "Фоменко",
            };

            Random random = new Random();

            Person person = new Person();

            person.Age = random.Next(Person.MinAge, Person.MaxAge);

            person.Gender = (Gender)random.Next(2);

            switch (person.Gender)
            {
                case Gender.Male:
                    {
                        person.Name = maleName[
                        random.Next(0, maleName.Length)];
                        person.Surname = maleSurname[
                            random.Next(0, maleSurname.Length)];
                        break;
                    }
                case Gender.Female:
                    {
                        person.Name = femaleName[
                        random.Next(0, femaleName.Length)];
                        person.Surname = femaleSurname[
                            random.Next(0, femaleSurname.Length)];
                        break;
                    }
            }

            return person;
        }
    }
}
