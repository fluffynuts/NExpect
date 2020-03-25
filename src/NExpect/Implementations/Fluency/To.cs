using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeProtected.Global

namespace NExpect.Implementations.Fluency
{
    internal class To<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          ITo<T>
    {
        public IBe<T> Be => ContinuationFactory.Create<T, Be<T>>(ActualFetcher, this);
        public IContain<T> Contain => ContinuationFactory.Create<T, Contain<T>>(ActualFetcher, this);
        public INotAfterTo<T> Not => ContinuationFactory.Create<T, NotAfterTo<T>>(ActualFetcher, this);
        public IHave<T> Have => ContinuationFactory.Create<T, Have<T>>(ActualFetcher, this);
        public IDeep<T> Deep => ContinuationFactory.Create<T, Deep<T>>(ActualFetcher, this);

        public IIntersection<T> Intersection
            => ContinuationFactory.Create<T, Intersection<T>>(ActualFetcher, this);

        public IApproximately<T> Approximately
            => ContinuationFactory.Create<T, Approximately<T>>(ActualFetcher, this);

        public To(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}