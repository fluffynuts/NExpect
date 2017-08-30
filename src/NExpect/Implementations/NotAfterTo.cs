using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class NotAfterTo<T>: ExpectationContext<T>, INotAfterTo<T>
    {
        public T Actual { get; }
        public NotAfterTo(T actual)
        {
            Actual = actual;
            // ReSharper disable once VirtualMemberCallInConstructor
            Negate();
        }

        public IBe<T> Be =>
            Factory.Create<T, Be<T>>(Actual, this);     
    }
}   