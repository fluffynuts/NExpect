using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect;
using NExpect.Exceptions;
using PeanutButter.RandomGenerators;
using static NExpect.Expectations;

namespace NExpect.Tests
{
    [TestFixture]
    public class Defaults
    {
        [Test]
        public void ShouldBeAbleToAssertThatNewObjectHasDefaultProperties()
        {
            // Arrange
            var defaults = new Person();
            var notDefaults = GetRandom<Person>();
            // Act
            Assert.That(() =>
            {
                Expect(defaults)
                    .To.Have.Default.Properties();
            }, Throws.Nothing);

            Assert.That(() =>
            {
                Expect(defaults)
                    .Not.To.Have.Default.Properties();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());

            Assert.That(() =>
            {
                Expect(notDefaults)
                    .Not.To.Have.Default.Properties();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(notDefaults)
                    .To.Have.Default.Properties();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertThatNewRefObjectHasDefaultFields()
        {
            // Arrange
            var defaults = new HasFields();
            var notDefaults = GetRandom<HasFields>();

            // Act
            Assert.That(() =>
            {
                Expect(defaults)
                    .To.Have.Default.Fields();
            }, Throws.Nothing);

            Assert.That(() =>
            {
                Expect(defaults)
                    .Not.To.Have.Default.Fields();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());

            Assert.That(() =>
            {
                Expect(notDefaults)
                    .Not.To.Have.Default.Fields();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(notDefaults)
                    .To.Have.Default.Fields();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertFieldsAndPropsViaAnd()
        {
            // Arrange
            var defaults = new HasFieldsAndProps();
            var notDefaults = GetRandom<HasFieldsAndProps>();
            var defaultPropertyOnly = GetRandom<HasFieldsAndProps>();
            defaultPropertyOnly.Name = default;

            // Act
            Assert.That(() =>
            {
                Expect(defaults)
                    .To.Have.Default.Fields()
                    .And
                    .To.Have.Default.Properties();
            }, Throws.Nothing);

            Assert.That(() =>
            {
                Expect(defaults)
                    .Not.To.Have.Default.Fields()
                    .And
                    .Not.To.Have.Default.Properties();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());

            Assert.That(() =>
            {
                Expect(notDefaults)
                    .Not.To.Have.Default.Fields()
                    .And
                    .Not.To.Have.Default.Properties();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(notDefaults)
                    .To.Have.Default.Fields()
                    .And
                    .To.Have.Default.Properties();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());


            Assert.That(() =>
            {
                Expect(defaultPropertyOnly)
                    .To.Have.Default.Fields()
                    .And
                    .Not.To.Have.Default.Properties();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            Assert.That(() =>
            {
                Expect(defaultPropertyOnly)
                    .Not.To.Have.Default.Properties()
                    .And
                    .To.Have.Default.Fields();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }

    [RequireNonZeroId]
    public class HasFieldsAndProps
    {
        public int Id;
        public string Name { get; set; }
    }

    [RequireNonZeroId]
    public struct HasFields
    {
        public int Id;
        public string Name;
        public AnotherStruct Friend;
        public Person Acquaintance;
    }

    public struct AnotherStruct
    {
    }
}