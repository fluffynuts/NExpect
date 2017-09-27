using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringBe :
        Be<string>,
        IStringBe
    {
        public IStringMatched Matched =>
            Factory.Create<string, StringMatched>(Actual, this);

        public StringBe(string actual)
            : base(actual)
        {
        }
    }
}