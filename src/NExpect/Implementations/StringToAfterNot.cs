using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringToAfterNot
        : ToAfterNot<string>,
            IStringToAfterNot
    {
        public IStringStart Start =>
            Factory.Create<string, StringStart>(Actual, this);

        public IStringEnd End =>
            Factory.Create<string, StringEnd>(Actual, this);

        public StringToAfterNot(string actual) : base(actual)
        {
        }

    }
}