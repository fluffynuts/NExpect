namespace NExpect
{
    public class BeAfterNot<T>: Be<T>, IBeAfterNot<T>
    {
        public BeAfterNot(T actual): base(actual)
        {
        }
    }
}