using System.Collections.Generic;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect;
using static NExpect.Expectations;
namespace NExpect.Tests.Collections;

[TestFixture]
public class OfType
{
    [Test]
    public void ShouldBeAbleToAssertTypesByCount()
    {
        // Arrange
        var l = new List<IFoo>();
        l.Add(new Foo1());
        l.Add(new Foo2());
        l.Add(new Foo3());
        l.Add(new Foo3());
        
        // Act
        Assert.That(() =>
        {
            Expect(l)
                .To.Contain.Exactly(1)
                .Of.Type(typeof(Foo1));
        }, Throws.Nothing);
        Assert.That(() =>
        {
            Expect(l)
                .To.Contain.Exactly(1)
                .Of.Type(typeof(Foo2));
        }, Throws.Nothing);
        Assert.That(() =>
        {
            Expect(l)
                .To.Contain.Exactly(2)
                .Of.Type(typeof(Foo3));
        }, Throws.Nothing);
        Assert.That(() =>
        {
            Expect(l)
                .To.Contain.None
                .Of.Type(typeof(Foo4));
        }, Throws.Nothing);
        // Assert
    }

    public interface IFoo
    {
    }

    public class Foo1 : IFoo
    {
    }

    public class Foo2 : IFoo
    {
    }

    public class Foo3 : IFoo
    {
    }

    public class Foo4 : IFoo
    {
    }
}