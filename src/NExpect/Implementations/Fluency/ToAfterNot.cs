using System;
using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations.Fluency;

internal class ToAfterNot<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      IToAfterNot<T>
{
    public IBe<T> Be => Next<Be<T>>();
    public IContain<T> Contain => Next<Contain<T>>();
    public IHave<T> Have => Next<Have<T>>();
    public IDeep<T> Deep => Next<Deep<T>>();
    public IIntersection<T> Intersection => Next<Intersection<T>>();
    public IApproximately<T> Approximately => Next<Approximately<T>>();
    public IFind<T> Find => Next<Find<T>>();
    public IRequire<T> Require => Next<Require<T>>();

    public ToAfterNot(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}