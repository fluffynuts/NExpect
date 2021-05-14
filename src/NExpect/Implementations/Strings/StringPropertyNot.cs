using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Strings
{
    internal class StringPropertyNot
        : ExpectationContextWithLazyActual<string>, IStringPropertyNot
    {
        public IToAfterNot<string> To => Next<ToAfterNot<string>>();
        public IStringPropertyEndingContinuation Ending => Next<StringPropertyContinuation>();
        public IStringPropertyEndingContinuation Starting => Next<StringPropertyContinuation>();

        public StringPropertyNot(Func<string> actualFetcher)
            : base(actualFetcher)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Negate();
        }
    }
}