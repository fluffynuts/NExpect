using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    public class To<T> : ExpectationContext<T>, ITo<T>
    {
        public T Actual { get; }
        public IBe<T> Be => Factory.Create<T, Be<T>>(Actual, this);

        public INotAfterTo<T> Not => Factory.Create<T, NotAfterTo<T>>(Actual, this);

        public To(T actual)
        {
            Actual = actual;
        }
    }

    public class Contain<T> : ExpectationContext<T>, IContain<T>
    {
        public T Actual { get; }

        public Contain(T actual)
        {
            Actual = actual;
        }
    }
}