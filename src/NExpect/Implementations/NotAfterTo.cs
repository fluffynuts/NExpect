﻿using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class NotAfterTo<T>: ExpectationContext<T>, INotAfterTo<T>
    {
        public IBe<T> Be =>
            Factory.Create<T, Be<T>>(Actual, this);
        public IDeep<T> Deep => 
            Factory.Create<T, Deep<T>>(Actual, this);

        public T Actual { get; }

        public NotAfterTo(T actual)
        {
            Actual = actual;
            // ReSharper disable once VirtualMemberCallInConstructor
            Negate();
        }
    }
}   