using NExpect.Implementations.Collections;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringTo :
        To<string>,
        IStringTo
    {
        public IStringStart Start =>
            ContinuationFactory.Create<string, StringStart>(Actual, this);

        public IStringEnd End =>
            ContinuationFactory.Create<string, StringEnd>(Actual, this);

        public new IStringNotAfterTo Not =>
            ContinuationFactory.Create<string, StringNotAfterTo>(Actual, this);

        public new IStringBe Be =>
            ContinuationFactory.Create<string, StringBe>(Actual, this);

        public new IStringContain Contain =>
            ContinuationFactory.Create<string, StringContain>(Actual, this);

        public StringTo(string actual) : base(actual)
        {
        }
    }
}