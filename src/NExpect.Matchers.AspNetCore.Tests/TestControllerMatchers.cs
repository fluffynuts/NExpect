using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using NExpect.Exceptions;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Matchers.AspNet.Tests
{
    [TestFixture]
    public class TestControllerMatchers
    {
        [TestFixture]
        public class RoutingAtControllerLevel
        {
            [Test]
            public void Positive_ShouldThrowWhenControllerDoesNotHaveDesiredRoute()
            {
                // Arrange
                // Act
                Expect(() => Expect(typeof(TestController))
                        .To.Have.Route("other"))
                    .To.Throw<UnmetExpectationException>()
                    .With.Message.Containing(
                        "TestController to have route 'other'"
                    );
                // Assert
            }

            [Test]
            public void Negated()
            {
                // Arrange
                // Act
                Expect(() => Expect(typeof(TestController))
                        .Not.To.Have.Route("test"))
                    .To.Throw<UnmetExpectationException>()
                    .With.Message.Containing(
                        "TestController not to have route 'test'"
                    );
                // Assert
            }

            [Test]
            public void ShouldAllowContinuation()
            {
                // Arrange
                // Act
                Expect(() => Expect(typeof(TestController))
                        .To.Have.Route("test")
                        .And.Not.To.Have.Route("other"))
                    .Not.To.Throw();
                // Assert
            }

            [TestFixture]
            public class Areas
            {
                [TestFixture]
                public class WhenControllerIsNotDecorated
                {
                    [Test]
                    public void ShouldFail()
                    {
                        // Arrange
                        var sut = typeof(UndecoratedController);
                        var areaName = GetRandomString(1);
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut)
                                .Not.To.Have.Area(areaName);
                        }, Throws.Nothing);

                        Assert.That(() =>
                            {
                                Expect(sut)
                                    .To.Have.Area(areaName);
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains($"[Area(\"{areaName}\")]")
                        );
                        // Assert
                    }

                    public class UndecoratedController : ControllerBase
                    {
                    }
                }

                [TestFixture]
                public class WhenControllerIsDecorated
                {
                    [Test]
                    public void ShouldAssertArea()
                    {
                        // Arrange
                        var sut = typeof(DecoratedController);
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut)
                                .To.Have.Area("api");
                        }, Throws.Nothing);
                        Assert.That(() =>
                        {
                            // area names should be case-insensitive
                            Expect(sut)
                                .To.Have.Area("Api");
                        }, Throws.Nothing);
                        Assert.That(() =>
                        {
                            Expect(sut)
                                .Not.To.Have.Area("api2");
                        }, Throws.Nothing);
                        Assert.That(() =>
                            {
                                Expect(sut)
                                    .To.Have.Area("not-api");
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("decorated with [Area(\"not-api\")]")
                        );
                        Assert.That(() =>
                        {
                            Expect(sut)
                                .Not.To.Have.Area("api");
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Area("api")]
                    public class DecoratedController : ControllerBase
                    {
                    }
                }
            }
        }

        [TestFixture]
        public class RoutingAtActionLevel
        {
            [Test]
            public void ShouldBeAbleToTestRoute()
            {
                // Arrange
                // Act
                Expect(() => Expect(typeof(TestController))
                        .To.Have.Method(nameof(TestController.DoStuff))
                        .With.Route("do-stuff"))
                    .Not.To.Throw();

                Expect(() => Expect(typeof(TestController))
                        .To.Have.Method(nameof(TestController.DoStuff))
                        .With.Route("do-other-stuff"))
                    .To.Throw<UnmetExpectationException>()
                    .With.Message.Containing(
                        $"{typeof(TestController).Name}.DoStuff"
                    ).Then("to have route 'do-other-stuff'")
                    .Then("Have route")
                    .Then("do-stuff")
                    .And.Not.To.Contain(" not ");
                // Assert
            }

            [Test]
            public void ShouldBeAbleToTestMultipleRoutes()
            {
                // Arrange
                // Act
                Expect(() => Expect(typeof(TestController))
                        .To.Have.Method(nameof(TestController.DoStuff))
                        .With.Route("do-stuff")
                        .And.Route("another-route"))
                    .Not.To.Throw();
                Expect(() => Expect(typeof(TestController))
                        .To.Have.Method(nameof(TestController.DoStuff))
                        .With.Route("do-stuff")
                        .And.Route("another-route2"))
                    .To.Throw<UnmetExpectationException>();
                // Assert
            }

            [Test]
            public void ShouldBeAbleToTestVerb()
            {
                // Arrange
                Expect(() => Expect(typeof(TestController))
                        .To.Have.Method(nameof(TestController.DoStuff))
                        .Supporting(HttpMethod.Get))
                    .Not.To.Throw();
                Expect(() =>
                        Expect(typeof(TestController))
                            .To.Have.Method(nameof(TestController.DoStuff))
                            .Supporting(HttpMethod.Delete)
                    ).To.Throw<UnmetExpectationException>()
                    .With.Message.Containing(
                        "support HttpMethod DELETE"
                    ).And.Not.Containing(" not ");
                // Act
                // Assert
            }

            [Test]
            public void ShouldBeAbleToTestArbitraryMethodAttributes()
            {
                // Arrange
                var type = typeof(TestController);
                var sut = new TestController();
                // Act
                Assert.That(() =>
                {
                    Expect(type)
                        .To.Have.Method(nameof(TestController.PostOnly))
                        .Supporting(HttpMethod.Post)
                        .With.Attribute<RouteAttribute>(o => o.Template == "post-only");
                }, Throws.Nothing);
                Assert.That(() =>
                {
                    Expect(sut)
                        .To.Have.Method(nameof(TestController.PostOnly))
                        .With.Attribute<RouteAttribute>(o => o.Template == "post-only");
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldBeAbleToTestMultipleVerbs()
            {
                // Arrange
                Expect(() => Expect(typeof(TestController))
                        .To.Have.Method(nameof(TestController.DoStuff))
                        .Supporting(HttpMethod.Get))
                    .Not.To.Throw();
                Expect(() =>
                    Expect(typeof(TestController))
                        .To.Have.Method(nameof(TestController.DoStuff))
                        .Supporting(HttpMethod.Get)
                        .And(HttpMethod.Post)
                ).Not.To.Throw();
                // Act
                // Assert
            }


            [Test]
            public void ShouldBeAbleToTestRouteAndVerb()
            {
                // Arrange
                // Act
                Expect(() => Expect(typeof(TestController))
                    .To.Have.Method(nameof(TestController.DoStuff))
                    .Supporting(HttpMethod.Get)
                    .With.Route("do-stuff")
                ).Not.To.Throw();
                Expect(() =>
                    Expect(typeof(TestController))
                        .To.Have.Method(nameof(TestController.DoStuff))
                        .With.Route("do-stuff")
                        .Supporting(HttpMethod.Get)
                ).Not.To.Throw();
                // Assert
            }

            [Test]
            public void ShouldImplicitlySupportHttpGetWithNoDecoration()
            {
                // Arrange
                var controller = typeof(TestController);
                // Act
                Assert.That(() =>
                {
                    Expect(controller)
                        .To.Have.Method(nameof(TestController.PostOnly))
                        .Supporting(HttpMethod.Get);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                Assert.That(() =>
                {
                    Expect(controller)
                        .To.Have.Method(nameof(TestController.ImplicitGet))
                        .Supporting(HttpMethod.Get);
                }, Throws.Nothing);
                Assert.That(() =>
                {
                    Expect(controller)
                        .To.Have.Method(nameof(TestController.ImplicitGet))
                        .Supporting(HttpMethod.Post);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }
        }

        [Route("test")]
        public class TestController
        {
            [Route("do-stuff")]
            [Route("another-route")]
            [HttpGet]
            [HttpPost]
            public void DoStuff()
            {
            }

            [Route("post-only")]
            [HttpPost]
            public void PostOnly()
            {
            }

            [Route("implicit-get")]
            public void ImplicitGet()
            {
            }
        }
    }
}