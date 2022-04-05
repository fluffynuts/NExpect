// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces;

/// <summary>
/// Provides the extension point for .Greater.Than()
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGreaterContinuation<T> : ICanAddMatcher<T>, IHasActual<T>
{
    /// <summary>
    /// Starts the .Greater.Than.Or.Equal.To chain
    /// </summary>
    IGreaterThan<T> Than { get; }
}

/// <summary>
/// Continues with the .Than part of .Greater.Than.Or.Equal.To
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGreaterThan<T>: IHasActual<T>
{
    /// <summary>
    /// Continues with the .Or of .Greater.Than.Or.Equal.To
    /// </summary>
    IGreaterThanOr<T> Or { get; }
}

/// <summary>
/// Continues with the .Or part of .Greater.Than.Or.Equal.To
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGreaterThanOr<T>: IHasActual<T>
{
    /// <summary>
    /// Continues with the .Equal of .Greater.Than.Or.Equal.To
    /// </summary>
    IGreaterThanOrEqual<T> Equal { get; }
}

/// <summary>
/// Penultimate part of .Greater.Than.Or.Equal.To
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGreaterThanOrEqual<T>: ICanAddMatcher<T>, IHasActual<T>
{
}