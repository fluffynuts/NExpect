using System;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Strings
{
    internal class StringPropertyAnd
        : ExpectationContextWithLazyActual<string>,
          IStringPropertyContinuation,
          // reset a prior negation, otherwise syntax like this fails:
          // Expect("foo")
          //   .To.Contain("o")
          //   .And.Not.To.Contain("x")
          //   .And.Not.To.Contain("y")
          IResetNegation
    {
        public IStringPropertyNot Not => Next<StringPropertyNot>();
        public IStringPropertyContinuation And => Next<StringPropertyAnd>();
        public IEqualityContinuation<string> Equal => Next<EqualityContinuation<string>>();
        public IStringPropertyStartingContinuation Starting => Next<StringPropertyContinuation>();
        public IStringPropertyEndingContinuation Ending => Next<StringPropertyContinuation>();

        public StringPropertyAnd(Func<string> actualFetcher) : base(actualFetcher)
        {
        }
    }
}