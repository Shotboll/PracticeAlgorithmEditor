using Contract;
using SignalAlgorithmEditor.Models;

namespace SignalAlgorithmEditor.Services
{
    public class ExecutionContext
    {
        private readonly Dictionary<string, Parameter> _parameters = new Dictionary<string, Parameter>();

        public Parameter GetParameter(string name)
        {
            if(_parameters.TryGetValue(name, out var parameter))
                return parameter;

            return new Parameter(0, 0);
        }

        public void SetParameter(string name, Parameter parameter)
        {
            _parameters[name] = parameter;
        }

        public List<string> GetParameterNames()
        {
            return _parameters.Keys.ToList();
        }
    }
}
