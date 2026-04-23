using System.Globalization;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Класс для валидации числовых значений
    /// </summary>
    public class DoubleValidator
    {
        /// <summary>
        /// Свойство для разрешения на ввод отрицательных значений
        /// </summary>
        public bool AllowNegative { get; set; }

        /// <summary>
        /// Свойство для разрешения на ввод нулевого значения
        /// </summary>
        public bool AllowZero { get; set; } = true;

        /// <summary>
        /// Получает текущее состояние валидации последнего проверенного
        /// значения
        /// </summary>
        public bool IsValid { get; private set; } = true;

        /// <summary>
        /// Событие, вызываемое после завершения проверки значения
        /// </summary>
        public event EventHandler<bool>? ValidationCompleted;

        /// <summary>
        /// Обработчик события изменения текста
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргумент</param>
        public void TextBox_TextChanged(object sender, EventArgs eventArgs)
        {
            if (sender is not TextBox textBox)
            {
                return;
            }

            string text = textBox.Text.Trim();
            bool isValid = false;

            if (!string.IsNullOrEmpty(text))
            {
                string normalized = text.Replace(',', '.');
                if (double.TryParse(normalized, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out double value))
                {
                    isValid = (AllowNegative || value >= 0) && 
                        (AllowZero || value != 0);
                }
            }
            else
            {
                isValid = true;
            }

            SetState(textBox, isValid);
        }

        /// <summary>
        /// Метод для визуального выделения текста с неверными параметрами
        /// </summary>
        /// <param name="textBox">TextBox, состояние которого 
        /// нужно обновить</param>
        /// <param name="isValid">Обновление свойства IsValid</param>
        private void SetState(TextBox textBox, bool isValid)
        {
            textBox.BackColor = isValid 
                ? Color.White 
                : Color.LightCoral;

            IsValid = isValid;
            ValidationCompleted?.Invoke(this, isValid);
        }
    }
}
