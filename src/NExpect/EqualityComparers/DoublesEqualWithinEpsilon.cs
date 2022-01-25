using System;
using System.Collections.Generic;

namespace NExpect.EqualityComparers;

/// <summary>
/// Compares two decimals to a specified number of decimal places,
/// truncated
/// </summary>
public class DoublesEqualWithinEpsilon : IEqualityComparer<double>
{
    /// <summary>
    /// Constructor
    /// </summary>
    public DoublesEqualWithinEpsilon()
    {
    }

    /// <summary>
    /// Tests equality within Double.Epsilon
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool Equals(double x,
        double y)
    {
        return Math.Abs(x - y) < Double.Epsilon;
    }

    /// <summary>
    /// Gets the hashcode of the double value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int GetHashCode(double value)
    {
        return value.GetHashCode();
    }
}