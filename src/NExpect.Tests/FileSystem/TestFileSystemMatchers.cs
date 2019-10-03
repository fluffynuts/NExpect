using System.IO;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect.Exceptions;
using PeanutButter.Utils;
using static NExpect.Expectations;

namespace NExpect.Tests.FileSystem
{
    [TestFixture]
    public class TestFileSystemMatchers
    {
        [TestFixture]
        public class To
        {
            [TestFixture]
            public class Exist
            {
                [TestFixture]
                public class WhenFileExists
                {
                    [Test]
                    public void ShouldNotThrow()
                    {
                        // Arrange
                        using (var tempFolder = new AutoTempFolder())
                        {
                            var filePath = Path.Combine(
                                tempFolder.Path,
                                GetRandomString(5)
                            );
                            File.WriteAllText(
                                filePath,
                                GetRandomString()
                            );
                            // Act
                            Assert.That(() =>
                            {
                                Expect(filePath).To.Exist();
                            }, Throws.Nothing);
                            // Assert
                        }
                    }
                }

                [TestFixture]
                public class WhenFolderExists
                {
                    [Test]
                    public void ShouldNotThrow()
                    {
                        // Arrange
                        using (var tempFolder = new AutoTempFolder())
                        {
                            var folderPath = Path.Combine(
                                tempFolder.Path,
                                GetRandomString(5)
                            );
                            Directory.CreateDirectory(folderPath);
                            // Act
                            Assert.That(() =>
                            {
                                Expect(folderPath).To.Exist();
                            }, Throws.Nothing);
                            // Assert
                        }
                    }
                }

                [TestFixture]
                public class WhenFileOrFolderDoesNotExist
                {
                    [Test]
                    public void ShouldThrow()
                    {
                        // Arrange
                        using (var tempFolder = new AutoTempFolder())
                        {
                            var filePath = Path.Combine(
                                tempFolder.Path,
                                GetRandomString(10)
                            );
                            // Act
                            Assert.That(() =>
                                {
                                    Expect(filePath).To.Exist();
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains($"{filePath} to exist")
                            );
                            // Assert
                        }
                    }

                    [Test]
                    public void ShouldThrowWithCustomMessage()
                    {
                        // Arrange
                        using (var tempFolder = new AutoTempFolder())
                        {
                            var filePath = Path.Combine(
                                tempFolder.Path,
                                GetRandomString(10)
                            );
                            var expected = GetRandomString(10);
                            // Act
                            Assert.That(() =>
                                {
                                    Expect(filePath).To.Exist(expected);
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains($"{filePath} to exist")
                                    .And.Message.Contains(expected)
                            );
                            // Assert
                        }
                    }

                    [Test]
                    public void ShouldThrowWithCustomMessageGenerator()
                    {
                        // Arrange
                        using (var tempFolder = new AutoTempFolder())
                        {
                            var filePath = Path.Combine(
                                tempFolder.Path,
                                GetRandomString(10)
                            );
                            var expected = GetRandomString(10);
                            // Act
                            Assert.That(() =>
                                {
                                    Expect(filePath).To.Exist(() => expected);
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains($"{filePath} to exist")
                                    .And.Message.Contains(expected)
                            );
                            // Assert
                        }
                    }
                }
            }
        }

        [TestFixture]
        public class Be
        {
            [TestFixture]
            public class A
            {
                [TestFixture]
                public class File
                {
                    [TestFixture]
                    public class WhenFileExists
                    {
                        [Test]
                        public void ShouldNotThrow()
                        {
                            // Arrange
                            using (var tempFolder = new AutoTempFolder())
                            {
                                var filePath = Path.Combine(
                                    tempFolder.Path,
                                    GetRandomString(5)
                                );
                                System.IO.File.WriteAllText(
                                    filePath,
                                    GetRandomString()
                                );
                                // Act
                                Assert.That(() =>
                                {
                                    Expect(filePath).To.Be.A.File();
                                }, Throws.Nothing);
                                // Assert
                            }
                        }
                    }

