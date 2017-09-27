namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides an extension point to test if a string starts with an expected
    /// value
    /// </summary>
    public interface IStringStart: ICanAddMatcher<string>
    {
    }
}