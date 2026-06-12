using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Model;

namespace ModelTests
{
    /// <summary>
    /// Базовый класс для тестирования всех типов движения
    /// </summary>
    /// <typeparam name="T">Тип движения (наследник MotionBase)</typeparam>
    public abstract class MotionTestsBase<T> where T : MotionBase
    {
        /// <summary>
        /// Создание экземпляра движения с заданными параметрами
        /// </summary>
        /// <param name="initialPosition">Начальная координата</param>
        /// <param name="time">Время</param>
        /// <param name="speed">Скорость</param>
        /// <param name="specificParam">Ускорение или частота</param>
        /// <returns>Экземпляр типа движения</returns>
        protected abstract T CreateMotion(
            double initialPosition, double time, double speed,
            double specificParam = 1);

        /// <summary>
        /// Создание экземпляра движения через конструктор по умолчанию
        /// </summary>
        /// <returns>Экземпляр типа движения</returns>
        protected abstract T CreateDefaultMotion();

        /// <summary>
        /// Ожидаемое значение свойства Name для данного типа движения
        /// </summary>
        protected abstract string ExpectedName { get; }

        /// <summary>
        /// Проверка значения специфического свойства 
        /// (Frequency, Acceleration или отсутствие)
        /// </summary>
        /// <param name="motion">Тип движения</param>
        /// <param name="expectedValue">Специфическое значение</param>
        protected abstract void AssertSpecificProperty(
            T motion, double expectedValue);

        /// <summary>
        /// Проверка, что GetInfo содержит информацию о 
        /// специфическом параметре
        /// </summary>
        /// <param name="motion">Тип движения</param>
        protected abstract void AssertGetInfoContainsSpecific(T motion);

        /// <summary>
        /// Проверка, что недопустимые значения специфического 
        /// свойства выбрасывают исключение
        /// </summary>
        /// <param name="motion">Тип движения</param>
        protected abstract void AssertInvalidSpecificProperty(T motion);

        /// <summary>
        /// Проверка корректности расчёта GetPosition с известными значениями
        /// </summary>
        /// <param name="motion">Тип движения</param>
        /// <param name="expected">Значение</param>
        protected abstract void AssertGetPositionCalculatesCorrectly(
            T motion, double expected);

        /// <summary>
        /// Проверка свойства Name на возврат ожидаемого значения
        /// </summary>
        [TestCase(TestName = "Name возвращает ожидаемое значение")]
        public void Name_ReturnsExpectedValue()
        {
            var motion = CreateDefaultMotion();
            Assert.That(motion.Name, Is.EqualTo(ExpectedName));
        }

        /// <summary>
        /// Проверка, что свойство Coordinate совпадает с 
        /// результатом GetPosition()
        /// </summary>
        [TestCase(TestName = "Coordinate совпадает с GetPosition")]
        public void Coordinate_ReturnsSameAsGetPosition()
        {
            var motion = CreateMotion(10, 2, 3, 5);
            Assert.That(motion.Coordinate, Is.EqualTo(motion.GetPosition()));
        }

        /// <summary>
        /// Проверка, что GetInfo содержит базовую информацию 
        /// (начальная координата, скорость, время) и 
        /// специфическую для типа движения
        /// </summary>
        [TestCase(TestName = "GetInfo содержит базовую и " +
            "специфическую информацию")]
        public void GetInfo_ContainsBaseInfo()
        {
            const double x0 = 10;
            const double t = 3;
            const double v = 7;
            var motion = CreateMotion(x0, t, v, 2);
            var info = motion.GetInfo();

            Assert.That(info, Does.Contain(
                $"Начальная координата Xo={x0} м"));
            Assert.That(info, Does.Contain(
                $"Скорость V={v} м/с"));
            Assert.That(info, Does.Contain(
                $"Время t={t} c"));
            AssertGetInfoContainsSpecific(motion);
        }

        /// <summary>
        /// Проверка, что при нулевом времени GetPosition возвращает 
        /// начальную координату
        /// </summary>
        [TestCase(TestName = "GetPosition при нулевом времени")]
        public void GetPosition_ZeroTime_ReturnsInitialPosition()
        {
            var motion = CreateMotion(42, 0, 10, 5);
            Assert.That(motion.GetPosition(), Is.EqualTo(42).Within(1e-10));
        }

