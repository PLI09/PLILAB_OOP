using System.Globalization;

namespace Lab4.MotionControls
{
    /// <summary>
    /// Класс для валидации ввода чисел
    /// </summary>
    public class ValidateMotionControl : UserControl
    {
        /// <summary>
        /// Свойство для разрешения отрицательных значений
        /// </summary>
        public bool AllowNegative { get; set; } = false;

        /// <summary>
        /// Свойство для разрешения нуля
        /// </summary>
        public bool AllowZero { get; set; } = true;

        /// <summary>
        /// Минимальное допустимое значение
        /// </summary>
        public double? MinValue { get; set; } = null;

        /// <summary>
        /// Максимальное допустимое значение
        /// </summary>
        public double? MaxValue { get; set; } = null;

        /// <summary>
        /// Событие, возникающее при изменении валидности поля
        /// </summary>
        public event EventHandler<bool>? ValidationCompleted;

        /// <summary>
        /// Последний результат валидации
        /// </summary>
        public bool IsValid { get; private set; } = true;

        /// <summary>
        /// Обработчик изменения текста в TextBox
        /// </summary>
        public void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is TextBox textBox))
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
                        (AllowZero || value != 0) &&
                        (!MinValue.HasValue || value >= MinValue.Value) &&
                        (!MaxValue.HasValue || value <= MaxValue.Value);
                }
            }

            if (isValid)
            {
                SetValid(textBox);
            }
            else
            {
                SetInvalid(textBox);
            }
        }

        /// <summary>
        /// Устанавливает состояние валидности: белый фон,true, 
        /// вызывает событие
        /// </summary>
        private void SetValid(TextBox textBox)
        {
            textBox.BackColor = Color.White;
            IsValid = true;
            ValidationCompleted?.Invoke(this, true);
        }

        /// <summary>
        /// Устанавливает состояние невалидности: подсветка фона, 
        /// флаг false,вызывает событие.
        /// </summary>
        private void SetInvalid(TextBox textBox)
        {
            textBox.BackColor = Color.LightCoral;
            IsValid = false;
            ValidationCompleted?.Invoke(this, false);
        }
    }
}
