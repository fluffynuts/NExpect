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
            Factory.Create<T, Be<T>>(Actual, this);

        public IDeep<T> Deep =>
            Factory.Create<T, Deep<T>>(Actual, this);

        public IIntersection<T> Intersection =>
            Factory.Create<T, Intersection<T>>(Actual, this);

        public T Actual { get; }

        public NotAfterTo(T actual)
        {
            Actual = actual;
            // ReSharper disable once VirtualMemberCallInConstructor
            Negate();
        }
    }
}