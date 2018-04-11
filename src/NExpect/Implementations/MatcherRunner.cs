using System;
using NExpect.Exceptions;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    internal static class MatcherRunner
    {
        public static void RunMatcher<T>(
            T actual,
            bool negated,
            Func<T, IMatcherResult> matcher
        )
        {
            IMatcherResult result;
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
                return;
            }

            ProcessMatcherResult(negated, result);
        }

        public static void ProcessMatcherException(
            Exception matcherException
        )
        {
            // TODO: make this better, ie, include the exception as an inner
            Assertions.Throw(
                $"Exception whilst running matcher: {matcherException}",
                matcherException);
        }

        public static void ProcessMatcherResult(
            bool negated,
            IMatcherResult result
        )
        {
            var isPass = negated
                ? !result.Passed
                : result.Passed;
            if (isPass)
                return;
            Assertions.Throw(result.Message);
        }
    }
}