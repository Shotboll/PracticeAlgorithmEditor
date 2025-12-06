using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class LogicalNotOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if(args.Length != 1)
                throw new ArgumentException("Операция '!' требует ровно один аргумент.");

            var operand = args[0];
            if(operand.Type != ParameterType.Boolean)
                throw new ArgumentException("Операция '!' применима только к логическим параметрам.");

            bool resultValue = !(Convert.ToInt32(operand.Value) == 1);
            int resultState = operand.State;

            return new Parameter
            {
                Type = ParameterType.Boolean,
                Value = resultValue ? 1 : 0,
                State = resultState
            };
        }
        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            return Execute(args); // игнорируем callKey и stateStore
        }
    }
}
