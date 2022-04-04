namespace NExpect;

// ReSharper disable once UnusedType.Global
internal class CharToDoubleComparer
    : CustomComparer<char, double>
{
    public override int Compare(char left, double right)
    {
        return ((double)left).CompareTo(right);
    }
}