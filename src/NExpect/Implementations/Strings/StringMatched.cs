using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringMatched :
        ExpectationContext<string>,
        IStringMatched,
        IHasActual<string>
    {
        public string Actual { get; }

        public StringMatched(string actual)
        {
            Actual = actual;
        }
    }
}