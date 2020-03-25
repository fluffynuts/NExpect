using System;
using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Strings
{
    internal class StringPropertyAnd
        : ExpectationContextWithLazyActual<string>,
            IStringPropertyContinuation
    {
        public IStringPropertyNot Not
            => ContinuationFactory.Create<string, StringPropertyNot>(ActualFetcher, this);

        public IStringPropertyContinuation And
            => ContinuationFactory.Create<string, StringPropertyAnd>(ActualFetcher, this);

        public IEqualityContinuation<string> Equal
            => ContinuationFactory.Create<string, EqualityContinuation<string>>(ActualFetcher, this);

        public IStringPropertyStartingContinuation Starting
            => ContinuationFactory.Create<string, StringPropertyContinuation>(ActualFetcher, this);

        public IStringPropertyEndingContinuation Ending
            => ContinuationFactory.Create<string, StringPropertyContinuation>(ActualFetcher, this);

        public StringPropertyAnd(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}