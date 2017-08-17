using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class An<T> : ExpectationContext<T>, IAn<T>
    {
        public T Actual { get; }

        public An(T actual)
        {
            Actual = actual;
        }

        public IInstance<T> Instance =>
            Factory.Create<T, Instance<T>>(Actual, this);
    }

    public class Instance<T> :
        ExpectationContext<T>,
        IInstance<T>
    {
        public T Actual { get; }
        public Type Type { get; }

        public Instance(T actual)
        {
            Actual = actual;
            Type = typeof(T);
        }
    }
}