using System;

namespace LB2
{
    /// <summary>
    /// Дочерний класс: ребенок
    /// </summary>
    public class Child : PersonBase
    {
        /// <summary>
        /// Мама ребенка
        /// </summary>
        private Adult _mother;

        /// <summary>
        /// Папа ребенка
        /// </summary>
        private Adult _father;

        /// <summary>
        /// Школа
        /// </summary>
        private string _school;

        /// <summary>
        /// Минииальный возраст человека 
        /// </summary>
        private const int MinAge = 0;

        /// <summary>
        /// Максимальный возраст человека
        /// </summary>
        private const int MaxAge = 18;

        /// <summary>
        /// Свойство для доступа к private полям
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="gender">Пол</param>
        /// <param name="mother">Мама ребенка</param>
        /// <param name="father">Папа ребенка</param>
        /// <param name="school">Школа</param>
        public Child(string name, string surname, int age, Gender gender,
            Adult mother, Adult father, string school) :
            base(name, surname, age, gender)
        {
            Father = father;
            Mother = mother;
            School = school;
        }

        /// <summary>
        /// Аксессор для матери ребенка 
        /// </summary>
        public Adult Mother
        {
            get
            {
                return _mother;
            }
            set
            {
                CheckParentGender(value, Gender.Male);
                _mother = value;
            }
        }

        /// <summary>
        /// Акксессор для отца
        /// </summary>
        public Adult Father
        {
            get
            {
                return _father;
            }
            set
            {
                CheckParentGender(value, Gender.Female);
                _father = value;
            }
        }

        //TODO: autoproperty
        /// <summary>
        /// Аксессор для школы ребенка
        /// </summary>
        public string School
        {
            get
            {
                return _school;
            }
            set
            {
                //TODO: validation?
                _school = value;
            }
        }
       
        //TODO: remove
        /// <summary>
        /// Конструктор по умолчанию 
        /// </summary>
        protected Child() { }

        /// <summary>
        /// Проверка возраста ребенка
        /// </summary>
        /// <param name="age">Возраст</param>
        /// <exception cref="Exception">Возраст должен соответсвовать 
        /// несоершеннолетнему человеку</exception>
        public override void CheckAge(int age)
        {
            if ((age > MinAge) || (age < MaxAge))
            {
                throw new Exception($"Возраст ребенка доджен быть " +
                    $"от {MinAge} до {MaxAge}");
            }
        }

        /// <summary>
        /// Метод, который выводит информацию о ребенке
        /// </summary>
        /// <returns>Информация о ребенке</returns>
        public override string GetInfo()
        {
            string fatherInfo = Father != null 
                ? $"Отец:{Father.Name} {Father.Surname}" 
                : "Отец отсутствует";
            string motherInfo = Mother != null 
                ? $"Мать:{Mother.Name} {Mother.Surname}" 
                : "Мать отсутствует";

            return $"{GetPersonInfo()};\n {fatherInfo};\n " +
                $"{motherInfo};\n {School}\n";
        }

        /// <summary>
        /// Метод для проверки пола родителя
        /// </summary>
        /// <param name="parent">Родитель</param>
        /// <param name="gender">Пол, противоположный тому, 
        /// что должен иметь родитель</param>
        /// <exception cref="Exception">Родители должны иметь 
        /// правильный пол</exception>
        private static void CheckParentGender(Adult parent, Gender gender)
        {
            if (parent != null && parent.Gender == gender)
            {
                throw new Exception($"Пол родителя должен быть другой!");
            }
        }
        
        /// <summary>
        /// Метод для создания родителей
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>Родитель или ничего</returns>
        public static Adult GetRandomParent(Gender gender)
        {
            var random = new Random();
            var parentExistence = random.Next(1, 3);
            if (parentExistence != 1)
            {
                return Adult.GetRandomAdult(gender);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Метод для создания случайного ребенка
        /// </summary>
        /// <returns>Данные о случайном ребенке</returns>
        public static Child GetRandomChild()
        {
            string[] nameMaleList = 
            { 
                "Александр", "Дмитрий" 
            };
            string[] nameFemaleList = 
            { 
                "Анастасия", "Екатерина" 
            };
            string[] surnameMaleList = 
            { 
                "Иванов", "Петров" 
            };
            string[] surnameFemaleList = 
            { 
                "Иванова", "Петрова" 
            };
            Gender[] genderList = 
            { 
                Gender.Male, Gender.Female 
            };
            string[] schoolList = 
            { 
                "БСОШ №1", "БСОШ №2", "Лицей при ТПУ", "Школу не посещает" 
            };
            string[] kindergartenList = 
            { 
                "Детский сад №1", "Детский сад №2", "Детский сад не посещает" 
            };

            Random random = new Random();

            Gender gender = genderList[random.Next(genderList.Length)];

            string namePerson = gender == Gender.Male
                ? nameMaleList[random.Next(nameMaleList.Length)]
                : nameFemaleList[random.Next(nameFemaleList.Length)];

            string surnamePerson = null;

            int age = random.Next(MinAge, MaxAge + 1);

            Adult parentFather = GetRandomParent(Gender.Male);

            Adult parentMother = GetRandomParent(Gender.Female);

            if ((parentMother == null) && (parentFather == null))
            {
                surnamePerson = gender == Gender.Male
                    ? surnameMaleList[random.Next(surnameMaleList.Length)]
                    : surnameFemaleList[random.Next(surnameFemaleList.Length)];
            }
            else if ((parentFather == null) && (parentMother != null))
            {
                surnamePerson = gender == Gender.Male
                    ? parentMother.Surname.TrimEnd('а')
                    : parentMother.Surname;
            }
            else if ((parentFather != null) && (parentMother == null))
            {
                surnamePerson = gender == Gender.Male
                    ? parentFather.Surname
                    : parentFather.Surname + "а";
            }
            else if ((parentFather != null) && (parentMother != null))
            {
                surnamePerson = gender == Gender.Male
                    ? parentFather.Surname
                    : parentMother.Surname;

                if (gender == Gender.Male)
                {
                    parentMother.Surname = parentFather.Surname + "а";
                }
                else
                { 
                    parentFather.Surname = parentMother.Surname.Trim('а');
                }
            }

            //TODO: magic (to const)
            string tmpStudent = age > 6
                ? schoolList[random.Next(schoolList.Length)]
                //TOOD: отступы
                : ((age > 2) && (age < 7))
                ? kindergartenList[random.Next(kindergartenList.Length)]
                : "Образовательное учреждение не посещает";

            return new Child(namePerson, surnamePerson, age, gender,
               parentMother, parentFather, tmpStudent);
        }

        /// <summary>
        /// Метод для опредления ребенка
        /// </summary>
        /// <returns>Сообщение о ребенке</returns>
        public static string GetChild()
        {
            return "Играет в Roblox, он Child";
        }
    }
}
