using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class StringEnd
        : ExpectationContext<string>,
            IStringEnd
    {
        public string Actual { get; }

        public StringEnd(string actual)
        {
            Actual = actual;
        }
    }
}