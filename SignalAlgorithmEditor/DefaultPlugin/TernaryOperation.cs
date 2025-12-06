using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class TernaryOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length != 3)
                throw new ArgumentException("Тернарная операция требует три аргумента: условие, значение_если_истина, значение_если_ложь.");

            var condition = args[0];
            var trueExpr = args[1];
            var falseExpr = args[2];

            if (condition.Type != ParameterType.Boolean)
                throw new ArgumentException("Первый аргумент тернарной операции должен быть логическим.");

            if (trueExpr.Type != falseExpr.Type)
                throw new ArgumentException("Второй и третий аргументы тернарной операции должны иметь одинаковый тип.");

            Parameter selectedOperand;
            bool conditionValue = (int)condition.Value == 1;

            if (conditionValue)
                selectedOperand = trueExpr;
            else
                selectedOperand = falseExpr;

            int conditionStateForNumeric = condition.Type == ParameterType.Boolean && condition.State == 1
                ? 4 
                : condition.State;

            int resultState;
            if (selectedOperand.Type == ParameterType.Boolean)
            {
                resultState = condition.State | selectedOperand.State;
            }
            else
            {
                resultState = conditionStateForNumeric | selectedOperand.State;
            }

            return new Parameter(selectedOperand.Type, selectedOperand.Value, resultState);
        }
        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            return Execute(args); // игнорируем callKey и stateStore
        }
    }
}
