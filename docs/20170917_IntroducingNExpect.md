About a year or so ago, I discovered <code>AssertionHelper</code>, a base class provided by <a href="https://github.com/nunit/nunit">NUnit</a> which allowed for a more familiar style of testing when one has to bounce back and forth between (Java|Type)Script and C#. Basically, it allows one to use the Expect keyword to start an assertion, eg:<br />
```csharp
[TestFixture]
public class TestSystem: AssertionHelper
{
  [Test]
  public void TestSomething()
  {
    Expect(true, Is.True);
  }
}
```
And, for a while, that sufficed. But there were some aspects of this which bothered me:
- This was accomplished via inheritence, which just seems like "not the right way to do it".<br />There have been times that I've used inheritence with test fixtures -- specifically for testing different implementations of the same interface, and also when I wrote a base class which made EF-based testing more convenient.<br />Having to inherit from AssertionHelper means that I have had to push that inheritence further down, or lose the syntax
- The tenses are wrong: `Expect` is _future-tense_, and `Is.True` is _present-tense_. Now, I happen to like the _future-tensed_ `Expect` syntax -- it really falls in line with writing your test first:
- I write code to set up the test
- I write code to run the system-under-test
- I expect some results
- I run the test
- It fails!
- I write the code
- I re-run the test
- It passes! (and if not, I go back to #7 and practice my sad-panda face)
- I refactor

A few months after I started using it, a bigger bother arrived: the NUnit team was deprecating <code>AssertionHelper</code> because they didn't think that it was used enough in the community to warrant maintenance. A healthy discussion ensued, wherein I offered to maintain <code>AssertionHelper</code> and, whilst no-one objected, the discussion seemed to be moth-balled a little (things may have changed by now). Nevertheless, my code still spewed warnings and I hate warnings. I suppressed them for a while with R# comments and <code>#pragma</code>, but I couldn't deal -- I kept seeing them creep back in again with new test fixtures.

This led me to the first-pass: [NUnit.StaticExpect](https://github.com/fluffynuts/NUnit.StaticExpect) where I'd realised that the existing `AssertionHelper` syntax could be accomplished via
- A very thin wrapper around `Assert.That` using a static class with static methods
- C#'s `using static`

This meant that the code above could become:
```csharp
using static NUnit.StaticExpect.Expectations;

[TestFixture]
public class TestSystem
{
  [Test]
  public void TestSomething()
  {
    Expect(true, Is.True);
  }
}
```
Which was better in that:

- I didn't have the warning about using the Obsoleted AssertionHelper
- I didn't have to inherit from AssertionHelper

But there was still that odd future-present tense mix. So I started hacking about on [NExpect](https://github.com/fluffynuts/NExpect)

[NExpect](https://github.com/fluffynuts/NExpect) states as its primary goals that it wants to be:
- Readable
  - Tests are easier to digest when they read like a story. Come to think of it, most code is. Code has two target audiences: the compiler and your co-workers (which includes you). The compiler is better at discerning meaning in a glob of logic than a human being is, which is why we try to write expressive code. It's why so many programming languages have evolved as people have sought to express their intent better.
    Your tests also form part of your documentation -- for that one target audience: developers.
- Expressive
  - Because the intent of a test should be easy to understand. The reader can delve into the details when she cares to.
    Tests should express their intention. A block of assertions proving that a bunch of fields on one object match those on another may be technically correct and useful, but probably has meaning. Are we trying to prove that the object is a valid FrobNozzle? It would be nice if the test could say so.
- Extensible
  - Because whilst pulling out a method like `AssertIsAFrobNozzle` is a good start, I was enamoured with the Jasmine way, along the lines of:

```
expect(result).toBeAFrobNozzle();
```

Which also negated well:
```
expect(result).not.toBeAFrobNozzle();
```

In NExpect, you can write an extension method `FrobNozzle()`, dangling off of `IA&lt;T&gt;`, and write something like:
```csharp
Expect(result).To.Be.A.FrobNozzle();<br />
// or, negated<br />
Expect(result).Not.To.Be.A.FrobNozzle();<br />
// or, negated alternative<br />
Expect(result).To.Not.Be.A.FrobNozzle();
```

The result is something which is still evolving, but is already quite powerful and useful -- and trivial to extend. I suggest checking out the [demo project](https://github.com/fluffynuts/NExpect.Demo) I made showing the evolution&nbsp;

- from "olde" testing (Assert.AreEqual)
- through the better, new `Assert.That` syntax of `NUnit` (which is quite expressive, but I really, _really_ want to Expect and I want to be able to extend my assertions language, two features I can't get with `Assert.That`, at least, not trivially)
- through expression via `AssertionHelper`
- then `NUnit.StaticExpect` and finally
- [NExpect](https://github.com/fluffynuts/NExpect), including some examples of basic "matchers" (language borrowed from Jasmine): extension methods which make the tests read easier and are easy to create and re-use.

For the best effect, clone the project, reset back to the first commit and "play through" the commits.

[NExpect](https://github.com/fluffynuts/NExpect) has extensibility inspired by [Jasmine](https://jasmine.github.io) and a syntax inspired by [Chai](http://chaijs.com) (which is a little more "dotty" than Jasmine).

I've also had some great contributions from [Cobus Smit](https://github.com/cobussmit74), a co-worker at my ex-employer [Chillisoft](http://www.chillisoft.co.za) who has not only helped with extending the NExpect language, but also through trial-by-fire usage in his own project.
