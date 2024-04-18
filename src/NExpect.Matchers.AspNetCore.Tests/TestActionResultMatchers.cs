using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using NExpect.Exceptions;
using PeanutButter.TestUtils.AspNetCore.Builders;
using PeanutButter.TestUtils.AspNetCore.Fakes;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestActionResultMatchers
{
    [TestFixture]
    public class Redirection
    {
        public static IEnumerable<IActionResult> RedirectTestCases()
        {
            yield return new RedirectResult(GetRandomHttpsUrl());
            yield return new RedirectToRouteResult(
                new RouteValueDictionary()
                {
                    ["controller"] = "Home",
                    ["action"] = "Index"
                }
            );
            yield return new RedirectToActionResult(GetRandomString(1), null, null);
        }

        [TestCaseSource(nameof(RedirectTestCases))]
        public void ShouldBeAbleToVerifyActionResultIsRedirection(
            IActionResult actionResult
        )
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Expect(actionResult)
                        .To.Redirect();
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(actionResult)
                        .Not.To.Redirect();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(actionResult)
                        .To.Not.Redirect();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [TestFixture]
        public class ToUrl
        {
            public static IEnumerable<IActionResult> NonRedirectToUrlTestCases()
            {
                yield return new RedirectToActionResult(GetRandomString(), GetRandomString(), null);
                yield return new RedirectToRouteResult(
                    new RouteValueDictionary()
                    {
                        ["controller"] = "Home",
                        ["action"] = "Index"
                    }
                );
                yield return new ViewResult();
            }

            [TestCaseSource(nameof(NonRedirectToUrlTestCases))]
            public void ShouldThrowIfActionResultIsNotRedirectResult(
                IActionResult actionResult
            )
            {
                // Arrange
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actionResult)
                            .To.Redirect()
                            .To.Url(GetRandomHttpsUrl());
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }

            [Test]
            public void ShouldBeAbleToVerifyUrlForRedirection()
            {
                // Arrange
                var expected = GetRandomHttpsUrl();
                var unexpected = GetAnother(expected, GetRandomHttpsUrl);
                var actionResult = new RedirectResult(expected);
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actionResult)
                            .To.Redirect()
                            .To.Url(expected);
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(actionResult)
                            .To.Redirect()
                            .To.Url(unexpected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }
        }

        [TestFixture]
        public class ToAction
        {
            public static IEnumerable<IActionResult> NonRedirectToActionResultTestCases()
            {
                yield return new RedirectResult(GetRandomHttpsUrl());
                yield return new JsonResult(
                    new
                    {
                        id = 1
                    }
                );
            }

            [TestCaseSource(nameof(NonRedirectToActionResultTestCases))]
            public void ShouldFailForNonRedirectToActionResult(
                IActionResult actionResult
            )
            {
                // Arrange
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actionResult)
                            .To.Redirect()
                            .To.Action(GetRandomString(1));
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }

            [Test]
            public void ShouldPassForMatchingRedirectToActionResult()
            {
                // Arrange
                var expected = GetRandomString();
                var unexpected = GetAnother(expected);
                var controller = GetRandomString();
                var actionResult = new RedirectToActionResult(
                    expected,
                    controller,
                    null
                );
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actionResult)
                            .To.Redirect()
                            .To.Action(expected);
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(actionResult)
                            .To.Redirect()
                            .To.Action(unexpected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }

            [Test]
            public void ShouldPassForMatchingRedirectToRouteResult()
            {
                // Arrange
                var expected = GetRandomString();
                var unexpected = GetAnother(expected);
                var controller = GetRandomString();
                var actionResult = new RedirectToRouteResult(
                    new
                    {
                        controller,
                        action = expected
                    }
                );
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actionResult)
                            .To.Redirect()
                            .To.Action(expected);
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(actionResult)
                            .To.Redirect()
                            .To.Action(unexpected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }

            [TestFixture]
            public class OnCurrentController
            {
                [Test]
                public void ShouldPassForMatchingRedirectToActionResult()
                {
                    // Arrange
                    var action = GetRandomString();
                    var actionResult = new RedirectToActionResult(
                        action,
                        null,
                        null
                    );
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(actionResult)
                                .To.Redirect()
                                .To.Action(action)
                                .On.CurrentController();
                        },
                        Throws.Nothing
                    );
                    var onAnotherController = new RedirectToActionResult(
                        action,
                        GetRandomString(),
                        null
                    );
                    Assert.That(
                        () =>
                        {
                            Expect(onAnotherController)
                                .To.Redirect()
                                .To.Action(action)
                                .On.CurrentController();
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                    );
                    // Assert
                }

                [Test]
                public void ShouldPassForMatchingRedirectToRouteResult()
                {
                    // Arrange
                    var action = GetRandomString();
                    var actionResult = new RedirectToRouteResult(
                        new
                        {
                            action
                        }
                    );
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(actionResult)
                                .To.Redirect()
                                .To.Action(action)
                                .On.CurrentController();
                        },
                        Throws.Nothing
                    );
                    var onAnotherController = new RedirectToRouteResult(
                        new
                        {
                            action,
                            controller = GetRandomString()
                        }
                    );
                    Assert.That(
                        () =>
                        {
                            Expect(onAnotherController)
                                .To.Redirect()
                                .To.Action(action)
                                .On.CurrentController();
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                    );
                    // Assert
                }
            }

            [TestFixture]
            public class OnController
            {
                [Test]
                public void ShouldPassForMatchingRedirectToActionResult()
                {
                    // Arrange
                    var action = GetRandomString();
                    var expected = GetRandomString();
                    var unexpected = GetAnother(expected);
                    var actionResult = new RedirectToActionResult(
                        action,
                        expected,
                        null
                    );
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(actionResult)
                                .To.Redirect()
                                .To.Action(action)
                                .On.Controller(expected);
                        },
                        Throws.Nothing
                    );
                    Assert.That(
                        () =>
                        {
                            Expect(actionResult)
                                .To.Redirect()
                                .To.Action(action)
                                .On.Controller(unexpected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                    );
                    // Assert
                }

                [Test]
                public void ShouldPassForMatchingRedirectToRouteResult()
                {
                    // Arrange
                    var action = GetRandomString();
                    var expected = GetRandomString();
                    var unexpected = GetAnother(expected);
                    var actionResult = new RedirectToRouteResult(
                        new
                        {
                            controller = expected,
                            action = action,
                            area = expected
                        }
                    );
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(actionResult)
                                .To.Redirect()
                                .To.Action(action)
                                .On.Controller(expected);
                        },
                        Throws.Nothing
                    );
                    Assert.That(
                        () =>
                        {
                            Expect(actionResult)
                                .To.Redirect()
                                .To.Action(action)
                                .On.Controller(unexpected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                    );
                    // Assert
                }

                [TestFixture]
                public class InArea
                {
                    [Test]
                    public void ShouldPassForMatchingRedirectToActionResult()
                    {
                        // Arrange
                        var action = GetRandomString();
                        var controller = GetRandomString();
                        var expected = GetRandomString();
                        var unexpected = GetAnother(expected);
                        var actionResult = new RedirectToActionResult(
                            action,
                            controller,
                            new
                            {
                                area = expected
                            }
                        );
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(actionResult)
                                    .To.Redirect()
                                    .To.Action(action)
                                    .On.Controller(controller)
                                    .In.Area(expected);
                            },
                            Throws.Nothing
                        );
                        Assert.That(
                            () =>
                            {
                                Expect(actionResult)
                                    .To.Redirect()
                                    .To.Action(action)
                                    .On.Controller(controller)
                                    .In.Area(unexpected);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                        );
                        // Assert
                    }

                    [Test]
                    public void ShouldPassForMatchingRedirectToRouteResult()
                    {
                        // Arrange
                        var action = GetRandomString();
                        var controller = GetRandomString();
                        var expected = GetRandomString();
                        var unexpected = GetAnother(expected);
                        var actionResult = new RedirectToRouteResult(
                            new
                            {
                                controller,
                                action,
                                area = expected
                            }
                        );
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(actionResult)
                                    .To.Redirect()
                                    .To.Action(action)
                                    .On.Controller(controller)
                                    .In.Area(expected);
                            },
                            Throws.Nothing
                        );
                        Assert.That(
                            () =>
                            {
                                Expect(actionResult)
                                    .To.Redirect()
                                    .To.Action(action)
                                    .On.Controller(controller)
                                    .In.Area(unexpected);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                        );
                        // Assert
                    }
                }

                [TestFixture]
                public class InRoot
                {
                    [Test]
                    public void ShouldPassForMatchingRedirectToActionResult()
                    {
                        // Arrange
                        var action = GetRandomString();
                        var controller = GetRandomString();
                        var unexpected = GetRandomString();
                        var actionResult = new RedirectToActionResult(
                            action,
                            controller,
                            null
                        );
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(actionResult)
                                    .To.Redirect()
                                    .To.Action(action)
                                    .On.Controller(controller)
                                    .In.Root();
                            },
                            Throws.Nothing
                        );
                        var inArea = new RedirectToActionResult(
                            action,
                            controller,
                            new
                            {
                                area = unexpected
                            }
                        );
                        Assert.That(
                            () =>
                            {
                                Expect(inArea)
                                    .To.Redirect()
                                    .To.Action(action)
                                    .On.Controller(controller)
                                    .In.Root();
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                        );
                        // Assert
                    }

                    [Test]
                    public void ShouldPassForMatchingRedirectToRouteResult()
                    {
                        // Arrange
                        var action = GetRandomString();
                        var controller = GetRandomString();
                        var unexpected = GetRandomString();
                        var actionResult = new RedirectToRouteResult(
                            new
                            {
                                controller,
                                action
                            }
                        );
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(actionResult)
                                    .To.Redirect()
                                    .To.Action(action)
                                    .On.Controller(controller)
                                    .In.Root();
                            },
                            Throws.Nothing
                        );
                        var inArea = new RedirectToActionResult(
                            action,
                            controller,
                            new
                            {
                                area = unexpected
                            }
                        );
                        Assert.That(
                            () =>
                            {
                                Expect(inArea)
                                    .To.Redirect()
                                    .To.Action(action)
                                    .On.Controller(controller)
                                    .In.Root();
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                        );
                        // Assert
                    }
                }
            }
        }

        [TestFixture]
        public class WithParameters
        {
            [Test]
            public void ShouldBeAbleToVerifyParameters()
            {
                // Arrange
                var controller = GetRandomString();
                var action = GetRandomString();
                var area = GetRandomString();
                var p1 = GetRandomInt();
                var p2 = GetRandomString();
                var actionResult = new RedirectToRouteResult(
                    new
                    {
                        controller,
                        action,
                        area,
                        p1,
                        p2
                    }
                );

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actionResult)
                            .To.Redirect()
                            .To.Action(action)
                            .On.Controller(controller)
                            .With.Parameters(
                                new
                                {
                                    p1,
                                    p2
                                }
                            );
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(actionResult)
                            .To.Redirect()
                            .To.Action(action)
                            .On.Controller(controller)
                            .With.Parameters(
                                new
                                {
                                    foo = "bar"
                                }
                            );
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(actionResult)
                            .To.Redirect()
                            .To.Action(action)
                            .On.Controller(controller)
                            .With.Parameters(
                                new
                                {
                                    p1
                                }
                            );
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }
        }
    }

    [TestFixture]
    public class TestingForNoContent
    {
        [Test]
        public void ShouldThrowForNullResult()
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Expect(null as IActionResult)
                        .To.Be.Empty();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertNoContent()
        {
            // Arrange
            var statusResult = new StatusCodeResult(
                (int)GetRandom<HttpStatusCode>()
            );
            // Act
            Assert.That(
                () =>
                {
                    Expect(statusResult)
                        .To.Be.Empty();
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(statusResult)
                        .Not.To.Be.Empty();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(statusResult)
                        .To.Not.Be.Empty();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }
    }

    [TestFixture]
    public class TestingForStringContent
    {
        [Test]
        public void ShouldBeAbleToTestStringContent()
        {
            // Arrange
            var data = new
            {
                id = GetRandomInt(1),
                name = GetRandomString()
            };
            var actionResult = new JsonResult(data);
            var expected = JsonSerializer.Serialize(data);

            // Act
            Assert.That(
                () =>
                {
                    Expect(actionResult)
                        .To.Have.Content(expected);
                },
                Throws.Nothing
            );
            // Assert
        }
    }

    [TestFixture]
    public class StatusCodes
    {
        [Test]
        public void ShouldBeAbleToAssertForbidden()
        {
            // Arrange
            var forbiddenResult = new ForbidResult();
            var otherForbiddenResult = new ViewResult()
            {
                StatusCode = (int)HttpStatusCode.Forbidden,
                ViewName = GetRandomString(),
                ViewData = ViewDataDictionaryBuilder.BuildDefault(),
                ContentType = "text/plain",
                TempData = new TempDataDictionary(
                    HttpContextBuilder.BuildDefault(),
                    new FakeTempDataProvider()
                )
            };
            var okResult = new OkResult();
            // Act
            Assert.That(
                () =>
                {
                    Expect(forbiddenResult)
                        .To.Be.Forbidden();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(forbiddenResult)
                        .Not.To.Be.Forbidden();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(forbiddenResult)
                        .To.Not.Be.Forbidden();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(otherForbiddenResult)
                        .To.Be.Forbidden();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(otherForbiddenResult)
                        .Not.To.Be.Forbidden();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(otherForbiddenResult)
                        .To.Not.Be.Forbidden();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(okResult)
                        .Not.To.Be.Forbidden();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(okResult)
                        .To.Not.Be.Forbidden();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(okResult)
                        .To.Be.Forbidden();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertOk()
        {
            // Arrange
            var okResult = new OkResult();
            var forbiddenResult = new ForbidResult();
            var otherOkResult = new JsonResult(
                new
                {
                    id = 1
                }
            );
            // Act
            Assert.That(
                () =>
                {
                    Expect(okResult)
                        .To.Be.Ok();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(okResult)
                        .Not.To.Be.Ok();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(okResult)
                        .To.Not.Be.Ok();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(otherOkResult)
                        .To.Be.Ok();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(otherOkResult)
                        .Not.To.Be.Ok();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(otherOkResult)
                        .To.Not.Be.Ok();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );


            Assert.That(
                () =>
                {
                    Expect(forbiddenResult)
                        .Not.To.Be.Ok();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(forbiddenResult)
                        .To.Not.Be.Ok();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(forbiddenResult)
                        .To.Be.Ok();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertArbitraryStatusCode()
        {
            // Arrange
            var expected = GetRandom<HttpStatusCode>();
            var actionResult = new ViewResult()
            {
                StatusCode = (int)expected,
                ViewName = GetRandomString(),
                ViewData = ViewDataDictionaryBuilder.BuildDefault(),
                // TODO (dotnet): update when PB is updated
                ContentType = GetRandomMIMEType()
            };
            var otherCode = GetAnother(expected);

            // Act
            Assert.That(
                () =>
                {
                    Expect(actionResult)
                        .To.Have.StatusCode(expected);
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                    Expect(actionResult)
                        .To.Have.StatusCode(otherCode),
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                    Expect(actionResult)
                        .Not.To.Have.StatusCode(otherCode),
                Throws.Nothing
            );
            Assert.That(
                () =>
                    Expect(actionResult)
                        .To.Not.Have.StatusCode(otherCode),
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertRejected()
        {
            // Arrange
            var rejectedResult = new ViewResult()
            {
                StatusCode = GetRandomInt(400, 600)
            };
            var okResult = new ViewResult()
            {
                StatusCode = 200
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(rejectedResult)
                        .To.Be.Rejected();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(okResult)
                        .Not.To.Be.Rejected();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(okResult)
                        .To.Not.Be.Rejected();
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(rejectedResult)
                        .Not.To.Be.Rejected();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(rejectedResult)
                        .To.Not.Be.Rejected();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(rejectedResult)
                        .Not.To.Be.Rejected();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(okResult)
                        .To.Be.Rejected();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            // Assert
        }

        [TestFixture]
        public class NamedRejections
        {
            [Test]
            public void ShouldBeAbleToAssertRejected401()
            {
                // Arrange
                var rejectedResult = new ViewResult()
                {
                    StatusCode = 401
                };
                var okResult = new ViewResult()
                {
                    StatusCode = 200
                };
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .To.Be.Rejected401();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .Not.To.Be.Rejected401();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .To.Not.Be.Rejected401();
                    },
                    Throws.Nothing
                );

                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .Not.To.Be.Rejected401();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .To.Not.Be.Rejected401();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .Not.To.Be.Rejected401();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .To.Be.Rejected401();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );

                // Assert
            }

            [Test]
            public void ShouldBeAbleToAssertRejected403()
            {
                // Arrange
                var rejectedResult = new ViewResult()
                {
                    StatusCode = 403
                };
                var okResult = new ViewResult()
                {
                    StatusCode = 200
                };
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .To.Be.Rejected403();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .Not.To.Be.Rejected403();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .To.Not.Be.Rejected403();
                    },
                    Throws.Nothing
                );

                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .Not.To.Be.Rejected403();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .To.Not.Be.Rejected403();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .Not.To.Be.Rejected403();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .To.Be.Rejected403();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );

                // Assert
            }

            [Test]
            public void ShouldBeAbleToAssertRejected404()
            {
                // Arrange
                var rejectedResult = new ViewResult()
                {
                    StatusCode = 404
                };
                var okResult = new ViewResult()
                {
                    StatusCode = 200
                };
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .To.Be.Rejected404();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .Not.To.Be.Rejected404();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .To.Not.Be.Rejected404();
                    },
                    Throws.Nothing
                );

                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .Not.To.Be.Rejected404();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .To.Not.Be.Rejected404();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .Not.To.Be.Rejected404();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .To.Be.Rejected404();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );

                // Assert
            }

            [Test]
            public void ShouldBeAbleToAssertNotFound()
            {
                // Arrange
                var rejectedResult = new ViewResult()
                {
                    StatusCode = 404
                };
                var okResult = new ViewResult()
                {
                    StatusCode = 200
                };
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .To.Be.NotFound();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .Not.To.Be.NotFound();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .To.Not.Be.NotFound();
                    },
                    Throws.Nothing
                );

                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .Not.To.Be.NotFound();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .To.Not.Be.NotFound();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(rejectedResult)
                            .Not.To.Be.NotFound();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(okResult)
                            .To.Be.NotFound();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );

                // Assert
            }
        }
    }
}