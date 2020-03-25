using System;
using NExpect.Helpers;
using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal abstract class ExpectationContextWithLazyActual<T>
        : ExpectationContext<T>
    {
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
        protected Func<T> ActualFetcher => _actualFetcher;

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