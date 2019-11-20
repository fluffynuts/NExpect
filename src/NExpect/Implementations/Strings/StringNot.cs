using NExpect.Implementations.Collections;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
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