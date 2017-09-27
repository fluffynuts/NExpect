using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringNotAfterTo :
        NotAfterTo<string>,
        IStringNotAfterTo
    {
        public IStringStart Start =>
            Factory.Create<string, StringStart>(Actual, this);

        public IStringEnd End =>
            Factory.Create<string, StringEnd>(Actual, this);

        public new IStringBe Be =>
            Factory.Create<string, StringBe>(Actual, this);

        public StringNotAfterTo(string actual)
            : base(actual)
        {
        }
    }
}