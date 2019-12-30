using Imported.PeanutButter.Utils;
using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries
{
    internal class DictionaryValueIntersection<T>
        : ExpectationContext<T>,
          IHasActual<T>,
          IDictionaryValueIntersection<T>
    {
        public T Actual { get; }

        public IDictionaryValueEqual<T> Equal
            => ContinuationFactory.Create<T, DictionaryValueEqual<T>>(
                Actual,
                this,
                SetIntersectionFlag
            );

        private void SetIntersectionFlag(DictionaryValueEqual<T> continuation)
        {
            continuation.SetMetadata(
                DictionaryValueEqual<T>.DICTIONARY_VALUE_DEEP_EQUALITY_TESTING,
                false
            );
        }

        public DictionaryValueIntersection(T actual)
        {
            Actual = actual;
        }
    }
}