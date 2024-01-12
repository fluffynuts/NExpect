using System;
using System.Collections.Generic;

namespace NExpect.EqualityComparers;

/// <summary>
/// Compares two decimals to a specified number of decimal places,
/// truncated
/// </summary>
public class DecimalsEqualToDecimalPlacesRounded 
    : IEqualityComparer<decimal>, IEqualityComparer<decimal?>
{
    private readonly int _decimalPlaces;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="decimalPlaces"></param>
    public DecimalsEqualToDecimalPlacesRounded(int decimalPlaces)
    {
        _decimalPlaces = decimalPlaces;
    }

    private decimal Round(decimal value)
    {
        return Math.Round(value, _decimalPlaces);
    }

    /// <summary>
    /// Tests equality up to the provided number of decimal places
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool Equals(decimal x,
        decimal y)
    {
        return Round(x) == Round(y);
    }

    /// <summary>
    /// Gets the hashcode of the decimal value to two decimal places,
    /// truncated
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int GetHashCode(decimal value)
    {
        return Round(value).GetHashCode();
    }

    /// <summary>
    /// Tests equality up to the provided number of decimal places
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool Equals(decimal? x, decimal? y)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Tests equality up to the provided number of decimal places
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int GetHashCode(decimal? value)
    {
        return value is null
            ? 0
            : Round(value.Value).GetHashCode();
    }
}