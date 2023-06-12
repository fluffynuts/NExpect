using System.Collections.Generic;

namespace NExpect.Interfaces;

/// <summary>
/// Provides the .By dangling grammar type
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBy<T> : ICanAddMatcher<T>
{
}