using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal sealed class Not<T>: ExpectationContext<T>, INot<T>
    {
        public T Actual { get; }
        public IToAfterNot<T> To => Factory.Create<T, ToAfterNot<T>>(Actual, this);

        public Not(T actual)
        {
            Actual = actual;
            Negate();
        }
    }
}