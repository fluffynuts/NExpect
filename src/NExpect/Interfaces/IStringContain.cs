namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the interface for .Contain in .To.Contain.In.Order(...)
    /// </summary>
    public interface IStringContain
    {
        /// <summary>
        /// Provides the .In for .to.Contain.In.Order(...)
        /// </summary>
        IStringIn In { get; }
    }
}