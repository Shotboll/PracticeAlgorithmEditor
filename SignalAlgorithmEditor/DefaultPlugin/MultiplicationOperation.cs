using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class MultiplicationOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length != 2)
                throw new ArgumentException("Операция '*' требует два аргумента.");

            var left = args[0];
            var right = args[1];

            if (left.Type == ParameterType.Boolean && right.Type == ParameterType.Boolean)
                throw new ArgumentException("Операция '*' требует, чтобы хотя бы один операнд был числовым.");

            double leftVal = ConvertToNumeric(left, out int leftState);
            double rightVal = ConvertToNumeric(right, out int rightState);

            double resultValue = leftVal * rightVal;
            int resultState = leftState | rightState;

            return new Parameter(ParameterType.Analog, (float)resultValue, resultState);
        }

        private double ConvertToNumeric(Parameter p, out int numericState)
        {
            numericState = p.State;

            if (p.Type == ParameterType.Boolean)
            {
                double val = (int)p.Value == 1 ? 1.0 : 0.0;
                if (p.State == 1)
                {
                    numericState = 4;
                }
                else
                {
                    numericState = 0;
                }
                return val;
            }
            else
            {
                return p.Type == ParameterType.Integer ? (int)p.Value : (float)p.Value;
            }
        }
    }
}