                    [TestFixture]
                    public class WhenFolderExists
                    {
                        [Test]
                        public void ShouldThrow()
                        {
                            // Arrange
                            using (var tempFolder = new AutoTempFolder())
                            {
                                var folderPath = Path.Combine(
                                    tempFolder.Path,
                                    GetRandomString(5)
                                );
                                System.IO.Directory.CreateDirectory(folderPath);
                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(folderPath).To.Be.A.File();
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.Message.Contains($"{folderPath} to be a file")
                                        .And.Message.Contains("found a folder")
                                );
                                // Assert
                            }
                        }
                    }

                    [TestFixture]
                    public class WhenFileDoesNotExist
                    {
                        [Test]
                        public void ShouldThrow()
                        {
                            // Arrange
                            using (var tempFolder = new AutoTempFolder())
                            {
                                var filePath = Path.Combine(
                                    tempFolder.Path,
                                    GetRandomString(5)
                                );
                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(filePath).To.Be.A.File();
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.Message.Contains($"{filePath} to be a file")
                                        .And.Message.Contains("found nothing")
                                );
                                // Assert
                            }
                        }

                        [Test]
                        public void ShouldThrowWithCustomMessage()
                        {
                            // Arrange
                            using (var tempFolder = new AutoTempFolder())
                            {
                                var filePath = Path.Combine(
                                    tempFolder.Path,
                                    GetRandomString(5)
                                );
                                var expected = GetRandomString(10);
                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(filePath).To.Be.A.File(expected);
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.Message.Contains($"{filePath} to be a file")
                                        .And.Message.Contains("found nothing")
                                        .And.Message.Contains(expected)
                                );
                                // Assert
                            }
                        }

                        [Test]
                        public void ShouldThrowWithCustomMessageGenerator()
                        {
                            // Arrange
                            using (var tempFolder = new AutoTempFolder())
                            {
                                var filePath = Path.Combine(
                                    tempFolder.Path,
                                    GetRandomString(5)
                                );
                                var expected = GetRandomString(10);
                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(filePath).To.Be.A.File(() => expected);
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.Message.Contains($"{filePath} to be a file")
                                        .And.Message.Contains("found nothing")
                                        .And.Message.Contains(expected)
                                );
                                // Assert
                            }
                        }
                    }
                }

                [TestFixture]
                public class Folder
                {
                    [TestFixture]
                    public class WhenFolderExists
                    {
                        [Test]
                        public void ShouldNotThrow()
                        {
                            // Arrange
                            using (var tempFolder = new AutoTempFolder())
                            {
                                var folderPath = Path.Combine(
                                    tempFolder.Path,
                                    GetRandomString(5)
                                );
                                System.IO.Directory.CreateDirectory(folderPath);
                                // Act
                                Assert.That(() =>
                                {
                                    Expect(folderPath).To.Be.A.Folder();
                                }, Throws.Nothing);
                                // Assert
                            }
                        }
                    }

                    [TestFixture]
                    public class WhenFileExists
                    {
                        [Test]
                        public void ShouldThrow()
                        {
                            // Arrange
                            using (var tempFolder = new AutoTempFolder())
                            {
                                var filePath = Path.Combine(
                                    tempFolder.Path,
                                    GetRandomString(5)
                                );
                                System.IO.File.WriteAllText(
                                    filePath,
                                    GetRandomString(100)
                                );
                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(filePath).To.Be.A.Folder();
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.Message.Contains($"{filePath} to be a folder")
                                        .And.Message.Contains("found a file")
                                );
                                // Assert
                            }
                        }
                    }

                    [TestFixture]
                    public class WhenFolderDoesNotExist
                    {
                        [Test]
                        public void ShouldThrow()
                        {
                            // Arrange
                            using (var tempFolder = new AutoTempFolder())
                            {
                                var folderPath = Path.Combine(
                                    tempFolder.Path,
                                    GetRandomString(5)
                                );
                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(folderPath).To.Be.A.Folder();
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.Message.Contains($"{folderPath} to be a folder")
                                        .And.Message.Contains("found nothing")
                                );
                                // Assert
                            }
                        }

