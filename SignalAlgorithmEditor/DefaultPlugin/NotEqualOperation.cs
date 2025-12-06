using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class NotEqualOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args?.Length != 2)
                throw new ArgumentException("Операция '!=' требует два аргумента.");

            var left = args[0];
            var right = args[1];

            int resultState = left.State | right.State;
            bool result;

            if (left.Type == ParameterType.Boolean && right.Type == ParameterType.Boolean)
            {
                result = (int)left.Value != (int)right.Value;
            }
            else if (IsNumeric(left.Type) && IsNumeric(right.Type))
            {
                double lv = GetNumericValue(left);
                double rv = GetNumericValue(right);
                result = Math.Abs(lv - rv) >= 1e-5;
            }
            else
            {
                throw new ArgumentException("Операция '!=' требует, чтобы типы операндов совпадали.");
            }

            return new Parameter(ParameterType.Boolean, result ? 1 : 0, resultState);
        }
        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            return Execute(args); // игнорируем callKey и stateStore
        }

        private bool IsNumeric(ParameterType t) => t == ParameterType.Analog || t == ParameterType.Integer;
        private double GetNumericValue(Parameter p) => p.Type == ParameterType.Integer ? (int)p.Value : (float)p.Value;
    }
}
