using NExpect.Implementations.Collections;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringBe :
        Be<string>,
        IStringBe
    {
        public IStringMatched Matched =>
            ContinuationFactory.Create<string, StringMatched>(Actual, this);

        public StringBe(string actual)
            : base(actual)
        {
        }
    }
}