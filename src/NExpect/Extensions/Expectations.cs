using System;
using NExpect.Implementations;
using NExpect.Interfaces;

namespace NExpect.Extensions
{
    public static class Expectations
    {
        public static IExpectation<T> Expect<T>(T value)
        {
            return new Expectation<T>(value);
        }

        public static IExpectation<Action> Expect(Action action)
        {
            return new Expectation<Action>(action);
        }
    }
}