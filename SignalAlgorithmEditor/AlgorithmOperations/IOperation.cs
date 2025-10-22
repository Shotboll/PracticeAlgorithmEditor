namespace Contract
{
    public interface IOperation
    {
        Parameter Execute(Parameter[] args);
    }
}
