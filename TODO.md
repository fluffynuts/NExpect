TODO
---

Some ideas for assertions which I'd like to implement when I get time (also
ideal candidates for a juicy PR!)


Params assertions for arbitrary groups
---

```csharp
Expect(a, b, c)
  .To.All.Be.Instances.Of<T>();

Expect(a, b, c)
  .To.All.Equal(d);
```

Better intersection equality testing
---
```csharp
// intersection equality usually tests properties
// which can be matched from _both_ sides, but
// that leaves the potential for an error when,
// eg, a property on the tested system is renamed
// as the rename will not follow back up to the anon
// object used for comparison, so I'd like to
// be able to:

// properties on left _must_ exist on right
Expect(left)
  .To.Left.Intersection.Equal(right);
// properties on the right _must_ exist on the left:
Expect(left)
  .To.Right.Intersection.Equal(right);
```

Easier collection assertions
---
```csharp
// for collections of numbers, DateTime, TimeSpan
Expect(collection)
  .To.Contain.All
  .Greater.Than(x);
Expect(collection)
  .To.Contain.All
  .Greater.Than.Or.Equal.To(x);
Expect(collection)
  .To.Contain.All
  .Less.Than(x);
Expect(collection)
  .To.Contain.All
  .Less.Than.Or.Equal.To(x);

Expect(someValue)
  .Not.To.Be.In(someCollection);
```

Easier file assertions
---
```csharp
Expect(someFilePath)
  .To.Be.A.Copy.Of(someOtherFilePath);
Expect(someFolderPath)
  .To.Contain.Files();
Expect(someFolderPath)
  .To.Contain.Folders();
Expect(someFilePath)
  .To.Have.Data(someBytes);
```

Read-only property assertions
---
```csharp
Expect(obj)
  .To.Have.Property("SomeProperty")
  .Which.Is.ReadOnly();
```