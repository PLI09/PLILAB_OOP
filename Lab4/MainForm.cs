using System.ComponentModel;
using System.Xml.Serialization;
using Model;

namespace Lab4
{
    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Основной список всех "движений"
        /// </summary>
        private BindingList<MotionBase> _moveCollection
            = new BindingList<MotionBase>();

        /// <summary>
        /// Отфильтрованый список "движений"
        /// </summary>
        private BindingList<MotionBase> _fileredMoveCollection
            = new BindingList<MotionBase>();

        /// <summary>
        /// Названия "Движений"
        /// </summary>
        private string[] _moveNames =
        {
            "Равномерное движение",
            "Равноускоренное движение",
            "Колебательное движение"
        };

        private MotionBase[] _moveTypes = {new UniformMotion(),
                new UniformlyAcceleratedMotion(), new OscillatoryMotion() };

        /// <summary>
        /// Источник информации для списка рассчитаных координат
        /// </summary>
        private BindingSource _bindingSource = new BindingSource();

        /// <summary>
        /// XML-сериализатор для сохранения и загрузки рассчитаных координат
        /// </summary>
        private readonly XmlSerializer _serializer =
            new XmlSerializer(typeof(BindingList<MotionBase>));

        /// <summary>
        /// Конструктор главной формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
            MoveCheckedListBox.Items.AddRange(_moveNames);
            for (int i = 0; i < MoveCheckedListBox.Items.Count; i++)
            {
                MoveCheckedListBox.SetItemChecked(i, true);
            }
            MoveCheckedListBox.ItemCheck +=
                new ItemCheckEventHandler(MoveCheckedListBox_ItemCheck);

            _bindingSource.DataSource = _fileredMoveCollection;
            CalculationDataGridView.DataSource = _bindingSource;

