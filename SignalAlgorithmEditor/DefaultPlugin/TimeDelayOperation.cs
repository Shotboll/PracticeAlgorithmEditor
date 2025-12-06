using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class TimeDelayOperation : IOperation
    {
        public Parameter Execute(Parameter[] args) =>
            throw new NotSupportedException("TD требует callKey и IStateStore.");

        public Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore)
        {
            if (args.Length != 2)
                throw new ArgumentException("TD требует два аргумента: сигнал и время задержки.");

            var signalArg = args[0];
            var timeArg = args[1];

            double timeMs;
            if (timeArg.Type == ParameterType.Integer)
                timeMs = Convert.ToDouble(timeArg.Value);
            else if (timeArg.Type == ParameterType.Analog)
                timeMs = Convert.ToDouble(timeArg.Value);
            else
                throw new ArgumentException("Второй аргумент TD должен быть числовым.");

            bool signalTrue;
            int signalState;

            if (signalArg.Type == ParameterType.Boolean)
            {
                signalTrue = Convert.ToInt32(signalArg.Value) == 1;
                signalState = signalArg.State;
            }
            else if (signalArg.Type == ParameterType.Integer || signalArg.Type == ParameterType.Analog)
            {
                double val = Convert.ToDouble(signalArg.Value);
                signalTrue = val != 0;
                signalState = signalArg.State;
            }
            else
            {
                throw new ArgumentException("Первый аргумент TD должен быть логическим или числовым (0/1).");
            }

            var now = DateTime.UtcNow;
            var state = stateStore.TryGetState<TdState>(callKey, out var s) ? s : new TdState();

            Parameter result;
            if (signalTrue)
            {
                var trueSince = state.LastFalseTime == DateTime.MinValue
                    ? now
                    : state.LastTrueTime == DateTime.MinValue
                        ? now
                        : state.LastTrueTime;

                if ((now - trueSince).TotalMilliseconds >= timeMs)
                {
                    result = new Parameter(ParameterType.Boolean, 1, signalState);
                }
                else
                {
                    result = new Parameter(ParameterType.Boolean, 0, signalState);
                    state.LastTrueTime = trueSince;
                }
            }
            else
            {
                result = new Parameter(ParameterType.Boolean, 0, signalState);
                state.LastFalseTime = now;
                state.LastTrueTime = DateTime.MinValue;
            }

            state.LastResult = result;
            stateStore.SetState(callKey, state);

            return result;
        }

        private class TdState
        {
            public DateTime LastTrueTime { get; set; } = DateTime.MinValue;
            public DateTime LastFalseTime { get; set; } = DateTime.MinValue;
            public Parameter LastResult { get; set; }
        }
    }
}
