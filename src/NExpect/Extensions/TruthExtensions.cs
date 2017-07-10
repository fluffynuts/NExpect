namespace NExpect
{
    public static class TruthExtensions
    {
        public static void True(this IContinuation<bool> expectation)
        {
            expectation.True(null);
        }

        public static void True(this IContinuation<bool> expectation, string message)
        {
            expectation.Equal(true, message);
        }

        public static void True(this IContinuation<bool?> expectation)
        {
            expectation.True(null);
        }

        public static void True(this IContinuation<bool?> expectation, string message)
        {
            expectation.Equal(true, message);
        }

        public static void False(this IContinuation<bool> expectation)
        {
            expectation.False(null);
        }

        public static void False(this IContinuation<bool> expectation, string message)
        {
            expectation.Equal(false, message);
        }

        public static void False(this IContinuation<bool?> expectation)
        {
            expectation.False(null);
        }

        public static void False(this IContinuation<bool?> expectation, string message)
        {
            expectation.Equal(false, message);
        }
    }
}