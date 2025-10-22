using Contract;
using SignalAlgorithmEditor.Services;
using System.Data;
using System.Formats.Tar;
using System.Text.RegularExpressions;

namespace SignalAlgorithmEditor.Forms
{
    public partial class DebugForm : Form
    {
        private string Algorithm;
        private Services.ExecutionContext context; 
        public DebugForm(string algorithm)
        {
            InitializeComponent();
            Algorithm = algorithm;
            context = new Services.ExecutionContext();
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {
            CreateDataGrid();
        }
        
        private void buttonEvaluate_Click(object sender, EventArgs e)
        {
            FillContext();

            string exeDirectory = Path.GetDirectoryName(Application.ExecutablePath)!;
            string pluginsPath = Path.Combine(exeDirectory, "Plugins");

            var pluginLoader = new PluginLoader();
            var operations = pluginLoader.LoadOperations(pluginsPath);

            try
            {
                var parser = new AlgorithmParser(operations);
                var result = parser.Evaluate(Algorithm, context);

                MessageBox.Show($"Результат: Value = {result.Value}, State = {result.State}");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка вычисления: {ex.Message}");
            }
        }

        private void FillContext()
        {
            foreach (DataGridViewRow param in dataGridViewParams.Rows)
            {
                var type = DetermineType(param.Cells["TypeColumn"].Value.ToString()!);
                var parameter = new Parameter(type, param.Cells["ValueColumn"].Value.ToString()!, Convert.ToInt32(param.Cells["StateColumn"].Value));
                context.SetParameter(param.Cells["ParameterNameColumn"].Value.ToString()!, parameter);
            }
        }

        private ParameterType DetermineType(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentException("Имя параметра не может быть пустым.", nameof(type));
            return type switch
            {
                "A" => ParameterType.Analog,
                "C" => ParameterType.Integer,
                "D" => ParameterType.Boolean,
                _ => throw new ArgumentException($"Ожидались D, C, A.")
            };
        }

        private void CreateDataGrid()
        {
            var parameters = GetParameters(Algorithm!);

            dataGridViewParams.AutoGenerateColumns = false;
            dataGridViewParams.Columns.Clear();

            var column = new DataGridViewTextBoxColumn
            {
                Name = "ParameterNameColumn",
                DataPropertyName = "Parameter",
                HeaderText = "Параметр",
                ReadOnly = true,
            };

            dataGridViewParams.Columns.Add(column);
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewParams.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "TypeColumn",
                DataPropertyName = "Type",
                HeaderText = "Тип",
                Width = 80,
                ReadOnly = false,
                
                DataSource = new List<string>() { "A", "C", "D" },
            });

            dataGridViewParams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValueColumn",
                DataPropertyName = "Value",
                HeaderText = "Значение",
                Width = 80,
                ReadOnly = false
            });

            dataGridViewParams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StateColumn",
                DataPropertyName = "State",
                HeaderText = "Состояние",
                Width = 80,
                ReadOnly = false
            });

            dataGridViewParams.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridViewParams.AllowUserToAddRows = false;
            dataGridViewParams.AllowUserToDeleteRows = false;

            foreach (var param in parameters)
            {
                dataGridViewParams.Rows.Add(param, "D");
            }
        }

        private List<string> GetParameters(string algorithm)
        {
            var paramRegex = new Regex(@"\b[A-Z]\d+(\.\d+)*\b");
            var matches = paramRegex.Matches(algorithm);
            return matches.Cast<Match>().Select(m => m.Value).Distinct().ToList();
        }
    }
}
