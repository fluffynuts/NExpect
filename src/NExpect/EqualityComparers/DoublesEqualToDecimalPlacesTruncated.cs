using System;
using System.Collections.Generic;

namespace NExpect.EqualityComparers;

/// <summary>
/// Compares two doubles to a specified number of double places,
/// truncated
/// </summary>
public class DoublesEqualToDecimalPlacesTruncated
    : IEqualityComparer<double>
{
    private readonly int _doublePlaces;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="doublePlaces"></param>
    public DoublesEqualToDecimalPlacesTruncated(int doublePlaces)
    {
        _doublePlaces = doublePlaces;
    }

    private double Truncate(double value)
    {
        var mul = (double)Math.Pow(10, _doublePlaces);
        return Math.Truncate(value * mul) / mul;
    }

    /// <summary>
    /// Tests equality up to the provided number of double places
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool Equals(double x,
        double y)
    {
        return Math.Abs(Truncate(x) - Truncate(y)) < Double.Epsilon;
    }

    /// <summary>
    /// Gets the hashcode of the double value to two double places,
    /// truncated
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int GetHashCode(double value)
    {
        return Truncate(value)
            .GetHashCode();
    }
}