using System;
using System.Collections.Generic;

namespace NExpect
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
        private List<Func<T, string>> _storedExpectations = new List<Func<T, string>>();

        public void Negate()
        {
            _storedNegations++;
            RunNegations();
        }

        public void Expect(Func<T, string> expectation)
        {
            _storedExpectations.Add(expectation);
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
                _parent.Expect(e);
            }
            _storedExpectations.Clear();
        }
    }
}