using System;
using NExpect.Interfaces;

// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Fluency;

internal class An<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IAn<T>
{
    public IInstanceContinuation Instance => new InstanceContinuation(
        () => Actual?.GetType(),
        this);

    public An(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}