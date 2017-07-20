using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Extensions
{
    public static class ExceptionExtensions
    {
        public static IThrowContinuation Throw(
            this IContinuation<Action> src
        )
        {
            var continuation = new ThrowContinuation();
            src.AddMatcher(fn =>
            {
                MatcherResult result;
                try
                {
                    fn();
                    result = new MatcherResult(false, "Expected to throw an exception but none was thrown");
                }
                catch (Exception ex)
                {
                    continuation.Exception = ex;
                    result = new MatcherResult(true, $"Exception thrown:\n${ex.Message}\n${ex.StackTrace}");
                }
                return result;
            });
            return continuation;
        }

        public static IThrowContinuation Throw<T>(
            this IContinuation<Action> src
        ) where T : Exception
        {
            var continuation = new ThrowContinuation();
            var expectedType = typeof(T);
            src.AddMatcher(fn =>
            {
                MatcherResult result;
                try
                {
                    fn();
                    result = new MatcherResult(false,
                        $"Expected to throw an exception of type {expectedType.Name} but none was thrown");
                }
                catch (Exception ex)
                {
                    var passed = ex is T;
                    var message = passed
                        ? $"Expected not to throw an exception of type {expectedType.Name}"
                        : $"Expected to throw an exception of type {expectedType.Name} but {ex.GetType().Name} was thrown instead ({ex.Message})";
                    result = new MatcherResult(passed, message);
                    continuation.Exception = ex;
                }
                return result;
            });
            return continuation;
        }

        public static IStringContainContinuation Containing(
            this IExceptionMessageContinuation src,
            string search)
        {
            var result = Factory.Create<string, ExceptionMessageContainuationToStringContainContinuation>(
                null, src as IExpectationContext<string>
            );
            src.AddMatcher(s =>
            {
                result.Actual = s;
                var passed = s?.Contains(search) ?? false;
                return new MatcherResult(
                    passed,
                    MessageHelpers.MessageForContainsResult(
                        passed, s, search
                    )
                );
            });
            return result;
        }

        public static IStringContainContinuation Containing(
            this INot<string> continuation,
            string search
        )
        {
            var result = Factory.Create<string, ExceptionMessageContainuationToStringContainContinuation>(
                null, continuation as IExpectationContext<string>
            );
            continuation.AddMatcher(s =>
            {
                result.Actual = s;
                var passed = !s?.Contains(search) ?? true;
                return new MatcherResult(
                    passed,
                    MessageHelpers.MessageForNotContainsResult(
                        passed, s, search
                    )
                );
            });
            return result;
        }
    }

    public class ExceptionMessageContainuationToStringContainContinuation
        : ExpectationContext<string>, IStringContainContinuation
    {
        public string Actual { get; set; }

        public ExceptionMessageContainuationToStringContainContinuation(string actual)
        {
            Actual = actual;
        }
    }
}