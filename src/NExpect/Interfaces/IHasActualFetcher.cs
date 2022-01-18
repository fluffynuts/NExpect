using System;

namespace NExpect.Interfaces;

/// <summary>
/// The contract that a context with a lazy fetcher provides
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IHasActualFetcher<out T>
{
    /// <summary>
    /// 
    /// </summary>
    Func<T> ActualFetcher { get; }
}