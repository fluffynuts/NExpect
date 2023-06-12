using System;
using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class Without<T>
    : With<T>,
      IWithout<T>
{
    public Without(Func<T> actualFetcher)
        : base(actualFetcher)
    {
    }
}
