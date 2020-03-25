using System;
using NExpect.Implementations.Collections;
using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations.Strings
{
    internal class StringMore
        : ExpectationContext<string>,
          IHasActual<string>,
          IStringMore
    {
        public string Actual =>
            _haveFetchedActual
                ? _actual
                : _actual = _actualGenerator();

        private string _actual;
        private bool _haveFetchedActual = false;
        private readonly Func<string> _actualGenerator;

        public IStringAnd And =>
            ContinuationFactory.Create<string, StringAnd>(Actual, this);

        public StringMore(Func<string> actualGenerator)
        {
            _actualGenerator = actualGenerator;
        }
    }
}