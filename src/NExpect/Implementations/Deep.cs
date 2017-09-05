using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace NExpect.Implementations
{
    internal class Deep<T>
        : ExpectationContext<T>,
            IDeep<T>
    {
        public T Actual { get; }

        public Deep(T actual)
        {
            Actual = actual;
        }
    }
}