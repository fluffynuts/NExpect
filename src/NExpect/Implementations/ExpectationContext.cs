using System;
using System.Collections.Generic;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    public abstract class ExpectationContext<T> : IExpectationContext<T>
    {
        private IExpectationContext<T> _parent;

        IExpectationContext<T> IExpectationContext<T>.Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                RunNegations();
                RunExpectations();
            }
        }

        private int _storedNegations;
        private List<Func<T, IMatcherResult>> _storedExpectations = new List<Func<T, IMatcherResult>>();

        public void Negate()
        {
            _storedNegations++;
            RunNegations();
        }

        public void RunMatcher(Func<T, IMatcherResult> matcher)
        {
            _storedExpectations.Add(matcher);
            RunExpectations();
        }

        private void RunNegations()
        {
            if (_parent == null)
                return;

            for (var i = 0; i < _storedNegations % 2; i++)
                _parent.Negate();
            _storedNegations = 0;
        }

        private void RunExpectations()
        {
            if (_parent == null)
                return;
            foreach (var e in _storedExpectations)
            {
                _parent.RunMatcher(e);
            }
            _storedExpectations.Clear();
        }

        internal void SetParent(IExpectationContext<T> parent)
        {
            (this as IExpectationContext<T>).Parent = parent;
        }
    }
}