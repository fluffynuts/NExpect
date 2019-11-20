using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Fluency
{
    internal class ToAfterNot<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IToAfterNot<T>
    {
        public T Actual { get; }
        public IBe<T> Be => 
            ContinuationFactory.Create<T, Be<T>>(Actual, this);
        public IContain<T> Contain => 
            ContinuationFactory.Create<T, Contain<T>>(Actual, this);
        public IHave<T> Have => 
            ContinuationFactory.Create<T, Have<T>>(Actual, this);
        public IDeep<T> Deep => 
            ContinuationFactory.Create<T, Deep<T>>(Actual, this);
        public IIntersection<T> Intersection => 
            ContinuationFactory.Create<T, Intersection<T>>(Actual, this);

        public ToAfterNot(T actual)
        {
            Actual = actual;
        }

    }
}