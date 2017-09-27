using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringEnd :
        ExpectationContext<string>,
        IHasActual<string>,
        IStringEnd
    {
        public string Actual { get; }

        public StringEnd(string actual)
        {
            Actual = actual;
        }
    }
}