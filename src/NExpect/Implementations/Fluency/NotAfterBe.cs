using System;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency;

internal sealed class NotAfterBe<T> : 
    Be<T>, 
    INotAfterBe<T>
{
    public NotAfterBe(Func<T> actualFetcher)
        : base(actualFetcher)
    {
        Negate();
    }
}