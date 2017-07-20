using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Extensions
{
    /// <summary>
    /// Adds extension methods for Greater and Less
    /// </summary>
    public static class GreaterOrLessContinuationExtensions
    {
        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this IGreaterOrLessContinuation<int> continuation,
            int expected
        )
        {
            var test =
                continuation is IGreaterContinuation<int>
                    ? (Func<int, int, bool>) ((a, e) => a > e)
                    : (a, e) => a < e;
            AddMatcher(continuation, expected, test);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this IGreaterOrLessContinuation<decimal> continuation,
            decimal expected
        )
        {
            var test =
                continuation is IGreaterContinuation<decimal>
                    ? (Func<decimal, decimal, bool>) ((a, e) => a > e)
                    : (a, e) => a < e;
            AddMatcher(continuation, expected, test);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this IGreaterOrLessContinuation<double> continuation,
            double expected
        )
        {
            var test =
                continuation is IGreaterContinuation<double>
                    ? (Func<double, double, bool>) ((a, e) => a > e)
                    : (a, e) => a < e;
            AddMatcher(continuation, expected, test);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this IGreaterOrLessContinuation<float> continuation,
            float expected
        )
        {
            var test =
                continuation is IGreaterContinuation<float>
                    ? (Func<float, float, bool>) ((a, e) => a > e)
                    : (a, e) => a < e;
            AddMatcher(continuation, expected, test);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this IGreaterOrLessContinuation<long> continuation,
            long expected
        )
        {
            var test =
                continuation is IGreaterContinuation<long>
                    ? (Func<long, long, bool>) ((a, e) => a > e)
                    : (a, e) => a < e;
            AddMatcher(continuation, expected, test);
        }


        private static void AddMatcher<T>(
            IGreaterOrLessContinuation<T> continuation,
            T expected,
            Func<T, T, bool> test
        )
        {
            continuation.AddMatcher(actual =>
            {
                var passed = test(actual, expected);
                var message = passed
                    ? $"Expected {actual} not to be less than {expected}"
                    : $"Expected {actual} to be less than {expected}";
                return new MatcherResult(passed, message);
            });
        }
    }
}