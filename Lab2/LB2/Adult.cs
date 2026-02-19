using System;
using static System.Net.Mime.MediaTypeNames;

namespace LB2
{
    /// <summary>
    /// Дочерний класс: Взрослый человек
    /// </summary>
    public class Adult : PersonBase
    {
        /// <summary>
        /// Информация о паспортных данных
        /// </summary>
        private int _passportInfo;

        /// <summary>
        /// Информация о рабочем месте
        /// </summary>
        private string _jobName;

        /// <summary>
        /// Информация о состоянии брака
        /// </summary>
        private Adult _spouse;

        /// <summary>
        /// Минимально возможный возраст взрослого человека 
        /// </summary>
        private const int MinAge = 18;

        /// <summary>
        /// Максимально возможный возраст взрослого человека
        /// </summary>
        private const int MaxAge = 130;

        /// <summary>
        /// Минимально возможный номер паспорта
        /// </summary>
        private const int MinPassportNumber = 100000;

        /// <summary>
        /// Максимально возможный номер паспорта
        /// </summary>
        private const int MaxPassportNumber = 999999;

        /// <summary>
        /// Длина номера паспорта
        /// </summary>
        private const int PassportNumberLenght = 6;

        /// <summary>
        /// Свойство для доступа к private полям
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="gender">Пол</param>
        /// <param name="passportInfo">Паспортные данные</param>
        /// <param name="jobName">Место работы</param>
        /// <param name="spouse">Супруг(а)</param>
        public Adult(string name, string surname, int age, Gender gender,
            int passportInfo, string jobName, Adult spouse) :
            base(name, surname, age, gender)
        {
            PassportInfo = passportInfo;
            JobName = jobName;
            Spouse = spouse;
        }

        /// <summary>
        /// Конструктор по умолчанию для взрослого человека
        /// </summary
        protected Adult() : base("Любовь", "Подопригора", 24, Gender.Female)
        { }

        /// <summary
        /// Проверка ввода паспортных данных
        /// </summary>
        public int PassportInfo
        {
            get
            {
                return _passportInfo;
            }
            set
            {
                //TODO: magic (to const) +
                if (Convert.ToString(value).Length != PassportNumberLenght)
                {
                    throw new Exception($"Номер паспорта " +
                        $"должен содержать {PassportNumberLenght} цифр");
                }
                _passportInfo = value;
            }
        }

        /// <summary>
        /// Аксессор для места работы
        /// </summary>
        public string JobName
        {
            get
            {
                return _jobName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Поле не должно быть пустым");
                }
                _jobName = value;
            }
        }

        /// <summary>
        /// Аксессор для информации о супруге 
        /// </summary>
        public Adult Spouse
        { 
            get 
            { 
                return _spouse; 
            }
            set { } 
        }

        /// <summary>
        /// Проверка человека на совешеннолетие 
        /// </summary>
        /// <param name="age">возраст человека</param>
        /// <exception cref="Exception">Возраст должен быть 
        /// от 18 до 130</exception>
        public override void CheckAge(int age)
        {
            if ((age < MinAge) || (age > MaxAge))
            {
                throw new Exception($"Возраст взрослого человека " +
                    $"должен быть от {MinAge} до {MaxAge}");
            }
        }

        /// <summary>
        /// Метод для вывода информации о состоянии брака
        /// </summary>
        /// <returns>Информация о взрослом человеке</returns>
        public override string GetInfo()
        {
            var marriedInfo = "Не состоит в браке";

            if (Spouse != null)
            {
                marriedInfo = $"Состоит в браке с: " +
                    $"{Spouse.Name} {Spouse.Surname}";
            }

            var jobName = "Безработный";

            if (!string.IsNullOrEmpty(JobName))
            {
                jobName = $"Место работы: {JobName}";
            }

            return $"{base.GetInfo()};\n Номер паспорта: {PassportInfo}; " +
                $"\n {marriedInfo}; \n {jobName}. \n";
        }

        /// <summary>
        /// Метод для создания случайного взрослого человека
        /// </summary>
        /// <returns>Данные о случайном взрослом человеке</returns>
        public static Adult GetRandomAdult(Gender gender)
        {
            string[] nameFemaleList = 
            { 
                "Любовь", "Анна", "Виктория", "Лариса" 
            };
            string[] nameMaleList = 
            { 
                "Виктор", "Олег", "Петр", "Алексей" 
            };
            string[] surnameFemaleList = 
            { 
                "Мамаева", "Александрова", "Куницына", "Петрова" 
            };
            string[] surnameMaleList = 
            { 
                "Рашитов", "Бондарев", "Алиев", "Сидоров" 
            };
            string[] jobList = 
            { 
                "Школа №4 г.Москва", "ГАЗПРОМ", "ВТБ Банк",
                "Агенство недвижимости", "Безработный" 
            };
            //TODO: refactor +

            Random random = new Random();

            string namePerson = gender == Gender.Male
                ? nameMaleList[random.Next(nameMaleList.Length)]
                : nameFemaleList[random.Next(nameFemaleList.Length)];

            string surnamePerson = gender == Gender.Male
                ? surnameMaleList[random.Next(surnameMaleList.Length)]
                : surnameFemaleList[random.Next(surnameFemaleList.Length)];

            Adult tmpmarried = new Adult();

            var isMarried = random.Next(2) == 0;

            if (isMarried)
            {
                tmpmarried.Name = (gender == Gender.Male)
                    ? nameFemaleList[random.Next(nameFemaleList.Length)]
                    : nameMaleList[random.Next(nameMaleList.Length)];

                tmpmarried.Surname = surnamePerson;

                tmpmarried.Surname = gender == Gender.Male
                    ? tmpmarried.Surname + "а"
                    : tmpmarried.Surname = tmpmarried.Surname.TrimEnd('а');
            }
            else
            {
                tmpmarried = null;
            }

            int age = random.Next(MinAge, MaxAge + 1);
            int passportNumber = random.Next
                (MinPassportNumber, MaxPassportNumber + 1);

            string jobName = jobList[random.Next(jobList.Length)];

            return new Adult(namePerson, surnamePerson, age, gender,
                passportNumber, jobName, tmpmarried);
        }

        /// <summary>
        /// Метод  определения взрослого человека
        /// </summary>
        /// <returns>Сообщение о взрослом</returns>
        public static string GetAdult()
        {
            return "Пьет пиво и грустит, он Adult";
        }
    }                   
}
