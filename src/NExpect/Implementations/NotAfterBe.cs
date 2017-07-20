using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class NotAfterBe<T>: Be<T>, INotAfterBe<T>
    {
        public NotAfterBe(T actual): base(actual)
        {
            Negate();
        }
    }
}