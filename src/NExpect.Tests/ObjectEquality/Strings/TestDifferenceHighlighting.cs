using System;
using NUnit.Framework;

namespace NExpect.Tests.ObjectEquality.Strings;

[TestFixture]
public class TestDifferenceHighlighting
{
    [TestFixture]
    public class WhenDifferenceIsAtOrNearStart
    {
        [Test]
        public void ShouldDoReverseArrowWhenDifferenceAtStart()
        {
            // Arrange
            var left = "aaa";
            var right = "bbb";
            // Act
            var result = DifferenceHighlighting.HighlightFirstPositionOfDifference(left, right, 100);
            // Assert
            Expect(result)
                .To.Contain("^--");
        }
    }

    [TestFixture]
    public class HighlightFirstPositionOfDifference
    {
        [Test]
        public void SeenInWild()
        {
            // Arrange
            var left = "text/plain";
            var right = "multipart/form-data";
            var expected = @"
first difference found at character 0
text/plain
^---
".Trim();
            // Act
            var result = DifferenceHighlighting.HighlightFirstPositionOfDifference(
                left,
                right,
                int.MaxValue
            );
            // Assert
            Expect(result)
                .To.Equal(expected);
        }

        [Test]
        public void SimpleOneLineDifference()
        {
            // Arrange
            var actualString = "foo to the bar";
            var expectedString = "foo to the quux";
            var expected = @"
first difference found at character 11
foo to the bar
-----------^
".Trim();
            // Act
            var result = DifferenceHighlighting.HighlightFirstPositionOfDifference(
                actualString,
                expectedString,
                int.MaxValue
            );
            // Assert
            Expect(result)
                .To.Equal(expected);
        }

        [Test]
        public void SimpleOneLineDifferenceWithLimit()
        {
            // Arrange
            var left = "foo to the bar";
            var right = "foo to the quux";
            var expected = @"
first difference found at character 11
to the bar
-------^
".Trim();
            // Act
            var result = DifferenceHighlighting.HighlightFirstPositionOfDifference(
                left,
                right,
                7
            );
            // Assert
            Expect(result)
                .To.Equal(expected);
        }

        [Test]
        public void TwoLineWithContextWithinSecondLine()
        {
            // Arrange
            var left = string.Join(
                "\n",
                "line 1 is here",
                "line 2 is_here"
            );
            var right = string.Join(
                "\n",
                "line 1 is here",
                "line 3 is_here"
            );
            var expected = @"
first difference found at character 20
ine 2 is_
----^
".Trim();
            // Act
            var result = DifferenceHighlighting.HighlightFirstPositionOfDifference(
                left,
                right,
                4
            );
            // Assert
            Expect(result)
                .To.Equal(expected);
        }

        [Test]
        public void ThreeLinesWithContextOutsideTargetLine()
        {
            // Arrange
            var left = string.Join(
                "\n",
                "line 1 is here",
                "line 2 is here",
                "line 3 is here"
            );
            var right = string.Join(
                "\n",
                "line 1 is here",
                "line 3 is here",
                "line 2 is here"
            );
            var expected = @"
first difference found at character 20
line 2 is here
-----^
".Trim();
            // Act
            var result = DifferenceHighlighting.HighlightFirstPositionOfDifference(
                left,
                right,
                int.MaxValue
            );
            // Assert
            Expect(result)
                .To.Equal(expected);
        }
    }

