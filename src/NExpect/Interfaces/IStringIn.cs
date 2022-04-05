namespace NExpect.Interfaces;

/// <summary>
/// Provides the interface from which the .Order(...)
/// part of .To.Contain.In.Order(...) can extend
/// </summary>
public interface IStringIn: ICanAddMatcher<string>
{
}