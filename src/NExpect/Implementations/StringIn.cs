using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringIn
        : ExpectationContext<string>,
            IHasActual<string>,
            ICanAddMatcher<string>,
            IStringIn
    {
        public string Actual { get; }
        public StringIn(string actual)
        {
            Actual = actual;
        }
    }
}