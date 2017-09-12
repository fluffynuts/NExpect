using NExpect.Interfaces;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class ContainAt<T>
        : ExpectationContext<T>, IContainAt<T>
    {
        T Actual { get; }
        public ContainAt(T actual)
        {
            Actual = actual;
        }
    }
}