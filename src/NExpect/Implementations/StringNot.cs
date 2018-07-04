using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringNot :
        Not<string>,
        IStringNot
    {
        public StringNot(string actual)
            : base(actual)
        {
        }

        public IStringStart Start =>
            ContinuationFactory.Create<string, StringStart>(Actual, this);

        public IStringEnd End =>
            ContinuationFactory.Create<string, StringEnd>(Actual, this);

        public new IStringToAfterNot To =>
            ContinuationFactory.Create<string, StringToAfterNot>(Actual, this);
    }
}