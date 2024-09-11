using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using PeanutButter.TestUtils.AspNetCore.Builders;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestHttpRequestMatching
{
    [Test]
    public void HeaderAccessShouldBeCaseInsensitive()
    {
        // Arrange
        var res = HttpRequestBuilder.BuildDefault();
        res.Headers["Moo-Cow"] = "beef";
        // Act
        Assert.That(
            () =>
            {
                Expect(res.Headers)
                    .To.Contain.Key("moo-cow");
            },
            Throws.Nothing
        );
        // Assert
    }

    [Test]
    public void HeaderAccessShouldConsiderTheHeaderContents()
    {
        // Arrange
        var h1 = GetRandomString();
        var h2 = GetRandomString();
        var v1 = GetRandomString();
        var v2 = GetRandomString();

        var req1 = HttpRequestBuilder.Create()
            .WithHeader(h1, v1)
            .WithHeader(h2, v2)
            .Build();
        var req2 = HttpRequestBuilder.Create()
            .WithHeader(h1, v1)
            .WithHeader(h2, v2)
            .Build();
        // Act
        Assert.That(
            () =>
            {
                Expect(req1.Headers)
                    .To.Equal(req2.Headers);
            },
            Throws.Nothing
        );
        // Assert
    }

    [TestFixture]
    public class SystemNetHeaders
    {
        [TestFixture]
        public class HttpRequestHeadersMatchers
        {
            [Test]
            public void HeaderAccessShouldConsiderTheHeaderContentsAsArrayCollection()
            {
                // Arrange
                var h1 = GetRandomString();
                var h2 = GetRandomString();
                var v1 = GetRandomString();
                var v2 = GetRandomString();
                var client1 = new HttpClient()
                {
                    DefaultRequestHeaders =
                    {
                        {
                            h1, v1
                        },
                        {
                            h2, v2
                        }
                    }
                };
                var client2 = new HttpClient()
                {
                    DefaultRequestHeaders =
                    {
                        {
                            h1, v1
                        },
                        {
                            h2, v2
                        }
                    }
                };
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(client1.DefaultRequestHeaders)
                            .To.Equal(client2.DefaultRequestHeaders);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void HeaderAccessShouldConsiderTheHeaderContentsAsStringCollection()
            {
                // Arrange
                var h1 = GetRandomString();
                var h2 = GetRandomString();
                var v1 = GetRandomString();
                var v2 = GetRandomString();
                var expected = new Dictionary<string, string>()
                {
                    [h1] = v1,
                    [h2] = v2
                };

                var client1 = new HttpClient()
                {
                    DefaultRequestHeaders =
                    {
                        {
                            h1, v1
                        },
                        {
                            h2, v2
                        }
                    }
                };
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(client1.DefaultRequestHeaders)
                            .To.Equal(expected);
                    },
                    Throws.Nothing
                );
                // Assert
            }
        }
        [TestFixture]
        public class HttpResponseHeadersMatchers
        {
            [Test]
            public void HeaderAccessShouldConsiderTheHeaderContentsAsArrayCollection()
            {
                // Arrange
                var h1 = GetRandomString();
                var h2 = GetRandomString();
                var v1 = GetRandomString();
                var v2 = GetRandomString();
                var message1 = new HttpResponseMessage()
                {
                    Headers =
                    {
                        {
                            h1, v1
                        },
                        {
                            h2, v2
                        }
                    }
                };
                var message2 = new HttpResponseMessage()
                {
                    Headers =
                    {
                        {
                            h1, v1
                        },
                        {
                            h2, v2
                        }
                    }
                };
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(message1.Headers)
                            .To.Equal(message2.Headers);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void HeaderAccessShouldConsiderTheHeaderContentsAsStringCollection()
            {
                // Arrange
                var h1 = GetRandomString();
                var h2 = GetRandomString();
                var v1 = GetRandomString();
                var v2 = GetRandomString();
                var expected = new Dictionary<string, string>()
                {
                    [h1] = v1,
                    [h2] = v2
                };

                var message = new HttpResponseMessage()
                {
                    Headers =
                    {
                        {
                            h1, v1
                        },
                        {
                            h2, v2
                        }
                    }
                };
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(message.Headers)
                            .To.Equal(expected);
                    },
                    Throws.Nothing
                );
                // Assert
            }
        }
    }
}