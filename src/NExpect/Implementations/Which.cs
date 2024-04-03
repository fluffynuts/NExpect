using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class Which<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IWhich<T>
{
    public IIs<T> Is => Next<Is<T>>();

    public Which(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}
