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

    }

    public enum ParameterType
    {
        Analog,
        Integer,
        Boolean
    }
}
