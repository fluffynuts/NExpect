using NExpect.Interfaces;
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable VirtualMemberCallInConstructor

namespace NExpect.Implementations
{
    internal class Not<T>: 
        ExpectationContext<T>, 
        IHasActual<T>,
        IPropertyNot<T>
    {
        public T Actual { get; }
        public IToAfterNot<T> To => Factory.Create<T, ToAfterNot<T>>(Actual, this);

        public Not(T actual)
        {
            Actual = actual;
            Negate();
        }
    }
}