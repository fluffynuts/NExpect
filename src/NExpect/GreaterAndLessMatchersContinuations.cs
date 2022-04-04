using NExpect.Implementations;
using NExpect.Implementations.Numerics;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect;

internal static class GreaterAndLessMatchersContinuations
{
    internal static IGreaterThanContinuation<T> Continue<T>(
        this IGreaterContinuation<T> continuation
    )
    {
        return ContinuationFactory.Create<T, GreaterThanContinuation<T>>(
            continuation.GetActual,
            continuation as IExpectationContext<T>
        );
    }

    internal static IGreaterThanContinuation<T> Continue<T>(
        this IGreaterThanOrEqual<T> continuation
    )
    {
        return ContinuationFactory.Create<T, GreaterThanContinuation<T>>(
            continuation.GetActual,
            continuation as IExpectationContext<T>
        );
    }

    internal static ILessThanContinuation<T> Continue<T>(
        this ILessContinuation<T> continuation
    )
    {
        return ContinuationFactory.Create<T, LessThanContinuation<T>>(
            continuation.GetActual,
            continuation as IExpectationContext<T>
        );
    }

    internal static ILessThanContinuation<T> Continue<T>(
        this ILessThanOrEqual<T> continuation
    )
    {
        return ContinuationFactory.Create<T, LessThanContinuation<T>>(
            continuation.GetActual,
            continuation as IExpectationContext<T>
        );
    }

}