using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalAlgorithmEditor.Models
{
    public class Signal
    {
        public string Code { get; set; } = string.Empty;
        public string Algorithm { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;

        public Signal() { }

        public Signal(string code, string algorithm, string format)
        {
            Code = code;
            Algorithm = algorithm;
            Format = format;
        }
    }
}
