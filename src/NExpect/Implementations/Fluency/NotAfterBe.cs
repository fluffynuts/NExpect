using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency
{
    internal sealed class NotAfterBe<T> : 
        Be<T>, 
        INotAfterBe<T>
    {
        public NotAfterBe(T actual)
            : base(actual)
        {
            Negate();
        }
    }
}