using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class UnaryMinusOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length != 1)
                throw new ArgumentException("Унарный '-' требует ровно один аргумент.");

            var operand = args[0];

            if (operand.Type == ParameterType.Boolean)
                throw new ArgumentException("Унарный '-' применим только к числовым параметрам (A, C).");

            object resultValue = operand.Type switch
            {
                ParameterType.Integer => -(int)operand.Value,
                ParameterType.Analog => -(float)operand.Value,
                _ => throw new InvalidOperationException($"Неподдерживаемый числовой тип: {operand.Type}")
            };

            return new Parameter(operand.Type, resultValue, operand.State);
        }
    }
}
