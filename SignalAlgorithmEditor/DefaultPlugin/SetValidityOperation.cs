using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class SetValidityOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length != 2)
                throw new ArgumentException("Функция SVLD требует два аргумента: exp и valid.");

            var exp = args[0];
            var valid = args[1];

            if (valid.Type != ParameterType.Boolean)
                throw new ArgumentException("Второй аргумент SVLD (valid) должен быть логическим.");

            object resultValue = exp.Value;
            ParameterType resultType = exp.Type;

            int resultState;

            bool isValidFlag = (int)valid.Value == 1;
            bool isVldReliable = (valid.State == 0);

            if (isValidFlag && isVldReliable)
            {
                resultState = 0;
            }
            else
            {
                resultState = exp.Type == ParameterType.Boolean ? 1 : 4;
            }

            return new Parameter(resultType, resultValue, resultState);
        }
    }
}
