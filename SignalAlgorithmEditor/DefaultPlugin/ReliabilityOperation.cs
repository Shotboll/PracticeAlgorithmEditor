using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class ReliabilityOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length != 1)
                throw new ArgumentException("Операция '#' требует ровно один аргумент.");

            var operand = args[0];

            if (operand.Type != ParameterType.Boolean)
                throw new ArgumentException("Операция '#' применима только к логическим параметрам.");

            bool isReliable = (operand.State == 0);
            int resultValue = isReliable ? 1 : 0;

            int resultState = 0;

            return new Parameter(ParameterType.Boolean, resultValue, resultState);
        }
        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            return Execute(args); // игнорируем callKey и stateStore
        }
    }
}
