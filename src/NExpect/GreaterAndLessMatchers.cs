using System;
using NExpect.Implementations;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Numerics;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace NExpect
{
    /// <summary>
    /// Adds matchers for Greater and Less
    /// </summary>
    public static class GreaterAndLessMatchers
    {
        private static IGreaterThanContinuation<T> Continue<T>(
            this IGreaterContinuation<T> continuation
        )
        {
            return ContinuationFactory.Create<T, GreaterThanContinuation<T>>(
                continuation.GetActual,
                continuation as IExpectationContext<T>
            );
        }

        private static IGreaterThanContinuation<T> Continue<T>(
            this IGreaterThanOrEqual<T> continuation
        )
        {
            return ContinuationFactory.Create<T, GreaterThanContinuation<T>>(
                continuation.GetActual,
                continuation as IExpectationContext<T>
            );
        }

        private static ILessThanContinuation<T> Continue<T>(
            this ILessContinuation<T> continuation
        )
        {
            return ContinuationFactory.Create<T, LessThanContinuation<T>>(
                continuation.GetActual,
                continuation as IExpectationContext<T>
            );
        }

        private static ILessThanContinuation<T> Continue<T>(
            this ILessThanOrEqual<T> continuation
        )
        {
            return ContinuationFactory.Create<T, LessThanContinuation<T>>(
                continuation.GetActual,
                continuation as IExpectationContext<T>
            );
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<T1?> Than<T1, T2>(
            this ILessContinuation<T1?> continuation,
            T2? expected
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<T1?> Than<T1, T2>(
            this ILessContinuation<T1?> continuation,
            T2? expected,
            string customMessage
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<T1?> Than<T1, T2>(
            this ILessContinuation<T1?> continuation,
            T2? expected,
            Func<string> customMessageGenerator
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue &&
                    e.HasValue &&
                    TryCompare(a, e) < 0,
                customMessageGenerator);
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<T1> Than<T1, T2>(
            this ILessContinuation<T1> continuation,
            T2? expected
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<T1> Than<T1, T2>(
            this ILessContinuation<T1> continuation,
            T2? expected,
            string customMessage
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<T1> Than<T1, T2>(
            this ILessContinuation<T1> continuation,
            T2? expected,
            Func<string> customMessageGenerator
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) =>
                {
                    var compareResult = TryCompare(a, e);
                    return compareResult == null || compareResult < 0;
                },
                customMessageGenerator);
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<T1?> Than<T1, T2>(
            this ILessContinuation<T1?> continuation,
            T2 expected
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<T1?> Than<T1, T2>(
            this ILessContinuation<T1?> continuation,
            T2 expected,
            string customMessage
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<T1?> Than<T1, T2>(
            this ILessContinuation<T1?> continuation,
            T2 expected,
            Func<string> customMessageGenerator
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue &&
                    TryCompare(a.Value, e) < 0,
                customMessageGenerator);
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<T1> Than<T1, T2>(
            this ILessContinuation<T1> continuation,
            T2 expected
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<T1> Than<T1, T2>(
            this ILessContinuation<T1> continuation,
            T2 expected,
            string customMessage
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<T1> Than<T1, T2>(
            this ILessContinuation<T1> continuation,
            T2 expected,
            Func<string> customMessageGenerator
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) =>
                {
                    var compareResult = TryCompare(a, e);
                    return compareResult == null || compareResult < 0;
                },
                customMessageGenerator);
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<T1?> Than<T1, T2>(
            this IGreaterContinuation<T1?> continuation,
            T2? expected
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1?> Than<T1, T2>(
            this IGreaterContinuation<T1?> continuation,
            T2? expected,
            string customMessage
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1?> Than<T1, T2>(
            this IGreaterContinuation<T1?> continuation,
            T2? expected,
            Func<string> customMessageGenerator
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue &&
                    e.HasValue &&
                    a.Value.CompareTo(e.Value) > 0,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<T1> Than<T1, T2>(
            this IGreaterContinuation<T1> continuation,
            T2? expected
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1> Than<T1, T2>(
            this IGreaterContinuation<T1> continuation,
            T2? expected,
            string customMessage
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1> Than<T1, T2>(
            this IGreaterContinuation<T1> continuation,
            T2? expected,
            Func<string> customMessageGenerator
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) =>
                {
                    var compareResult = TryCompare(a, e);
                    return compareResult == null || compareResult > 0;
                },
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<T1?> Than<T1, T2>(
            this IGreaterContinuation<T1?> continuation,
            T2 expected
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1?> Than<T1, T2>(
            this IGreaterContinuation<T1?> continuation,
            T2 expected,
            string customMessage
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1?> Than<T1, T2>(
            this IGreaterContinuation<T1?> continuation,
            T2 expected,
            Func<string> customMessageGenerator
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) =>
                {
                    var compareResult = TryCompare(a, e);
                    return compareResult == null || compareResult > 0;
                },
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<T1> Than<T1, T2>(
            this IGreaterContinuation<T1> continuation,
            T2 expected
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1> Than<T1, T2>(
            this IGreaterContinuation<T1> continuation,
            T2 expected,
            string customMessage
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1> Than<T1, T2>(
            this IGreaterContinuation<T1> continuation,
            T2 expected,
            Func<string> customMessageGenerator
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => TryCompare(a, e) > 0,
                customMessageGenerator);
            return continuation.Continue();
        }

        private static int? TryCompare<T1, T2>(
            T1? a,
            T2? e
        ) where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            if (a == null || e == null)
            {
                return null;
            }

            return TryCompare(a.Value, e.Value);
        }

        private static int? TryCompare<T1, T2>(
            T1? a,
            T2 e
        ) where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            if (a == null)
            {
                return null;
            }

            return TryCompare(a.Value, e);
        }

        private static int? TryCompare<T1, T2>(
            T1 a,
            T2? e
        ) where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            if (e == null)
            {
                return null;
            }

            return TryCompare(a, e.Value);
        }

        private static int? TryCompare<T1, T2>(
            T1 a,
            T2 e
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            if (a == null || e == null)
            {
                return null;
            }

            var aType = a.GetType();
            var eType = e.GetType();
            if (aType == eType)
            {
                return a.CompareTo(e);
            }


            try
            {
                var eConverted = Convert.ChangeType(e, aType);
                return a.CompareTo(eConverted);
            }
            catch
            {
                try
                {
                    var aConverted = Convert.ChangeType(a, eType) as IComparable;
                    return aConverted?.CompareTo(e);
                }
                catch
                {
                    // can't convert, so can't compare
                    return null;
                }
            }
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static ILessThanContinuation<decimal?> Than(
            this ILessContinuation<decimal?> continuation,
            double expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static ILessThanContinuation<decimal?> Than(
            this ILessContinuation<decimal?> continuation,
            double expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static ILessThanContinuation<decimal?> Than(
            this ILessContinuation<decimal?> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a < new Decimal(e),
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static ILessThanContinuation<decimal> Than(
            this ILessContinuation<decimal> continuation,
            double expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static ILessThanContinuation<decimal> Than(
            this ILessContinuation<decimal> continuation,
            double expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static ILessThanContinuation<decimal> Than(
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
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<decimal?> Than(
            this IGreaterContinuation<decimal?> continuation,
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
        public static IGreaterThanContinuation<decimal?> Than(
            this IGreaterContinuation<decimal?> continuation,
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
        public static IGreaterThanContinuation<decimal?> Than(
            this IGreaterContinuation<decimal?> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a > new Decimal(e),
                customMessageGenerator);
            return continuation.Continue();
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
        public static ILessThanContinuation<decimal?> Than(
            this ILessContinuation<decimal?> continuation,
            long expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static ILessThanContinuation<decimal?> Than(
            this ILessContinuation<decimal?> continuation,
            long expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static ILessThanContinuation<decimal?> Than(
            this ILessContinuation<decimal?> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a < new Decimal(e),
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static ILessThanContinuation<decimal> Than(
            this ILessContinuation<decimal> continuation,
            long expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static ILessThanContinuation<decimal> Than(
            this ILessContinuation<decimal> continuation,
            long expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static ILessThanContinuation<decimal> Than(
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
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<decimal?> Than(
            this IGreaterContinuation<decimal?> continuation,
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
        public static IGreaterThanContinuation<decimal?> Than(
            this IGreaterContinuation<decimal?> continuation,
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
        public static IGreaterThanContinuation<decimal?> Than(
            this IGreaterContinuation<decimal?> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a > new Decimal(e),
                customMessageGenerator);
            return continuation.Continue();
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
        public static ILessThanContinuation<double?> Than(
            this ILessContinuation<double?> continuation,
            double expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static ILessThanContinuation<double?> Than(
            this ILessContinuation<double?> continuation,
            double expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        public static ILessThanContinuation<double?> Than(
            this ILessContinuation<double?> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a < e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static ILessThanContinuation<double> Than(
            this ILessContinuation<double> continuation,
            double expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static ILessThanContinuation<double> Than(
            this ILessContinuation<double> continuation,
            double expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
        public static ILessThanContinuation<double> Than(
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
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<double?> Than(
            this IGreaterContinuation<double?> continuation,
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
        public static IGreaterThanContinuation<double?> Than(
            this IGreaterContinuation<double?> continuation,
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
        public static IGreaterThanContinuation<double?> Than(
            this IGreaterContinuation<double?> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a > e,
                customMessageGenerator);
            return continuation.Continue();
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
        public static ILessThanContinuation<double?> Than(
            this ILessContinuation<double?> continuation,
            decimal expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static ILessThanContinuation<double?> Than(
            this ILessContinuation<double?> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static ILessThanContinuation<double?> Than(
            this ILessContinuation<double?> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && new Decimal(a.Value) < e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static ILessThanContinuation<double> Than(
            this ILessContinuation<double> continuation,
            decimal expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static ILessThanContinuation<double> Than(
            this ILessContinuation<double> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static ILessThanContinuation<double> Than(
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
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<double?> Than(
            this IGreaterContinuation<double?> continuation,
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
        public static IGreaterThanContinuation<double?> Than(
            this IGreaterContinuation<double?> continuation,
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
        public static IGreaterThanContinuation<double?> Than(
            this IGreaterContinuation<double?> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(continuation,
                expected,
                (a, e) => a.HasValue && new Decimal(a.Value) > e,
                customMessageGenerator);
            return continuation.Continue();
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
            AddMatcher(continuation,
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
        public static IMore<double?> Than(
            this ILessContinuation<double?> continuation,
            long expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<double?> Than(
            this ILessContinuation<double?> continuation,
            long expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<double?> Than(
            this ILessContinuation<double?> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && new Decimal(a.Value) < e,
                customMessageGenerator);
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<double> Than(
            this ILessContinuation<double> continuation,
            long expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<double> Than(
            this ILessContinuation<double> continuation,
            long expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<double> Than(
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
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<double?> Than(
            this IGreaterContinuation<double?> continuation,
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
        public static IGreaterThanContinuation<double?> Than(
            this IGreaterContinuation<double?> continuation,
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
        public static IGreaterThanContinuation<double?> Than(
            this IGreaterContinuation<double?> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && new Decimal(a.Value) > e,
                customMessageGenerator);
            return continuation.Continue();
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
        public static IMore<long?> Than(
            this ILessContinuation<long?> continuation,
            long? expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<long?> Than(
            this ILessContinuation<long?> continuation,
            long? expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<long?> Than(
            this ILessContinuation<long?> continuation,
            long? expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a < e,
                customMessageGenerator);
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<long> Than(
            this ILessContinuation<long> continuation,
            long expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<long> Than(
            this ILessContinuation<long> continuation,
            long expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<long> Than(
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
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<long?> Than(
            this IGreaterContinuation<long?> continuation,
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
        public static IGreaterThanContinuation<long?> Than(
            this IGreaterContinuation<long?> continuation,
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
        public static IGreaterThanContinuation<long?> Than(
            this IGreaterContinuation<long?> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a > e,
                customMessageGenerator);
            return continuation.Continue();
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
        public static IMore<long?> Than(
            this ILessContinuation<long?> continuation,
            decimal expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<long?> Than(
            this ILessContinuation<long?> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<long?> Than(
            this ILessContinuation<long?> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a < e,
                customMessageGenerator);
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<long> Than(
            this ILessContinuation<long> continuation,
            decimal expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<long> Than(
            this ILessContinuation<long> continuation,
            decimal expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<long> Than(
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
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<long?> Than(
            this IGreaterContinuation<long?> continuation,
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
        public static IGreaterThanContinuation<long?> Than(
            this IGreaterContinuation<long?> continuation,
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
        public static IGreaterThanContinuation<long?> Than(
            this IGreaterContinuation<long?> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a > e,
                customMessageGenerator);
            return continuation.Continue();
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
        public static IMore<long?> Than(
            this ILessContinuation<long?> continuation,
            double expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<long?> Than(
            this ILessContinuation<long?> continuation,
            double expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<long?> Than(
            this ILessContinuation<long?> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a < e,
                customMessageGenerator);
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<long> Than(
            this ILessContinuation<long> continuation,
            double expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<long> Than(
            this ILessContinuation<long> continuation,
            double expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<long> Than(
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
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<long?> Than(
            this IGreaterContinuation<long?> continuation,
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
        public static IGreaterThanContinuation<long?> Than(
            this IGreaterContinuation<long?> continuation,
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
        public static IGreaterThanContinuation<long?> Than(
            this IGreaterContinuation<long?> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a > e,
                customMessageGenerator);
            return continuation.Continue();
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
        public static IMore<DateTime?> Than(
            this ILessContinuation<DateTime?> continuation,
            DateTime expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<DateTime?> Than(
            this ILessContinuation<DateTime?> continuation,
            DateTime expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<DateTime?> Than(
            this ILessContinuation<DateTime?> continuation,
            DateTime expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a < e,
                customMessageGenerator);
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<DateTime> Than(
            this ILessContinuation<DateTime> continuation,
            DateTime expected
        )
        {
            return continuation.Than(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<DateTime> Than(
            this ILessContinuation<DateTime> continuation,
            DateTime expected,
            string customMessage
        )
        {
            return continuation.Than(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<DateTime> Than(
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
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<DateTime?> To(
            this ILessThanOrEqual<DateTime?> continuation,
            DateTime expected
        )
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<DateTime?> To(
            this ILessThanOrEqual<DateTime?> continuation,
            DateTime expected,
            string customMessage
        )
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<DateTime?> To(
            this ILessThanOrEqual<DateTime?> continuation,
            DateTime expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a <= e,
                customMessageGenerator);
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<DateTime> To(
            this ILessThanOrEqual<DateTime> continuation,
            DateTime expected
        )
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<DateTime> To(
            this ILessThanOrEqual<DateTime> continuation,
            DateTime expected,
            string customMessage
        )
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<DateTime> To(
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
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<DateTime> To(
            this IGreaterThanOrEqual<DateTime> continuation,
            DateTime expected
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
        public static IMore<DateTime> To(
            this IGreaterThanOrEqual<DateTime> continuation,
            DateTime expected,
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
        public static IMore<DateTime> To(
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
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<DateTime?> To(
            this IGreaterThanOrEqual<DateTime?> continuation,
            DateTime expected
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
        public static IMore<DateTime?> To(
            this IGreaterThanOrEqual<DateTime?> continuation,
            DateTime expected,
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
        public static IMore<DateTime?> To(
            this IGreaterThanOrEqual<DateTime?> continuation,
            DateTime expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a >= e,
                customMessageGenerator);
            return continuation.More();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<DateTime?> Than(
            this IGreaterContinuation<DateTime?> continuation,
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
        public static IGreaterThanContinuation<DateTime?> Than(
            this IGreaterContinuation<DateTime?> continuation,
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
        public static IGreaterThanContinuation<DateTime?> Than(
            this IGreaterContinuation<DateTime?> continuation,
            DateTime expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a > e,
                customMessageGenerator);
            return continuation.Continue();
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
            this ILessThanOrEqual<decimal?> continuation,
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
            this ILessThanOrEqual<decimal?> continuation,
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
            this ILessThanOrEqual<decimal?> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a <= e,
                customMessageGenerator);
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
            this ILessThanOrEqual<double?> continuation,
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
            this ILessThanOrEqual<double?> continuation,
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
            this ILessThanOrEqual<double?> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a <= e,
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
            this ILessThanOrEqual<double?> continuation,
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
            this ILessThanOrEqual<double?> continuation,
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
            this ILessThanOrEqual<double?> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && new Decimal(a.Value) <= e,
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
        public static IGreaterThanContinuation<decimal?> To(
            this IGreaterContinuation<decimal?> continuation,
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
        public static IGreaterThanContinuation<decimal?> To(
            this IGreaterContinuation<decimal?> continuation,
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
        public static IGreaterThanContinuation<decimal?> To(
            this IGreaterContinuation<decimal?> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a > e,
                customMessageGenerator);
            return continuation.Continue();
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
            this ILessThanOrEqual<long?> continuation,
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
            this ILessThanOrEqual<long?> continuation,
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
            this ILessThanOrEqual<long?> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a <= e,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IMore<T1> To<T1, T2>(
            this ILessThanOrEqual<T1> continuation,
            T2 expected
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IMore<T1> To<T1, T2>(
            this ILessThanOrEqual<T1> continuation,
            T2 expected,
            string customMessage
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IMore<T1> To<T1, T2>(
            this ILessThanOrEqual<T1> continuation,
            T2 expected,
            Func<string> customMessageGenerator
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            return AddMatcher(
                continuation,
                expected,
                (a, e) => TryCompare(a, e) < 1,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To<T1, T2>(
            this ILessThanOrEqual<T1?> continuation,
            T2 expected
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To<T1, T2>(
            this ILessThanOrEqual<T1?> continuation,
            T2 expected,
            string customMessage
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To<T1, T2>(
            this ILessThanOrEqual<T1?> continuation,
            T2 expected,
            Func<string> customMessageGenerator
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => TryCompare(a, e) < 1,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To<T1, T2>(
            this ILessThanOrEqual<T1> continuation,
            T2? expected
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To<T1, T2>(
            this ILessThanOrEqual<T1> continuation,
            T2? expected,
            string customMessage
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To<T1, T2>(
            this ILessThanOrEqual<T1> continuation,
            T2? expected,
            Func<string> customMessageGenerator
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => TryCompare(a, e) < 1,
                customMessageGenerator);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static void To<T1, T2>(
            this ILessThanOrEqual<T1?> continuation,
            T2? expected
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static void To<T1, T2>(
            this ILessThanOrEqual<T1?> continuation,
            T2? expected,
            string customMessage
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Less.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static void To<T1, T2>(
            this ILessThanOrEqual<T1?> continuation,
            T2? expected,
            Func<string> customMessageGenerator
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => TryCompare(a, e) < 1,
                customMessageGenerator);
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
            this IGreaterThanOrEqual<long?> continuation,
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
            this IGreaterThanOrEqual<long?> continuation,
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
            this IGreaterThanOrEqual<long?> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a >= e,
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
            this ILessThanOrEqual<long?> continuation,
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
            this ILessThanOrEqual<long?> continuation,
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
            this ILessThanOrEqual<long?> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a <= e,
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
            this ILessThanOrEqual<long?> continuation,
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
            this ILessThanOrEqual<long?> continuation,
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
            this ILessThanOrEqual<long?> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a <= e,
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
        public static IGreaterThanContinuation<long?> To(
            this IGreaterThanOrEqual<long?> continuation,
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
        public static IGreaterThanContinuation<long?> To(
            this IGreaterThanOrEqual<long?> continuation,
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
        public static IGreaterThanContinuation<long?> To(
            this IGreaterThanOrEqual<long?> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a >= e,
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
        public static IGreaterThanContinuation<T1> To<T1, T2>(
            this IGreaterThanOrEqual<T1> continuation,
            T2 expected
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1> To<T1, T2>(
            this IGreaterThanOrEqual<T1> continuation,
            T2 expected,
            string customMessage
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1> To<T1, T2>(
            this IGreaterThanOrEqual<T1> continuation,
            T2 expected,
            Func<string> customMessageGenerator
        )
            where T1 : IComparable
            where T2 : IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => TryCompare(a, e) > -1,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<T1?> To<T1, T2>(
            this IGreaterThanOrEqual<T1?> continuation,
            T2 expected
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1?> To<T1, T2>(
            this IGreaterThanOrEqual<T1?> continuation,
            T2 expected,
            string customMessage
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1?> To<T1, T2>(
            this IGreaterThanOrEqual<T1?> continuation,
            T2 expected,
            Func<string> customMessageGenerator
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => TryCompare(a, e) > -1,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<T1> To<T1, T2>(
            this IGreaterThanOrEqual<T1> continuation,
            T2? expected
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.To(expected, NULL_STRING);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1> To<T1, T2>(
            this IGreaterThanOrEqual<T1> continuation,
            T2? expected,
            string customMessage
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            return continuation.To(expected, () => customMessage);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
        public static IGreaterThanContinuation<T1> To<T1, T2>(
            this IGreaterThanOrEqual<T1> continuation,
            T2? expected,
            Func<string> customMessageGenerator
        )
            where T1 : struct, IComparable
            where T2 : struct, IComparable
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => TryCompare(a, e) > -1,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<decimal?> To(
            this IGreaterThanOrEqual<decimal?> continuation,
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
        public static IGreaterThanContinuation<decimal?> To(
            this IGreaterThanOrEqual<decimal?> continuation,
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
        public static IGreaterThanContinuation<decimal?> To(
            this IGreaterThanOrEqual<decimal?> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a >= e,
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
        public static IGreaterThanContinuation<long?> To(
            this IGreaterThanOrEqual<long?> continuation,
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
        public static IGreaterThanContinuation<long?> To(
            this IGreaterThanOrEqual<long?> continuation,
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
        public static IGreaterThanContinuation<long?> To(
            this IGreaterThanOrEqual<long?> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a >= e,
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
        public static IGreaterThanContinuation<double?> To(
            this IGreaterThanOrEqual<double?> continuation,
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
        public static IGreaterThanContinuation<double?> To(
            this IGreaterThanOrEqual<double?> continuation,
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
        public static IGreaterThanContinuation<double?> To(
            this IGreaterThanOrEqual<double?> continuation,
            long expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && new Decimal(a.Value) >= e,
                customMessageGenerator);
            return continuation.Continue();
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="continuation">.Greater.Than.Or.Equal.To</param>
        /// <param name="expected">value to compare with</param>
        public static IGreaterThanContinuation<double?> To(
            this IGreaterThanOrEqual<double?> continuation,
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
        public static IGreaterThanContinuation<double?> To(
            this IGreaterThanOrEqual<double?> continuation,
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
        public static IGreaterThanContinuation<double?> To(
            this IGreaterThanOrEqual<double?> continuation,
            decimal expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && new Decimal(a.Value) >= e,
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
        public static IGreaterThanContinuation<double?> To(
            this IGreaterThanOrEqual<double?> continuation,
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
        public static IGreaterThanContinuation<double?> To(
            this IGreaterThanOrEqual<double?> continuation,
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
        public static IGreaterThanContinuation<double?> To(
            this IGreaterThanOrEqual<double?> continuation,
            double expected,
            Func<string> customMessageGenerator
        )
        {
            AddMatcher(
                continuation,
                expected,
                (a, e) => a.HasValue && a >= e,
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

        private static IMore<T1> AddMatcher<T1, T2>(
            ICanAddMatcher<T1> continuation,
            T2 expected,
            Func<T1, T2, bool> test,
            Func<string> customMessageGenerator
        )
        {
            return continuation.AddMatcher(
                actual =>
                {
                    var kindsMatch = HaveMatchingKindsOrCannotCompare(actual, expected);
                    if (!kindsMatch)
                    {
                        Assertions.Throw(
                            @$"Unable to compare dates:\n{
                                    actual.Stringify()
                                }\nand\n{
                                    expected.Stringify()
                                }\nDates have different kinds, so comparisons are non-deterministic"
                        );
                    }

                    var passed = test(actual, expected);
                    var compare = CreateCompareMessageFor(continuation);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => passed
                                ? $"Expected {actual.Stringify()} not to be {compare} {expected.Stringify()}"
                                : $"Expected {actual.Stringify()} to be {compare} {expected.Stringify()}",
                            customMessageGenerator));
                });
        }

        private static bool HaveMatchingKindsOrCannotCompare(
            object left,
            object right
        )
        {
            var leftDate = TryGetDate(left);
            if (leftDate is null)
            {
                return true;
            }

            var rightDate = TryGetDate(right);
            if (rightDate is null)
            {
                return true;
            }

            // if either kind is Unspecified then allow the comparison
            if (leftDate.Value.Kind == DateTimeKind.Unspecified ||
                rightDate.Value.Kind == DateTimeKind.Unspecified)
            {
                return true;
            }

            return leftDate.Value.Kind == rightDate.Value.Kind;
        }

        private static DateTime? TryGetDate(object obj)
        {
            return obj is DateTime dt
                ? dt as DateTime?
                : null;
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