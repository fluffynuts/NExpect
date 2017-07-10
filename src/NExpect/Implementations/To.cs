namespace NExpect
{
    public class To<T>: ExpectationContext<T>, ITo<T>
    {
        public T Actual { get; }
        public IBe<T> Be => Factory.Create<T, Be<T>>(Actual, this);

        public INotAfterTo<T> Not => Factory.Create<T, NotAfterTo<T>>(Actual, this);

        public To(T actual)
        {
            Actual = actual;
        }
    }
}