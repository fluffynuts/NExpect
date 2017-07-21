# NExpect
An assertions framework for .NET with a BDD-like feel, inspired by Chai and Jasmine, designed to be user-extensible

## Usage
1. Download from [nuget.org](https://nuget.org): `install-package nexpect`
2. Import Expectations statically:
```
using static NExpect.Expectations;
```
3. `Expect` inside your tests, with fluent syntax:
```
Expect(() => { }).Not.To.Throw();
Expect(() => { throw new ArgumentException("moo", "moo cow"); })
  .To.Throw<ArgumentException>().With.Message.Containing("moo").And.("cow");
Expect(true).Not.To.Be.False();
Expect(true).To.Not.Be.False(); // alt. grammar
Expect(null).To.Be.Null();
```

## Extending
Mostly, you can extend by adding extension methods for IContinuatin<T> where T is the 
type you want. You can also extend at any point in the grammar -- some of the "better"
points are ITo<T> and IBe<T>. You will need another namespace import:
```
using NExpect.MatcherLogic
```
And your extension methods can be like:

```
public static class MyMatchers
{
  public static void Five(this IBe<int> continuation)
  {
    continuation.AddMatcher(actual =>
    {
      var passed = actual == 5;
      var message = passed
                    ? $"Expected {actual} not to be 5"
                    : $"Expected {actual} to be 5";
      return new MatcherResult(passed, message);
    });
  }
}
```

If you've ever written a Jasmine matcher, this should feel familiar.