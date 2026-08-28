using System;
using Imported.PeanutButter.Utils;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class Only<T>
    : ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        IOnly<T>
{
    public Only(Func<T> actualFetcher)
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