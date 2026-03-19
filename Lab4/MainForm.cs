using System.ComponentModel;
using System.Xml.Serialization;
using Model;

namespace Lab4
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly BindingList<MotionBase> _moveCollection = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly BindingList<MotionBase> _filteredMoveCollection = new();

        /// <summary>
        /// 
        /// </summary>
        private readonly string[] _moveNames =
        {
            "Равномерное движение",
            "Равноускоренное движение",
            "Колебательное движение"
        };

        /// <summary>
        /// 
        /// </summary>
        private readonly MotionBase[] _moveTypes =
        {
            new UniformMotion(),
            new UniformlyAcceleratedMotion(),
            new OscillatoryMotion()
        };

        /// <summary>
        /// 
        /// </summary>
        private readonly BindingSource _bindingSource = new();
        private readonly XmlSerializer _serializer =
            new XmlSerializer(typeof(BindingList<MotionBase>));

        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeMoveFilter();
            InitializeDataGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeMoveFilter()
        {
            MoveCheckedListBox.Items.AddRange(_moveNames);
            for (int i = 0; i < MoveCheckedListBox.Items.Count; i++)
                MoveCheckedListBox.SetItemChecked(i, true);
            MoveCheckedListBox.ItemCheck += MoveCheckedListBox_ItemCheck;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeDataGrid()
        {
            CalculationDataGridView.AutoGenerateColumns = false;
            CalculationDataGridView.DataSource = _bindingSource;
            _bindingSource.DataSource = _filteredMoveCollection;

            CreateBaseColumns();
            SyncExtraColumns();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                FilteredDataGrid(e);
                RefreshExtraColumnValues();
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void FilteredDataGrid(ItemCheckEventArgs e)
        {
            _filteredMoveCollection.Clear();

            foreach (var move in _moveCollection)
            {
                if (IsMotionVisible(move))
                {
                    _filteredMoveCollection.Add(move);
                }
            }
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private bool IsMotionVisible(MotionBase move)
        {
            for (int index = 0; index < MoveCheckedListBox.Items.Count; index++)
            {
                if (MoveCheckedListBox.GetItemChecked(index) &&
                    move.GetType() == _moveTypes[index].GetType())
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshExtraColumnValues()
        {
            foreach (DataGridViewRow row in CalculationDataGridView.Rows)
            {
                if (row.DataBoundItem is MotionBase motion)
                {
                    FillExtraCells(row, motion);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="motion"></param>
        private void FillExtraCells(DataGridViewRow row, MotionBase motion)
        {
            var extraNames = motion.GetExtraColumnNames().ToList();
            var extraValues = motion.GetExtraColumnValues().ToList();

            for (int i = 0; i < extraNames.Count && i < extraValues.Count; i++)
            {
                var columnName = extraNames[i];
                if (CalculationDataGridView.Columns.Contains(columnName))
                {
                    row.Cells[columnName].Value = extraValues[i];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateBaseColumns()
        {
            var baseColumns = new[]
            {
                new { Property = "Name", Header = "Название", Width = 150 },
                new { Property = "InitialPosition", Header = "Начальная координата, м", Width = 120 },
                new { Property = "Speed", Header = "Скорость, м/с", Width = 100 },
                new { Property = "Time", Header = "Время, с", Width = 100 },
                new { Property = "Coordinate", Header = "Координата, м", Width = 120 }
            };

            foreach (var col in baseColumns)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = col.Property,
                    HeaderText = col.Header,
                    Name = col.Property,
                    Width = col.Width,
                    ReadOnly = true
                };
                CalculationDataGridView.Columns.Add(column);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SyncExtraColumns()
        {
            var extraColumnNames = _moveCollection
                .SelectMany(m => m.GetExtraColumnNames())
                .Distinct()
                .ToList();

            foreach (var columnName in extraColumnNames)
            {
                if (CalculationDataGridView.Columns[columnName] == null)
                {
                    var column = new DataGridViewTextBoxColumn
                    {
                        Name = columnName,
                        HeaderText = columnName,
                        DataPropertyName = string.Empty,
                        Width = 120,
                        ReadOnly = true
                    };
                    CalculationDataGridView.Columns.Add(column);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DefaultFilter()
        {
            _filteredMoveCollection.Clear();

            foreach (var move in _moveCollection)
            {
                if (IsMotionVisible(move))
                {
                    _filteredMoveCollection.Add(move);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMovementButton_Click(object sender, EventArgs e)
        {
            var formAdd = new CalculateMotion { MoveAdded = CalculateMotion_MotionAdded };
            formAdd.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateMotion_MotionAdded(object sender, AddedCalculationMotion e)
        {
            _moveCollection.Add(e.Motion);
            SyncExtraColumns();
            DefaultFilter();
            RefreshExtraColumnValues();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveMovementButton_Click(object sender, EventArgs e)
        {
            var selectedRows = CalculationDataGridView.SelectedRows
               .Cast<DataGridViewRow>()
               .ToList();

            if (selectedRows.Count == 0 && CalculationDataGridView.CurrentRow != null)
            {
                selectedRows.Add(CalculationDataGridView.CurrentRow);
            }

            if (selectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку(и) для удаления.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var motionsToDelete = selectedRows
                .Select(row => row.DataBoundItem as MotionBase)
                .Where(m => m != null)
                .ToList();

            foreach (var motion in motionsToDelete)
            {
                _moveCollection.Remove(motion);
            }

            DefaultFilter();
            RefreshExtraColumnValues();
            RemoveUnusedExtraColumns();

            if (motionsToDelete.Count > 1)
            {
                MessageBox.Show($"Удалено строк: {motionsToDelete.Count}",
                    "Удаление завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Загрузка списка фигур из файла
        /// </summary>
        private void LoadToolStripMenuItemClick(object sender, EventArgs e)
        {

            var openFileDialog = new OpenFileDialog
            {
                Filter = "Файлы (*.elmt)|*.elmt|Все файлы (*.*)|*.*",
                Title = "Загрузка данных о движениях"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                using var stream = new FileStream(openFileDialog.FileName, FileMode.Open);
                var loadedList = (BindingList<MotionBase>)_serializer.Deserialize(stream);

                for (int i = 0; i < loadedList.Count; i++)
                {
                    var m = loadedList[i];
                    if (m.Time < 0 || m.Speed < 0 || m.InitialPosition < 0 ||
                        (m is OscillatoryMotion osc && osc.Frequency < 0))
                    {
                        throw new IncorrectArgumentException(
                            $"Некорректные данные в файле.\n\n" +
                            $"Отрицательными параметрами не могут быть:\n" +
                            $"• Время\n" +
                            $"• Скорость\n" +
                            $"• Начальная координата\n" +
                            $"• Частота (для колебательного движения)");
                    }
                }

                _moveCollection.Clear();
                foreach (var motion in loadedList)
                    _moveCollection.Add(motion);

                SyncExtraColumns();
                RemoveUnusedExtraColumns();
                DefaultFilter();
                RefreshExtraColumnValues();
                _bindingSource.ResetBindings(false);

                MessageBox.Show($"Файл успешно загружен.\nЗаписей: {_moveCollection.Count}",
                    "Загрузка завершена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IncorrectArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка в данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                _moveCollection.Clear();
                DefaultFilter();
                RemoveUnusedExtraColumns();
                _bindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке файла:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _moveCollection.Clear();
                DefaultFilter();
                RemoveUnusedExtraColumns();
                _bindingSource.ResetBindings(false);
            }
        }

        /// <summary>
        /// Сохранение списка фигур в файл
        /// </summary>
        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
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
                using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    _serializer.Serialize(stream, _moveCollection);

                    MessageBox.Show("Файл успешно сохранён.", "Сохранение завершено",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении файла:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удаляет дополнительные колонки, которые больше не нужны
        /// </summary>
        private void RemoveUnusedExtraColumns()
        {
            var requiredExtraColumns = _moveCollection
                .SelectMany(m => m.GetExtraColumnNames())
                .Distinct()
                .ToHashSet();

            var baseColumnNames = new[] { "Name", "InitialPosition", "Speed", "Time", "Coordinate" };

            for (int i = CalculationDataGridView.Columns.Count - 1; i >= 0; i--)
            {
                var column = CalculationDataGridView.Columns[i];

                if (baseColumnNames.Contains(column.DataPropertyName))
                    continue;
                if (requiredExtraColumns.Contains(column.Name))
                    continue;
                CalculationDataGridView.Columns.Remove(column);
            }
        }
    }
    
}



