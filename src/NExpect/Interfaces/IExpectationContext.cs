namespace NExpect.Interfaces
{
    internal interface IExpectationContext<T> : IExpectationParentContext<T>
    {
        IExpectationContext<T> Parent { get; set; }
    }
}