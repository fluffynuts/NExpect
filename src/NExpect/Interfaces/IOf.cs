using System.Collections.Generic;

namespace NExpect.Interfaces;

/// <summary>
/// Provides the .Of dangling grammar type
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOf<T> : ICanAddMatcher<T>
{
}