using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    internal abstract class ExpectationContext<T> :
        IExpectationContext<T>
    {
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


        public virtual void Negate()
        {
            _storedNegation = true;
            RunNegations();
        }

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