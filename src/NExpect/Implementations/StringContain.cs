using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class StringContain
        : ExpectationContext<string>,
            IStringContain,
            IHasActual<string>
    {
        public string Actual { get; }
        public IStringIn In =>
            Factory.Create<string, StringIn>(Actual, this);
        public StringContain(string actual)
        {
            Actual = actual;
        }
    }
}