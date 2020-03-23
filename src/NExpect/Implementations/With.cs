using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class With<T>
        : ExpectationContext<T>,
          IHasActual<T>,
          IWith<T>
    {
        public T Actual { get; set; }

        public With(T actual)
        {
            Actual = actual;
        }
    }
}