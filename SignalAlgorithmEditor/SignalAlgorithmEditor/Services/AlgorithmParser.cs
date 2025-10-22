using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalAlgorithmEditor.Services
{
    public class AlgorithmParser
    {
        private readonly Dictionary<string, IOperation> _operations;

        public AlgorithmParser(Dictionary<string, IOperation> operations)
        {
            _operations = operations;
        }

        public Parameter Evaluate(string algorithm, ExecutionContext context)
        {
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(algorithm);

            var parser = new Parser(tokens);
            var ast = parser.Parse();

            var evaluator = new Evaluator(context, _operations);
            var result = evaluator.Evaluate(ast);

            return result;
        }
    }
}
