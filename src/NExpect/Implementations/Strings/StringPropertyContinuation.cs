using System;
using NExpect.Interfaces;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings;

internal class StringPropertyContinuation
    : ExpectationContextWithLazyActual<string>,
      IStringPropertyContinuation,
      IStringPropertyStartingContinuation,
      IStringPropertyEndingContinuation
{
    public IStringPropertyContinuation And => Next<StringPropertyAnd>();
    public IEqualityContinuation<string> Equal => Next<EqualityContinuation<string>>();
    public IStringIn In => Next<StringIn>();
    public IStringPropertyNot Not => Next<StringPropertyNot>();
    public IStringPropertyEndingContinuation Ending => Next<StringPropertyContinuation>();
    public IStringPropertyStartingContinuation Starting => Next<StringPropertyContinuation>();

    public StringPropertyContinuation(Func<string> actualFetcher)
        : base(actualFetcher)
    {
    }
}