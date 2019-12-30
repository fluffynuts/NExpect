using NExpect.Interfaces;

namespace NExpect.Implementations.Dictionaries
{
    internal class DictionaryValueEqual<T>
        : ExpectationContext<T>,
          IHasActual<T>,
          IDictionaryValueEqual<T>
    {
        public const string DICTIONARY_VALUE_DEEP_EQUALITY_TESTING = "__dictionary_value_deep_equality_testing__";
        public T Actual { get; }

        public DictionaryValueEqual(T actual)
        {
            Actual = actual;
        }
    }
}