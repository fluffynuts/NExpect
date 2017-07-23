namespace NExpect.Interfaces
{
    public interface IA<T> : ICanAddMatcher<T>
    {
    }

    public interface IAn<T> : ICanAddMatcher<T>
    {
    }

    public interface IHave<T>: ICanAddMatcher<T>
    {
        IA<T> A { get; }
        IAn<T> An { get; }
    }
}