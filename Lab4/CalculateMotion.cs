using Lab4.MotionControls;
using Model;

namespace Lab4
{
    public partial class CalculateMotion : Form
    {
        public EventHandler<AddedCalculationMotion> MoveAdded;

        private UserControl _moveControl => _baseMove[TypeMoveComboBox.Text];

        private Dictionary<string, UserControl> _baseMove =
            new Dictionary<string, UserControl>()
        {
            {"Равномерное движение", new UniformMotionControl()},
            {"Равноускоренное движение", new AcceleratedMotionControl()},
            {"Колебательное движение", new OscillatoryMotionControl()}
        };

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

#if (!DEBUG)
            RandomButton.Visible = false;
#endif
        }

        private void TypeMoveComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var control in _baseMove.Values)
                control.Visible = false;
            _moveControl.Visible = true;
        }

        private void СalculateButton_Click(object sender, EventArgs e)
        {
            var current = _moveControl;
            if (current is not MotionControls.IMotionInput motionInput)
            {
                MessageBox.Show("Контрол не реализует IMotionInput", "Ошибка");
                return;
            }
            if (!motionInput.ValidateInput())
            {
                MessageBox.Show("Заполните все поля корректно.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var motion = motionInput.GetMotion();

                MoveAdded?.Invoke(this, new AddedCalculationMotion(motion));
            }
            catch (IncorrectArgumentException ex)
            {
                MessageBox.Show($"Ошибка в параметрах:\n{ex.Message}",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расчете:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RandomButton_Click(object sender, EventArgs e)
        {
            var randomMotion = RandomMove.GetRandomMove();
            MoveAdded?.Invoke(this, new AddedCalculationMotion(randomMotion));
        }
    }
}
