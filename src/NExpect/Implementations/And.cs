using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class And<T>
        : ExpectationContext<T>, IAnd<T>
    {
        public T Actual { get; }

        public IA<T> A => Factory.Create<T, A<T>>(Actual, this);
        public IAn<T> An => Factory.Create<T, An<T>>(Actual, this);
        public IHave<T> Have => Factory.Create<T, Have<T>>(Actual, this);
        public INot<T> Not => Factory.Create<T, Not<T>>(Actual, this);
        public ITo<T> To => Factory.Create<T, To<T>>(Actual, this);

        public And(T actual)
        {
            Actual = actual;
        }
    }
}