using System;
using NExpect.Helpers;
using NExpect.Interfaces;

namespace NExpect.Implementations.Strings
{
    /// <summary>
    /// Provides an expectation context to inherit from where
    /// the context has a lazy retriever for the Actual value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ExpectationContextWithLazyActual<T>
        : ExpectationContext<T>
    {
        /// <summary>
        /// Shortcut to call into ContinuationFactory.Create
        /// </summary>
        /// <typeparam name="TContinuation"></typeparam>
        /// <returns></returns>
        protected TContinuation Next<TContinuation>() 
            where TContinuation : IExpectationContext<T>
        {
            return ContinuationFactory.Create<T, TContinuation>(ActualFetcher, this);
        }

        /// <summary>
        /// Retrieves the value from the actual fetcher
        /// (memoized, so this only ever happens once)
        /// </summary>
        public T Actual
        {
            get =>
                _fetchedActual
                    ? _actual
                    : _actual = _actualFetcher();
            set
            {
                _actual = value;
                _fetchedActual = true;
            }
        }

        private T _actual;
        private bool _fetchedActual = false;
        private readonly Func<T> _actualFetcher;
        /// <summary>
        /// The func to use to fetch the value on-demand
        /// </summary>
        protected Func<T> ActualFetcher => _actualFetcher;

        /// <summary>
        /// Constructs a new Expectation context with a lazy fetcher
        /// </summary>
        /// <param name="actualFetcher"></param>
        protected ExpectationContextWithLazyActual(
            Func<T> actualFetcher)
        {
            _actualFetcher = FuncFactory.Memoize(actualFetcher);
        }
    }
}