using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests.ObjectEquality.Strings
{
    [TestFixture]
    public class TestDifferenceHighlighting
    {
        [TestFixture]
        public class HighlightFirstPositionOfDifference
        {
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
}