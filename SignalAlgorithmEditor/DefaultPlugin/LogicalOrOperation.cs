using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class LogicalOrOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if(args == null || args.Length != 2)
                throw new ArgumentException("Операция '|' требует ровно два аргумента.");
            
            var left = args[0];
            var right = args[1];

            if(left.Type != ParameterType.Boolean || right.Type != ParameterType.Boolean)
                throw new ArgumentException("Операция '|' применима только к логическим параметрам.");

            bool v1 = Convert.ToInt32(left.Value) == 1;
            bool v2 = Convert.ToInt32(right.Value) == 1;

            bool resultValue = v1 || v2;

            int e1 = left.State;
            int e2 = right.State;

            int notV1 = v1 ? 0 : 1;
            int notV2 = v2 ? 0 : 1;

            int resultState = (e1 | e2) & (e1 | notV1) & (e2 | notV2);

            return new Parameter(ParameterType.Boolean, resultValue ? 1 : 0, resultState);
        }
        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            return Execute(args); // игнорируем callKey и stateStore
        }
    }
}
