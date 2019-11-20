using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency
{
    internal class Deep<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IDeep<T>
    {
        public T Actual { get; }

        public IDeepEqual<T> Equal 
            => ContinuationFactory.Create<T, DeepEqual<T>>(Actual, this);

        public Deep(T actual)
        {
            Actual = actual;
        }
    }
}