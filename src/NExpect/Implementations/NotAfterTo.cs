using NExpect.Interfaces;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class NotAfterTo<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        INotAfterTo<T>
    {
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

        public T Actual { get; }

        public NotAfterTo(T actual)
        {
            Actual = actual;
            // ReSharper disable once VirtualMemberCallInConstructor
            Negate();
        }
    }
}