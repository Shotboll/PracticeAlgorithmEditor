using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public class Parameter
    {
        public ParameterType Type { get; set; }
        public object Value { get; set; }
        public int State { get; set; }

        public Parameter() { }

        public Parameter(ParameterType type, object value, int state = 0)
        {
            Type = type;
            Value = value;
            State = state;
        }

        // Вспомогательное свойство для логических параметров
        public bool BoolValue
        {
            get => Type == ParameterType.Boolean && Convert.ToInt32(Value) == 1;
            set
            {
                if (Type != ParameterType.Boolean)
                    throw new InvalidOperationException("Параметр не является логическим.");
                Value = value ? 1 : 0;
            }
        }

        // Вспомогательное свойство для числовых значений
        public double NumericValue
        {
            get
            {
                return Type switch
                {
                    ParameterType.Boolean => Convert.ToInt32(Value),
                    ParameterType.Integer => Convert.ToInt32(Value),
                    ParameterType.Analog => Convert.ToDouble(Value),
                    _ => throw new InvalidOperationException($"Неподдерживаемый тип: {Type}")
                };
            }
        }
    }

    public enum ParameterType
    {
        Analog,
        Integer,
        Boolean
    }
}