        /// <summary>
        /// Проверка специфических свойств (ускорение, частота)
        /// на недопустимые параметры
        /// </summary>
        [TestCase(TestName = "Недопустимое значение специфического свойства "
            + "вызывает исключение")]
        public void SpecificProperty_InvalidValue_ThrowsException()
        {
            var motion = CreateDefaultMotion();
            AssertInvalidSpecificProperty(motion);
        }

        /// <summary>
        /// Проверка корректности расчёта координаты при известных значениях
        /// </summary>
        [TestCase(TestName = "GetPosition корректно вычисляет позицию" +
            " на известных значениях")]
        public void GetPosition_CalculatesCorrectly_WithKnownValues()
        {
            var motion = CreateMotion(10, 0.5, 2, Math.PI);
            double expected = ComputeExpectedPositionForKnownValues(motion);
            AssertGetPositionCalculatesCorrectly(motion, expected);
        }

        /// <summary>
        /// Расчет координаты для различных типов движений
        /// </summary>
        /// <param name="motion">Тип движения</param>
        /// <returns>Рассчитанная координата</returns>
        protected virtual double ComputeExpectedPositionForKnownValues(
            T motion) => motion.GetPosition();

        /// <summary>
        /// Класс-наследник для тестирования базового класса
        /// </summary>
        private class TestableMotion : MotionBase
        {
            /// <summary>
            /// Конструктор класса
            /// </summary>
            /// <param name="initialPosition">Начальная координата</param>
            /// <param name="time">Время</param>
            /// <param name="speed">Скорость</param>
            public TestableMotion(
                double initialPosition, double time, double speed)
                : base(initialPosition, time, speed) { }

            /// <summary>
            /// Реализация абстрактного класса
            /// </summary>
            /// <returns>Начальная координата</returns>
            public override double GetPosition() => InitialPosition;
        }

        /// <summary>
        /// Проверяет свойства Name и Coordinate на ожидаемые значения
        /// </summary>
        /// <param name="expectedName">Название в свойство Name</param>
        /// <param name="initialPosition">Начальная координата</param>
        /// <param name="expectedCoord">Значение координаты</param>
        [TestCase("Тип движения", 100, 100,
            TestName = "Проверка свойства Name")]
        public void Name_AndCoordinate_ReturnExpected(
            string expectedName,
            double initialPosition,
            double expectedCoord)
        {
            var motion = new TestableMotion(initialPosition, 5, 2);

            Assert.That(motion.Name, Is.EqualTo(expectedName));
            Assert.That(motion.Coordinate, Is.EqualTo(expectedCoord));
        }

        /// <summary>
        /// Проверка на невалидные значения
        /// </summary>
        /// <param name="invalidValue">Невалидное значение</param>
        /// <param name="expectedMessagePart">Сообщение о 
        /// некоректном значении</param>
        [TestCase(double.NaN, "некорректное значение",
            TestName = "Проверка на NaN")]
        [TestCase(double.PositiveInfinity, "некорректное значение",
            TestName = "Проверка на плюс бесконечность")]
        [TestCase(double.NegativeInfinity, "некорректное значение",
            TestName = "Проверка на минус бесконечность")]
        public void ValidateParameters_InvalidValue_ThrowsException(
            double invalidValue, string expectedMessagePart)
        {
            var motion = new TestableMotion(0, 1, 1);

            var ex = Assert.Throws<IncorrectArgumentException>(() =>
                motion.InitialPosition = invalidValue);

            Assert.That(ex.Message, Does.Contain(expectedMessagePart));
        }

        /// <summary>
        /// Проверяет, что отрицательное время или 
        /// скорость вызывают исключение
        /// </summary>
        /// <param name="negativeValue">отрицательное значение</param>
        /// <param name="property">название параметра</param>
        [TestCase(-5, "Time",
            TestName = "тест времени при отрицательном значении")]
        [TestCase(-10, "Speed",
            TestName = "тест скорости при отрицательном значении")]
        public void ValidateNegativeParameters_NegativeValue_ThrowsException(
            double negativeValue, string property)
        {
            var motion = new TestableMotion(0, 1, 1);

            var ex = Assert.Throws<IncorrectArgumentException>(() =>
            {
                if (property == "Time")
                    motion.Time = negativeValue;
                else
                    motion.Speed = negativeValue;
            });

            Assert.That(ex.Message, Does.Contain(
                "не может быть отрицательным"));
            Assert.That(ex.Message, Does.Contain(
                negativeValue.ToString()));
        }
    }
}