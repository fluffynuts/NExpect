namespace NExpect.Interfaces;

/// <summary>
/// Provides the .And on .Less.Than
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ILessThanAnd<T>
{
    /// <summary>
    /// Prepares to test the value for being greater than another
    /// </summary>
    IGreaterContinuation<T> Greater { get; }
}