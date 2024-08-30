using NSubstitute;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect;
using NExpect.Exceptions;
using static NExpect.Expectations;

namespace NExpect.Matchers.NSubstitute.Tests;

[TestFixture]
public class TestCalledMatchers
{
    [Test]
    public void ShouldPassNegatedWhenSubHasReceivedNoCalls()
    {
        // Arrange
        var service = Substitute.For<IService>();
        // Act
        Assert.That(() =>
        {
            Expect(service)
                .Not.To.Have.Been.Called();
        }, Throws.Nothing);
        // Assert
    }

    [Test]
    public void ShouldPassWhenSubHasReceivedAnyCall()
    {
        // Arrange
        var service = Substitute.For<IService>();
        if (GetRandomBoolean())
        {
            service.Method1();
        }
        else
        {
            service.Method2();
        }

        // Act
        Assert.That(() =>
        {
            Expect(service)
                .To.Have.Been.Called();
        }, Throws.Nothing);
        // Assert
    }

    [Test]
    public void ShouldFailWhenSubHasReceivedNoCalls()
    {
        // Arrange
        var service = Substitute.For<IService>();

        // Act
        Assert.That(() =>
        {
            Expect(service)
                .To.Have.Been.Called();
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }

    [Test]
    public void ShouldFailWhenNegatedAndSubHasReceivedCall()
    {
        // Arrange
        var service = Substitute.For<IService>();
        if (GetRandomBoolean())
        {
            service.Method1();
        }
        else
        {
            service.Method2();
        }

        // Act
        Assert.That(() =>
        {
            Expect(service)
                .Not.To.Have.Been.Called();
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }

    [Test]
    public void ShouldGiveUsefulErrorWhenNotWorkingWithASub()
    {
        // Arrange
        var service = new Service();
        // Act
        Assert.That(() =>
        {
            Expect(service)
                .Not.To.Have.Been.Called();
        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
            .With.Message.EndsWith("not a substitute"));
        // Assert
    }

    public interface IService
    {
        void Method1();
        void Method2();
    }

    public class Service : IService
    {
        public void Method1()
        {
            throw new System.NotImplementedException();
        }

        public void Method2()
        {
            throw new System.NotImplementedException();
        }
    }
}