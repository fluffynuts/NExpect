// ReSharper disable UnusedTypeParameter

namespace NExpect.Interfaces;

/// <summary>
/// Continuation to provide the ".An" grammar for collections
/// </summary>
/// <typeparam name="T">Type of the continuation</typeparam>
public interface ICollectionAn<T>
{
    /// <summary>
    /// Prepares to check the Type of Actual
    /// </summary>
    IInstanceContinuation Instance { get; }
}