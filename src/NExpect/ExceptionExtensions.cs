using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
// ReSharper disable UnusedMethodReturnValue.Global

namespace NExpect
{
    /// <summary>
    /// Provides extensions for exception testing
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Expects the Action to throw any kind of exception
        /// </summary>
        /// <param name="src">Action to run</param>
        /// <returns>Continuation which can be used to test exception messages</returns>
        
        public static IThrowContinuation<Exception> Throw(
            this ICanAddMatcher<Action> src
        )
        {
            var continuation = new ThrowContinuation<Exception>();
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

        /// <summary>
        /// Allows testing a specific property on the thrown exception
        /// </summary>
        /// <param name="continuation">Exception-based continuation</param>
        /// <param name="fetcher">Func to invoke to get the property you'd like to test</param>
        /// <typeparam name="T">Type of the Exception</typeparam>
        /// <typeparam name="TValue">Type of the property you're testing</typeparam>
        /// <returns>Throw continuation, used to continue with testing the property you just selected</returns>
        /// <exception cref="ArgumentException">Thrown if the continuation is not a known ThrowContinuation. Userland implementation of IThrowContinuation is not supported - yet. Make a request if you need it!</exception>
        public static IExceptionPropertyContinuation<TValue> With<T, TValue>(
            this IThrowContinuation<T> continuation, 
            Func<T, TValue> fetcher
        ) where T: Exception
        {
            var moo = continuation as ThrowContinuation<T>;
            var actual = moo?.Exception ?? moo.TryGetPropertyValue<T>("Exception");
            if (actual == null)
            {
                // TODO: can we kinda-duck this to work on user-generated implementations of IThrowContinuation<T>? And do we care to?
                throw new ArgumentException("With should operate on a ThrowContinuation<T> or at least something with an Exception property that is an Exception.");
            }
            var exceptionPropertyValue = fetcher(actual);

            return Factory.Create<TValue, ValueContinuation<TValue>>(
                exceptionPropertyValue,
                new WrappingContinuation<T, TValue>(
                    moo, c => exceptionPropertyValue
                )
            );
        }

        /// <summary>
        /// Expects the action to throw an exception of type T
        /// </summary>
        /// <param name="src">Action to test</param>
        /// <typeparam name="T">Type of exception which is expected</typeparam>
        /// <returns>Continuation which can be used to test exception messages</returns>
        public static IThrowContinuation<T> Throw<T>(
            this ICanAddMatcher<Action> src
        ) where T : Exception
        {
            var continuation = new ThrowContinuation<T>();
            var expectedType = typeof(T);
            src.AddMatcher(fn =>
            {
                MatcherResult result;
                try
                {
                    fn();
                    result = new MatcherResult(false,
                        $"Expected to throw an exception of type {expectedType.Name} but none was thrown");
                }
                catch (Exception ex)
                {
                    var passed = ex is T;
                    var message = passed
                        ? $"Expected not to throw an exception of type {expectedType.Name}"
                        : $"Expected to throw an exception of type {expectedType.Name} but {ex.GetType().Name} was thrown instead ({ex.Message})";
                    result = new MatcherResult(passed, message);
                    continuation.Exception = ex as T;
                }
                return result;
            });
            return continuation;
        }

        /// <summary>
        /// Used to test exception messages
        /// </summary>
        /// <param name="src">Continuation carrying an exception message</param>
        /// <param name="search">String to look for in the message</param>
        /// <returns>Another continuation so you can do .And() on it</returns>
        public static IStringContainContinuation Containing(
            this IExceptionPropertyContinuation<string> src,
            string search)
        {
            var result = Factory.Create<string, ExceptionMessageContainuationToStringContainContinuation>(
                null, src as IExpectationContext<string>
            );
            src.AddMatcher(s =>
            {
                result.Actual = s;
                var passed = s?.Contains(search) ?? false;
                return new MatcherResult(
                    passed,
                    MessageHelpers.MessageForContainsResult(
                        passed, s, search
                    )
                );
            });
            return result;
        }

        /// <summary>
        /// Used to test exception messages
        /// </summary>
        /// <param name="src">Continuation containing exception message to test</param>
        /// <param name="test">Custom function to test the message -- return true if the test should pass</param>
        /// <returns>Another continuation so you can do .And()</returns>
        public static IStringContainContinuation Matching(
            this IExceptionPropertyContinuation<string> src,
            Func<string, bool> test)
        {
            var result = Factory.Create<string, ExceptionMessageContainuationToStringContainContinuation>(
                null, src as IExpectationContext<string>
            );
            src.AddMatcher(s =>
            {
                result.Actual = s;
                var passed = test(s);
                return new MatcherResult(
                    passed,
                    MessageHelpers.MessageForMatchResult(
                        passed, s
                    )
                );
            });
            return result;
        }

        /// <summary>
        /// Used to test exception messages in the negative
        /// </summary>
        /// <param name="continuation">Continuation containing the exception message</param>
        /// <param name="search">String to search for</param>
        /// <returns>Continuation so you can perform more tests on the message</returns>
        public static IStringContainContinuation Containing(
            this INot<string> continuation,
            string search
        )
        {
            var result = Factory.Create<string, ExceptionMessageContainuationToStringContainContinuation>(
                null, continuation as IExpectationContext<string>
            );
            continuation.AddMatcher(s =>
            {
                result.Actual = s;
                var passed = !s?.Contains(search) ?? true;
                return new MatcherResult(
                    passed,
                    MessageHelpers.MessageForNotContainsResult(
                        passed, s, search
                    )
                );
            });
            return result;
        }

        /// <summary>
        /// Used to test exception messages in the negative
        /// </summary>
        /// <param name="continuation">Continuation containing exception message to test</param>
        /// <param name="test">Custom function to test the message -- return true if the test should pass</param>
        /// <returns>Another continuation so you can do .And()</returns>
        public static IStringContainContinuation Matching(
            this INot<string> continuation,
            Func<string, bool> test
        )
        {
            var result = Factory.Create<string, ExceptionMessageContainuationToStringContainContinuation>(
                null, continuation as IExpectationContext<string>
            );
            continuation.AddMatcher(s =>
            {
                result.Actual = s;
                var passed = !test(s);
                return new MatcherResult(
                    passed,
                    MessageHelpers.MessageForNotMatchResult(
                        passed, s
                    )
                );
            });
            return result;
        }
    }
}