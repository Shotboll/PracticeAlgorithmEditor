using SignalAlgorithmEditor.Forms;
using SignalAlgorithmEditor.Models;
using SignalAlgorithmEditor.Services;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SignalAlgorithmEditor
{
    public partial class MainForm : Form
    {
        private DatabaseService DBService;
        public MainForm()
        {
            DBService = new DatabaseService();
            InitializeComponent();
        }

        private void buttonLoadSignals_Click(object sender, EventArgs e)
        {
            string selectedFile = string.Empty;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Файлы Access (*.accdb)|*.accdb|Все файлы (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.Title = "Выберите файл базы данных";

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFile = dialog.FileName;
                }
            }

            var signals = DBService.LoadSignals(selectedFile);
            CreateDataGrid(signals);
        }

        private void buttonTestAlgorithm_Click(object sender, EventArgs e)
        {
            if(dataGridView.DataSource == null)
            {
                MessageBox.Show("Загрузите данные сигналов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridView.SelectedRows[0] != null)
            {
                var selectedrow = dataGridView.SelectedRows[0];
                var selectedSignal = (Signal)selectedrow.DataBoundItem;

                if (selectedSignal == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранного сигнала.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var editorForm = new AlgorithmTestForm(DBService, selectedSignal))
                {
                    var result = editorForm.ShowDialog();

                    if(result == DialogResult.OK)
                    {
                        dataGridView.Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сигнал", "Ошибка" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDataGrid(List<Signal> signals)
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.Columns.Clear();

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SignalCodeColumn",
                DataPropertyName = "Code",
                HeaderText = "Код сигнала",
                Width = 60,
                ReadOnly = true,
            });

            var algorithmColumn = new DataGridViewTextBoxColumn
            {
                Name = "AlgorithmColumn",
                DataPropertyName = "Algorithm",
                HeaderText = "Алгоритм",
                ReadOnly = true,
                DefaultCellStyle = { WrapMode = DataGridViewTriState.True }
            };

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FormatColumn",
                DataPropertyName = "Format",
                HeaderText = "Формат",
                Width = 60,
                ReadOnly = true
            });

            dataGridView.Columns.Add(algorithmColumn);

            algorithmColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView.DataSource = signals;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
        }
    }
}
