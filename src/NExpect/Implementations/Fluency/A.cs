using NExpect.Interfaces;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency
{
    internal class A<T> : 
        ExpectationContext<T>, 
        IHasActual<T>,
        IA<T>
    {
        public T Actual { get; }

        public A(T actual)
        {
            Actual = actual;
        }
    }
}