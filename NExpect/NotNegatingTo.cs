namespace NExpect
{
    public class NotNegatingTo<T>: INonNegatingTo<T>
    {
        public T Actual { get; }
        public bool Negated => true;
        public IBe<T> Be => new NegatedBe<T>(Actual);

        public NotNegatingTo(T actual)
        {
            Actual = actual;
        }
    }
}