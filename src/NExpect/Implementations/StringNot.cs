using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringNot
        : Not<string>,
            IStringNot
    {
        public StringNot(string actual): base(actual)
        {
        }

        public IStringStart Start =>
            Factory.Create<string, StringStart>(Actual, this);

        public IStringEnd End =>
            Factory.Create<string, StringEnd>(Actual, this);

        public new IStringToAfterNot To =>
            Factory.Create<string, StringToAfterNot>(Actual, this);
    }
}