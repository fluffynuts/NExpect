using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    /// <summary>
    /// Base behavior for expectation contexts
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ExpectationContext<T>
        : CannotBeCompared,
          IExpectationContext<T>
    {
        /// <summary>
        /// Parent context for this expectation
        /// </summary>
        public virtual IExpectationContext Parent => _parent;

        private IExpectationContext<T> _parent;

        IExpectationContext<T> IExpectationContext<T>.TypedParent
        {
            get => _parent;
            set
            {
                _parent = value;
                RunNegations();
                RunExpectations();
            }
        }

        private bool _storedNegation;
        private bool _shouldResetNegation;
        private Func<T, IMatcherResult> _storedExpectation;


        /// <summary>
        /// Negates the current expectation
        /// </summary>
        public virtual void Negate()
        {
            _storedNegation = true;
            RunNegations();
        }

        /// <summary>
        /// Resets expectation negatin
        /// </summary>
        public virtual void ResetNegation()
        {
            _storedNegation = false;
            RunResetNegations();
        }

        private void RunResetNegations()
        {
            if (_parent == null ||
                !_shouldResetNegation)
            {
                return;
            }

            _parent.ResetNegation();
        }

        /// <summary>
        /// Runs an expectation by pushing it up the ancestry to be
        /// run within NExpect.
        /// </summary>
        /// <param name="matcher"></param>
        public virtual IMatcherResult RunMatcher(Func<T, IMatcherResult> matcher)
        {
            _storedExpectation = matcher;
            return RunExpectations();
        }

        private void RunNegations()
        {
            if (_parent == null ||
                !_storedNegation)
            {
                return;
            }

            _parent.Negate();
            _shouldResetNegation = false;
        }

        private IMatcherResult RunExpectations()
        {
            if (_storedExpectation is null)
            {
                return default;
            }

            if (_parent is null)
            {
                if (this is IHasActualFetcher<T> hasActualFetcher)
                {
                    return MatcherRunner.RunMatcher(
                        hasActualFetcher.ActualFetcher(),
                        _storedNegation,
                        _storedExpectation
                    );
                }

                if (this is IHasActual<T> hasActual)
                {
                    return MatcherRunner.RunMatcher(
                        hasActual.Actual,
                        _storedNegation,
                        _storedExpectation
                    );
                }

                throw new InvalidOperationException(
                    "Current expectation context has no parent and no actual value or fetcher, so the stored expectation cannot be run."
                );
            }

            var result = _parent.RunMatcher(_storedExpectation);
            _storedExpectation = null;
            return result;
        }

        internal void SetParent(IExpectationContext<T> parent)
        {
            (this as IExpectationContext<T>).TypedParent = parent;
        }
    }
}