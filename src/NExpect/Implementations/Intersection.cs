using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class Intersection<T>:
        ExpectationContext<T>,
        IIntersection<T>
    {
        public T Actual { get; }
        public Intersection(T actual)
        {
            Actual = actual;
        }
    }
}