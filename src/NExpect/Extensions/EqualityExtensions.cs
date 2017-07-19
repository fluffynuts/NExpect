using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect.Extensions
{
    public static class EqualityExtensions
    {
        public static void Equal<T>(this IContinuation<T> expectation, T expected)
        {
            expectation.Equal(expected, null);
        }

        public static void Equal<T>(this IContinuation<T> continuation, T expected, string customMessage)
        {
            continuation.AddMatcher(actual =>
            {
                if (actual.Equals(expected))
                    return new MatcherResult(true, $"Did not expect {expected}, but got exactly that");
                return new MatcherResult(false,
                    FinalMessageFor(
                        $"Expected {expected} but got {actual}",
                        customMessage
                    ));
            });
        }

        public static void To<T>(
            this IEqualityContinuation<T> continuation,
            T expected
        )
        {
            continuation.AddMatcher(actual =>
            {
                var passed = actual.Equals(expected);
                var message = passed
                    ? $"Expected {Quote(actual)} not to equal {Quote(expected)}"
                    : $"Expected {Quote(actual)} to equal {Quote(expected)}";
                return new MatcherResult(passed, message);
            });
        }

        // TODO: extend for other numeric types (float, double, long)
        public static void Than(
            this IGreaterOrLessContinuation<int> continuation,
            int expected
        )
        {
            var test = 
                continuation is IGreaterContinuation<int>
                ? (Func<int, int, bool>)((a, e) => a > e)
                : (a, e) => a < e;
            AddMatcher(continuation, expected, test);
        }

        public static void Than(
            this IGreaterOrLessContinuation<decimal> continuation,
            decimal expected
        )
        {
            var test = 
                continuation is IGreaterContinuation<decimal>
                ? (Func<decimal, decimal, bool>)((a, e) => a > e)
                : (a, e) => a < e;
            AddMatcher(continuation, expected, test);
        }

        public static void Than(
            this IGreaterOrLessContinuation<double> continuation,
            double expected
        )
        {
            var test = 
                continuation is IGreaterContinuation<double>
                ? (Func<double, double, bool>)((a, e) => a > e)
                : (a, e) => a < e;
            AddMatcher(continuation, expected, test);
        }

        public static void Than(
            this IGreaterOrLessContinuation<float> continuation,
            float expected
        )
        {
            var test = 
                continuation is IGreaterContinuation<float>
                ? (Func<float, float, bool>)((a, e) => a > e)
                : (a, e) => a < e;
            AddMatcher(continuation, expected, test);
        }

        public static void Than(
            this IGreaterOrLessContinuation<long> continuation,
            long expected
        )
        {
            var test = 
                continuation is IGreaterContinuation<long>
                ? (Func<long, long, bool>)((a, e) => a > e)
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