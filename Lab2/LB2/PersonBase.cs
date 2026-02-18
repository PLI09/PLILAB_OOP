using System;

namespace LB2
{
    /// <summary>
    /// Класс PersonBase
    /// </summary>
    public abstract class PersonBase 
    {
        /// <summary>
        /// Имя человека
        /// </summary>
        private string _name;

        /// <summary>
        /// Фамилия человека
        /// </summary>
        private string _surname;

        /// <summary>
        /// Возраст человека
        /// </summary>
        private int _age;

        /// <summary>
        /// Пол человека
        /// </summary>
        private Gender _gender;

        /// <summary>
        /// Минимальный возраст
        /// </summary>
        private const int minAge = 0;

        /// <summary>
        /// Максимальный возраст
        /// </summary>
        private const int maxAge = 130;

        /// <summary>
        /// Свойство для доступа к private полям
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамлия</param>
        /// <param name="age">Возраст</param>
        /// <param name="gender">Пол</param>
        protected PersonBase(string name, string surname, int age, Gender gender)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Gender = gender;
        }

        /// <summary>
        /// Конструктор класса по умолчанию
        /// </summary>
        protected PersonBase() : this("Любовь", "Подопригора",
            24, Gender.Female)
        { }

        /// <summary>
        /// Проверка корректности ввода имени
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception($"{nameof(Name)} " +
                        $"не может быть пустым");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Проверка корректности ввода фамилии
        /// </summary>
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception($"{nameof(Surname)} " +
                        $"не может быть пустым");
                }
                _surname = value;
            }
        }

        /// <summary>
        /// Проверка корректности ввода возраста
        /// </summary>
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    throw new Exception("Введите возраст");
                }

                if ((value < minAge) || (value > maxAge))
                {
                    throw new Exception($"Возраст должен быть " +
                        $"от {minAge} до {maxAge}");
                }
                _age = value;
            }
        }

        /// <summary>
        /// Проверка корректности ввода пола человека
        /// </summary>
        public Gender Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

        /// <summary>
        /// Вывод информации о человеке
        /// </summary>
        /// <returns>Информация о человеке</returns>
        public string GetPersonInfo()
        {
            return $"{Name} {Surname}, возраст: {Age}, пол: {Gender}";
        }

        /// <summary>
        /// Абстрактный метод для вывода информации 
        /// о человеке в зависимости от возраста
        /// </summary>
        /// <returns>Информация о человеке</returns>
        public abstract string GetInfo();

        /// <summary>
        /// Абстрактный метод для проверки возраста человека
        /// </summary>
        /// <param name="age">Возраст</param>
        public abstract void CheckAge(int age);
    }
}

