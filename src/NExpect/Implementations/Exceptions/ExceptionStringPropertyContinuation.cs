using System;
using NExpect.Implementations.Fluency;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Exceptions;

// ReSharper disable once ClassNeverInstantiated.Global
internal class ExceptionStringPropertyContinuation
    : Be<string>,
      IStringPropertyContinuation
{
    public new IStringPropertyNot Not => Next<StringPropertyNot>();
    public IStringPropertyContinuation And => Next<StringPropertyAnd>();
    public IStringPropertyStartingContinuation Starting => Next<StringPropertyContinuation>();
    public IStringPropertyEndingContinuation Ending => Next<StringPropertyContinuation>();

    public ExceptionStringPropertyContinuation(Func<string> actualFetcher) : base(actualFetcher)
    {
    }
}