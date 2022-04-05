namespace NExpect.Interfaces;

/// <summary>
/// Provides the .And on .Greater.Than
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGreaterThanContinuation<T> 
{
    /// <summary>
    /// Prepares to test for more than just greatness
    /// </summary>
    IGreaterThanAnd<T> And { get; }
}

/// <summary>
/// Provides the .And on .Less.Than
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ILessThanContinuation<T>
{
    /// <summary>
    /// Prepares to test for more than just lesserness
    /// </summary>
    ILessThanAnd<T> And { get; }
}