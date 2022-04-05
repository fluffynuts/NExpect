using System;
using NExpect.Interfaces;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency;

internal class A<T> : 
    ExpectationContextWithLazyActual<T>, 
    IHasActual<T>,
    IA<T>
{
    public A(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}