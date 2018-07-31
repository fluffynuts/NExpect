using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class Approximately<T>
        : ExpectationContext<T>,
          IApproximately<T>
    {
        public T Actual { get; set; }

        public Approximately(T actual)
        {
            Actual = actual;
        }
    }
}