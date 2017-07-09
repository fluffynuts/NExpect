namespace NExpect
{
    public class To<T>: ITo<T>
    {
        public T Actual { get; }
        public IBe<T> Be => new Be<T>(Actual);

        public INegatedExpectationTo<T> Not => new NegatedExpectationTo<T>(Actual);

        public To(T actual)
        {
            Actual = actual;
        }
    }
}