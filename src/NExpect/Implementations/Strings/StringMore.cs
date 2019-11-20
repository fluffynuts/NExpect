using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringMore :
        ExpectationContext<string>,
        IHasActual<string>,
        IStringMore
    {
        public string Actual { get; }

        public IStringAnd And =>
            ContinuationFactory.Create<string, StringAnd>(Actual, this);

        public StringMore(string actual)
        {
            Actual = actual;
        }
    }
}