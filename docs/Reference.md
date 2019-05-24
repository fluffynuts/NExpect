* Table of Contents
{:toc}

### Getting started
To get started using NExpect, you will need to:
- install the `NExpect` package into your project
- create a test fixture as below, with:
  - a static import of `NExpect.Expectations`
    - this allows calling directly into `Expect` from within the tests
  - an import of `NExpect`
    - many assertions are done via extension methods which are brought
      into your local scope with this namespace
      
```csharp
using static NExpect.Expectations;
using NExpect;

[TestFixture]
public class MyTextFixture
{
  [Test]
  public void MyTest()
  {
    // positive assertion
    Expect(1).To.Equal(1);
    // negative assertion
    Expect(1).Not.To.Equal(2);
    // alternative negative
    Expect(1).To.Not.Equal(2);
  }
}
```

- Expectations can always be negated by using `.To.Not` or `.Not.To`.
- Expectations carry type, so the compiler will help to prevent you from,
  eg, comparing strings and numbers, instead of failing at test-time like
  some other assertion frameworks do.
- Generally, expectations are written how you might say them out loud
  - if you're not sure how to proceed, try typing out your expectation
    with periods between the words -- you may be surprised (:
- All assertions can take an argument providing a better contextual message
  for the sad case where the expectation is not met:
  ```csharp
  var price = 1.23;
  Expect(price).To.Equal(4, "Mismatched price");
  ```

### Primitives
Testing primitive values (or primitive properties) is quite simple:

#### Booleans
```csharp
Expect(true).To.Be.True();
Expect(false).To.Be.False();
Expect(true).Not.To.Be.False();
Expecct(false.Not.To.Be.True();
```

#### Numbers
```csharp
Expect(1).To.Equal(1);
// alternative
Expect(1).To.Be.Equal.To(1);
Expect(2).To.Be.Greater.Than(1);
Expect(2).To.Be.Greater.Than.Or.Equal.To(2);
Expect(1).To.Be.Less.Than(2);
Expect(1).To.Be.Less.Than.Or.Equal.To(1);
Expect(1)
  .To.Be.Greater.Than(0)
  .And.Less.Than(2);
Expect(1)
  .To.Be.Greater.Than.Or.Equal.To(1)
  .And.Less.Than(10);
```
NExpect is strongly typed, so you may find times when you're trying to compare
"upward". This happens if you start the `Expect()` with a lower-bit numeric
(eg `short`) and compare with a higher-bit numeric (eg `long`). You can work
around this by up-casting the original call to `Expect()`:
```csharp
short result = 1;
Expect((int)result).To.Equal(1);
```

#### Strings
There are basic tests for
- equality
- starts-with
- contains
- ends-with

```csharp
Expect("hello").To.Equal("hello");
Expect("hello").To.Start.With("he");
Expect("hello").To.Contain("ell");
Expect("hello").To.End.With("o");
Expect("hello").Not.To.Be.Null();
Expect("hello").Not.To.Be.Null.Or.Empty();
Expect("hello").Not.To.Be.Null.Or.Whitespace();
```
In addition, NExpect string assertions can fluently test
multiple parts of a string. For example, it might be useful
to know that a string starts with one substring, contains
another, and then another, and then ends with another:
```
var result = "NExpect is a fluent, expressive assertions framework for .net";
Expect(result)
  .To.Start.With("NExpect")
  .And.Contain("fluent")
  .Then("expressive")
  .Then("framework")
  .And.End.With(".net");
```
This style is often easier to read and write than using regular expressions.
However, you can still use regular expressions:
```
var result = "Regular expressions are powerful";
// you can provide a Regex object:
Expect(result).To.Match(new Regex("^Regular"));
// or let NExpect compile one for you
Expect(result).To.Match("^Regular");
```

#### DateTime values
NExpect can perform assertions against `DateTime` values in much the same
way you'd expect to perform numeric assertions:
```csharp
var today = DateTime.Now;
var yesterday = DateTime.Now.AddDays(-1);
var tomorrow = DateTime.Now.AddDays(1);

Expect(today).To.Be.Less.Than(tomorrow);
Expect(today).To.Be.Greater.Than(yesterday)
    .And.Less.Than.Or.Equal.To(tomorrow);
```
By default, NExpect takes into account all properties of `DateTime` values,
_including_ the `Kind`. So a `UTC` `DateTime` could _never_ equal a `Local`
or `Unspecified` value. Normally, this is what you want -- the `Kind` changes
the absolute value of the `DateTime` value. However, if this is _not_ what you
want, this behavior can be overridden by setting the environment variable
`DEEP_EQUALITY_IGNORES_DATETIME_KIND` to any of the following (case-insensitive):
- yes
- true
- 1

NExpect also allows for approximate `DateTime` comparison, which can be useful when
you're looking for `DateTime` values which are "close-enough":
```csharp
var first = DateTime.Now;
Thread.Sleep(1);
var second = DateTime.Now;
Expect(first).To.Approximately.Equal(second);
```
This approximation defaults to mean "within a second of each other", but you can
control this behavior by providing another argument, a `TimeSpan` which
specifies the maximum allowable drift:
```
var first = DateTime.Now;
Thread.Sleep(1500);
var second = DateTime.Now;
Expect(first).To.Approximately.Equal(second, TimeSpan.FromSeconds(2));

```
### Objects
NExpect has rich support for complex object assertions:

#### Reference equality
```csharp
Expect(new {}).Not.To.Be(new {});
```

#### Deep equality
This is most useful when there are multiple assertions which would
have to be made to prove that two objects are equivalent:
```
[TestFixture]
public class DeepEqualityTesting
{
  public class Person
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }
  public class Entity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }

  [Test]
  public void TestDeepEquality
  {
    var left = new Person()
    {
      Id = 1,
      Name = "bob",
      Description = "123"
    };
    var right = new Entity()
    {
      Id = 1,
      Name = "bob",
      Description = "123";
    }
    
    // incoming types are not important, properties are matched by name and type
    Expect(left).To.Deep.Equal(right);
    // since the types don't matter, we can use anonymous types:
    Expect(left).To.Deep.Equal(new
    {
      Id = 1,
      Name = "bob",
      Description = "123"
    });
  }
}
```
Deep equality testing traverses all the way down through all properties, so
even complex properties are compared:
```csharp
Expect(new { Child = new { id = 1 } })
  .To.Deep.Equal(new { Child = new { id = 1 } });
```

#### Intersection Equality
Sometimes, we're only interested in a subset of properties, for example when
adding a record to the database, where some fields are auto-generated (eg
timestamps) and are not crucial to the test at hand:
```csharp
public class DatabaseEntity
{
  // this could be an auto-incrementing key
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  // this would be auto-generated at the database
  public DateTime Created { get; set; }
}

public class EntityRepository
{
  public DatabaseEntity Create(string name, string description)
  {
    // inserts data
    // returns the full database object
  }
}

[TestFixture]
public class TestingIntersectionEquality
{
  
  [Test]
  public class ShouldSaveANewEntity()
  {
    var repository = new EntityRepository();
    var result = repository.Create("Durban", "City in South Africa");
    Expect(result).To.Intersection.Equal(new
    {
      Name = "Durban",
      Description = "City in South Africa"
    });
  }
}
```

### Collections
We can perform basic testing against sizes of collections:
```csharp
Expect(collection).To.Be.Empty();
// collection must contain exactly, and only, 3 items
Expect(collection).To.Contain.Only(3).Items();
```

#### Collections of primitives
```
Expect(strings)
  .To.Contain.Exactly(1)
  .Equal.To("abc");
// we can use other string assertions
Expect(strings)
  .To.Contain.Exactly(1)
  .Starting.With("foo");
// should have some even numbers in the collection
Expect(numbers)
  .To.Contain.Any()
  .Matched.By(i => i % 2 == 0);
// collection should have exactly one member, equal to 42
Expect(numbers)
  .To.Contain.Only(1)
  .Equal.To(42);
```

#### Collections of objects
```
Expect(objects)
  .To.Contain.Exactly(2)
  .Deep.Equal.To(expected);
Expect(objects)
  .To.Contain.Any()
  .Intersection.Equal.To(expected);
```

### Exceptions
#### Asserting no exception thrown
```
Expect(() => { } ).Not.To.Throw();
```
#### Asserting anything was thrown
```csharp
// expect to throw anything
Expect(() => throw new Exception("die"))
  .To.Throw();
```

#### Asserting a specific exception was thrown
```csharp
// expect a specific exception
Expect(() => throw new InvalidOperationException("uh-oh"))
  .To.Throw<InvalidOperationException>();
// useful for TestCases, etc
Expect(() => throw new ArgumentException("moo"))
  .To.Throw().With.Type(typeof(ArgumentException));
```
#### Asserting against properties on any exception
```csharp
Expect(() => throw new ArgumentException("bad wolf"))
  .To.Throw()
  .With.Message.Containing("wolf");
```

#### Asserting against properties on specific exceptions
```csharp
Expect(() => throw new ArgumentException("name"))
  .To.Throw<ArgumentException>()
  .With.Message.Containing("name");
Expect(() => throw new CustomException("stuff"))
  .To.Throw<CustomException>()
  .With.Property(e => e.MoreInformation)
  .Equal.To("happens only at night");
```

### Extending NExpect
NExpect extension was originally inspired by Jasmine. 
#### Composing Expectations (ie: refactoring blocks of Expectations)
A "lower-hanging branch" of extension was added to facilitate easier extension from a refactor: 
Composition. Messages from Composition may not be as specific as from going all the way into
adding matchers, but they can be "good enough" and are quicker to grasp and produce. If we
were to start with this block of assertions, and imagine that the same assertions are
found in several tests, so it would be worthwhile to have a matcher to help out:
```

var obj = Person()
{
  Id = 1,
  Name = "Billy Bob",
  DateOfBirth = new DateTime(1972, 1, 13)
};
Expect(obj.Id)
  .To.Be.Greater.Than(0, () => "Invalid Id");
Expect(obj.Name)
  .Not.To.Be.Null.Or.Empty(() => "Invalid Name");
// should start with an upper-case letter
// (this could be it's own custom matcher too!)
Expect(obj.Name[0].ToString().ToUpper())
  .To.Equal(obj.Name[0].ToString());
Expect(obj.DateOfBirth)
  .To.Be.Greater.Than(new Date(1970, 1, 1), () => "Invalid date of birth");
```
It might be convenient if we could replace that with:
```
Expect(obj).To.Be.Valid();
```
We would start by mousing-over the `.Be` of the first expectation, to see the type -- we're
going to write an extension method! We should see that the `.Be` is of type `IBe<Person>`, so
we can make up an extension method like so:
```
public static class PersonMatchers
{
  public static void Valid(this IBe<Person> be)
  {
    be.Compose(actual => // the actual value being tested is passed in
    {
      Expect(actual.Id)
        .To.Be.Greater.Than(0, () => "Invalid Id");
      Expect(actual.Name)
        .Not.To.Be.Null.Or.Empty(() => "Invalid Name");
      // should start with an upper-case letter
      // (this could be it's own custom matcher too!)
      Expect(actual.Name[0].ToString().ToUpper())
        .To.Equal(actual.Name[0].ToString());
      Expect(actual.DateOfBirth)
        .To.Be.Greater.Than(new Date(1970, 1, 1), () => "Invalid date of birth");
    });
  }
}
```
The first expectation to fail in the block will cause the entire expectation to fail with
the custom message for that expectation included in the output.

The same method can be used against any dangling verb or noun:
- `.Be`
- `.Be.A`
- `.Be.An`
- `.Have`
- `.Have.A`
- `.Have.An`
#### Adding a matcher
This method is a little more complex, but allows for full customisation of the flow,
as well as performing whatever complexity of expectation you'd like, with composite results.

Say, for example, we'd like the `PersonMatchers.Valid` extension method to tell us _everything_
which is wrong with a `Person` object, not just the first thing it finds:
```
public static class PersonMatchers
{
  public static void Valid(this IBe<Person> be)
  {
    be.AddMatcher(actual => // the actual value being tested is passed in
    {
      var hasValidId = actual.Id > 0;
      var hasValidName = !string.IsNullOrEmpty(actual.Name) &&
        actual.Name[0].ToString() == actual.Name[0].ToString().ToUpper();
      var hasValidBirthday = actual.DateOfBirth > new Date(1970, 1, 1);
      var passed = hasValidId &&
        hasValidName &&
        hasValidBirthday;
        
       return new MatcherResult(
        passed,
        () =>
        {
          var errors = new List<string>();
          if (!hasValidId)
          {
            errors.Add("Must have an Id > 0");
          }
          if (!hasValidName)
          {
            errors.Add("Must have a name with the first letter capitalized");
          }
          if (!hasValidBirthday)
          {
            errors.Add("Must have been born after the UNIX epoch");
          }
          return "- " + string.Join("\n- ", errors);
        });
    });
  }
}
```

### Matcher libraries
Some specific libraries of matchers have already been created for your convenience --
install them if they are useful to you. If you'd like to publish your own matcher
libraries that others might find useful, let me know so I can add them to the list below:

#### NSubstitute matchers
- Install: `NExpect.Matchers.NSubstitute`
- Use:
  ```csharp
  var sub = Substitute.For<IInterface>();
  // do the work of the test
  // now combine Expect syntax with NSubstitute syntax for
  //  a more unified testing expression
  Expect(sub).To.Have.Received(1).SomeMethod(Arg.Any<string>());
  ```
  
#### AspNetCore matchers
- Install: `NExpect.Matchers.AspNetCore`
- Use:
  ```
  [Route("api/some")]
  public class SomeController
  {
    [Route("add")]
    [HttpGet]
    public int Add(int a, int b)
    {
      return a + b;
    }
  }
  
  [TestFixture]
  public class TestSomeController
  {
    [Test]
    public void TestAspNetRouting
    {
      var controllerType = typeof(SomeController);
      Expect(controllerType)
        .To.Have.Route("api/some");
      Expect(controllerType)
        .To.Have.Method("Add")
        .Supporting(HttpMethod.Get)
        .With.Route("add");
    }
  }
  ```
