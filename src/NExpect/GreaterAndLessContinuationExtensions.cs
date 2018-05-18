using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace NExpect
{
    /// <summary>
    /// Adds extension methods for Greater and Less
    /// </summary>
    public static class GreaterAndLessContinuationExtensions
    {
        private static IGreaterThanContinuation<T> Continue<T>(
            this IGreaterContinuation<T> continuation
        )
        {
            return Factory.Create<T, GreaterThanContinuation<T>>(
                continuation.GetActual(),
                continuation as IExpectationContext<T>
            );
        }

        private static IGreaterThanContinuation<T> Continue<T>(
            this IGreaterThanOrEqual<T> continuation
        )
        {
            return Factory.Create<T, GreaterThanContinuation<T>>(
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
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<decimal> continuation,
            decimal expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<decimal> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a < e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            decimal expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a > e,
                customMessageGenerator);
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
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<decimal> continuation,
            double expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<decimal> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a < new Decimal(e),
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            double expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            double expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a > new Decimal(e),
                customMessageGenerator);
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
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<decimal> continuation,
            long expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<decimal> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a < new Decimal(e),
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            long expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            long expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<decimal> Than(
            this IGreaterContinuation<decimal> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a > new Decimal(e),
                customMessageGenerator);
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
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<double> continuation,
            double expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<double> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a < e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<double> Than(
            this IGreaterContinuation<double> continuation,
            double expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<double> Than(
            this IGreaterContinuation<double> continuation,
            double expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<double> Than(
            this IGreaterContinuation<double> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a > e,
                customMessageGenerator);
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
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<double> continuation,
            decimal expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<double> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => new Decimal(a) < e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<double> Than(
            this IGreaterContinuation<double> continuation,
            decimal expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<double> Than(
            this IGreaterContinuation<double> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<double> Than(
            this IGreaterContinuation<double> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(continuation, expected, (a, e) => new Decimal(a) > e, customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<double> continuation,
            long expected
        )
        {
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<double> continuation,
            long expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<double> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => new Decimal(a) < e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<double> Than(
            this IGreaterContinuation<double> continuation,
            long expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<double> Than(
            this IGreaterContinuation<double> continuation,
            long expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<double> Than(
            this IGreaterContinuation<double> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => new Decimal(a) > e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            long expected
        )
        {
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            long expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a < e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<long> Than(
            this IGreaterContinuation<long> continuation,
            long expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<long> Than(
            this IGreaterContinuation<long> continuation,
            long expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<long> Than(
            this IGreaterContinuation<long> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a > e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            decimal expected
        )
        {
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            decimal expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a < e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<long> Than(
            this IGreaterContinuation<long> continuation,
            decimal expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<long> Than(
            this IGreaterContinuation<long> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<long> Than(
            this IGreaterContinuation<long> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a > e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            double expected
        )
        {
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            double expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<long> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a < e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<long> Than(
            this IGreaterContinuation<long> continuation,
            double expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<long> Than(
            this IGreaterContinuation<long> continuation,
            double expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<long> Than(
            this IGreaterContinuation<long> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a > e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<DateTime> continuation,
            DateTime expected
        )
        {
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<DateTime> continuation,
            DateTime expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<DateTime> continuation,
            DateTime expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a < e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To(
            this ILessThanOrEqual<DateTime> continuation,
            DateTime expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<DateTime> continuation,
            DateTime expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<DateTime> continuation,
            DateTime expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a <= e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To(
            this IGreaterThanOrEqual<DateTime> continuation,
            DateTime expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To(
            this IGreaterThanOrEqual<DateTime> continuation,
            DateTime expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To(
            this IGreaterThanOrEqual<DateTime> continuation,
            DateTime expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a >= e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<DateTime> Than(
            this IGreaterContinuation<DateTime> continuation,
            DateTime expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<DateTime> Than(
            this IGreaterContinuation<DateTime> continuation,
            DateTime expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<DateTime> Than(
            this IGreaterContinuation<DateTime> continuation,
            DateTime expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a > e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To(
            this ILessThanOrEqual<decimal> continuation,
            decimal expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<decimal> continuation,
            decimal expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<decimal> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a <= e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To(
            this ILessThanOrEqual<double> continuation,
            double expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<double> continuation,
            double expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<double> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a <= e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To(
            this ILessThanOrEqual<double> continuation,
            decimal expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<double> continuation,
            decimal expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<double> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => new Decimal(a) <= e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<decimal> To(
            this IGreaterContinuation<decimal> continuation,
            decimal expected
        )
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<decimal> To(
            this IGreaterContinuation<decimal> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<decimal> To(
            this IGreaterContinuation<decimal> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a > e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To(
            this ILessThanOrEqual<long> continuation,
            long expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<long> continuation,
            long expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<long> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a <= e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To(
            this IGreaterThanOrEqual<long> continuation,
            double expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To(
            this IGreaterThanOrEqual<long> continuation,
            double expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To(
            this IGreaterThanOrEqual<long> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a >= e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To(
            this ILessThanOrEqual<long> continuation,
            double expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<long> continuation,
            double expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<long> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a <= e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To(
            this ILessThanOrEqual<long> continuation,
            decimal expected
        )
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<long> continuation,
            decimal expected,
            string customMessage
        )
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To(
            this ILessThanOrEqual<long> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a <= e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<long> To(
            this IGreaterThanOrEqual<long> continuation,
            long expected
        )
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<long> To(
            this IGreaterThanOrEqual<long> continuation,
            long expected,
            string customMessage
        )
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<long> To(
            this IGreaterThanOrEqual<long> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a >= e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<decimal> To(
            this IGreaterThanOrEqual<decimal> continuation,
            decimal expected
        )
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<decimal> To(
            this IGreaterThanOrEqual<decimal> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<decimal> To(
            this IGreaterThanOrEqual<decimal> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a >= e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<long> To(
            this IGreaterThanOrEqual<long> continuation,
            decimal expected
        )
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<long> To(
            this IGreaterThanOrEqual<long> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<long> To(
            this IGreaterThanOrEqual<long> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a >= e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<double> To(
            this IGreaterThanOrEqual<double> continuation,
            decimal expected
        )
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<double> To(
            this IGreaterThanOrEqual<double> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<double> To(
            this IGreaterThanOrEqual<double> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => new Decimal(a) >= e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<double> To(
            this IGreaterThanOrEqual<double> continuation,
            double expected
        )
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<double> To(
            this IGreaterThanOrEqual<double> continuation,
            double expected,
            string customMessage
        )
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<double> To(
            this IGreaterThanOrEqual<double> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a >= e,
                customMessageGenerator);
            return continuation.Continue();
        }

        private static void AddMatcher<T1, T2>(
            ICanAddMatcher<T1> continuation,
            T2 expected,
            Func<T1, T2, bool> test,
            Func<string> customMessageGenerator
        )
        {
            continuation.AddMatcher(
                actual =>
                {
                    var passed = test(actual, expected);
                    var compare = CreateCompareMessageFor(continuation);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => passed
                                ? $"Expected {actual} not to be {compare} {expected}"
                                : $"Expected {actual} to be {compare} {expected}",
                            customMessageGenerator));
                });
        }

        private static string CreateCompareMessageFor<T>(
            ICanAddMatcher<T> continuation
        )
        {
            switch (continuation)
            {
                case IGreaterContinuation<T> _:
                    return "greater than";
                case ILessContinuation<T> _:
                    return "less than";
                case IGreaterThanOrEqual<T> _:
                    return "greater than or equal to";
                case ILessThanOrEqual<T> _:
                    return "less than or equal to";
            }

            throw new Exception($"Unknown comparison type: {continuation.GetType()}");
        }
    }
}