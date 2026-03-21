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
        /// Основная коолекция для всех объектов движения
        /// </summary>
        private readonly BindingList<MotionBase> _moveCollection = new();

        /// <summary>
        /// Отфильтрованная коллекция для отображения в GridView
        /// </summary>
        private readonly BindingList<MotionBase> 
            _filteredMoveCollection = new();

        /// <summary>
        /// Массив отображаемых названий типов движений для CheckedListBox
        /// </summary>
        private readonly string[] _moveNames =
        {
            "Равномерное движение",
            "Равноускоренное движение",
            "Колебательное движение"
        };

        /// <summary>
        ///  Массив экземпляров типов движений для сопоставления с фильтрами
        /// </summary>
        private readonly MotionBase[] _moveTypes =
        {
            new UniformMotion(),
            new UniformlyAcceleratedMotion(),
            new OscillatoryMotion()
        };

        /// <summary>
        /// Источник данных для привязки к DataGridView
        /// </summary>
        private readonly BindingSource _bindingSource = new();

        /// <summary>
        /// Сериализатор XML для сохранения и загрузки коллекции движений
        /// </summary>
        private readonly XmlSerializer _serializer = 
            new XmlSerializer(typeof(BindingList<MotionBase>));

        /// <summary>
        /// Конструктор главной формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeMoveFilter();
            InitializeDataGrid();
        }

        /// <summary>
        /// Метод для фильтрации типов движения
        /// </summary>
        private void InitializeMoveFilter()
        {
            MoveCheckedListBox.Items.AddRange(_moveNames);
            for (int i = 0; i < MoveCheckedListBox.Items.Count; i++)
            {
                MoveCheckedListBox.SetItemChecked(i, true);
            }

            MoveCheckedListBox.ItemCheck += MoveCheckedListBox_ItemCheck;
        }

        /// <summary>
        /// Метод для настройки DataGridView для отображения 
        /// данных о движениях
        /// </summary>
        private void InitializeDataGrid()
        {
            {
                CalculationDataGridView.AutoGenerateColumns = false;
                CalculationDataGridView.DataSource = _bindingSource;
                _bindingSource.DataSource = _filteredMoveCollection;
                CalculationDataGridView.CellFormatting +=
                    CalculationDataGridView_CellFormatting;

                CreateBaseColumns();
                ApplyFilter();
            }
        }

        /// <summary>
        /// Метод для создания и добавления базовых колонок в DataGridView
        /// </summary>
        private void CreateBaseColumns()
        {
            CalculationDataGridView.Columns.Clear();

            var nameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Название движения",
                Name = "Name",
                Width = 180,
                ReadOnly = true,
                DefaultCellStyle = 
                { 
                    WrapMode = DataGridViewTriState.True 
                }
            };
            CalculationDataGridView.Columns.Add(nameColumn);

            var paramsColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Parameters",
                HeaderText = "Параметры",
                Name = "Parameters",
                Width = 350,
                ReadOnly = true,
                DefaultCellStyle = 
                { 
                    WrapMode = DataGridViewTriState.True 
                }
            };
            CalculationDataGridView.Columns.Add(paramsColumn);

            var coordColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Coordinate",
                HeaderText = "Рассчитанная координата, м",
                Name = "Coordinate",
                Width = 300,
                ReadOnly = true,
                DefaultCellStyle = 
                { 
                    Alignment = DataGridViewContentAlignment.MiddleRight 
                }
            };
            CalculationDataGridView.Columns.Add(coordColumn);
        }

        /// <summary>
        /// Метод для форматирования ячеек в DataGridView
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргументы события форматирования</param>
        private void CalculationDataGridView_CellFormatting(
            object sender, DataGridViewCellFormattingEventArgs eventArgs)
        {
            if (eventArgs.RowIndex < 0)
            {
                return;
            }

            var row = CalculationDataGridView.Rows[eventArgs.RowIndex];
            if (row.DataBoundItem is not MotionBase motion)
            {
                return;
            }

            if (CalculationDataGridView.
                Columns[eventArgs.ColumnIndex].Name == "Parameters")
            {
                eventArgs.Value = FormatMotionParameters(motion);
                eventArgs.FormattingApplied = true;
            }
            else if (CalculationDataGridView.
                Columns[eventArgs.ColumnIndex].Name == "Coordinate")
            {
                eventArgs.Value = FormatNumber(motion.Coordinate);
                eventArgs.FormattingApplied = true;
            }
        }

        /// <summary>
        /// Обработчик события изменения состояния флажка в CheckedListBox
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">События изменения флажка</param>
        private void MoveCheckedListBox_ItemCheck(
            object sender, ItemCheckEventArgs eventArgs)
        {
            BeginInvoke(ApplyFilterAndRefresh);
        }

        /// <summary>
        /// Применяет текущий фильтр к коллекции движений и обновляет данные
        /// </summary>
        private void ApplyFilterAndRefresh()
        {
            ApplyFilter();
            _bindingSource.ResetBindings(false);
        }

        /// <summary>
        /// Очищает отфильтрованную коллекцию и добавляет в неё только
        /// те движения, типы которых отмечены в CheckedListBox
        /// </summary>
        private void ApplyFilter()
        {
            _filteredMoveCollection.Clear();
            foreach (var move in _moveCollection)
            {
                if (IsMotionVisible(move))
                    _filteredMoveCollection.Add(move);
            }
        }

        /// <summary>
        /// Определяет, должно ли данное движение отображаться
        /// в фильтрованном списке
        /// </summary>
        /// <param name="move">Экземпляр движения для проверки</param>
        /// <returns>true, если тип движения отмечен в CheckedListBox,
        /// иначе false<returns>
        private bool IsMotionVisible(MotionBase move)
        {
            for (int index = 0; 
                index < MoveCheckedListBox.Items.Count; index++)
            {
                if (MoveCheckedListBox.GetItemChecked(index) &&
                    move.GetType() == _moveTypes[index].GetType())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Обновляет отображение данных в DataGridView
        /// </summary>
        private void RefreshGridData()
        {
            _bindingSource.ResetBindings(false);
        }

        /// <summary>
        /// Форматирует параметры движения в строку для отображения в сетке
        /// </summary>
        /// <param name="motion">Экземпляр движения</param>
        /// <returns>Отформатированная строка с параметрами 
        /// движения</returns>
        private string FormatMotionParameters(MotionBase motion)
        {
            var parts = new List<string>();

            parts.Add($"х0={FormatNumber(motion.InitialPosition)} м");
            parts.Add($"v={FormatNumber(motion.Speed)} м/с");
            parts.Add($"t={FormatNumber(motion.Time)} с");

            switch (motion)
            {
                case UniformlyAcceleratedMotion accelerated:
                {
                    parts.Add(
                        $"a={FormatNumber(accelerated.Acceleration)} м/с²");
                    break;
                }
                    
                case OscillatoryMotion oscillatory:
                {
                    parts.Add($"f={FormatNumber(oscillatory.Frequency)} Гц");
                    break;
                }
            }

            return string.Join(", ", parts);
        }

        /// <summary>
        /// Форматирует числовое значение для отображения в интерфейсе
        /// </summary>
        /// <param name="value">Числовое значение</param>
        /// <returns>Отформатированная строка</returns>
        private string FormatNumber(double value)
        {
            if (value == 0)
            {
                return "0";
            }

            return value.ToString("F2");
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Рассчитать"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Событие</param>
        private void AddMovementButton_Click(
            object sender, EventArgs eventArgs)
        {
            var formAdd = new CalculateMotion 
            { 
                MoveAdded = CalculateMotion_MotionAdded 
            };
            formAdd.ShowDialog();
        }

        /// <summary>
        /// Обработчик события добавления нового движения из формы
        /// CalculateMotion
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">событие, содержащие добавленное 
        /// движение</param>
        private void CalculateMotion_MotionAdded(
            object sender, AddedCalculationMotion eventArgs)
        {
            _moveCollection.Add(eventArgs.Motion);
            ApplyFilter();
            RefreshGridData();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Удалить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">событие</param>
        private void RemoveMovementButton_Click(
            object sender, EventArgs eventArgs)
        {
            var selectedRows = CalculationDataGridView.SelectedRows
                .Cast<DataGridViewRow>()
                .ToList();

            if (selectedRows.Count == 0 &&
                CalculationDataGridView.CurrentRow != null)
            {
                selectedRows.Add(CalculationDataGridView.CurrentRow);
            }

            if (selectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку(и) для удаления.", 
                    "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var motionsToDelete = selectedRows
                .Select(row => row.DataBoundItem as MotionBase)
                .Where(motion => motion != null)
                .ToList();

            foreach (var motion in motionsToDelete)
            {
                _moveCollection.Remove(motion);
            }

            ApplyFilter();
            RefreshGridData();

            if (motionsToDelete.Count > 1)
            {
                MessageBox.Show($"Удалено строк: {motionsToDelete.Count}",
                    "Удаление завершено",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Обработчик пункта меню "Загрузить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Событие</param>
        private void LoadToolStripMenuItemClick(
            object sender, EventArgs eventArgs)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Файлы (*.elmt)|*.elmt|Все файлы (*.*)|*.*",
                Title = "Загрузка данных о движениях"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                var stream = new FileStream
                    (openFileDialog.FileName, FileMode.Open);
                var loadedList = (BindingList<MotionBase>)
                    _serializer.Deserialize(stream);
                stream.Close();

                var errors = new List<string>();
                var validMotions = new List<MotionBase>();

                for (int i = 0; i < loadedList.Count; i++)
                {
                    var motion = loadedList[i];
                    var error = motion?.ValidateParameters();

                    if (string.IsNullOrEmpty(error))
                    {
                        validMotions.Add(motion);
                    }
                    else
                    {
                        errors.Add($"Запись #{i + 1} " +
                            $"({motion?.Name ?? "Неизвестный тип"}):" +
                            $"\n{error}");
                    }
                }

                if (errors.Count > 0)
                {
                    var result = MessageBox.Show(
                        $"Обнаружено некорректных записей: {errors.Count} " +
                        $"из {loadedList.Count}\n\n" +
                        string.Join("\n\n", errors.Take(errors.Count)) +
                        (errors.Count > 5 ? $"\n\n... и ещё " +
                        $"{errors.Count}" : "") +
                        $"\n\nЗагрузить только корректные данные " +
                        $"({validMotions.Count} записей)?",
                        "Ошибка валидации данных",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Cancel)
                    {
                        _moveCollection.Clear();
                        ApplyFilter();
                        RefreshGridData();
                        return;
                    }
                    if (result == DialogResult.No)
                    {
                        validMotions = loadedList.ToList();
                    }                
                }
                else
                {
                    validMotions = loadedList.ToList();
                }

                _moveCollection.Clear();
                foreach (var motion in validMotions)
                {
                    _moveCollection.Add(motion);
                }

                ApplyFilter();
                RefreshGridData();

                MessageBox.Show(
                    $"Файл загружен.\nУспешно: " +
                    $"{validMotions.Count}\nОтклонено: {errors.Count}",
                    "Загрузка завершена",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (IncorrectArgumentException exception)
            {
                MessageBox.Show($"Ошибка в данных: {exception.Message}",
                    "Ошибка валидации", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _moveCollection.Clear();
                ApplyFilter();
                RefreshGridData();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка при загрузке файла:" +
                    $"\n{exception.Message}", 
                    "Ошибка", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                _moveCollection.Clear();
                ApplyFilter();
                RefreshGridData();
            }
        }

        /// <summary>
        /// Обработчик пункта меню "Сохранить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Событие</param>
        private void SaveToolStripMenuItemClick(
            object sender, EventArgs eventArgs)
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
                using var stream = new FileStream(
                    saveFileDialog.FileName, FileMode.Create);
                _serializer.Serialize(stream, _moveCollection);

                MessageBox.Show("Файл успешно сохранён.", 
                    "Сохранение завершено",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка при сохранении файла:" +
                    $"\n{exception.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}