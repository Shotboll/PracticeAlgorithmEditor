using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class DivisionOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length != 2)
                throw new ArgumentException("Операция '/' требует два аргумента.");

            var left = args[0];
            var right = args[1];

            if (left.Type == ParameterType.Boolean || right.Type == ParameterType.Boolean)
                throw new ArgumentException("Операция '/' применима только к числовым параметрам.");

            float leftVal = (float)(left.Type == ParameterType.Integer ? (int)left.Value : (float)left.Value);
            float rightVal = (float)(right.Type == ParameterType.Integer ? (int)right.Value : (float)right.Value);

            float resultValue;
            int resultState = left.State | right.State;

            if (rightVal == 0)
            {
                resultValue = 0;
                resultState |= 4;
            }
            else
            {
                resultValue = leftVal / rightVal;
            }

            return new Parameter(ParameterType.Analog, resultValue, resultState);
        }
    }
}
