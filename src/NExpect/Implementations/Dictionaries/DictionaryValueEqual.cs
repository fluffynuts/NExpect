using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries
{
    internal class DictionaryValueEqual<T>
        : ExpectationContext<T>,
          IHasActual<T>,
          IDictionaryValueEqual<T>
    {
        public T Actual { get; }

        public DictionaryValueEqual(T actual)
        {
            Actual = actual;
        }
    }
}