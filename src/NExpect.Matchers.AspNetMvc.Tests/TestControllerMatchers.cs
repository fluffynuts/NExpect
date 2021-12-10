using System.Net.Http;
using System.Web.Mvc;
using NExpect.Exceptions;
using static NExpect.Expectations;
using NUnit.Framework;

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
        }

        [TestFixture]
        public class ControllerActions
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
        }
    }
}