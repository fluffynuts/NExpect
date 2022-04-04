using System;

namespace NExpect;

internal abstract class CustomComparer<TLeft, TRight>
    : ICustomComparer<TLeft, TRight>
{
    public abstract int Compare(TLeft left, TRight right);

    public Type LeftType => typeof(TLeft);
    public Type RightType => typeof(TRight);
}