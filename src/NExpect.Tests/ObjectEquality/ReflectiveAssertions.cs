using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;
using static NExpect.Expectations;

namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class ReflectiveAssertions
    {
        [TestFixture]
        public class TestingPropertyValueByName
        {
            public class Data
            {
                public int id { get; set; }
            }

            [Test]
            public void ShouldHandleMatchingNameAndValue()
            {
                // Arrange
                var data = new Data { id = 1 };
                // Act
                Assert.That(() =>
                {
                    Expect(data)
                        .To.Have.Property("id")
                        .With.Value<Data>(1);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldHandlePropertyNameMismatch()
            {
                // Arrange
                var badData = new { foo = "bar" };
                // Act
                Assert.That(() =>
                    {
                        Expect(badData)
                            .To.Have.Property("id")
                            .With.Value(1);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(
                            $"{badData.GetType().PrettyName()} to have a public property named 'id'"
                        ));
                Assert.That(() =>
                {
                    Expect(badData)
                        .Not.To.Have.Property("id")
                        .With.Value(1);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldHandleValueMismatchOnFoundProperty()
            {
                // Arrange
                var data = new { id = 1 };
                // Act
                Assert.That(() =>
                    {
                        Expect(data)
                            .To.Have.Property("id")
                            .With.Value(2);
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("expected to find value of 2 for property 'id', but found 1")
                );

                Assert.That(() =>
                    {
                        Expect(data)
                            .Not.To.Have.Property("id")
                            .With.Value(2);
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("not to have property 'id'")
                );
                // Assert
            }
        }
    }
}