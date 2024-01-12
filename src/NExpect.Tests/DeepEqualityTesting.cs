using System;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests
{
    [TestFixture]
    public class DeepEqualityTesting
    {
        [Test]
        public void ShouldNotComparePrivateFieldsOrProperties()
        {
            // Arrange
            var left = new HasPrivates(1, "Bob", "red");
            var right = new HasPrivates(1, "Mary", "blue");
            // Act
            Assert.That(() => Expect(left).To.Deep.Equal(right), Throws.Nothing);
            // Assert
        }

        [Test]
        public void ShouldNotStackOverflowOnEnum()
        {
            // Arrange
            var left = new
            {
                LogLevel = LogLevel.Critical
            };
            var right = new
            {
                LogLevel = LogLevel.Critical
            };
            // Act
            Assert.That(() => Expect(left)
                .To.Deep.Equal(right), Throws.Nothing);
            // Assert
        }

        [Test]
        public void ShouldIgnoreStaticProperties()
        {
            // Arrange
            HasAStaticProp.Id = GetRandomInt();
            var data = new HasAStaticProp()
            {
                Name = GetRandomString()
            };
            // Act
            Assert.That(() =>
            {
                Expect(data)
                    .To.Deep.Equal(
                        new
                        {
                            Name = data.Name,
                            Id = HasAStaticProp.Id
                        });
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            Assert.That(() =>
            {
                Expect(data)
                    .To.Deep.Equal(
                        new
                        {
                            Name = data.Name,
                        });
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        [Ignore("TODO: need to figure out a nice api for this without muddling the existing ones")]
        public void ShouldBeAbleToExcludePropertiesByName()
        {
            // Arrange
            var left = GetRandom<HasManyProps>();
            var right = left.DeepClone()
                .With(o => o.Id++)
                .With(o => o.Created = GetAnother(o.Created));
            // Act
            Assert.That(() =>
            {
                
            }, Throws.Nothing);
            // Assert
        }

        public class HasManyProps
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Created { get; set; }
            public bool Flag { get; set; }
        }

        public class HasAStaticProp
        {
            public string Name { get; set; }
            public static int Id { get; set; }
        }

        public enum LogLevel
        {
            Trace,
            Debug,
            Information,
            Warning,
            Error,
            Critical,
            None,
        }

        public class HasPrivates
        {
            public int Id { get; set; }
            private string Name { get; set; }
            private string _color;

            public HasPrivates(int id, string name, string color)
            {
                Id = id;
                Name = name;
                _color = color;
            }
        }
    }
}