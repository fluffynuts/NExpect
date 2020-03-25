using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Strings
{
    internal class StringPropertyNot
        : ExpectationContextWithLazyActual<string>, IStringPropertyNot
    {
        public IToAfterNot<string> To
            => ContinuationFactory.Create<string, ToAfterNot<string>>(ActualFetcher, this);

        public IStringPropertyEndingContinuation Ending
            => ContinuationFactory.Create<string, StringPropertyContinuation>(ActualFetcher, this);

        public IStringPropertyEndingContinuation Starting
            => ContinuationFactory.Create<string, StringPropertyContinuation>(ActualFetcher, this);

        public StringPropertyNot(Func<string> actualFetcher)
            : base(actualFetcher)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Negate();
        }
    }
}