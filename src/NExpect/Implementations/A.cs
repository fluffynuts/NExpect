using NExpect.Interfaces;

namespace NExpect.Implementations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class A<T> : ExpectationContext<T>, IA<T>
    {
        public object Actual { get; }

        public A(object actual)
        {
            Actual = actual;
        }
    }
}