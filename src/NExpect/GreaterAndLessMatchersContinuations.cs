using NExpect.Implementations;
using NExpect.Implementations.Numerics;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Assertions;

namespace NExpect;

internal static class GreaterAndLessMatchersContinuations
{
    internal static IGreaterThanContinuation<T> Continue<T>(
        this IGreaterContinuation<T> continuation
    )
    {
        return Forget(
            ContinuationFactory.Create<T, GreaterThanContinuation<T>>(
                continuation.GetActual,
                continuation as IExpectationContext<T>
            )
        );
    }

    internal static IGreaterThanContinuation<T> Continue<T>(
        this IGreaterThanOrEqual<T> continuation
    )
    {
        return Forget(
            ContinuationFactory.Create<T, GreaterThanContinuation<T>>(
                continuation.GetActual,
                continuation as IExpectationContext<T>
            )
        );
    }

    internal static ILessThanContinuation<T> Continue<T>(
        this ILessContinuation<T> continuation
    )
    {
        return Forget(
            ContinuationFactory.Create<T, LessThanContinuation<T>>(
                continuation.GetActual,
                continuation as IExpectationContext<T>
            )
        );
    }

    internal static ILessThanContinuation<T> Continue<T>(
        this ILessThanOrEqual<T> continuation
    )
    {
        return Forget(
            ContinuationFactory.Create<T, LessThanContinuation<T>>(
                continuation.GetActual,
                continuation as IExpectationContext<T>
            )
        );
    }
}