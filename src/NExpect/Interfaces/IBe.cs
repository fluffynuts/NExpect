namespace NExpect.Interfaces
{
    public interface IBe<T>
    {
        INotAfterBe<T> Not { get; }
        IEqualityContinuation<T> Equal { get; }
        IGreaterContinuation<T> Greater { get; }
    }

    public interface IGreaterContinuation<T>
    {
    }

}