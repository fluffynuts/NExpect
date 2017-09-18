using System;
using NExpect.Exceptions;
// ReSharper disable UnusedMember.Global

namespace NExpect
{
    /// <summary>
    /// Used by NExpect to throw assertion errors
    /// </summary>
    public static class Assertions
    {
        /// <summary>
        /// Register your own factory for generating assertion exceptions
        /// </summary>
        /// <param name="generator">Func to invoke when NExpect needs an assertion exception</param>
        /// <typeparam name="T">Type of exception</typeparam>
        public static void RegisterAssertionsFactory<T>(Func<string, T> generator) where T : Exception
        {
            _assertionsGenerator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        private static Func<string, Exception> _assertionsGenerator;

        internal static void Throw(
            string message
        )
        {
            throw _assertionsGenerator?.Invoke(message) ?? new UnmetExpectationException(message);
        }

    }
}