using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringTo :
        To<string>,
        IStringTo
    {
        public IStringStart Start =>
            Factory.Create<string, StringStart>(Actual, this);

        public IStringEnd End =>
            Factory.Create<string, StringEnd>(Actual, this);

        public new IStringNotAfterTo Not =>
            Factory.Create<string, StringNotAfterTo>(Actual, this);

        public new IStringBe Be =>
            Factory.Create<string, StringBe>(Actual, this);

        public IStringContain Contain =>
            Factory.Create<string, StringContain>(Actual, this);

        public StringTo(string actual) : base(actual)
        {
        }
    }
}