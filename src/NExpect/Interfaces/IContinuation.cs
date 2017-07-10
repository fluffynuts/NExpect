using System;

namespace NExpect
{
    internal interface IExpectationParentContext<T>
    {
        void Negate();
        void Expect(Func<T, string> expectation);
    }

    internal interface IExpectationContext<T> : IExpectationParentContext<T>
    {
        IExpectationContext<T> Parent { get; set; }
    }

    public interface IContinuation<T>
    {
        T Actual { get; }
    }
}