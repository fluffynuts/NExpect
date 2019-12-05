using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency
{
    internal class DeepEqual<T>
        : ExpectationContext<T>, 
            IDeepEqual<T>,
            IHasActual<T>
    {
        public T Actual { get; }

        public DeepEqual(T actual)
        {
            Actual = actual;
        }
    }
}