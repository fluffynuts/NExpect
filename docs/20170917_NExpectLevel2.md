In a [prior post](20170917_NExpectLevel1.md), I covered simple value testing with
[NExpect](https://github.com/fluffynuts/NExpect). In this post, I'd like to delve into collection assertions, since they are fairly common.

First, the simplest: asserting that a collection contains a desired value:
```csharp
[Test]
public void SimpleContains()
{
    // Arrange
    var collection = new[] { "a", "b", "c" };

    // Assert
    Expect(collection).To.Contain("a");
}
```
This is what you would expect from any other assertions framework.

Something has always bothered me about this kind of testing though. In particular, 
the test above passes just as well as this one:
```csharp
[Test]
public void MultiContains()
{
    // Arrange
    var collection = new[] { "a", "b", "c", "a" };

    // Assert
    Expect(collection).To.Contain("a");
}
```
And yet they are not functionally equivalent from where I stand.
Which makes the test feel a little flaky to me. This is why
[NExpect](https://github.com/fluffynuts/NExpect) actually didn't even have the
above assertion first. Instead, I was interested in being more specific:
```csharp
[Test]
public void SpecificContains()
{
    // Arrange
    var collection = new[] { "a", "b", "c", "a" };

    // Assert
    Expect(collection)
        .To.Contain.Exactly(1).Equal.To("b");
    Expect(collection)
        .To.Contain.At.Least(1).Equal.To("c");
    Expect(collection)
        .To.Contain.At.Most(2).Equal.To("a");
}
```
Now my tests are speaking specifically about what they expect.

Sometimes you just want to test the size of a collection, but you
don't really care if it's an `IEnumerable<T>`, a `List<T>` or an array.
Other testing frameworks may let you down, requiring you to write a test against the
`Count` or `Length` property, meaning that when
your implementation changes from returning, eg, `List<T>` to array
(which may be smart: `List<T>` is not only a heavier construct but implies that you can add
to the collection), your tests will fail for no really good reason --
your implementation still returns 2 `FrobNozzle`s, so who cares if the correct property to
check is `Length` or `Count`? I know that I don't.

That's Ok, [NExpect](https://github.com/fluffynuts/NExpect) takes away the care of having to consider that nuance and allows you to spell out what you actually mean:

```csharp
  [Test]
  public void SizeTest()
  {
    // Arrange
    var collection = new[] { "a", "b", "c" };
    var lonely = new[] { 1 };
    var none = new bool[0];

    // Assert
    Expect(collection).To.Contain.Exactly(3).Items();
    Expect(lonely).To.Contain.Exactly(1).Item();

    Expect(none).To.Contain.No().Items();
    Expect(none).Not.To.Contain.Any().Items();
    Expect(none).To.Be.Empty();
  }
```

Note that the last three are functionally equivalent. They are just different ways to say the same thing. [NExpect](https://github.com/fluffynuts/NExpect) is
designed to help you express your intent in your tests, and, as such, there may be more than one way to achieve the same goal:

```csharp
  [Test]
  public void AnyWayYouLikeIt()
  {
    // Assert
    Expect(1).Not.To.Equal(2);
    // ... is exactly equivalent to
    Expect(1).To.Not.Equal(2);

    Expect(3).To.Equal(3);
    // ... is exactly equivalent to
    Expect(3).To.Be.Equal.To(3);
  }
```

There are bound to be other examples. The point is that 
[NExpect](https://github.com/fluffynuts/NExpect) attempts to provide you with the language
to write your assertions in a readable manner without enforcing a specific grammar.

Anyway, on with collection testing!

You can test for equality, meaning items match at the same point in the collection (this is 
_not_ reference equality testing on the collection, but would equate to reference equality 
testing on items of `class` type or value equality testing on items of `struct` type:
```csharp
[Test]
public void CollectionEquality()
{
  // Assert
  Expect(new[] { 1, 2, 3 })
    .To.Be.Equal.To(new[] { 1, 2, 3 });
}
```
You can also test out-of-order:
```csharp
[Test]
public void CollectionEquivalence()
{
  // Assert
  Expect(new[] { 3, 1, 2 })
    .To.Be.Equivalent.To(new[] { 1, 2, 3 });
}
```
Which is all nice and dandy if you're testing value types or can do reference equality testing 
(or at least testing where each object has a `.Equals` override which does the comparison for 
you). It doesn't help when you have more complex objects -- but 
[NExpect](https://github.com/fluffynuts/NExpect) hasn't forgotten you there: you can do deep 
equality testing on collections too:

```csharp
[Test]
public void CollectionDeepEquality()
{
  var input = new[] {
    new Person() { Id = 1, Name = "Jane", Alive = true },
    new Person() { Id = 2, Name = "Bob", Alive = false }
  };
  // Assert
  Expect(input.AsObjects())
    .To.Be.Deep.Equal.To(new[]
      {
        new { Id = 1, Name = "Jane", Alive = true },
        new { Id = 2, Name = "Bob", Alive = false }
      });
}
```

Note that, much like the points on "Who's line is it, anyway?", the types don't matter. 
This is deep equality testing (: However, we did need to "dumb down" the input collection to 
a collection of objects with the provided `.AsObjects()` extension method so that the test 
would compile, otherwise there's a type mismatch at the other end. Still, this is, imo, 
more convenient than the alternative: item-for-item testing, property-by-property.

The above is incomplete without equivalence, of course:

```csharp
[Test]
public void CollectionDeepEquivalence()
{
  var input = new[] {
    new Person() { Id = 1, Name = "Jane", Alive = true },
    new Person() { Id = 2, Name = "Bob", Alive = false }
  };
  // Assert
  Expect(input.AsObjects())
    .To.Be.Deep.Equivalent.To(new[] {
      new { Id = 2, Name = "Bob", Alive = false },
      new { Id = 1, Name = "Jane", Alive = true }
    });
}
```

And intersections are thrown in for good measure:

```csharp
[Test]
public void CollectionIntersections()
{
  var input = new[] {
    new Person() { Id = 1, Name = "Jane", Alive = true },
    new Person() { Id = 2, Name = "Bob", Alive = false }
  };
  // Assert
  Expect(input.AsObjects())
    .To.Be.Intersection.Equivalent.To(new[] {
      new { Id = 2, Name = "Bob" },
      new { Id = 1, Name = "Jane" }
    });
  Expect(input.AsObjects())
    .To.Be.Intersection.Equivalent.To(new[] {
      new { Id = 1, Name = "Jane" },
      new { Id = 2, Name = "Bob" }
    });
}
```

You can also test with a custom IEqualityComparer&lt;T&gt;:

```csharp
[Test]
public void CollectionIntersections()
{
  var input = new[] {
    new Person() { Id = 1, Name = "Jane", Alive = true },
    new Person() { Id = 2, Name = "Bob", Alive = false }
  };
  // Assert
  Expect(input)
    .To.Contain.Exactly(1).Equal.To(
      new Person() { Id = 2, Name = "Bob" },
      new PersonEqualityComparer()
  );
}
```

or with a quick-and-dirty Func&lt;T&gt;:

```csharp
[Test]
public void CollectionIntersections()
{
  var input = new[] {
    new Person() { Id = 1, Name = "Jane", Alive = true },
    new Person() { Id = 2, Name = "Bob", Alive = false }
  };
  // Assert
  Expect(input.AsObjects())
    .To.Contain.Exactly(1).Matched.By(
      p =&gt; p.Id == 1 &amp;&amp; p.Name == "Jane"
    );
}
```

And all of this is really just the start. The real expressive power of 
[NExpect](https://github.com/fluffynuts/NExpect) comes in how _you_ extend it.

But more on that in the [next episode](20170917_NExpectLevel3.md) (:
