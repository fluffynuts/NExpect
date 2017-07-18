using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    public abstract class ExpectationBase<T>
    {
        protected bool IsNegated;
        public void Negate() {
            Console.WriteLine("-> negated!");
            IsNegated = !IsNegated;
        }

        protected void RunMatcher(
            T actual, 
            bool negated, 
            Func<T, IMatcherResult> matcher)
        {
            IMatcherResult result = null;
            try
            {
                result = matcher(actual);
                var isPass = negated ? !result.Passed : result.Passed;
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

    public class Expectation<T> :
        ExpectationBase<T>,
        IExpectation<T>,
        IExpectationContext<T>
    {
        public T Actual { get; }
        public ITo<T> To => Factory.Create<T, To<T>>(Actual, this);
        public INot<T> Not => Factory.Create<T, Not<T>>(Actual, this);

        IExpectationContext<T> IExpectationContext<T>.Parent { get; set; }

        public Expectation(T actual)
        {
            Actual = actual;
        }

        public void RunMatcher(Func<T, IMatcherResult> matcher)
        {
            RunMatcher(Actual, IsNegated, matcher);
        }
    }
}