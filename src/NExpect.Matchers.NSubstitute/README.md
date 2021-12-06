NExpect.Matchers.NSubstitute
---

This library offers NSubistitute-specific extensions so you can have Expect-style
syntax for your NSubstitute assertions. For example, one may previously have done:
```csharp
Expect(result)
    .To.Equal(expected);
someService.Received(1).SomeMethodCall();
```

and now you can keep it consistent:
```csharp
Expect(result)
    .To.Equal(expected);
Expect(someService)
    .To.Have.Received(1)
    .SomeMethodCall();
```

also, there's a handy way to assert against no calls at all
against a substitute:

```csharp
Expect(someService)
    .Not.To.Have.Been.Called();
```
