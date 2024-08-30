using NUnit.Framework;
using PeanutButter.Utils;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Tests;

[TestFixture]
public class PartialDeepEqualityTesting
{
    [Test]
    public void ShouldBeAbleToOmitAProperty()
    {
        // Arrange
        var rex = GetRandom<Pet>();
        var nearlyRex = new Pet();
        rex.CopyPropertiesTo(nearlyRex);
        nearlyRex.Type = GetAnother(nearlyRex.Type);

        Expect(nearlyRex.Id)
            .To.Equal(rex.Id);
        Expect(nearlyRex.Name)
            .To.Equal(rex.Name);
        Expect(nearlyRex.Type)
            .Not.To.Equal(rex.Type);

        // Act
        Assert.That(
            () =>
            {
                Expect(nearlyRex)
                    .Omitting("Type")
                    .To.Deep.Equal(rex);
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(nearlyRex)
                    .Omitting("Type")
                    .To.Intersection.Equal(rex);
            },
            Throws.Nothing
        );
        // Assert
    }

    [Test]
    public void ShouldBeAbleToOmitAPropertyFromACollection()
    {
        // Arrange
        var rex = GetRandom<Pet>().InArray();
        var nearlyRex = new Pet().InArray();
        rex[0].CopyPropertiesTo(nearlyRex[0]);
        nearlyRex[0].Type = GetAnother(nearlyRex[0].Type);

        Expect(nearlyRex[0].Id)
            .To.Equal(rex[0].Id);
        Expect(nearlyRex[0].Name)
            .To.Equal(rex[0].Name);
        Expect(nearlyRex[0].Type)
            .Not.To.Equal(rex[0].Type);

        // Act
        Assert.That(
            () =>
            {
                Expect(nearlyRex)
                    .Omitting("Type")
                    .To.Deep.Equal(rex);
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(nearlyRex)
                    .Omitting("Type")
                    .To.Intersection.Equal(rex);
            },
            Throws.Nothing
        );
        // Assert
    }

    public enum PetTypes
    {
        Cat,
        Dog,
        Other
    }

    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PetTypes Type { get; set; }
    }
}