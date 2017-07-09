namespace NExpect
{
    public class Not<T>: INot<T>
    {
        public T Actual { get; set; }
        public INonNegatingTo<T> To => new NotNegatingTo<T>(Actual);

        public Not(T actual)
        {
            Actual = actual;
        }
    }
}