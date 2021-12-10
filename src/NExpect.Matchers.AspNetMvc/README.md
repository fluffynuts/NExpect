NExpect.Matchers.AspNetMvc
---

Use this matcher library if you're doing route
and supported verb tests for controllers in a 
legacy ASP.NET MVC application.

This library adds ASP.Net MVC extensions for NExpect so you can test your
[Route] and [Http*] annotations like so:

```csharp
Expect(typeof(SomeController)
    .To.Have.Method(nameof(SomeController.MethodName))
    .Supporting(HttpMethod.Delete)
    .And(HttpMethod.Post)
    .With.Route("first-route")
    .And.Route("second-route");
```
