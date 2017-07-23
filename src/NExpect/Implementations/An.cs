using NExpect.Interfaces;

namespace NExpect.Implementations
{
    public class An<T> : ExpectationContext<T>, IAn<T>
    {
        public object Actual { get; }

        public An(object actual)
        {
            Actual = actual;
        }
    }
}