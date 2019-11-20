using NExpect.Implementations.Collections;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringToAfterNot :
        ToAfterNot<string>,
        IStringToAfterNot
    {
        public IStringStart Start =>
            ContinuationFactory.Create<string, StringStart>(Actual, this);

        public IStringEnd End =>
            ContinuationFactory.Create<string, StringEnd>(Actual, this);

        public new IStringBe Be =>
            ContinuationFactory.Create<string, StringBe>(Actual, this);

        public StringToAfterNot(string actual)
            : base(actual)
        {
        }
    }
}