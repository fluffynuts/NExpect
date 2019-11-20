using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable MemberCanBeProtected.Global

namespace NExpect.Implementations.Fluency
{
    internal class And<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IAnd<T>
    {
        public T Actual { get; }

        public IA<T> A => ContinuationFactory.Create<T, A<T>>(Actual, this);
        public IAn<T> An => ContinuationFactory.Create<T, An<T>>(Actual, this);
        public IHave<T> Have => ContinuationFactory.Create<T, Have<T>>(Actual, this);
        public IPropertyNot<T> Not => ContinuationFactory.Create<T, Not<T>>(Actual, this);
        public ITo<T> To => ContinuationFactory.Create<T, To<T>>(Actual, this);

        public And(T actual)
        {
            Actual = actual;
        }
    }
}