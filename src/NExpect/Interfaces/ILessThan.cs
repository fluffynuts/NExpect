namespace NExpect.Interfaces;

/// <summary>
/// Continues with the .Than part of .Less.Than.Or.Equal.To
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ILessThan<T>: IHasActual<T>
{
    /// <summary>
    /// Continues with the .Or of .Less.Than.Or.Equal.To
    /// </summary>
    ILessThanOr<T> Or { get; }
}