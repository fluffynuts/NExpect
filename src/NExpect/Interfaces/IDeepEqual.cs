namespace NExpect.Interfaces;

/// <summary>
/// Provides the longer .To.Be.Deep.Equal.To syntax
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDeepEqual<T>: ICanAddMatcher<T>
{
}