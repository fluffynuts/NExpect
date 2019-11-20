using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringContain
        : ExpectationContext<string>,
            IStringContain,
            IHasActual<string>
    {
        public string Actual { get; }
        public IStringIn In =>
            ContinuationFactory.Create<string, StringIn>(Actual, this);
        public StringContain(string actual)
        {
            Actual = actual;
        }
    }
}