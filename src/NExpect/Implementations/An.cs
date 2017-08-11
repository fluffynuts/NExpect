using NExpect.Interfaces;

namespace NExpect.Implementations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class An<T> : ExpectationContext<T>, IAn<T>
    {
        public object Actual { get; }

        public An(object actual)
        {
            Actual = actual;
        }
    }
}