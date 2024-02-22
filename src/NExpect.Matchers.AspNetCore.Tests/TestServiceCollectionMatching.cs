using Microsoft.Extensions.DependencyInjection;
using NExpect.Exceptions;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestServiceCollectionMatching
{
    [Test]
    public void ShouldBeAbleToMatchAsCollectionOfDescriptors()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddTransient<IService1, Service1>();
        // Act
        Assert.That(
            () =>
            {
                Expect(services)
                    .To.Contain.Only(1)
                    .Matched.By(
                        o => o.ServiceType == typeof(IService1)
                            && o.ImplementationType == typeof(Service1)
                            && o.Lifetime == ServiceLifetime.Transient
                    );
            },
            Throws.Nothing
        );
        // Assert
    }

    [Test]
    public void ShouldFailOnMisMatches()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddScoped<IService1, Service1>();

        // Act
        Assert.That(
            () =>
            {
                Expect(services)
                    .To.Contain.Only(1)
                    .Matched.By(
                        o => o.ServiceType == typeof(IService1)
                            && o.ImplementationType == typeof(Service1)
                            && o.Lifetime == ServiceLifetime.Transient
                    );
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        
        Assert.That(
            () =>
            {
                Expect(services)
                    .Not.To.Contain.Any
                    .Matched.By(
                        o => o.ServiceType == typeof(IService1)
                            && o.ImplementationType == typeof(Service1)
                            && o.Lifetime == ServiceLifetime.Scoped
                    );
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        
        Assert.That(
            () =>
            {
                Expect(services)
                    .To.Not.Contain.Any
                    .Matched.By(
                        o => o.ServiceType == typeof(IService1)
                            && o.ImplementationType == typeof(Service1)
                            && o.Lifetime == ServiceLifetime.Scoped
                    );
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        // Assert
    }

    public interface IService1;

    public class Service1 : IService1;

    public interface IService2;
}