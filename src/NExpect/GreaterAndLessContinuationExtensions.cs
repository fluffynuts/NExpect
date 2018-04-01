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
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<int> continuation,
            int expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }
        
        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<int> continuation,
            int expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(continuation, expected, (a, e) => a < e, customMessageGenerator);
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
            int expected)
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Tests if a value is greater than an expected value, allowing continuation
        /// to test if it is also less than another value
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="expected"></param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <returns></returns>
        public static IGreaterThan<int> Than(
            this IGreaterContinuation<int> continuation,
            int expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }
        
        /// <summary>
        /// Tests if a value is greater than an expected value, allowing continuation
        /// to test if it is also less than another value
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="expected"></param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        /// <returns></returns>
        public static IGreaterThan<int> Than(
            this IGreaterContinuation<int> continuation,
            int expected,
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

        private static IGreaterThan<T> Continue<T>(
            this IGreaterContinuation<T> continuation
        )
        {
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
        public static IGreaterThan<decimal> Than(
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
        public static IGreaterThan<decimal> Than(
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
        public static IGreaterThan<decimal> Than(
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
        public static IGreaterThan<decimal> Than(
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
        public static IGreaterThan<decimal> Than(
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
        public static IGreaterThan<decimal> Than(
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
        public static IGreaterThan<decimal> Than(
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
        public static IGreaterThan<decimal> Than(
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
        public static IGreaterThan<decimal> Than(
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
        public static IGreaterThan<double> Than(
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
        public static IGreaterThan<double> Than(
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
        public static IGreaterThan<double> Than(
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
        public static IGreaterThan<double> Than(
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
        public static IGreaterThan<double> Than(
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
        public static IGreaterThan<double> Than(
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<double> Than(
            this IGreaterContinuation<double> continuation,
            long expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThan<double> Than(
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThan<double> Than(
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<float> continuation,
            float expected
        )
        {
            continuation.Than(expected, NULL_STRING);
        }


        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<float> continuation,
            float expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }
        
        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<float> continuation,
            float expected,
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<float> Than(
            this IGreaterContinuation<float> continuation,
            float expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThan<float> Than(
            this IGreaterContinuation<float> continuation,
            float expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }
        
        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThan<float> Than(
            this IGreaterContinuation<float> continuation,
            float expected,
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static void Than(
            this ILessContinuation<float> continuation,
            decimal expected
        )
        {
            continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<float> continuation,
            decimal expected,
            string customMessage
        )
        {
            continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void Than(
            this ILessContinuation<float> continuation,
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<float> Than(
            this IGreaterContinuation<float> continuation,
            decimal expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThan<float> Than(
            this IGreaterContinuation<float> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThan<float> Than(
            this IGreaterContinuation<float> continuation,
            decimal expected,
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<long> Than(
            this IGreaterContinuation<long> continuation,
            long expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThan<long> Than(
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThan<long> Than(
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<long> Than(
            this IGreaterContinuation<long> continuation,
            decimal expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThan<long> Than(
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThan<long> Than(
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<long> Than(
            this IGreaterContinuation<long> continuation,
            double expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThan<long> Than(
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThan<long> Than(
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThan<DateTime> Than(
            this IGreaterContinuation<DateTime> continuation,
            DateTime expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThan<DateTime> Than(
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
        /// <param name="continuation">.Greater or .Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        public static IGreaterThan<DateTime> Than(
            this IGreaterContinuation<DateTime> continuation,
            DateTime expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(continuation, expected, (a, e) => a > e, customMessageGenerator);
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
                    var compare = continuation is IGreaterContinuation<T1>
                        ? "greater"
                        : "less";
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => passed
                                ? $"Expected {actual} not to be {compare} than {expected}"
                                : $"Expected {actual} to be {compare} than {expected}",
                            customMessageGenerator));
                });
        }
    }
}