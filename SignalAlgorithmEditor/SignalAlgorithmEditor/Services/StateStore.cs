using Contract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalAlgorithmEditor.Services
{
    public class StateStore : IStateStore
    {
        private readonly ConcurrentDictionary<string, object> _store = new();

        public object GetState(string key) => _store.TryGetValue(key, out var val) ? val : null!;

        public void SetState(string key, object state) => _store[key] = state;

        public bool TryGetState<T>(string key, out T value)
        {
            if (_store.TryGetValue(key, out var obj) && obj is T t)
            {
                value = t;
                return true;
            }
            value = default;
            return false;
        }
    }
}
