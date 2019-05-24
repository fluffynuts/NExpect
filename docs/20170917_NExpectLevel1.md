I [recently introduced](20170917_IntroducingNExpect.md)
[NExpect](https://github.com/fluffynuts/NExpect) as an alternative assertions library. I thought it might be nice to go through usage, from zero to hero.

NExpect is available for .NET Framework 4.5.2 and above as well as
anything which can target .NET Standard 1.6 (tested with .NET Core 2.0)

So here goes, level 1: testing objects and values.

NExpect facilitates assertions (or, as I like to call them: expectations) against basic 
value types in a fairly unsurprising way:
```csharp
[Test]
public void SimplePositiveExpectations
{
  // Arrange
  object obj = null;
  var intValue = 1;
  var trueValue = true;
  var falseValue = false;

  // Assert
  Expect(obj).To.Be.Null();
  Expect(intValue).To.Equal(1);
  Expect(trueValue).To.Be.True();
  Expect(falseValue).To.Be.False();
}
```
So far, nothing too exciting or unexpected there. NExpect also caters for negative expectations:
```csharp
[Test]
public void SimpleNegativeExpectations
{
  // Arrange
  object obj = new object();
  var intValue = 1;
  var trueValue = true;
  var falseValue = false;

  // Assert
  Expect(obj).Not.To.Be.Null();
  Expect(intValue).Not.To.Equal(2);
  Expect(trueValue).Not.To.Be.False();
  Expect(falseValue).Not.To.Be.True();

  Expect(intValue).To.Be.Greater.Than(0);
  Expect(intValue).To.Be.Less.Than(10);
}
```
(Though, in the above, I'm sure we all agree that the boolean expectations are neater without 
the .Not).

Expectations carry type forward, so you won't be able to, for example:
```csharp
[Test]
public void ExpectationsCarryType
{
  Expect(1).To.Equal("a");  // does not compile!
}
```
However, expectations around numeric values perform upcasts in much the same way that you'd expect 
in live code, such that you can:
```csharp
[Test]
public void ShouldUpcastAsRequired()
{
  // Arrange
  int a = 1;
  byte b = 2;
  uint c = 3;
  long d = 4;

  // Assert
  Expect(b).To.Be.Greater.Than(a);
  Expect(c).To.Be.Greater.Than(b);
  Expect(d).To.Be.Greater.Than(a);
}
```
All good and well, but often we need to check that a more complex object has a bunch of expected 
properties.

`.Equal` is obviously going to do reference-equality testing for `class` types and value 
equality testing for `struct` types. We could:
```csharp
[Test]
public void TestingPropertiesOneByOne()
{
  // Arrange
  var person = new {
    Id = 1,
    Name = "Jane",
    Alive = true
  };

  // Assert
  Expect(person.Id).To.Equal(1);
  Expect(person.Name).To.Equal("Jane");
  Expect(person.Alive).To.Be.True();
}
```
But that kind of test, whilst perfectly accurate, comes at a cognitive overhead for the reader. 
Ok, perhaps not much overhead in this example, but imagine if that `person` had come from 
another method:
```csharp
[Test]
public void TestingPropertiesOneByOne()
{
  // Arrange
  var sut = new PersonRepository();

  // Act
  var person = sut.FindById(1);

  // Assert
  Expect(person).Not.To.Be.Null();
  Expect(person.Id).To.Equal(1);
  Expect(person.Name).To.Equal("Jane");
  Expect(person.Alive).To.Be.True();
}
```

In this case, we'd expect the result to also have a defined type, not some anonymous type. 
It would be super-convenient if we could do deep equality testing. Which we can:
```csharp
[Test]
public void DeepEqualityTesting()
{
  // Arrange
  var sut = new PersonRepository();

  // Act
  var person = sut.FindById(1);

  // Assert
  Expect(person).To.Deep.Equal(new {
    Id = 1,
    Name = "Jane",
    Alive = 1
  });
 }
```
This exposes our test for what it's really doing: when searching for the person with the Id of 
1, we should get back an object which describes Jane in our system. Our test is speaking about 
_intent_, not just confirming value equality. Notice that the type of the object used for 
comparison doesn't matter, and this holds for properties too.

Note that I omitted the test for null in the second variant. You
don't need it because the deep equality tester will deal with that
just fine. However, you are obviously still free to include it for the
sake of clarity.

[NExpect](https://github.com/fluffynuts/NExpect) gets this "for free"
by depending on a git submodule of [PeanutButter](https://github.com/fluffynuts/PeanutButter) 
and importing only the bits it needs. In this way, I can re-use well-tested code and 
consumers don't have to depend on another Nuget package. Seems like a win to me.

What if we didn't care about all of the properties? What if we only cared about, 
for example, `Name` and `Id`. A dead Jane is still a Jane, right?

[NExpect](https://github.com/fluffynuts/NExpect) has you covered:
```csharp
[Test]
public void IntersectionEqualityTesting()
{
  // Arrange
  var sut = new PersonRepository();

  // Act
  var person = sut.FindById(1);

  // Assert
  Expect(person).To.Intersection.Equal(new {
    Id = 1,
    Name = "Jane"
  });
}
```
We can also test the type of a returned object:
```csharp
[Test]
public void TypeTesting()
{
  // Arrange
  var sut = new PersonRepository();

  // Act
  var person = sut.FindById(1);

  // Assert
  Expect(person).To.Be.An.Instance.Of<Person>();
  Expect(person).To.Be.An.Instance.Of<IPerson>();
  Expect(person).To.Be.An.Instance.Of<BaseEntity>();
}
```
We can test for the exact type (`Person`), implemented interfaces (`IPerson`) and base types (`BaseEntity`).

We can also apply similar logic to testing exception throwing:
```csharp
[TestFixture]
public class TestingExceptions
{
  [Test]
  public void AssertingAgainstThrownExceptions()
  {
    Expect(() => {}).Not.To.Throw();
    Expect(() => Chucky()).To.Throw();
    // can drop the lambda though, for brevity:
    Expect(Chucky).To.Throw();
    // let's get more specific:
    Expect(Chucky)
      .To.Throw<ArgumentException>();
    // more specific:
    Expect(Chucky)
      .To.Throw<ArgumentException>()
      .With.Message.Containing("cow says");
    // or
    Expect(Chucky)
      .To.Throw<ArgumentException>()
      .With.Property(e => e.ParamName)
      .Equal.To("moo");
  }

  private void Chucky()
  {
    throw new ArgumentException(
      "The cow says moo",
      "moo",
      new Exception("inner exception"));
  }
}
```

Next, we'll explore simple collection testing. [Tune in for Level 2](20170917_NExpectLevel2.md)!
