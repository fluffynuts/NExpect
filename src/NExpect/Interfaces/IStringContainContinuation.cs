// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the Contain hook point specifically for strings
    /// </summary>
    public interface IStringContainContinuation
        : IStringMore
    {
        /// <summary>
        /// Provides the .In continuation for expecting string fragments
        /// in order
        /// </summary>
        IStringIn In { get; }
    }
}