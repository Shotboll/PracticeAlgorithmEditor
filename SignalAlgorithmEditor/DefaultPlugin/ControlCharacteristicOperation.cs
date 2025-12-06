using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class ControlCharacteristicOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length != 1)
                throw new ArgumentException("Функция CHS требует один числовой аргумент (код характеристики).");

            // Заглушка: возвращаем фиксированное значение 0
            return new Parameter(ParameterType.Analog, 0f, 0);
        }
        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            return Execute(args); // игнорируем callKey и stateStore
        }
    }
}
