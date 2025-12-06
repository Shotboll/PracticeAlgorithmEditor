using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class NaturalLogOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args?.Length != 1)
                throw new ArgumentException("LN требует один числовой аргумент.");

            var arg = args[0];
            if (arg.Type == ParameterType.Boolean)
                throw new ArgumentException("LN применим только к числовым параметрам.");

            double val = arg.Type == ParameterType.Integer ? (int)arg.Value : (float)arg.Value;
            int state = arg.State;

            float resultValue;
            if (val <= 0)
            {
                resultValue = 0;
                state |= 4;
            }
            else
            {
                resultValue = (float)Math.Log(val);
            }

            return new Parameter(ParameterType.Analog, resultValue, state);
        }
        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            return Execute(args); // игнорируем callKey и stateStore
        }
    }
}