                        [Test]
                        public void ShouldThrowWithCustomMessage()
                        {
                            // Arrange
                            using (var tempFolder = new AutoTempFolder())
                            {
                                var folderPath = Path.Combine(
                                    tempFolder.Path,
                                    GetRandomString(5)
                                );
                                var expected = GetRandomString(10);
                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(folderPath).To.Be.A.Folder(expected);
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.Message.Contains($"{folderPath} to be a folder")
                                        .And.Message.Contains("found nothing")
                                        .And.Message.Contains(expected)
                                );
                                // Assert
                            }
                        }

                        [Test]
                        public void ShouldThrowWithCustomMessageGenerator()
                        {
                            // Arrange
                            using (var tempFolder = new AutoTempFolder())
                            {
                                var folderPath = Path.Combine(
                                    tempFolder.Path,
                                    GetRandomString(5)
                                );
                                var expected = GetRandomString(10);
                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(folderPath).To.Be.A.Folder(() => expected);
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.Message.Contains($"{folderPath} to be a folder")
                                        .And.Message.Contains("found nothing")
                                        .And.Message.Contains(expected)
                                );
                                // Assert
                            }
                        }
                    }
                }
            }
        }

        [TestFixture]
        public class Negations
        {
            [Test]
            public void NotFirst()
            {
                using (var tempFolder = new AutoTempFolder())
                {
                    // Arrange
                    var filePath = Path.Combine(
                        tempFolder.Path,
                        GetRandomString(10));
                    // Act
                    Assert.That(() =>
                    {
                        Expect(filePath).Not.To.Exist();
                        Expect(filePath).Not.To.Be.A.File();
                        Expect(filePath).Not.To.Be.A.Folder();
                    }, Throws.Nothing);

                    File.WriteAllText(
                        filePath,
                        GetRandomString(100)
                    );

                    Assert.That(() =>
                    {
                        Expect(filePath).Not.To.Be.A.Folder();
                    }, Throws.Nothing);

                    Assert.That(() =>
                    {
                        Expect(filePath).Not.To.Exist();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                    Assert.That(() =>
                    {
                        Expect(filePath).Not.To.Be.A.File();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                    File.Delete(filePath);
                    Directory.CreateDirectory(filePath);

                    Assert.That(() =>
                    {
                        Expect(filePath).Not.To.Be.A.Folder();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                    Assert.That(() =>
                    {
                        Expect(filePath).Not.To.Exist();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                    Assert.That(() =>
                    {
                        Expect(filePath).Not.To.Be.A.File();
                    }, Throws.Nothing);
                }
            }

            [Test]
            public void ToFirst()
            {
                using (var tempFolder = new AutoTempFolder())
                {
                    // Arrange
                    var filePath = Path.Combine(
                        tempFolder.Path,
                        GetRandomString(10));
                    // Act
                    Assert.That(() =>
                    {
                        Expect(filePath).To.Not.Exist();
                        Expect(filePath).To.Not.Be.A.File();
                        Expect(filePath).To.Not.Be.A.Folder();
                    }, Throws.Nothing);


                    File.WriteAllText(
                        filePath,
                        GetRandomString(100)
                    );

                    Assert.That(() =>
                    {
                        Expect(filePath).To.Not.Be.A.Folder();
                    }, Throws.Nothing);

                    Assert.That(() =>
                    {
                        Expect(filePath).To.Not.Exist();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                    Assert.That(() =>
                    {
                        Expect(filePath).To.Not.Be.A.File();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                    File.Delete(filePath);
                    Directory.CreateDirectory(filePath);

                    Assert.That(() =>
                    {
                        Expect(filePath).To.Not.Be.A.Folder();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                    Assert.That(() =>
                    {
                        Expect(filePath).To.Not.Exist();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                    Assert.That(() =>
                    {
                        Expect(filePath).To.Not.Be.A.File();
                    }, Throws.Nothing);
                }
            }
        }
    }
}