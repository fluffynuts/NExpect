namespace NExpect.Interfaces;

/// <summary>
/// Provides the interface for .Contain in .To.Contain.In.Order(...)
/// </summary>
public interface IStringContain : ICanAddMatcher<string>
{
    /// <summary>
    /// Provides the .In for .To.Contain.In.Order(...)
    /// </summary>
    IStringIn In { get; }
    /// <summary>
    /// Provides the .All for .To.Contain.All.Of
    /// </summary>
    IStringAll All { get; }
    /// <summary>
    /// Provides the .None for .To.Contain.None.Of
    /// </summary>
    IStringNone None { get; }
}