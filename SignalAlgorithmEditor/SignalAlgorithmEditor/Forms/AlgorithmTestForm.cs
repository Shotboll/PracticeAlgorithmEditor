using FastColoredTextBoxNS;
using SignalAlgorithmEditor.Models;
using SignalAlgorithmEditor.Services;
using System.Text.RegularExpressions;

namespace SignalAlgorithmEditor.Forms
{
    public partial class AlgorithmTestForm : Form
    {

        private Signal EditedSignal { get; set; } = new Signal();
        private readonly DatabaseService _dbService;
        private TextStyle? _operatorStyle;
        private TextStyle? _functionStyle;
        private TextStyle? _paramStyle;
        private TextStyle? _numberStyle;

        public AlgorithmTestForm(DatabaseService databaseService, Signal edSignal)
        {
            InitializeComponent();
            _dbService = databaseService;
            EditedSignal = edSignal;

            SetupAlgorithmHighlighting();

            fastColoredTextBox.Text = EditedSignal.Algorithm;
        }

        private void SetupAlgorithmHighlighting()
        {
            var operators = new[] { "+", "-", "*", "/", "^", "&", "|", "!", "#", "==", "!=", "<", ">", "<=", ">=", "?", ":" };
            var functions = new[] { "LN", "TG", "PLF", "SVLD", "VLD", "TD", "ASUB", "PREV", "BITS", "CHS" };

            _operatorStyle = new TextStyle(Brushes.Red, null, FontStyle.Bold);
            _functionStyle = new TextStyle(Brushes.Purple, null, FontStyle.Bold);
            _paramStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
            _numberStyle = new TextStyle(Brushes.Purple, null, FontStyle.Regular);

            fastColoredTextBox.AddStyle(_operatorStyle);
            fastColoredTextBox.AddStyle(_functionStyle);
            fastColoredTextBox.AddStyle(_paramStyle);
            fastColoredTextBox.AddStyle(_numberStyle);

            fastColoredTextBox.TextChanged += (sender, e) =>
            {
                fastColoredTextBox.Range.ClearStyle(_functionStyle, _numberStyle, _operatorStyle, _paramStyle);

                // Подсветка операторов
                foreach (string op in operators)
                {
                    fastColoredTextBox.Range.SetStyle(_operatorStyle, Regex.Escape(op));
                }

                // Подсветка функций
                foreach (string func in functions)
                {
                    fastColoredTextBox.Range.SetStyle(_functionStyle, $@"\b{Regex.Escape(func)}\b(?=\s*\()");
                }

                // Подсветка идентификаторов параметров (например, D161.11.11, V161.11.11)
                fastColoredTextBox.Range.SetStyle(_paramStyle, @"\b[A-Z]\d+(\.\d+)*\b");

                // Подсветка чисел (целые и с плавающей точкой)
                fastColoredTextBox.Range.SetStyle(_numberStyle, @"\b\d+(\.\d+)?\b");
            };
        }
    }
}
