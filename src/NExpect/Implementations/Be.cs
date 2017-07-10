namespace NExpect
{
    public class Be<T>: ExpectationContext<T>, IBe<T>
    {
        public T Actual { get; }
        public IBeAfterNot<T> Not => new BeAfterNot<T>(Actual);

        public Be(T actual)
        {
            Actual = actual;
        }

    }
}