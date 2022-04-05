using System;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class WithPropertyName<T> : With<T>, IWithPropertyName<T>
{
    public string PropertyName { get; set; }

    public WithPropertyName(
        Func<T> actualFetcher
    ) : base(actualFetcher)
    {
    }
}