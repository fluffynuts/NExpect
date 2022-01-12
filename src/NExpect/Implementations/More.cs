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
        public IAnd<T> And => Next<And<T>>();

        /// <inheritdoc />
        public IWith<T> With => Next<With<T>>();

        /// <inheritdoc />
        public IOf<T> Of => Next<Of<T>>();

        /// <inheritdoc />
        public IBy<T> By => Next<By<T>>();

        /// <inheritdoc />
        public IMax<T> Max => Next<Max<T>>();

        /// <inheritdoc />
        public ITo<T> To => Next<To<T>>();

        /// <inheritdoc />
        public IWhich<T> Which => Next<Which<T>>();

        /// <inheritdoc />
        public IWithout<T> Without => NextNegated<Without<T>>();

        /// <inheritdoc />
        public IFor<T> For => Next<For<T>>();

        /// <inheritdoc />
        public IHaving<T> Having => Next<Having<T>>();

        /// <inheritdoc />
        public IOn<T> On => Next<On<T>>();

        /// <inheritdoc />
        public IIn<T> In => Next<In<T>>();

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