using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Fluency
{
    internal class NotAfterTo<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        INotAfterTo<T>
    {
        public IBe<T> Be =>
            ContinuationFactory.Create<T, Be<T>>(ActualFetcher, this);

        public IContain<T> Contain =>
            ContinuationFactory.Create<T, Contain<T>>(ActualFetcher, this);
        
        public IHave<T> Have =>
            ContinuationFactory.Create<T, Have<T>>(ActualFetcher, this);

        public IDeep<T> Deep =>
            ContinuationFactory.Create<T, Deep<T>>(ActualFetcher, this);

        public IIntersection<T> Intersection =>
            ContinuationFactory.Create<T, Intersection<T>>(ActualFetcher, this);

        public NotAfterTo(Func<T> actualFetcher): base(actualFetcher)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Negate();
        }
    }
}