using System;
using System.Collections.Generic;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

namespace NExpect.Implementations;

/// <summary>
/// Implements IMore&lt;T&gt;, provides a mechanism for extending
/// continuations when the extension should deviate from the original
/// type
/// </summary>
/// <typeparam name="T"></typeparam>
// ReSharper disable once ClassNeverInstantiated.Global
public class CollectionMore<T>
    : ExpectationContextWithLazyActual<IEnumerable<T>>,
      IHasActual<IEnumerable<T>>,
      ICollectionMore<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="actualFetcher"></param>
    public CollectionMore(Func<IEnumerable<T>> actualFetcher) : base(actualFetcher)
    {
        Assertions.Forget(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="actualFetcher"></param>
    /// <param name="negate"></param>
    public CollectionMore(Func<IEnumerable<T>> actualFetcher, bool negate) : base(actualFetcher, negate)
    {
        Assertions.Forget(this);
    }

    /// <inheritdoc />
    public ICollectionAnd<T> And => Next<CollectionAnd<T>>();

    /// <inheritdoc />
    public ICollectionHaving<T> Having => Next<CollectionHaving<T>>();
}