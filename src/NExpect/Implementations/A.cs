using NExpect.Interfaces;

namespace NExpect.Implementations
{
    public class A<T> : ExpectationContext<T>, IA<T>
    {
        public object Actual { get; }

        public A(object actual)
        {
            Actual = actual;
        }
    }
}