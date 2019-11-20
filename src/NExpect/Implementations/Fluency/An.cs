using NExpect.Interfaces;

// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Fluency
{
    internal class An<T> : 
        ExpectationContext<T>, 
        IHasActual<T>,
        IAn<T>
    {
        public T Actual { get; }

        public An(T actual)
        {
            Actual = actual;
        }

        public IInstanceContinuation Instance => new InstanceContinuation(Actual.GetType(), this);
    }
}