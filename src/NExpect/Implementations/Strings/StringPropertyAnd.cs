using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Strings
{
    internal class StringPropertyAnd
        : ExpectationContext<string>,
            IStringPropertyContinuation
    {
        public string Actual { get; }

        public IStringPropertyNot Not
            => ContinuationFactory.Create<string, StringPropertyNot>(() => Actual, this);

        public IStringPropertyContinuation And
            => ContinuationFactory.Create<string, StringPropertyAnd>(Actual, this);

        public IEqualityContinuation<string> Equal
            => ContinuationFactory.Create<string, EqualityContinuation<string>>(Actual, this);

        public IStringPropertyStartingContinuation Starting
            => ContinuationFactory.Create<string, StringPropertyContinuation>(Actual, this);

        public IStringPropertyEndingContinuation Ending
            => ContinuationFactory.Create<string, StringPropertyContinuation>(Actual, this);

        public StringPropertyAnd(string actual)
        {
            Actual = actual;
        }
    }
}