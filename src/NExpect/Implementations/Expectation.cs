using System;
using Imported.PeanutButter.Utils;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations;

internal class Expectation<T>
    : ExpectationBase<T>,
      IExpectation<T>,
      IHasActual<T>,
      IExpectationContext<T>
{
    public IExpectationContext Parent => (this as IExpectationContext<T>).TypedParent;
    IExpectationContext<T> IExpectationContext<T>.TypedParent { get; set; }

    public T Actual { get; }
    public ITo<T> To => ContinuationFactory.Create<T, To<T>>(() => Actual, this);
    public IPropertyNot<T> Not => ContinuationFactory.Create<T, Not<T>>(() => Actual, this);


    public Expectation(T actual)
    {
        Actual = actual;
        if (Actual != null)
        {
            Actual.SetMetadata(Expectations.METADATA_KEY, this);
        }
    }

    public IMatcherResult RunMatcher(Func<T, IMatcherResult> matcher)
    {
        return RunMatcher(
            Actual,
            IsNegated,
            matcher,
            resetNegationAfterRun: true
        );
    }
}