using System;
using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeProtected.Global

namespace NExpect.Implementations.Fluency;

internal class To<T>
    : ExpectationContextWithLazyActual<T>,
      IHasActual<T>,
      ITo<T>
{
    public IBe<T> Be => Next<Be<T>>();
    public IContain<T> Contain => Next<Contain<T>>();
    public INotAfterTo<T> Not => Next<NotAfterTo<T>>();
    public IHave<T> Have => Next<Have<T>>();
    public IDeep<T> Deep => Next<Deep<T>>();
    public IIntersection<T> Intersection => Next<Intersection<T>>();
    public IApproximately<T> Approximately => Next<Approximately<T>>();
    public IFind<T> Find => Next<Find<T>>();
    public IRequire<T> Require => Next<Require<T>>();

    public To(Func<T> actualFetcher) : base(actualFetcher)
    {
    }
}