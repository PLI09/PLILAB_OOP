using Lab4.MotionControls;

namespace Lab4
{
    /// <summary>
    /// Форма для расчета параметров движения и добаления результата
    /// в основное окно
    /// </summary>
    public partial class CalculateMotion : Form
    {
        /// <summary>
        /// Событие, возникающее при успешном расчете движения
        /// </summary>
        public EventHandler<AddedCalculationMotion> MoveAdded;

        /// <summary>
        /// Возвращает текущий выбранный пользовательский элемент управления
        /// для ввода параметров движения
        /// </summary>
        private UserControl _moveControl => _baseMove[TypeMoveComboBox.Text];

        /// <summary>
        /// Словарь, сопоставляющий название типа движения с соответствующим 
        /// пользовательским контролом
        /// </summary>
        private Dictionary<string, UserControl> _baseMove = 
            new Dictionary<string, UserControl>()
        {
            {"Равномерное движение", new UniformMotionControl()},
            {"Равноускоренное движение", new AcceleratedMotionControl()},
            {"Колебательное движение", new OscillatoryMotionControl()}
        };

        /// <summary>
        /// Конструктор формы
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
                TypeMoveComboBox.SelectedIndex = 0;

            СalculateButton.Enabled = true;

            #if(!DEBUG)
            RandomButton.Visible = false;
            #endif
        }

        /// <summary>
        /// Выбор типа движения
        /// </summary>
        /// <param name="sender">источник события</param>
        /// <param name="e">данные события</param>
        private void TypeMoveComboBox_SelectedIndexChanged
            (object sender, EventArgs e)
        {
            foreach (var control in _baseMove.Values)
                control.Visible = false;
            
            _moveControl.Visible = true;
        }

        /// <summary>
        /// Обработчик кнопки "Расчет"
        /// </summary>
        /// <param name="sender">источник события</param>
        /// <param name="e">данные события</param>
        private void СalculateButton_Click(object sender, EventArgs e)
        {
            var current = _moveControl;

            dynamic dyn = current;
            bool isValid = dyn.ValidateInput();

            if (!isValid)
            {
                MessageBox.Show("Заполните все поля корректно.",
                    "Ошибка ввода", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                var move = ((IMove)current).MovementParameters;
                MoveAdded?.Invoke(this, new AddedCalculationMotion(move));
            }
            catch
            {
                MessageBox.Show("Ошибка при расчете данных:" +
                    "\nОдин из параметров не задан","Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Рандомный расчет"
        /// </summary>
        /// <param name="sender">источник события</param>
        /// <param name="e">данные события</param>
        private void RandomButton_Click(object sender, EventArgs e)
        {
            MoveAdded?.Invoke(this,
                new AddedCalculationMotion(RandomMove.GetRandomMove()));
        }
    }
}
