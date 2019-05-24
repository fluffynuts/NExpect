## [Quick Reference](Reference.md)

## Why
The _why_ of [NExpect](https://github.com/fluffynuts/NExpect) is covered in  detail in a
[blog post](20170917_IntroducingNExpect.md).
Basically, after working with Javascript assertion frameworks
([Chai](http://chaijs.com/) and [Jasmine](https://jasmine.github.io/)), I wasn't satisfied
with [NUnit](https://github.com/nunit/nunit) assertions. I wanted something:
- more expressive
- easier to extend
- with similar syntax, since I was switching back and forth between Javascript and C# environments

The result is [NExpect](https://github.com/fluffynuts/NExpect), which I'm quite happy with,
and which has proven (to me at least) that there are some benefits in tacking an assertions
framework from a user-first, expression-first perspective.

## How to get it
[NExpect](https://github.com/fluffynuts/NExpect) should be available via [Nuget](https://nuget.org/packages/NExpect). In addition, there are some add-on packages which provide
out-of-the-box matchers for specialised environments:
- [NExpect.Matchers.NSubstitute](https://nuget.org/packages/NExpect.Matchers.NSubstitute)
  provides `Expect` syntax for NSubstitute expressions
- [NExpect.Matchers.AspNetCore](https://nuget.org/packages/NExpect.Matchers.AspNetCore)
  provides easier assertions against routing and verbs for asp.net core controllers

## How to use it
- install, via [Nuget](https://nuget.org/packages/NExpect)
- statically import `Expectations`:
  ```csharp
  using static NExpect.Expectations;
  ```
- depending on usage, you may also need:
  ```csharp
  using NExpect;
  ```
- start expecting!
  ```csharp
  [Test]
  public void SomeTest()
  {
      // Arrange
      var sut = new SystemUnderTest();
      // Act
      var result = sut.PerformSomeAction();
      // Assert
      Expect(result).To.Be.True();
  }
  ```

There are a few introductory blog posts about usage:
- [Level 1](20170917_NExpectLevel1.md)
- [Level 2](20170917_NExpectLevel2.md)
- [Level 3](20170917_NExpectLevel3.md)

