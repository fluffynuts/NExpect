using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringStart :
        ExpectationContext<string>,
        IHasActual<string>,
        IStringStart
    {
        public string Actual { get; }

        public StringStart(string actual)
        {
            Actual = actual;
        }
    }
}