            SetupRussianHeaders();
        }

        /// <summary>
        /// Метод для установки русских заголовков колонок
        /// </summary>
        private void SetupRussianHeaders()
        {
            CalculationDataGridView.AutoGenerateColumns = true;

            var russianHeaders = new Dictionary<string, string>
            {
                { "Name", "Название" },
                { "MotionType", "Тип движения" },
                { "InitialPosition", "Начальная координата, м" },
                { "Speed", "Скорость, м/с" },
                { "Time", "Время, с" },
                { "Coordinate", "Координата, м" },
                { "Frequency", "Частота, рад/с" },
                { "Acceleration", "Ускорение, м/с²" }
            };

            foreach (DataGridViewColumn column in 
                CalculationDataGridView.Columns)
            {
                if (russianHeaders.ContainsKey(column.DataPropertyName))
                {
                    column.HeaderText = 
                        russianHeaders[column.DataPropertyName];
                }
            }
        }

        /// <summary>
        /// Обрабока кнопки добавления расчета
        /// </summary>
        /// <param name="sender">источник события</param>
        /// <param name="e">данные события</param>
        private void AddMovementButton_Click(object sender, EventArgs e)
        {
            var formAdd = new CalculateMotion();
            formAdd.MoveAdded = CalculateMotion_MotionAdded;
            formAdd.ShowDialog();
        }

        /// <summary>
        /// Добавление рассчитанной координаты
        /// </summary>
        /// <param name="sender">источник события</param>
        /// <param name="obj">данные события</param>
        private void CalculateMotion_MotionAdded
            (object sender, EventArgs obj)
        {
            _moveCollection.Add
                ((obj as AddedCalculationMotion).MovementParameters);
            DefaultFilter();
        }

        /// <summary>
        /// Фильтр таблицы расчетов при изменении выбора движения
        /// </summary>
        /// <param name="sender">источник события</param>
        /// <param name="e">состояние галочки</param>
        private void MoveCheckedListBox_ItemCheck
            (object sender, ItemCheckEventArgs e)
        {
            FilteredDataGrid(e);
        }

        /// <summary>
        /// Фильтр таблицы расчетов
        /// </summary>
        /// <param name="e">состояние галочки</param>
        private void FilteredDataGrid(ItemCheckEventArgs e)
        {
            _fileredMoveCollection.Clear();

            foreach (MotionBase move in _moveCollection)
            {
                AddItemsInFilreredList(e, move);
            }
        }

        /// <summary>
        /// Добавление отфильтрованных данных
        /// </summary>
        /// <param name="e">состояние галочки</param>
        /// <param name="move">тип движения</param>
        private void AddItemsInFilreredList(ItemCheckEventArgs e,
                                            MotionBase move)
        {
            for (int index = 0; index < MoveCheckedListBox.Items.Count; 
                index++)
            {
                if (MoveCheckedListBox.GetItemChecked(e.Index))
                {
                    if (index != e.Index
                        && move.GetType() == _moveTypes[index].GetType()
                        && MoveCheckedListBox.GetItemChecked(index))
                    {
                        _fileredMoveCollection.Add(move);
                    }
                }
                else
                {
                    if (move.GetType() == _moveTypes[e.Index].GetType()
                            && index == e.Index)
                    {
                        _fileredMoveCollection.Add(move);
                    }

                    if (index != e.Index
                        && move.GetType() == _moveTypes[index].GetType()
                        && MoveCheckedListBox.GetItemChecked(index))
                    {
                        _fileredMoveCollection.Add(move);
                    }
                }
            }
        }

        /// <summary>
        /// Фильтр, при котором не происходит действий с MoveCheckedListBox
        /// </summary>
        private void DefaultFilter()
        {
            _fileredMoveCollection.Clear();

            foreach (MotionBase move in _moveCollection)
            {
                for (int index = 0; index < MoveCheckedListBox.Items.Count; 
                    index++)
                {
                    if (MoveCheckedListBox.GetItemChecked(index)
                        && move.GetType() == _moveTypes[index].GetType())
                    {
                        _fileredMoveCollection.Add(move);
                    }
                }
            }
        }

        /// <summary>
        /// Удалить расчет
        /// </summary>
        /// <param name="sender">кнопка удалить расчет</param>
        /// <param name="e">данные о кнопке, которая нажата</param>
        private void RemoveMovementButton_Click(object sender, EventArgs e)
        {
            if (CalculationDataGridView.CurrentRow?.DataBoundItem is 
                MotionBase motion)
            {
                _moveCollection.Remove(motion);
                DefaultFilter();
            }
        }

        /// <summary>
        /// Открыть файл
        /// </summary>
        /// <param name="sender">кнопка открыть</param>
        /// <param name="e">данные о кнопке, которая нажата</param>
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Файлы (*.elmt)|*.elmt|Все файлы (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                using (var stream = new FileStream
                    (openFileDialog.FileName, FileMode.Open))
                {
                    var loadedList = (BindingList<MotionBase>)
                        _serializer.Deserialize(stream);

                    _moveCollection.Clear();
                    foreach (var figure in loadedList)
                    {
                        _moveCollection.Add(figure);
                    }
                    DefaultFilter();
                    CalculationDataGridView.DataSource = 
                        _fileredMoveCollection;
                    MessageBox.Show("Файл успешно загружен.", 
                        "Загрузка завершена",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.InnerException == null
                    ? ex.Message
                    : ex.InnerException.Message;
                MessageBox.Show("Ошибка при загрузке файла:\n" + 
                    exceptionMessage,"Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="sender">кнопка сохранить как</param>
        /// <param name="e">данные о кнопке, котора нажата</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_moveCollection.Count == 0)
            {
                MessageBox.Show("Нет данных для сохранения.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Файлы (*.elmt)|*.elmt|Все файлы (*.*)|*.*",
                AddExtension = true,
                DefaultExt = ".elmt"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                using (var stream = new FileStream(saveFileDialog.FileName, 
                    FileMode.Create))
                {
                    _serializer.Serialize(stream, _moveCollection);

                    MessageBox.Show("Файл успешно сохранён.", 
                        "Сохранение завершено",MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении файла:\n" + 
                    ex.Message,"Ошибка", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }

        }
    }
}
