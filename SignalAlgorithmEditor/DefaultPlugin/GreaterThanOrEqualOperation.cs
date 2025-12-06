using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class GreaterThanOrEqualOperation : IOperation
    {
        private (double left, double right, int state) PrepareOperands(Parameter left, Parameter right)
        {
            if (left.Type == ParameterType.Boolean || right.Type == ParameterType.Boolean)
                throw new ArgumentException("Операции сравнения (> >= < <=) применимы только к числовым параметрам.");

            double leftVal = left.Type == ParameterType.Integer ? (int)left.Value : (float)left.Value;
            double rightVal = right.Type == ParameterType.Integer ? (int)right.Value : (float)right.Value;
            int resultState = left.State | right.State;

            return (leftVal, rightVal, resultState);
        }
        public Parameter Execute(Parameter[] args)
        {
            if (args?.Length != 2)
                throw new ArgumentException("Операция '>=' требует два числовых аргумента.");

            var (left, right, state) = PrepareOperands(args[0], args[1]);
            bool result = left >= right;
            return new Parameter(ParameterType.Boolean, result ? 1 : 0, state);
        }
        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            return Execute(args); // игнорируем callKey и stateStore
        }
    }
}
