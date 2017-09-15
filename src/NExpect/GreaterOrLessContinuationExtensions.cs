using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
// ReSharper disable UnusedMember.Global

namespace NExpect
{
    // TODO: allow passing in a custom message for all Thans
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
            this IGreaterOrLessContinuation<decimal> continuation,
            double expected
        )
        {
            var test =
                continuation is IGreaterContinuation<decimal>
                    ? ((a, e) => a > new Decimal(e))
                    : (Func<decimal, double, bool>) ((a, e) => a < new Decimal(e));
            AddMatcher(continuation, expected, test);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this IGreaterOrLessContinuation<decimal> continuation,
            long expected
        )
        {
            var test =
                continuation is IGreaterContinuation<decimal>
                    ? ((a, e) => a > new Decimal(e))
                    : (Func<decimal, double, bool>) ((a, e) => a < new Decimal(e));
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
            this IGreaterOrLessContinuation<double> continuation,
            decimal expected
        )
        {
            var test =
                continuation is IGreaterContinuation<double>
                    ? (Func<double, decimal, bool>) ((a, e) => new Decimal(a) > e)
                    : (a, e) => new Decimal(a) < e;
            AddMatcher(continuation, expected, test);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this IGreaterOrLessContinuation<double> continuation,
            long expected
        )
        {
            var test =
                continuation is IGreaterContinuation<double>
                    ? (Func<double, decimal, bool>) ((a, e) => new Decimal(a) > e)
                    : (a, e) => new Decimal(a) < e;
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
            this IGreaterOrLessContinuation<float> continuation,
            decimal expected
        )
        {
            var test =
                continuation is IGreaterContinuation<float>
                    ? (Func<float, decimal, bool>) ((a, e) => new Decimal(a) > e)
                    : (a, e) => new Decimal(a) < e;
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

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this IGreaterOrLessContinuation<long> continuation,
            decimal expected
        )
        {
            var test =
                continuation is IGreaterContinuation<long>
                    ? (Func<long, decimal, bool>) ((a, e) => a > e)
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
            double expected
        )
        {
            var test =
                continuation is IGreaterContinuation<long>
                    ? (Func<long, double, bool>) ((a, e) => a > e)
                    : (a, e) => a < e;
            AddMatcher(continuation, expected, test);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this IGreaterOrLessContinuation<DateTime> continuation,
            DateTime expected
        )
        {
            var test =
                continuation is IGreaterContinuation<DateTime>
                    ? (Func<DateTime, DateTime, bool>)((a, e) => a > e)
                    : (a, e) => a < e;
            AddMatcher(continuation, expected, test);
        }


        private static void AddMatcher<T1, T2>(
            IGreaterOrLessContinuation<T1> continuation,
            T2 expected,
            Func<T1, T2, bool> test
        )
        {
            continuation.AddMatcher(actual =>
            {
                var passed = test(actual, expected);
                var compare = continuation is IGreaterContinuation<T1> ? "greater" : "less";
                var message = passed
                    ? $"Expected {actual} not to be {compare} than {expected}"
                    : $"Expected {actual} to be {compare} than {expected}";
                return new MatcherResult(passed, message);
            });
        }
    }
}