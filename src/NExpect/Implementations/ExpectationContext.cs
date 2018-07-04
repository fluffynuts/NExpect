using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ExpectationContext<T> :
        CannotBeCompared,
        IExpectationContext<T>
    {
        /// <summary>
        /// Parent context for this expectation
        /// </summary>
        public IExpectationContext Parent => _parent;
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
        public void ResetNegation()
        {
            _storedNegation = false;
            RunResetNegations();
        }

        private void RunResetNegations()
        {
            if (_parent == null ||
                !_shouldResetNegation)
                return;
            _parent.ResetNegation();
        }

        /// <summary>
        /// Runs an expectation by pushing it up the ancestry to be
        /// run within NExpect.
        /// </summary>
        /// <param name="matcher"></param>
        public virtual void RunMatcher(Func<T, IMatcherResult> matcher)
        {
            _storedExpectation = matcher;
            RunExpectations();
        }

        private void RunNegations()
        {
            if (_parent == null ||
                !_storedNegation)
                return;

            _parent.Negate();
            _shouldResetNegation = false;
        }

        private void RunExpectations()
        {
            if (_parent == null ||
                _storedExpectation == null)
                return;
            _parent.RunMatcher(_storedExpectation);
            _storedExpectation = null;
        }

        internal void SetParent(IExpectationContext<T> parent)
        {
            (this as IExpectationContext<T>).TypedParent = parent;
        }

    }
}