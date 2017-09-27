using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringMore :
        ExpectationContext<string>,
        IHasActual<string>,
        IStringMore
    {
        public string Actual { get; }

        public IStringAnd And =>
            Factory.Create<string, StringAnd>(Actual, this);

        public StringMore(string actual)
        {
            Actual = actual;
        }
    }
}