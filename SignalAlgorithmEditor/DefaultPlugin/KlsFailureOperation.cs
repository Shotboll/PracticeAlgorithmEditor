using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class KlsFailureOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length != 1)
                throw new ArgumentException("Операция типа 8 требует один аргумент (код шины/абонента).");

            // Заглушка: всегда возвращаем "исправен"
            return new Parameter(ParameterType.Boolean, 0, 0);
        }
        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            return Execute(args); // игнорируем callKey и stateStore
        }
    }
}
