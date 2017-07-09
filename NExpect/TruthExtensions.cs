namespace NExpect
{
    public static class TruthExtensions
    {
        public static void True(this IExpectationContinuation<bool> expectation)
        {
            expectation.True(null);
        }

        public static void True(this IExpectationContinuation<bool> expectation, string message)
        {
            expectation.Equal(true, message);
        }

        public static void True(this IExpectationContinuation<bool?> expectation)
        {
            expectation.True(null);
        }

        public static void True(this IExpectationContinuation<bool?> expectation, string message)
        {
            expectation.Equal(true, message);
        }

        public static void False(this IExpectationContinuation<bool> expectation)
        {
            expectation.False(null);
        }

        public static void False(this IExpectationContinuation<bool> expectation, string message)
        {
            expectation.Equal(true, message);
        }

        public static void False(this IExpectationContinuation<bool?> expectation)
        {
            expectation.False(null);
        }

        public static void False(this IExpectationContinuation<bool?> expectation, string message)
        {
            expectation.Equal(true, message);
        }
    }
}