// ReSharper disable UnusedMemberInSuper.Global
namespace NExpect.Interfaces
{
    internal interface IExpectationContext
    {
        IExpectationContext Parent { get; }
    }

    internal interface IExpectationContext<T> : 
        IExpectationContext,
        IExpectationParentContext<T>
    {
        IExpectationContext<T> TypedParent { get; set; }
    }
}