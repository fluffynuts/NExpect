// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
namespace NExpect.MatcherLogic
{
    /// <summary>
    /// Implements the IMatcher result, use to contain your
    /// matcher result
    /// </summary>
    public class MatcherResult: IMatcherResult
    {
        /// <inheritdoc />
        public bool Passed { get; set; }

        /// <inheritdoc />
        public string Message { get; set; }

        /// <summary>
        /// Empty constructor -- allows setting properties later
        /// </summary>
        public MatcherResult()
        {
        }

        /// <summary>
        /// Constructor with just pased value -- set message later
        /// </summary>
        /// <param name="passed">Whether or not the matcher passed</param>
        public MatcherResult(bool passed)
        {
            Passed = passed;
        }

        /// <summary>
        /// Constructor with all parameters
        /// </summary>
        /// <param name="passed">Did the matcher pass?</param>
        /// <param name="message">Message about the pass/fail</param>
        public MatcherResult(bool passed, string message)
        {
            Passed = passed;
            Message = message;
        }
    }
}