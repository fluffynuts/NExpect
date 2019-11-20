using NExpect.Interfaces;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Collections
{
    internal class ContainAt<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IContainAt<T>
    {
        public T Actual { get; }

        public ContainAt(T actual)
        {
            Actual = actual;
        }
    }
}