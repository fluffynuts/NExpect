using System.Collections.Generic;

namespace NExpect.Tests.Collections;

public class FirstLetterComparer : IEqualityComparer<string>
{
    public bool Equals(string x, string y)
    {
        if (x == null &&
            y == null)
        {
            return true;
        }

        if (x == null ||
            y == null)
        {
            return false;
        }

        if (x.Length == 0 &&
            y.Length == 0)
        {
            return true;
        }

        if (x.Length == 0 ||
            y.Length == 0)
        {
            return false;
        }

        return x[0] == y[0];
    }

    public int GetHashCode(string obj)
    {
        var chr = string.IsNullOrEmpty(obj)
            ? null
            : obj[0] as char?;
        return chr == null
            ? 0
            : chr.GetHashCode();
    }
}
