namespace NExpect
{
    public static class Expectations
    {
        public static IExpectation<T> Expect<T>(T value)
        {
            return new Expectation<T>(value);
        }

        public static void Equal<T>(this IExpectationContinuation<T> expectation, T expected)
        {
            expectation.Equal(expected, null);
        }

        public static void Equal<T>(this IExpectationContinuation<T> expectation, T expected, string customMessage)
        {
            var failed = !expectation.Actual.Equals(expected);
            if (IsNegated(expectation))
                failed = !failed;
                            
            ThrowIf(
                failed,
                $"Expected {expected} but got {expectation.Actual}",
                customMessage
            );
        }


        private static void ThrowIf(
            bool failed,
            string standardMessage,
            string customMessage
        )
        {
            if (!failed)
                return;
            var finalMessage = customMessage == null ? standardMessage : $"{customMessage}\n\n{standardMessage}";
            Assertion.Throw(finalMessage);
        }


        public static bool IsNegated<T>(IExpectationContinuation<T> expectation)
        {
            var asNegated = expectation as INegated;
            return asNegated?.Negated ?? false;
        }
    }
}