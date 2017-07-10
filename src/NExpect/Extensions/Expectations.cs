using System;

namespace NExpect
{
    public interface IMatcherResult
    {
        bool Passed { get; }
        string Message { get; }
    }
    public class MatcherResult: IMatcherResult
    {
        public bool Passed { get; }
        public string Message { get; }
        public MatcherResult()
        {
        }
        public MatcherResult(bool passed)
        {
            Passed = passed;
        }

        public MatcherResult(bool passed, string message)
        {
            Passed = passed;
            Message = message;
        }
    }

    public static class AddExpectationsExtensions
    {
        public static void AddMatcher<T>(this IContinuation<T> continuation, Func<T, IMatcherResult> expectation)
        {
            var asContext = continuation as IExpectationContext<T>;
            if (asContext == null)
            {
                throw new InvalidOperationException($"{continuation} does not implement IExpectationContext<T>");
            }
            asContext.Expect(expectation);
        }
    }

    public static class Expectations
    {
        public static IExpectation<T> Expect<T>(T value)
        {
            return new Expectation<T>(value);
        }
    }

    public static class EqualityExpectations
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
                    $"Expected {expected} but got {continuation.Actual}",
                    customMessage
                ));
            });
        }

        private static string FinalMessageFor(
            string standardMessage,
            string customMessage
        )
        {
            return string.IsNullOrWhiteSpace(customMessage) ? standardMessage : $"{customMessage}\n\n{standardMessage}";
        }
    }
}