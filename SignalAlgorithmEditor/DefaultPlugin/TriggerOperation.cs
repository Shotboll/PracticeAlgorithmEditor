using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class TriggerOperation : IOperation
    {
        public Parameter Execute(Parameter[] args) =>
            throw new NotSupportedException("TG требует callKey и IStateStore.");

        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            if (args.Length != 2)
                throw new ArgumentException("TG требует два логических аргумента: setCond, resetCond.");

            var setCond = args[0];
            var resetCond = args[1];

            if (setCond.Type != ParameterType.Boolean || resetCond.Type != ParameterType.Boolean)
                throw new ArgumentException("Оба аргумента TG должны быть логическими.");

            Parameter result;

            if ((int)setCond.Value == 1)
            {
                result = new Parameter(ParameterType.Boolean, 1, setCond.State);
            }
            else if ((int)resetCond.Value == 1)
            {
                int state = setCond.State | resetCond.State;
                result = new Parameter(ParameterType.Boolean, 0, state);
            }
            else
            {
                if (!stateStore.TryGetState<Parameter>(callKey, out result))
                {
                    result = new Parameter(ParameterType.Boolean, 0, 0);
                }
            }

            stateStore.SetState(callKey, result);

            return result;
        }
    }
}
