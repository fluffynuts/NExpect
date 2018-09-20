# NExpect
An assertions framework for .NET with a BDD-like feel, inspired by Chai and Jasmine, designed to be user-extensible

## Goals
- Expect(NExpect).To.Be.Readable();
  - Because code is for co-workers, not compilers. And your tests are part of your documentation.
- Expect(NExpect).To.Be.Expressive();
  - Because the intent of a test should be easy to understand. The reader can delve into the details when she cares to.
- Expect(NExpect).To.Be.Extensible();
  - Because I can't predict every use-case. I believe that your assertions framework should enable expressive, readable tests through extension.

## Usage
1. Download from [nuget.org](https://nuget.org): `install-package nexpect`
2. Import Expectations statically:
```
using static NExpect.Expectations;
```
3. `Expect` inside your tests, with fluent syntax:
```csharp
// simple equality checks
Expect(1).To.Equal(1);
Expect(true).To.Not.Be.False(); // alt. grammar
Expect(null).To.Be.Null();
// - with negation, order doesn't matter
Expect("moo").Not.To.Equal("cow");
Expect("moo").To.Not.Equal("cow");
Expect(true).Not.To.Be.False();
Expect(false).To.Not.Be.True();

// exceptions
Expect(() => { }).Not.To.Throw();
Expect(() => { throw new ArgumentException("moo", "moo cow"); })
  .To.Throw<ArgumentException>().With.Message.Containing("moo").And.("cow");

// smarter string tests, with fluency
Expect(someString).To.Contain("moo").And("cow");
Expect("moo, said the cow")
    .To.Start.With("moo")
    .And.Contain("said")
    .Then("the")
    .And.End.With("cow");

// collection tests
Expect(someCollection).To.Contain.Exactly(2)
  .Matched.By(item => item.IsWhatWeWant());
Expect(someCollection).To.Contain.Only(1)
  .Deep.Equal.To(new { id = 42, name = "Douglas" });
Expect(someFlags).To.Contain.At.Least(3)
  .Equal.To(true);
Expect(someObject).To.Be
  .An.Instance.Of<Cow>();

// deep and intersection equality testing
var person = new {
  id = 1,
  name = "bob"
};
Expect(person)
  .To.Deep.Equal(new { id = 1, name = "bob" });
Expect(person)
  .To.Intersection.Equal(new { name = "bob" });
```

## Extending
Mostly, you can extend by adding extension methods for ICanAddMatcher<T> where T is the
type you want. You can also extend at any point in the grammar -- some of the "better"
points are ITo<T>, IBe<T> and IHave<T>. You will need another namespace import:
```csharp
using NExpect.MatcherLogic
```
And your extension methods can be like:

```csharp
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

```csharp
// somewhere else...
[Test]
public void FifteenDividedByThree_ShouldEqual_Five()
{
  var result = 15 / 3;
  Expect(result).To.Be.Five();
}
// Yes, yes, simple example is simple.
```

If you've ever written a Jasmine matcher, this should feel familiar.