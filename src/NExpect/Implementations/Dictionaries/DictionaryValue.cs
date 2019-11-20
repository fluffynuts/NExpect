using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class DictionaryValue<T>
        : ExpectationContext<T>,
          IHasActual<T>,
          IDictionaryValue<T>
    {
        public T Actual { get; }
        public IDictionaryValueDeep<T> Deep 
            => ContinuationFactory.Create<T, DictionaryValueDeep<T>>(Actual, this);

        public DictionaryValue(T actual)
        {
            Actual = actual;
        }
    }
}