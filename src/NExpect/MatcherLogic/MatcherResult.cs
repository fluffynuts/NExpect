// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

using System;

namespace NExpect.MatcherLogic
{
    /// <summary>
    /// Implements the IMatcher result, use to contain your
    /// matcher result
    /// </summary>
    public class MatcherResult : IMatcherResult
    {
        private Func<string> _messageGenerator;
        private string _message;
        private Func<Func<string>> _messageGeneratorGenerator;

        /// <inheritdoc />
        public bool Passed { get; set; }

        /// <inheritdoc />
        public string Message => _message ?? (_message = MessageGenerator?.Invoke());

        private Func<string> MessageGenerator => _messageGenerator ??
                                                 (_messageGenerator = _messageGeneratorGenerator?.Invoke());

        /// <summary>
        /// Constructor with just pased value -- set message later
        /// </summary>
        /// <param name="passed">Whether or not the matcher passed</param>
        public MatcherResult(bool passed)
        {
            Passed = passed;
        }

        /// <summary>
        /// Constructor with static message
        /// Consider rather using the constructor with the Func&lt;string&gt;
        /// since that message will only be evaluated late, once, meaning that
        /// if there is any costly work to be done (eg deep .Stringify()) calls,
        /// then that work will only be done when the message is read to display
        /// to the user, instead of every time the assertion is made.
        /// </summary>
        /// <param name="passed">Did the matcher pass?</param>
        /// <param name="message">Message about the pass / fail</param>
        public MatcherResult(bool passed, string message) :
            this(passed, () => message)
        {
        }

        /// <summary>
        /// Cosntructor with message generator
        /// </summary>
        /// <param name="passed">Did the matcher pass?</param>
        /// <param name="messageGenerator">Generator about the pass / fail</param>
        public MatcherResult(bool passed, Func<string> messageGenerator)
        {
            _messageGenerator = messageGenerator;
            Passed = passed;
        }

        /// <summary>
        /// Constructor with delayed message generator generator
        /// </summary>
        /// <param name="passed"></param>
        /// <param name="messageGeneratorGenerator"></param>
        public MatcherResult(
            bool passed,
            Func<Func<string>> messageGeneratorGenerator
        )
        {
            _messageGeneratorGenerator = messageGeneratorGenerator;
            Passed = passed;
        }
    }
}