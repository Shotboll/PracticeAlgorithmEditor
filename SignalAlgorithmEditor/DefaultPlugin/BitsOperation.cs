using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class BitsOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length < 2 || args.Length > 3)
                throw new ArgumentException("Функция BITS требует 2 или 3 числовых аргумента.");

            var exp = args[0];
            var msbParam = args[1];
            var lsbParam = args.Length == 3 ? args[2] : null;

            int value = exp.Type switch
            {
                ParameterType.Integer => (int)exp.Value,
                ParameterType.Analog => (int)(float)exp.Value,
                _ => throw new ArgumentException("Первый аргумент BITS должен быть числовым.")
            };

            int msb = GetBitIndex(msbParam, "msb");
            int lsb = lsbParam != null ? GetBitIndex(lsbParam, "lsb") : msb;

            if (msb < 0 || lsb < 0 || msb < lsb)
                throw new ArgumentException("Некорректный диапазон битов: msb >= lsb >= 0.");

            int resultState = exp.State;

            if (args.Length == 2)
            {
                bool isBitSet = (value & (1 << msb)) != 0;
                return new Parameter(ParameterType.Boolean, isBitSet ? 1 : 0, resultState);
            }
            else
            {
                uint mask = 0;
                for (int i = lsb; i <= msb; i++)
                    mask |= (1u << i);

                int extracted = (value & (int)mask) >> lsb;
                return new Parameter(ParameterType.Integer, extracted, resultState);
            }
        }

        private static int GetBitIndex(Parameter p, string name)
        {
            if (p.Type == ParameterType.Boolean)
                throw new ArgumentException($"{name} должен быть числовым.");

            double val = p.Type == ParameterType.Integer ? (int)p.Value : (float)p.Value;
            if (val < 0 || val != Math.Floor(val))
                throw new ArgumentException($"{name} должен быть неотрицательным целым числом.");

            return (int)val;
        }
    }
}
