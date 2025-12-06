using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class UnaryPlusOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length != 1)
                throw new ArgumentException("Унарный '+' требует ровно один аргумент.");

            var operand = args[0];

            if (operand.Type == ParameterType.Boolean)
                throw new ArgumentException("Унарный '+' применим только к числовым параметрам (A, C).");

            return new Parameter(operand.Type, operand.Value, operand.State);
        }
    }
}
