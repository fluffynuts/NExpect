using System;
using System.Collections.Generic;

namespace NExpect
{
    public class Expectation<T> : IExpectation<T>, IExpectationContext<T>
    {
        public T Actual { get; }
        public ITo<T> To => Factory.Create<T, To<T>>(Actual, this);
        public INot<T> Not => Factory.Create<T, Not<T>>(Actual, this);

        IExpectationContext<T> IExpectationContext<T>.Parent { get; set; }

        private bool _negated;

        public Expectation(T actual)
        {
            Actual = actual;
        }

        public void Negate()
        {
            Console.WriteLine("-> negated!");
            _negated = !_negated;
        }

        public void Expect(Func<T, IMatcherResult> expectation)
        {
            IMatcherResult result = null;
            try
            {
                result = expectation(Actual);
                var isPass = _negated ? !result.Passed : result.Passed;
                if (isPass)
                    return;
            }
            catch (Exception ex)
            {
                // TODO: make this better, ie, include the exception as an inner
                Assertion.Throw(ex.Message);
                return;
            }
            Assertion.Throw(result.Message);
        }
    }
}