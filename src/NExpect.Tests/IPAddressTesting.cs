using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Tests;

[TestFixture]
public class IpAddressTesting
{
    [TestCase("10.1.0.23")]
    [TestCase("192.168.50.42")]
    [TestCase("430c:0000:4eba:aa17:38f3:e44d:0000:0320")]
    [TestCase("430c::4eba:aa17:38f3:e44d:0000:320")]
    public void ShouldMatchIpAddress_(string address)
    {
        // Arrange
        // Act
        Expect(() =>
            {
                Expect(address)
                    .To.Be.An.IpAddress();
            }
        ).Not.To.Throw();
        // Assert
    }

    [Test]
    public void ShouldNotMatchNonIpAddress()
    {
        // Arrange
        // Act
        Expect(() =>
            Expect(GetRandomString())
                .To.Be.An.IpAddress()
        ).To.Throw<UnmetExpectationException>();
        // Assert
    }

    [TestFixture]
    public class Ipv4Address
    {
        [TestCase("10.1.0.23")]
        [TestCase("192.168.50.42")]
        public void ShouldMatchIpv4Address_(string address)
        {
            // Arrange
            // Act
            Expect(() =>
                {
                    Expect(address)
                        .To.Be.An.Ipv4Address();
                }
            ).Not.To.Throw();
            // Assert
        }

        [TestCase("430c:0000:4eba:aa17:38f3:e44d:0000:0320")]
        [TestCase("430c::4eba:aa17:38f3:e44d:0000:320")]
        public void ShouldNotMatchIpv6Address_(string address)
        {
            // Arrange
            // Act
            Expect(() =>
                {
                    Expect(address)
                        .To.Be.An.Ipv4Address();
                }
            ).To.Throw<UnmetExpectationException>();
            // Assert
        }
    }

    [TestFixture]
    public class Ipv6Address
    {
        [TestCase("430c:0000:4eba:aa17:38f3:e44d:0000:0320")]
        [TestCase("430c::4eba:aa17:38f3:e44d:0000:320")]
        public void ShouldMatchIpv6Address_(string address)
        {
            // Arrange
            // Act
            Expect(() =>
                {
                    Expect(address)
                        .To.Be.An.Ipv6Address();
                }
            ).Not.To.Throw();
            // Assert
        }

        [TestCase("10.1.0.23")]
        [TestCase("192.168.50.42")]
        public void ShouldNotMatchIpv4Address_(string address)
        {
            // Arrange
            // Act
            Expect(() =>
                {
                    Expect(address)
                        .To.Be.An.Ipv6Address();
                }
            ).To.Throw<UnmetExpectationException>();
            // Assert
        }
    }
}