using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class DifferentTypesOfCollections
    {
        [TestFixture]
        public class Containing
        {
            [Test]
            public void ShouldBeAbleToOperateOnOtherCollections()
            {
                // Arrange
                var collection = new List<string>(new[] {"a", "b", "c"});
                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Contain.Exactly(1).Equal.To("a");
                    },
                    Throws.Nothing);

                Assert.That(() =>
                    {
                        Expect(new Queue<string>(collection)).To.Contain.Exactly(1).Equal.To("a");
                    },
                    Throws.Nothing);

                Assert.That(() =>
                    {
                        Expect(collection as IList<string>).To.Contain.Exactly(1).Equal.To("a");
                    },
                    Throws.Nothing);

                Assert.That(() =>
                    {
                        Expect(collection as ICollection<string>).To.Contain.Exactly(1).Equal.To("a");
                    },
                    Throws.Nothing);

                Assert.That(() =>
                    {
                        Expect(new Stack<string>(collection)).To.Contain.Exactly(1).Equal.To("a");
                    },
                    Throws.Nothing);

                Assert.That(() =>
                    {
                        Expect(new HashSet<string>(collection)).To.Contain.Exactly(1).Equal.To("a");
                    },
                    Throws.Nothing);

                Assert.That(() =>
                    {
                        Expect(new Dictionary<string, string>()
                            {
                                ["a"] = "aye"
                            }.Keys)
                            .To.Contain.Exactly(1)
                            .Equal.To("a");
                    },
                    Throws.Nothing);

                Assert.That(() =>
                    {
                        Expect(new Dictionary<string, string>()
                            {
                                ["a"] = "aye"
                            }.Values)
                            .To.Contain.Exactly(1)
                            .Equal.To("aye");
                    },
                    Throws.Nothing);

                // Assert
            }
        }
    }
}