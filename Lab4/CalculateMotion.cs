using Lab4.MotionControls;
using Model;

namespace Lab4
{
    /// <summary>
    /// Форма для расчета движения
    /// </summary>
    public partial class CalculateMotion : Form
    {
        /// <summary>
        /// Событие вызываемое после успешного добавления нового движения
        /// </summary>
        public EventHandler<AddedCalculationMotion> MoveAdded;

        /// <summary>
        /// Добавление пользовательских интерфейсов для расчета
        /// </summary>
        private UserControl _moveControl => _baseMove[TypeMoveComboBox.Text];

        /// <summary>
        /// Словарь с названиями типов движений
        /// </summary>
        private readonly Dictionary<string, UserControl> _baseMove =
            new Dictionary<string, UserControl>()
            {
                {"Равномерное движение", new UniformMotionControl()},
                {"Равноускоренное движение", new AcceleratedMotionControl()},
                {"Колебательное движение", new OscillatoryMotionControl()}
            };

        /// <summary>
        /// Экземпляр класса
        /// </summary>
        public CalculateMotion()
        {
            InitializeComponent();
            foreach (var pair in _baseMove)
            {
                TypeMoveComboBox.Items.Add(pair.Key);
                var control = pair.Value;
                control.Location = new Point(
                    TypeMoveLabel.Location.X,
                    TypeMoveLabel.Location.Y + TypeMoveLabel.Height + 5);
                control.Visible = false;
                Controls.Add(control);
            }

            if (TypeMoveComboBox.Items.Count > 0)
            {
                TypeMoveComboBox.SelectedIndex = 0;
            }

            СalculateButton.Enabled = true;

#if (DEBUG)
            RandomButton.Visible = true;
#endif
        }

        /// <summary>
        /// Обработчик изменения выбранного типа движения в ComboBox
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Событие</param>
        private void TypeMoveComboBox_SelectedIndexChanged(
            object sender, EventArgs eventArgs)
        {
            foreach (var control in _baseMove.Values)
            {
                control.Visible = false;
                _moveControl.Visible = true;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки «Рассчитать»
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Событие</param>
        private void СalculateButton_Click(
            object sender, EventArgs eventArgs)
        {
            var current = _moveControl;
            if (current is not IMotionInput motionInput)
            {
                MessageBox.Show("Контрол не реализует IMotionInput", 
                    "Ошибка");
                return;
            }

            if (!ValidateNoLeadingZeros(current))
            {
                MessageBox.Show("Некорректный ввод!\n\n" +
                    "Числа не могут начинаться с 0 (кроме 0 и 0.xxx).\n" +
                    "Примеры ошибок: 0125, 007\n" +
                    "Примеры верно: 0, 0.5, 5", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var motion = motionInput.GetMotion();

                string validationError = motion.ValidateParameters();

                if (!string.IsNullOrEmpty(validationError))
                {
                    MessageBox.Show(validationError, "Ошибка ввода",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MoveAdded?.Invoke(this, new AddedCalculationMotion(motion));
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Заполните поля числами. Проверьте ввод.\n",
                    "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Неожиданная ошибка:\n{exception.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Проверка на правильность веденных значений, начинающихся с 0 
        /// </summary>
        /// <param name="controlInput">UserControl</param>
        /// <returns>true, если все значения валидны или поля пусты;
        /// false, если найдено значение с недопустимым ведущим нулём
        /// </returns>
        /// //TODO: refactor +
        private bool ValidateNoLeadingZeros(UserControl controlInput)
        {
            //TODO: RSDN +
            foreach (Control control in controlInput.Controls)
            {
                if (control is TextBox textBox)
                {
                    string text = textBox.Text.Trim();
                    if (!string.IsNullOrEmpty(text) && HasInvalidLeadingZero(text))
                    {
                        textBox.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        ///  Определяет, содержит ли строковое представление числа
        ///  некорректный ноль
        /// </summary>
        /// <param name="text">Строка для проверки</param>
        /// <returns>true, если строка начинается с '0'и следующий
        /// символ — цифра</returns>
        private bool HasInvalidLeadingZero(string text)
        {
            if (text == "0" || text.StartsWith("0."))
            { 
                return false; 
            }

            if (text.StartsWith("0") && text.Length > 1 &&
                char.IsDigit(text[1]))
            {
                return true;
            }

            return false;
        }
#if DEBUG
        /// <summary>
        /// Создаёт движение со случайными параметрами
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Событие</param>
        private void RandomButton_Click(object sender, EventArgs eventArgs)
        {
            var randomMotion = RandomMove.GetRandomMove();
            MoveAdded?.Invoke(this, 
                new AddedCalculationMotion(randomMotion));
        }
#endif
    }
}