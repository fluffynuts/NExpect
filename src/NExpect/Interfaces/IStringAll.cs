namespace NExpect.Interfaces;

/// <summary>
/// Provides the interface for .Contain.All in .To.Contain.All.Of
/// </summary>
public interface IStringAll: ICanAddMatcher<string>
{
}

/// <summary>
/// Provides the interface for .Contain.None in .To.Contain.None.Of
/// </summary>
public interface IStringNone: ICanAddMatcher<string>
{
}