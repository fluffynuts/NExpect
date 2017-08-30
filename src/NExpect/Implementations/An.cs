using NExpect.Interfaces;
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class An<T> : ExpectationContext<T>, IAn<T>
    {
        public T Actual { get; }

        public An(T actual)
        {
            Actual = actual;
        }

        public IInstance<T> Instance =>
            Factory.Create<T, Instance<T>>(Actual, this);
    }
}