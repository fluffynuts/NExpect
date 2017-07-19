namespace NExpect.Interfaces
{
    public interface IBe<T>
    {
        INotAfterBe<T> Not { get; }
        IEqualityContinuation<T> Equal { get; }
        IGreaterContinuation<T> Greater { get; }
        ILessContinuation<T> Less { get; }
    }

    public interface IGreaterContinuation<T>: IGreaterOrLessContinuation<T>
    {
    }

    public interface ILessContinuation<T>: IGreaterOrLessContinuation<T>
    {
    }

    // TODO: I'm being lazy, finding a common root to extend. 
    //  Does it pollute the grammar?
    public interface IGreaterOrLessContinuation<T>
    {
    }
}