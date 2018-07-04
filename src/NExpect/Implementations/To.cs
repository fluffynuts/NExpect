using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeProtected.Global

namespace NExpect.Implementations
{
    internal class To<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        ITo<T>
    {
        public T Actual { get; }
        public IBe<T> Be => ContinuationFactory.Create<T, Be<T>>(Actual, this);
        public IContain<T> Contain =>
            ContinuationFactory.Create<T, Contain<T>>(Actual, this);
        public INotAfterTo<T> Not => 
            ContinuationFactory.Create<T, NotAfterTo<T>>(Actual, this);
        public IHave<T> Have => 
            ContinuationFactory.Create<T, Have<T>>(Actual, this);
        public IDeep<T> Deep => 
            ContinuationFactory.Create<T, Deep<T>>(Actual, this);
        public IIntersection<T> Intersection => 
            ContinuationFactory.Create<T, Intersection<T>>(Actual, this);

        public To(T actual)
        {
            Actual = actual;
        }
    }
}