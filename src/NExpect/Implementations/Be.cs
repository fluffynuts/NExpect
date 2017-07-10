namespace NExpect
{
    public class Be<T>: ExpectationContext<T>, IBe<T>
    {
        public T Actual { get; }
        public INotAfterBe<T> Not => Factory.Create<T, NotAfterBe<T>>(Actual, this);

        public Be(T actual)
        {
            Actual = actual;
        }

    }
}