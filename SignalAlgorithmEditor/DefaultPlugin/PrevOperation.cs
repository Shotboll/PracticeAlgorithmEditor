using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class PrevOperation : IOperation
    {
        public Parameter Execute(Parameter[] args) =>
            throw new NotSupportedException("PREV требует callKey и IStateStore.");

        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            if (args.Length != 1)
                throw new ArgumentException("PREV требует один аргумент.");

            var current = args[0];

            if (!stateStore.TryGetState<Parameter>(callKey, out var prev))
            {
                prev = new Parameter(current.Type, GetDefaultValue(current.Type), 0);
            }

            stateStore.SetState(callKey, current);

            return prev;
        }

        private object GetDefaultValue(ParameterType type) => type switch
        {
            ParameterType.Boolean => 0,
            ParameterType.Integer => 0,
            ParameterType.Analog => 0f,
            _ => 0
        };
    }
}
