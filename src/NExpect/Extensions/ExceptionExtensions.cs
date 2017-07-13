using System;
using System.Threading.Tasks;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Extensions
{
    public interface IWithAfterThrowContinuation : IContinuation<Exception>
    {
        IContinuation<string> Message { get; }
    }

    public interface IThrowContinuation
    {
        IWithAfterThrowContinuation With { get; }
    }


    internal class CollectionContinuation<T> : IContinuation<T>
    {
        public T Actual { get; }
    }

    public static class ExceptionExtensions
    {
        public static IThrowContinuation Throw(this IContinuation<Action> src)
        {
            var continuation = new ThrowContinuation();
            src.AddMatcher(fn =>
            {
                MatcherResult result;
                try
                {
                    fn();
                    result = new MatcherResult(false, "Expected to throw an exception but none was thrown");
                }
                catch (Exception ex)
                {
                    continuation.Exception = ex;
                    result = new MatcherResult(true, $"Exception thrown:\n${ex.Message}\n${ex.StackTrace}");
                }
                return result;
            });
            return continuation;
        }
    }

    public class ThrowContinuation : ExpectationContext<Exception>, IThrowContinuation
    {
        public Exception Exception { get; set; }

        public IWithAfterThrowContinuation With => Factory.Create<Exception, WithAfterThrowContinuation>(Exception,
            this);
    }

    public class WithAfterThrowContinuation :
        ExpectationContext<Exception>, IWithAfterThrowContinuation
    {
        public IContinuation<string> Message => 
            new WrappingContinuation<Exception,string>
        public Exception Actual { get; set; }


        public WithAfterThrowContinuation(Exception ex)
        {
            Actual = ex;
        }
    }

    internal class WrappingContinuation<TFrom, To> : ExpectationContext<To>, IContinuation<To>
    {
        public To Actual => _unwrap(_wrapped);

        private readonly IExpectationContext<TFrom> _wrapped;
        private readonly Func<IExpectationContext<TFrom>, To> _unwrap;

        internal WrappingContinuation(IExpectationContext<TFrom> toWrap, Func<IExpectationContext<TFrom>, To> unwrap)
        {
            _wrapped = toWrap;
            _unwrap = unwrap;
        }
    }


    public static class CollectionExtensions
    {
        public static void Containing(IContinuation<string> src)
        {
        }
    }

    public class CollectionContinuationOfString :
        ExpectationContext<string>,
        ICollectionContinuation<string>,
        IContinuation<string>
    {
        public void Containing(string search)
        {
            this.AddMatcher
        }

        public string Actual { get; }
    }
}