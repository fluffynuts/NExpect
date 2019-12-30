using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries
{
    internal class DictionaryValueDeep<T>
        : ExpectationContext<T>,
          IHasActual<T>,
          IDictionaryValueDeep<T>
    {
        public T Actual { get; }

        public IDictionaryValueEqual<T> Equal
            => ContinuationFactory.Create<T, DictionaryValueEqual<T>>(Actual, this);

        public DictionaryValueDeep(T actual)
        {
            Actual = actual;
        }
    }
}