using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    internal class Expectation<T> :
        ExpectationBase<T>,
        IExpectation<T>,
        IExpectationContext<T>
    {
        public IExpectationContext Parent => (this as IExpectationContext<T>).TypedParent;
        IExpectationContext<T> IExpectationContext<T>.TypedParent { get; set; }

        public T Actual { get; }
        public ITo<T> To => Factory.Create<T, To<T>>(Actual, this);
        public INot<T> Not => Factory.Create<T, Not<T>>(Actual, this);


        public Expectation(T actual)
        {
            Actual = actual;
        }

        public void RunMatcher(Func<T, IMatcherResult> matcher)
        {
            RunMatcher(Actual, IsNegated, matcher, true);
        }
    }

}