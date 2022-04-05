namespace NExpect.Interfaces;

/// <summary>
/// Penultimate part of .Less.Than.Or.Equal.To
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ILessThanOrEqual<T>: ICanAddMatcher<T>, IHasActual<T>
{
}