namespace Contract
{
    public interface IOperation
    {
        Parameter Execute(Parameter[] args);

        Parameter Execute(Parameter[] args, string callKey, IStateStore stateStore);
    }

    public interface IStateStore
    {
        object GetState(string key);
        void SetState(string key, object state);
        bool TryGetState<T>(string key, out T value);
    }
}
