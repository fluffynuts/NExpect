namespace NExpect
{
    public class Be<T>: IBe<T>
    {
        public T Actual { get; set; }

        public Be(T actual)
        {
            Actual = actual;
        }

        public INegatedBe<T> Not => new NegatedBe<T>(Actual);
    }
}