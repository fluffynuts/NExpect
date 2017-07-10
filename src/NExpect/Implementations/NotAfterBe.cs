namespace NExpect
{
    public class NotAfterBe<T>: Be<T>, INotAfterBe<T>
    {
        public NotAfterBe(T actual): base(actual)
        {
            Negate();
        }
    }
}