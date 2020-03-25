using System;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable VirtualMemberCallInConstructor

namespace NExpect.Implementations.Fluency
{
    internal class Not<T>: 
        ExpectationContextWithLazyActual<T>, 
        IHasActual<T>,
        IPropertyNot<T>
    {
        public IToAfterNot<T> To => ContinuationFactory.Create<T, ToAfterNot<T>>(ActualFetcher, this);

        public Not(Func<T> actualFetcher): base(actualFetcher)
        {
            Negate();
        }
    }
}