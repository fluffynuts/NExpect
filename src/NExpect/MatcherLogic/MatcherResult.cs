namespace NExpect.MatcherLogic
{
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
}