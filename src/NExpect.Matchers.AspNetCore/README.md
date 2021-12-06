NExpect.Matchers.AspNetCore
---

This library adds ASP.Net core extensions for NExpect so you can test your
[Route] and [Http*] annotations like so:

```csharp
Expect(typeof(SomeController)
    .To.Have.Method(nameof(SomeController.MethodName))
    .Supporting(HttpMethod.Delete)
    .And(HttpMethod.Post)
    .With.Route("first-route")
    .And.Route("second-route");
```
