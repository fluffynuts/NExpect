namespace NExpect
{
    public class NegatedExpectationTo<T>: INegatedExpectationTo<T>
    {
        public T Actual { get; }
        public bool Negated => true;
        public NegatedExpectationTo(T actual)
        {
            Actual = actual;
        }
    }
}