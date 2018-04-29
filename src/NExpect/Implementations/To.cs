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
        public IBe<T> Be => Factory.Create<T, Be<T>>(Actual, this);
        public IContain<T> Contain =>
            Factory.Create<T, Contain<T>>(Actual, this);
        public INotAfterTo<T> Not => 
            Factory.Create<T, NotAfterTo<T>>(Actual, this);
        public IHave<T> Have => 
            Factory.Create<T, Have<T>>(Actual, this);
        public IDeep<T> Deep => 
            Factory.Create<T, Deep<T>>(Actual, this);
        public IIntersection<T> Intersection => 
            Factory.Create<T, Intersection<T>>(Actual, this);

        public To(T actual)
        {
            Actual = actual;
        }
    }
}