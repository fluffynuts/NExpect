using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency
{
    internal class IntersectionEqual<T>
        : ExpectationContext<T>,
            IIntersectionEqual<T>
    {
        public T Actual { get; }

        public IntersectionEqual(T actual)
        {
            Actual = actual;
        }
    }
}