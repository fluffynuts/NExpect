In previous posts, I've examined how to do [simple](20170917_NExpectLevel1.md)
and [collection-based](20170917_NExpectLevel2.md) assertions with
[NExpect](https://github.com/fluffynuts/NExpect)

These have enabled two of the design goals of NExpect:<br />

- Expect(NExpect).To.Be.Readable();
    - Because code is for co-workers, not compilers. And your tests are
    part of your documentation.
- Expect(NExpect).To.Be.Expressive();
    - Because the intent of a test should be easy to understand.
    The reader can delve into the details when she cares to.

Now, we come on to the third goal, inspired by [Jasmine]("https://jasmine.github.io/"):
easy user extension of the testing framework to facilitate expressive testing of more
complex concepts.

Most of the "words" in NExpect can be "attached to" with extension methods. So the first question you have to ask is "how do I want to phrase my assertion?". You could use the already-covered .To or .To.Be:
```csharp
internal static class Matchers
{
  internal static void Odd(
    this IBe<int> be
  )
  {
    be.AddMatcher(actual =&gt;
    {
      var passed = actual % 2 == 1;
      var message = passed
                    ? $"Expected {actual} not to be odd"
                    : $"Expected {actual} to be odd";
      return new MatcherResult(
        passed,
        message
      );
    });
  }
}
</int>```
<br />
The above extension enables the following test:
<br />
```csharp
  [Test]
  public void ILikeOddNumbers()
  {
    // Assert
    Expect(1).To.Be.Odd();
    Expect(2).Not.To.Be.Odd();
  }
```

There are a few concepts to unpack:
#### .AddMatcher()
This is how you add a "matcher" (term borrowed from Jasmine... Sorry, I couldn't come up with a
better name, so it stuck) to a grammar continuation like .To or .Be. Note that we just create an
extension method on IBe&lt;T&gt; where T is the type you'd like to test against, and the
internals of that extension basically just add a matcher. This takes in a Func&lt;T,
IMatcherResult&gt; so your actual matcher needs to return an IMatcher result, which really is
just a flag about whether or not the test passed and the message to display if the test failed.

#### Pass or fail?
This is the heart of your assertion and can be as tricky as you like. Obviously, your matcher
could also have multiple exit strategies with specific messages about each possible failure. But
the bit that takes a little getting used to is that you're writing a matcher which could be used
with .Not in the grammar chain, so you should cater for that eventuality.

#### Meaningful messages
There's a simple strategy here: get the passed value as if you're doing a positive assertion (ie,
as if there is no .Not in the chain) and set your message as follows:

- If you've "passed", the message will only be shown if the expectation was negated
    (ie, there's a .Not in the chain), so you need to negate your message (see the first message above)
- If you've "failed", the message will only be show if the message was _not_ negated,
    so you need to show the relevant message for that scenario.

It turns out that (mostly), we can write messages like so:
```csharp
internal static class Matchers
{
  internal static void Odd(
    this IBe&lt;int&gt; be
  )
  {
    be.AddMatcher(actual =&gt;
    {
      var passed = actual % 2 == 1;
      var message =
        $"Expected {actual} {passed ? "not " : ""}to be odd";
      return new MatcherResult(
        passed,
        message
      );
    });
  }
}
```

Doing this is tedious enough that [NExpect](https://github.com/fluffynuts/NExpect) offers a
`.AsNot()` extension for booleans:
```csharp
internal static class Matchers
{
  internal static void Odd(
    this IBe&lt;int&gt; be
  )
  {
    be.AddMatcher(actual =&gt;
    {
      var passed = actual % 2 == 1;
      var message =
        $"Expected {actual} {passed.AsNot()}to be odd";
      return new MatcherResult(
        passed,
        message
      );
    });
  }
}
```

Also, [NExpect](https://github.com/fluffynuts/NExpect) surfaces a convenience extension method
for printing values: `.Stringify()` which will:

- print strings with quotes
- null values as "null"
- and objects and collections in a "JSON-like" format.

Use as follows:

```csharp
internal static class NumberMatchers
{
  internal static void Odd(
    this IBe&lt;int&gt; be
  )
  {
    be.AddMatcher(actual =&gt;
    {
      var passed = actual % 2 == 1;
      var message =
        $"Expected {actual.Stringify()} {passed.AsNot()}to be odd";
      return new MatcherResult(
        passed,
        message
      );
    });
  }
}
```

You'll have to think (a little) about your first matcher, but it starts getting easier the more you write (:

Now you can write more meaningful tests like those in the
[demo project](https://github.com/fluffynuts/NExpect.Demo/blob/master/src/NExpect.Demo.Tests/TestAnimalFactory.cs)

The above is fine if, like me, you can see a pattern you'd like to test for really easily (if you have a kind of "matcher-first" mindset), but does provide a minor
barrier to entry for those who like to write a few tests and refactor out common assertions.

Not to worry: [NExpect](https://github.com/fluffynuts/NExpect) has you covered with
`.Compose()`:

```csharp
internal static class PersonMatchers
{
  internal static void Jane(
    this IA&lt;Person&gt; a
  )
  {
     a.Compose(actual =&gt;
     {
        Expect(actual.Id).To.Equal(1);
        Expect(actual.Name).To.Equal("Jane");
     });
  }

// ....
  [Test]
  public void TestJane()
  {
    // Arrange
    var person = new Person() { Id = 1, Name = "Jane", Alive = true };

    // Assert
    Expect(person).To.Be.A.Jane();
  }
}
```

.Compose uses `[CallerMemberName]` to determine the name of your matcher and attempts to throw a
useful `UnmetExpectationException` when one of your sub-expectations fails. You may also provide
a `Func` to `.Compose` to generate a more meaningful message.

These are some rather simple examples -- I'm sure you can get much more creative! I know I have (:

Some parts of NExpect are simply there to make fluent extension easier, for example:<br />

- `To.Be.A`
- `To.Be.An`
- `To.Be.For`

NExpect will be extended with more "grammar parts" as the need arises. If NExpect is missing a
"grammar dangler" that you'd like, open an issue on
[GitHub](https://github.com/fluffynuts/NExpect)