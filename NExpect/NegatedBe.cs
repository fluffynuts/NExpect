namespace NExpect
{
    public class NegatedBe<T>: Be<T>, INegatedBe<T>
    {
        public bool Negated { get; set; }

        public NegatedBe(T actual): base(actual)
        {
            Negated = true;
        }
    }
}