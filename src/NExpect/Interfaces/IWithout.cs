using System.Collections.Generic;

namespace NExpect.Interfaces;

/// <summary>
/// Provides the .Without dangling grammar type
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IWithout<T>: IWith<T>
{
}