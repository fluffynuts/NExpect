using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
// ReSharper disable UnusedMember.Global

namespace NExpect
{
    // TODO: allow passing in a custom message for all Thans
    /// <summary>
    /// Adds extension methods for Greater and Less
    /// </summary>
    public static class GreaterAndLessContinuationExtensions
    {
        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<int> continuation,
            int expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a < e);
        }

        /// <summary>
        /// Tests if a value is greater than an expected value, allowing continuation
        /// to test if it is also less than another value
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static IGreaterThan<int> Than(
            this IGreaterContinuation<int> continuation,
            int expected
        ) {
            AddMatcher(continuation, expected, (a, e) => a > e);
            return continuation.Continue();
        }

        private static IGreaterThan<T> Continue<T>(
            this IGreaterContinuation<T> continuation
        ) {
            return Factory.Create<T, GreaterThan<T>>(
                continuation.GetActual(),
                continuation as IExpectationContext<T>
            );
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<decimal> continuation,
            decimal expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a < e);
        }
        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            decimal expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a > e);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<decimal> continuation,
            double expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a < new Decimal(e));
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            double expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a > new Decimal(e));
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<decimal> continuation,
            long expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a < new Decimal(e));
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            long expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a > new Decimal(e));
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<double> continuation,
            double expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a < e);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<double> Than(
            this IGreaterContinuation<double> continuation,
            double expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a > e);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<double> continuation,
            decimal expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => new Decimal(a) < e);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<double> Than(
            this IGreaterContinuation<double> continuation,
            decimal expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => new Decimal(a) > e);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<double> continuation,
            long expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => new Decimal(a) < e);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<double> Than(
            this IGreaterContinuation<double> continuation,
            long expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => new Decimal(a) > e);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<float> continuation,
            float expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a < e);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<float> Than(
            this IGreaterContinuation<float> continuation,
            float expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a > e);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<float> continuation,
            decimal expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => new Decimal(a) < e);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<float> Than(
            this IGreaterContinuation<float> continuation,
            decimal expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => new Decimal(a) > e);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            long expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a < e);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<long> Than(
            this IGreaterContinuation<long> continuation,
            long expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a > e);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            decimal expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a < e);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<long> Than(
            this IGreaterContinuation<long> continuation,
            decimal expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a > e);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            double expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a < e);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<long> Than(
            this IGreaterContinuation<long> continuation,
            double expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a > e);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<DateTime> continuation,
            DateTime expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a < e);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<DateTime> Than(
            this IGreaterContinuation<DateTime> continuation,
            DateTime expected
        )
        {
            AddMatcher(continuation, expected, (a, e) => a > e);
            return continuation.Continue();
        }


        private static void AddMatcher<T1, T2>(
            ICanAddMatcher<T1> continuation,
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