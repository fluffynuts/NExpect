using System;
using NExpect.Helpers;
using NExpect.Interfaces;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

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

    internal class StringPropertyContinuation
        : ExpectationContextWithLazyActual<string>,
          IStringPropertyContinuation,
          IStringPropertyStartingContinuation,
          IStringPropertyEndingContinuation
    {
        public IStringPropertyContinuation And =>
            ContinuationFactory.Create<string, StringPropertyAnd>(ActualFetcher, this);

        public IEqualityContinuation<string> Equal
            => ContinuationFactory.Create<string, EqualityContinuation<string>>(ActualFetcher, this);

        public IStringIn In =>
            ContinuationFactory.Create<string, StringIn>(ActualFetcher, this);

        public IStringPropertyNot Not
            => ContinuationFactory.Create<string, StringPropertyNot>(ActualFetcher, this);

        public IStringPropertyEndingContinuation Ending
            => ContinuationFactory.Create<string, StringPropertyContinuation>(ActualFetcher, this);

        public IStringPropertyStartingContinuation Starting
            => ContinuationFactory.Create<string, StringPropertyContinuation>(ActualFetcher, this);

        public StringPropertyContinuation(Func<string> actualFetcher)
            : base(actualFetcher)
        {
        }
    }
}