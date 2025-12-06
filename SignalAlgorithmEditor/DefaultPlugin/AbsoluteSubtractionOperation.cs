using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class AbsoluteSubtractionOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length != 2)
                throw new ArgumentException("Функция ASUB требует два числовых аргумента.");

            var left = args[0];
            var right = args[1];

            if (left.Type == ParameterType.Boolean || right.Type == ParameterType.Boolean)
                throw new ArgumentException("ASUB применима только к числовым параметрам.");

            double leftVal = left.Type == ParameterType.Integer ? (int)left.Value : (float)left.Value;
            double rightVal = right.Type == ParameterType.Integer ? (int)right.Value : (float)right.Value;

            double resultValue = Math.Abs(leftVal - rightVal);
            int resultState = left.State | right.State;

            return new Parameter(ParameterType.Analog, (float)resultValue, resultState);
        }
    }
}
