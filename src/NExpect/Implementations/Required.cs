using System;
using Imported.PeanutButter.Utils;
using NExpect.Interfaces;

namespace NExpect.Implementations;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Required<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IRequired<T>
{
    public Required(Func<T> actualFetcher)
        : base(SetNotOptional(actualFetcher))
    {
    }

    private static Func<T> SetNotOptional(Func<T> fetcher)
    {
        var result = fetcher();
        result.SetMetadata(
            ReflectiveExtensions.METADATA_KEY_EXPECT_OPTIONAL,
            false
        );
        return fetcher;
    }
}