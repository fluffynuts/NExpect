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
            _assertionsGenerator = (s, e) => generator(GenerateMessageFor(e, s))
                                             ?? throw new ArgumentNullException(nameof(generator));
        }

        /// <summary>
        /// Register your own factory for generating assertion exceptions
        /// where you assertion can also take an inner exception
        /// </summary>
        /// <param name="generator"></param>
        /// <typeparam name="T"></typeparam>
        public static void RegisterAssertionsFactory<T>(
            Func<string, Exception, T> generator) where T : Exception
        {
            _assertionsGenerator = (s, e) => generator(GenerateMessageFor(e, s), e)
                                             ?? throw new ArgumentNullException(nameof(generator));
        }

        /// <summary>
        /// Resets the factory for generating assertion exceptions to the default
        /// (ie, throws UnmetExpectationException instances)
        /// </summary>
        public static void UseDefaultAssertionsFactory()
        {
            _assertionsGenerator = null;
        }

        private static string GenerateMessageFor(Exception exception, string s)
        {
            return exception == null
                ? s
                : $"{s}\n{exception.Message}\n{exception.StackTrace}";
        }

        private static Func<string, Exception, Exception> _assertionsGenerator;

        internal static void Throw(
            string message
        )
        {
            Throw(message, null);
        }

        internal static void Throw(
            string message,
            Exception innerException
        )
        {
            throw _assertionsGenerator?.Invoke(message, innerException)
                  ?? new UnmetExpectationException(message, innerException);
        }
    }
}