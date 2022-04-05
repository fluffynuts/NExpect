namespace NExpect.Interfaces;

/// <summary>
/// Continues with the .Or part of .Less.Than.Or.Equal.To
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ILessThanOr<T>: IHasActual<T>
{
    /// <summary>
    /// Continues with the .Equal of .Less.Than.Or.Equal.To
    /// </summary>
    ILessThanOrEqual<T> Equal { get; }
}