using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect.Exceptions;
using PeanutButter.Utils;
using static NExpect.Expectations;

namespace NExpect.Tests.FileSystem;

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
                        Assert.That(
                            () =>
                            {
                                Expect(filePath).To.Exist();
                            },
                            Throws.Nothing
                        );
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
                        Assert.That(
                            () =>
                            {
                                Expect(folderPath).To.Exist();
                            },
                            Throws.Nothing
                        );
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
                        Assert.That(
                            () =>
                            {
                                Expect(filePath).To.Exist();
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                        Assert.That(
                            () =>
                            {
                                Expect(filePath).To.Exist(expected);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                        Assert.That(
                            () =>
                            {
                                Expect(filePath).To.Exist(() => expected);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                            Assert.That(
                                () =>
                                {
                                    Expect(filePath).To.Be.A.File();
                                },
                                Throws.Nothing
                            );
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
                            Assert.That(
                                () =>
                                {
                                    Expect(folderPath).To.Be.A.File();
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                            Assert.That(
                                () =>
                                {
                                    Expect(filePath).To.Be.A.File();
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                            Assert.That(
                                () =>
                                {
                                    Expect(filePath).To.Be.A.File(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                            Assert.That(
                                () =>
                                {
                                    Expect(filePath).To.Be.A.File(() => expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                            Assert.That(
                                () =>
                                {
                                    Expect(folderPath).To.Be.A.Folder();
                                },
                                Throws.Nothing
                            );
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
                            Assert.That(
                                () =>
                                {
                                    Expect(filePath).To.Be.A.Folder();
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                            Assert.That(
                                () =>
                                {
                                    Expect(folderPath).To.Be.A.Folder();
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                            Assert.That(
                                () =>
                                {
                                    Expect(folderPath).To.Be.A.Folder(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                            Assert.That(
                                () =>
                                {
                                    Expect(folderPath).To.Be.A.Folder(() => expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                    GetRandomString(10)
                );
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(filePath).Not.To.Exist();
                        Expect(filePath).Not.To.Be.A.File();
                        Expect(filePath).Not.To.Be.A.Folder();
                    },
                    Throws.Nothing
                );

                File.WriteAllText(
                    filePath,
                    GetRandomString(100)
                );

                Assert.That(
                    () =>
                    {
                        Expect(filePath).Not.To.Be.A.Folder();
                    },
                    Throws.Nothing
                );

                Assert.That(
                    () =>
                    {
                        Expect(filePath).Not.To.Exist();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );

                Assert.That(
                    () =>
                    {
                        Expect(filePath).Not.To.Be.A.File();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );

                File.Delete(filePath);
                Directory.CreateDirectory(filePath);

                Assert.That(
                    () =>
                    {
                        Expect(filePath).Not.To.Be.A.Folder();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );

                Assert.That(
                    () =>
                    {
                        Expect(filePath).Not.To.Exist();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );

                Assert.That(
                    () =>
                    {
                        Expect(filePath).Not.To.Be.A.File();
                    },
                    Throws.Nothing
                );
            }
        }

        [Test]
        public void BigHairyTest()
        {
            using var tempFolder = new AutoTempFolder();
            // Arrange
            var filePath = Path.Combine(
                tempFolder.Path,
                GetRandomString(10)
            );
            // Act
            Assert.That(
                () =>
                {
                    Expect(filePath).To.Not.Exist();
                    Expect(filePath).To.Not.Be.A.File();
                    Expect(filePath).To.Not.Be.A.Folder();
                },
                Throws.Nothing
            );


            File.WriteAllText(
                filePath,
                GetRandomString(100)
            );

            Assert.That(
                () =>
                {
                    Expect(filePath).To.Not.Be.A.Folder();
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(filePath).To.Not.Exist();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(filePath).To.Not.Be.A.File();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            File.Delete(filePath);
            Directory.CreateDirectory(filePath);

            Assert.That(
                () =>
                {
                    Expect(filePath).To.Not.Be.A.Folder();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(filePath).To.Not.Exist();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(filePath).To.Not.Be.A.File();
                },
                Throws.Nothing
            );
        }

        [TestFixture]
        public class CollectionsOfPaths
        {
            [TestFixture]
            public class GivenCollectionOfExistingFolders
            {
                [TestFixture]
                public class AssertingFolders
                {
                    [Test]
                    public void ShouldNotThrow()
                    {
                        // Arrange
                        using var arena = new TestArena();
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(arena.Folders)
                                    .To.Be.Folders();
                            },
                            Throws.Nothing
                        );
                        // Assert
                    }
                }

                [TestFixture]
                public class AssertingExistence
                {
                    [Test]
                    public void ShouldNotThrow()
                    {
                        // Arrange
                        using var arena = new TestArena();
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(arena.Folders)
                                    .To.Exist();
                            },
                            Throws.Nothing
                        );
                        // Assert
                    }
                }

                [TestFixture]
                public class AssertingFiles
                {
                    [Test]
                    public void ShouldThrow()
                    {
                        // Arrange
                        using var arena = new TestArena();
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(arena.Folders)
                                    .To.Be.Files();
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("all files")
                        );
                        // Assert
                    }
                }
            }

            [TestFixture]
            public class GivenCollectionOfExistingFiles
            {
                [TestFixture]
                public class AssertingFiles
                {
                    [Test]
                    public void ShouldNotThrow()
                    {
                        // Arrange
                        using var arena = new TestArena();
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(arena.Files)
                                    .To.Be.Files();
                            },
                            Throws.Nothing
                        );
                        // Assert
                    }
                }

                [TestFixture]
                public class AssertingExistence
                {
                    [Test]
                    public void ShouldNotThrow()
                    {
                        // Arrange
                        using var arena = new TestArena();
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(arena.Files)
                                    .To.Exist();
                            },
                            Throws.Nothing
                        );
                        // Assert
                    }
                }

                [TestFixture]
                public class AssertingFolders
                {
                    [Test]
                    public void ShouldThrow()
                    {
                        // Arrange
                        using var arena = new TestArena();
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(arena.Folders)
                                    .To.Be.Files();
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("all files")
                        );
                        // Assert
                    }
                }
            }

            public class TestArena : IDisposable
            {
                public string TempFolder { get; set; }
                private AutoTempFolder _tempFolder;
                public string[] Folders { get; private set; }
                public string[] Files { get; private set; }

                public TestArena()
                {
                    _tempFolder = new AutoTempFolder();
                    TempFolder = _tempFolder.Path;
                    PopulateSomeFolders();
                    PopulateSomeFiles();
                }

                private void PopulateSomeFiles()
                {
                    Files = new[]
                        {
                            Path.Combine(TempFolder, "README.md")
                        }.Concat(
                            Folders.Select(
                                f => Path.Combine(
                                    f,
                                    GetRandomString()
                                )
                            )
                        )
                        .ToArray();
                    foreach (var file in Files)
                    {
                        _tempFolder.WriteFile(
                            file,
                            GetRandomString()
                        );
                    }
                }

                private void PopulateSomeFolders()
                {
                    var level1a = GetRandomString();
                    var level1b = GetRandomString();
                    var level2 = Path.Combine(level1a, GetRandomString());
                    var level3 = Path.Combine(level2, GetRandomString());
                    Folders = new[]
                    {
                        _tempFolder.ResolvePath(level1a),
                        _tempFolder.ResolvePath(level1b),
                        _tempFolder.ResolvePath(level2),
                        _tempFolder.ResolvePath(level3)
                    };
                    foreach (var folder in Folders)
                    {
                        _tempFolder.CreateFolder(folder);
                    }
                }

                public void Dispose()
                {
                    _tempFolder?.Dispose();
                    _tempFolder = null;
                }
            }
        }
    }
}