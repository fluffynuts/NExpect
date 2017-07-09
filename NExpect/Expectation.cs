namespace NExpect
{
    public class Expectation<T>: IExpectation<T>
    {
        public T Actual { get; }

        public ITo<T> To => new To<T>(Actual);
        public INot<T> Not => new Not<T>(Actual);

        public Expectation(T actual)
        {
            Actual = actual;
        }
    }
}