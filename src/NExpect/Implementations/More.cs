using System;
using NExpect.Implementations.Fluency;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    /// <summary>
    /// Implements IMore&lt;T&gt;, provides a mechanism for extending
    /// continuations when the extension should deviate from the original
    /// type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class More<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IMore<T>
    {
        /// <inheritdoc />
        public IAnd<T> And =>
            ContinuationFactory.Create<T, And<T>>(ActualFetcher, this);

        /// <inheritdoc />
        public IWith<T> With =>
            ContinuationFactory.Create<T, With<T>>(ActualFetcher, this);

        /// <inheritdoc />
        public IOf<T> Of =>
            ContinuationFactory.Create<T, Of<T>>(ActualFetcher, this);

        /// <inheritdoc />
        public IBy<T> By =>
            ContinuationFactory.Create<T, By<T>>(ActualFetcher, this);


        /// <inheritdoc />
        public IMax<T> Max =>
            ContinuationFactory.Create<T, Max<T>>(ActualFetcher, this);

        /// <inheritdoc />
        public ITo<T> To =>
            ContinuationFactory.Create<T, To<T>>(ActualFetcher, this);

        /// <summary>
        /// Construct a More&lt;T&gt;
        /// - provide the late-fetching func for the actual value
        /// </summary>
        /// <param name="actualFetcher"></param>
        public More(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}