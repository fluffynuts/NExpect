namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation for Throw
    /// </summary>
    public interface IThrowContinuation
    {
        /// <summary>
        /// Throw continuation to facilitate testing the exception message
        /// </summary>
        IWithAfterThrowContinuation With { get; }
    }
}