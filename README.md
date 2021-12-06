# NExpect
An assertions framework for .NET with a BDD-like feel, inspired by Chai and Jasmine, designed to be user-extensible

![Build and Test](https://github.com/fluffynuts/NExpect/workflows/Build%20and%20Test/badge.svg)

![Nuget current version badge](https://img.shields.io/nuget/v/NExpect)

## Goals
- Expect(NExpect).To.Be.Readable();
  - Because code is for co-workers, not compilers. And your tests are part of your documentation.
- Expect(NExpect).To.Be.Expressive();
  - Because the intent of a test should be easy to understand. The reader can delve into the details when she cares to.
- Expect(NExpect).To.Be.Extensible();
  - Because I can't predict every use-case. I believe that your assertions framework should enable expressive, readable tests through extension.

## Tutorial / blog posts:
[https://fluffynuts.github.io/NExpect](https://fluffynuts.github.io/NExpect)
[dev.to](https://dev.to/fluffynuts/introducing-nexpect-555c)

## Usage
1. Download from [nuget.org](https://nuget.org): `install-package nexpect`
2. Import Expectations statically:
```csharp
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
Expect(() =>
  {
    throw new ArgumentException("moo", "moo cow");
  }).To.Throw<ArgumentException>()
  .With.Message.Containing("moo")
  .And.("cow");

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
Expect(new[] { 1, 2, 3 })
  .To.Be.Ordered.Ascending();
Expect(new[] { "c", "b", "a" })
  .To.Be.Ordered.Descending();

// type testing
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
points are ITo<T>, IBe<T>, IHave<T>, IA<T>, IAn<T>. You will need another namespace import:
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

If you have a bunch of existing expectations that you'd like to wrap
up into a nicely-named matcher, `.Compose` has you covered:

```csharp
// before
var cow = animalFactory.MakeCow();
var beetle = animalFactory.MakeBeetle();

// animal factory should make a Jersey cow
Expect(cow.Classification).To.Equal("Mammal");
Expect(cow.Legs).To.Equal(4);
Expect(cow.HasTail).To.Be.True();
Expect(cow.HasHorns).To.Be.True();
Expect(cow.HasSpots).To.Be.True();

// Animal factory should make a rhinoceros beetle
Expect(beetle.Classification).To.Equal("Insect");
Expect(beetle.Legs).To.Equal(6);
Expect(beetle.HasTail).To.Be.False();
Expect(beetle.HasHorns).To.Be.True();
Expect(beetle.HasSpots).To.Be.False();
```

```csharp
// after
var cow = animalFactory.MakeJerseyCow();
var beetle = animalFactory.MakeRhinocerosBeetle();

Expect(cow).To.Be.A.JerseyCow();
Expect(beetle).To.Be.A.RhinocerosBeetle();


// elsewhere:

public static class AnimalMatchers
{
  // the IMore<T> interface allows fluent chaining of expectations
  //  eg:
  //  Expect(cow).To.Be.A.JerseyCow()
  //     .And
  //     .Not.To.Be.A.FrieslandCow();
  public static IMore<Animal> JerseyCow(this IA<Animal> a)
  {
    return a.Compose(actual =>
    {
      Expect(cow.Classification).To.Equal("Mammal");
      Expect(cow.Legs).To.Equal(4);
      Expect(cow.HasTail).To.Be.True();
      Expect(cow.HasHorns).To.Be.True();
      Expect(cow.HasSpots).To.Be.True();
    });
  }
  public static IMore<Animal> RhinocerosBeetle(this IA<Animal> a)
  {
    return a.Compose(actual =>
    {
      Expect(beetle.Classification).To.Equal("Insect");
      Expect(beetle.Legs).To.Equal(6);
      Expect(beetle.HasTail).To.Be.False();
      Expect(beetle.HasHorns).To.Be.True();
      Expect(beetle.HasSpots).To.Be.False();
    });
  }
}
```

When one of the inner expectations fails, NExpect attempts to construct
a nice failure message. As with all expectations, you can always make
failures easier to understand with a custom message string or generator:

```csharp
using NExpect.Implementations;
using NExpect.MatcherLogic;
using NExpect;
using static NExpect.Expectations;

public static class AnimalMatchers
{
  public static IMore<Animal> JerseyCow(this IA<Animal> a)
  {
    return a.Compose(actual =>
    {
      // the Stringify extension method, available on all types,
      // comes from NExpect.Implementation.MessageHelpers and
      // produces a string representation of the object it's
      // operating on which is similar to JSON, so it's easier
      // to read what the object was
      var customMessage = $"Expected {actual.Stringify()} to be a cow";
      Expect(cow.Classification).To.Equal("Mammal", customMessage);
      Expect(cow.Legs).To.Equal(4, customMessage);
      Expect(cow.HasTail).To.Be.True(customMessage);
      Expect(cow.HasHorns).To.Be.True(customMessage);
      Expect(cow.HasSpots).To.Be.True(customMessage);
    });
  }
  public static IMore<Animal> RhinocerosBeetle(this IA<Animal> a)
  {
    return a.Compose(actual =>
    {
      // we can use a generator func to delay generation of the message
      //  which is especially helpful if message generation is expensive
      //  and we'd only like to spend that cpu time on a failure
      Func<string> customMessageGenerator = () => $"Expected {actual.Stringify()} to be a cow";
      Expect(beetle.Classification).To.Equal("Insect", customMessageGenerator);
      Expect(beetle.Legs).To.Equal(6, customMessageGenerator);
      Expect(beetle.HasTail).To.Be.False(customMessageGenerator);
      Expect(beetle.HasHorns).To.Be.True(customMessageGenerator);
      Expect(beetle.HasSpots).To.Be.False(customMessageGenerator);
    });
  }
}
```
