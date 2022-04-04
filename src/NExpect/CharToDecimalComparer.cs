namespace NExpect;

// ReSharper disable once UnusedType.Global
internal class CharToDecimalComparer
    : CustomComparer<char, decimal>
{
    public override int Compare(char left, decimal right)
    {
        return ((decimal)left).CompareTo(right);
    }
}