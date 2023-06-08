using System;
using NExpect.Exceptions;
using NExpect.MatcherLogic;

namespace NExpect.Implementations;

internal static class MatcherRunner
{
    public static IMatcherResult RunMatcher<T>(
        SweepableItem owner,
        T actual,
        bool negated,
        Func<T, IMatcherResult> matcher
    )
    {
        IMatcherResult result;
        Assertions.Forget(owner);
        try
        {
            result = matcher(actual);
        }
        catch (UnmetExpectationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            ProcessMatcherException(ex);
            return default;
        }

        ProcessMatcherResult(negated, result);
        return result;
    }

    public static void ProcessMatcherException(
        Exception matcherException
    )
    {
        // TODO: make this better, ie, include the exception as an inner
        NExpect.Assertions.Throw(
            $"Exception whilst running matcher: {matcherException}",
            matcherException);
    }

    public static void ProcessMatcherResult(
        bool negated,
        IMatcherResult result
    )
    {
        var isPass = negated && result is not EnforcedMatcherResult
            ? !result.Passed
            : result.Passed;

        if (isPass)
        {
            return;
        }

        NExpect.Assertions.Throw(result.Message, result.LocalException);
    }
}