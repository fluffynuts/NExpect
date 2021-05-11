using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class HasWith<T1, T> : With<T>, IHasWith<T>
    {
        public IWith<T> With { get; }

        internal HasWith(
            IExpectationContext<T1> parent,
            string propertyName,
            Func<T> actualFetcher
        ) : base(actualFetcher)
        {
            With = ContinuationFactory.Create<T, WithPropertyName<T>>(
                actualFetcher,
                this
            );
            (this as IExpectationContext<T1>).TypedParent = parent;
            (With as WithPropertyName<T>).PropertyName = propertyName;
        }
    }
}