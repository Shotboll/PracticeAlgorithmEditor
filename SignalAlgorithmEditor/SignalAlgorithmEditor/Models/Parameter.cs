using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalAlgorithmEditor.Models
{
    public class Parameter
    {
        //Тип параметра A C D
        public ParameterType Type { get; set; }
        public object Value { get; set; }
        public int State { get; set; }

        public Parameter(bool value, int state = 0)
        {
            Type = ParameterType.Boolean;
            Value = value ? 1 : 0;
            State = state;
        }

        public Parameter(int value, int state = 0)
        {
            Type = ParameterType.Integer;
            Value = value;
            State = state;
        }

        public Parameter(float value, int state = 0)
        {
            Type = ParameterType.Analog;
            Value = value;
            State = state;
        }

        public bool BoolValue
        {
            get => (int)Value == 1;
            set => Value = value ? 1 : 0;
        }

        //Проверка является ли достоверным
        public bool IsReliable
        {
            get => Type == ParameterType.Boolean ? State == 0 : (State & 1) == 0;
        }
    }

    public enum ParameterType
    {
        Analog, // A
        Integer, // C
        Boolean // D
    }
}
