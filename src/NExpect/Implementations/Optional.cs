using System;
using Imported.PeanutButter.Utils;
using NExpect.Interfaces;

namespace NExpect.Implementations;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Optional<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IOptional<T>
{
    public Optional(Func<T> actualFetcher)
        : base(SetOptionalOn(actualFetcher))
    {
    }

    private static Func<T> SetOptionalOn(Func<T> fetcher)
    {
        var result = fetcher();
        result.SetMetadata(
            ReflectiveExtensions.METADATA_KEY_EXPECT_OPTIONAL,
            true
        );
        return fetcher;
    }
}