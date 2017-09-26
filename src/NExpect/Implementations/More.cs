using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class More<T>
        : ExpectationContext<T>,
            IMore<T>
    {
        public T Actual { get; }

        public IAnd<T> And =>
            Factory.Create<T, And<T>>(Actual, this);

        public More(T actual)
        {
            Actual = actual;
        }
    }

    internal class StringMore
        : ExpectationContext<string>,
            IStringMore
    {
        public string Actual { get; }

        public IStringAnd And =>
            Factory.Create<string, StringAnd>(Actual, this);

        public StringMore(string actual)
        {
            Actual = actual;
        }
    }
}