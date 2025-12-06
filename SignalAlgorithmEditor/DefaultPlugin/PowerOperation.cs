using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class PowerOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length != 2)
                throw new ArgumentException("Операция '^' требует два аргумента.");

            var left = args[0];
            var right = args[1];

            if (left.Type == ParameterType.Boolean || right.Type == ParameterType.Boolean)
                throw new ArgumentException("Операция '^' применима только к числовым параметрам.");

            double baseVal = left.Type == ParameterType.Integer ? (int)left.Value : (float)left.Value;
            double expVal = right.Type == ParameterType.Integer ? (int)right.Value : (float)right.Value;

            double resultValue;
            int resultState = left.State | right.State;

            try
            {
                if (baseVal == 0 && expVal == 0)
                {
                    resultValue = 0;
                    resultState |= 4;
                }
                else if (baseVal < 0 && expVal != Math.Floor(expVal))
                {
                    resultValue = 0;
                    resultState |= 4;
                }
                else
                {
                    resultValue = Math.Pow(baseVal, expVal);
                }
            }
            catch
            {
                resultValue = 0;
                resultState |= 4;
            }

            return new Parameter(ParameterType.Analog, (float)resultValue, resultState);
        }
    }
}
