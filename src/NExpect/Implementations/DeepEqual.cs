using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class DeepEqual<T>
        : ExpectationContext<T>, 
            IDeepEqual<T>
    {
        T Actual { get; }

        public DeepEqual(T actual)
        {
            Actual = actual;
        }
    }
}