    [Test]
    public void WildIssue()
    {
        // Arrange
        var left = @"{
    ""AppSettings_dbHost"": ""databases.com"",
    ""AppSettings_password"": ""beef"",
    ""AppSettings_schema"": ""le_schema"",
    ""AppSettings_Server"": ""my-app.databases.com"",
    ""AppSettings_Subhost"": ""my-app"",
    ""AppSettings_user"": ""moocakes"",
    ""ConnectionStrings_Main"": ""SERVER\u003dmy-app.databases.com; DATABASE\u003dle_schema; UID\u003dmoocakes; Password\u003dbeef;""
}";
        var right = @"{
    ""AppSettings_dbHost"": ""databases.com"",
    ""AppSettings_password"": ""beef"",
    ""AppSettings_schema"": ""le_schema"",
    ""AppSettings_Server"": ""my-app.databases.com"",
    ""AppSettings_Subhost"": ""my-app"",
    ""AppSettings_user"": ""moocakes"",
    ""ConnectionStrings_Main"": ""SERVER=my-app.databases.com; DATABASE=le_schema; UID=moocakes; Password=beef;"",
}";
        // Act
        var result = DifferenceHighlighting.HighlightFirstPositionOfDifference(
            left,
            right,
            10
        );
        // Assert
        Expect(result)
            .To.End.With(
                @"
': 'SERVER\u003dmy-ap
----------^"
                    .Replace(
                        "'",
                        "\""
                    ) /* makes it visually easier to see the arrow line up, rather than "" for quotes inside @-strings */
            );
    }

    [Test]
    public void WildIssue2()
    {
        // Arrange
        var left = @"{
    ""AppSettings_dbHost"": ""databases.com"",
    ""AppSettings_password"": ""beef"",
    ""AppSettings_schema"": ""le_schema"",
    ""AppSettings_Server"": ""my-app.databases.com"",
    ""AppSettings_Subhost"": ""my-app"",
    ""AppSettings_user"": ""moocakes"",
    ""ConnectionStrings_Main"": ""SERVER=my-app.databases.com; DATABASE=le_schema; UID=moocakes; Password=beef;""
}";
        var right = @"{
    ""AppSettings_dbHost"": ""databases.com"",
    ""AppSettings_password"": ""beef"",
    ""AppSettings_schema"": ""le_schema"",
    ""AppSettings_Server"": ""my-app.databases.com"",
    ""AppSettings_Subhost"": ""my-app"",
    ""AppSettings_user"": ""moocakes"",
    ""ConnectionStrings_Main"": ""SERVER=my-app.databases.com; DATABASE=le_schema; UID=moocakes; Password=beef;"",
}";
        // Act
        var result = DifferenceHighlighting.HighlightFirstPositionOfDifference(
            left,
            right,
            10
        );
        // Assert
        Expect(result)
            .To.End.With(
                @"
ord=beef;""
----------^"
            );
    }

    [TestFixture]
    public class FindLineAround
    {
        [TestFixture]
        public class GivenOneLineWithIndexInside
        {
            [Test]
            public void ShouldReturnTheLineWithStart0()
            {
                // Arrange
                var line = GetRandomWords();
                var idx = GetRandomInt(1, line.Length - 1);
                // Act
                var result = DifferenceHighlighting.FindLineAround(line, idx);
                // Assert
                Expect(result.Line)
                    .To.Equal(line);
                Expect(result.Start)
                    .To.Equal(0);
            }
        }

        [TestFixture]
        public class GivenTwoLinesWithIndexInFirst
        {
            [TestCase("\r\n")]
            [TestCase("\n")]
            public void ShouldReturnTheFirstLineWIthStart0(
                string newline
            )
            {
                // Arrange
                var lines = new[]
                {
                    GetRandomWords(),
                    GetRandomWords()
                };
                var idx = GetRandomInt(1, lines[0].Length);
                var text = string.Join(newline, lines);

                // Act
                var result = DifferenceHighlighting.FindLineAround(text, idx);
                // Assert
                Expect(result.Line)
                    .To.Equal(lines[0]);
                Expect(result.Start)
                    .To.Equal(0);
            }
        }
    }

    [TestFixture]
    public class RealWorld
    {
        [Test]
        public void ShouldDiffStringsThatStartTheSame()
        {
            // Arrange
            var len = 1189;
            var left = new String('a', len);
            var right = $"{left}b";
            var expected = new[]
            {
                "aaaaaaaaaa",
                "----------^"
            };

            // Act
            var result = DifferenceHighlighting.HighlightFirstPositionOfDifference(
                left,
                right,
                10
            );
            // Assert
            var lines = result.Split(
                new[]
                {
                    "\n"
                },
                StringSplitOptions.TrimEntries
            );
            var lastTwoLines = new[]
            {
                lines[lines.Length - 2],
                lines[lines.Length - 1]
            };
            Expect(lastTwoLines)
                .To.Equal(expected);
        }
    }
}