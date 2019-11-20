using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency
{
    internal class Intersection<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IIntersection<T>
    {
        public T Actual { get; }

        public IIntersectionEqual<T> Equal
            => ContinuationFactory.Create<T, IntersectionEqual<T>>(Actual, this);

        public Intersection(T actual)
        {
            Actual = actual;
        }
    }
}