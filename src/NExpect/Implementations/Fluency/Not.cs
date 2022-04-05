using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable VirtualMemberCallInConstructor

namespace NExpect.Implementations.Fluency;

internal class Not<T>: 
    ExpectationContextWithLazyActual<T>, 
    IHasActual<T>,
    IPropertyNot<T>
{
    public IToAfterNot<T> To => Next<ToAfterNot<T>>();

    public Not(Func<T> actualFetcher): base(actualFetcher)
    {
        Negate();
    }
}