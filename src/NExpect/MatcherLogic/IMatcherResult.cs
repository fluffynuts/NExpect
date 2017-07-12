namespace NExpect.MatcherLogic
{
    public interface IMatcherResult
    {
        bool Passed { get; }
        string Message { get; }
    }
}