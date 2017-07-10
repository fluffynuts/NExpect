namespace NExpect
{
    public class ToAfterNot<T>: ExpectationContext<T>, IToAfterNot<T>
    {
        public T Actual { get; }
        public IBe<T> Be => Factory.Create<T, BeAfterNot<T>>(Actual, this);

        public ToAfterNot(T actual)
        {
            Actual = actual;
        }
    }
}