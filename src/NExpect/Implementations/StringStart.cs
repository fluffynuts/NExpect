using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    internal class StringStart
        : ExpectationContext<string>,
            IStringStart
    {
        public string Actual { get; }

        public StringStart(string actual)
        {
            Actual = actual;
        }
    }
}