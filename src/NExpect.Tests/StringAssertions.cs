using System;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;

namespace NExpect.Tests;

public class StringAssertions
{
    [Test]
    public void ShouldBeAbleToAssertExactEquality()
    {
        // Arrange
        var str1 = GetRandomString();
        var str2 = GetAnother(str1, () => GetRandomString());
        // Act
        Expect(
            () =>
            {
                Expect(str1)
                    .To.Equal(str1);
            }
        ).Not.To.Throw();

        Expect(
            () =>
            {
                Expect(str1)
                    .To.Equal(str2);
            }
        ).To.Throw<UnmetExpectationException>();
        // Assert
    }

    [Test]
    public void ShouldBeAbleToAssertInequality()
    {
        // Arrange
        var str1 = GetRandomString();
        var str2 = GetAnother(str1, () => GetRandomString());
        // Act
        Expect(
            () =>
            {
                Expect(str1)
                    .Not.To.Equal(str2);
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(str1)
                    .To.Not.Equal(str2);
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(str1)
                    .To.Not.Equal(str2, StringComparison.OrdinalIgnoreCase);
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(str1)
                    .To.Not.Equal(
                        str2,
                        StringComparison.OrdinalIgnoreCase,
                        "should pass!"
                    );
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(str1)
                    .To.Not.Equal(
                        str2,
                        StringComparison.OrdinalIgnoreCase,
                        () => "should pass!"
                    );
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(str1)
                    .Not.To.Equal(str2, StringComparison.OrdinalIgnoreCase);
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(str1)
                    .Not.To.Equal(
                        str2,
                        StringComparison.OrdinalIgnoreCase,
                        "should pass!"
                    );
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(str1)
                    .Not.To.Equal(
                        str2,
                        StringComparison.OrdinalIgnoreCase,
                        "should pass!"
                    );
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(str1)
                    .To.Equal(str2);
            }
        ).To.Throw<UnmetExpectationException>();
        var customMessage = GetRandomWords();
        Expect(
                () =>
                {
                    Expect(str1)
                        .To.Equal(str2, customMessage);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing(customMessage);
        Expect(
                () =>
                {
                    Expect(str1)
                        .To.Equal(str2, StringComparison.Ordinal, customMessage);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing(customMessage);
        Expect(
                () =>
                {
                    Expect(str1)
                        .To.Equal(str2, StringComparison.Ordinal, () => customMessage);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing(customMessage);
        // Assert
    }

    [Test]
    public void ShouldBeAbleToSpecifyStringComparison()
    {
        // Arrange
        var str1 = GetRandomString();
        var str2 = str1.ToRandomCase();
        while (str2 == str1)
        {
            str2 = str1.ToRandomCase();
        }

        // Act
        Expect(
            () =>
            {
                Expect(str1)
                    .To.Equal(str2, StringComparison.OrdinalIgnoreCase);
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(str1)
                    .To.Equal(str2, StringComparison.Ordinal);
            }
        ).To.Throw<UnmetExpectationException>();

        Expect(
            () =>
            {
                Expect(str1)
                    .To.Equal(str2);
            }
        ).To.Throw<UnmetExpectationException>(
            "default comparison should be case-sensitive ordinal"
        );

        // Assert
    }

    [Test]
    public void ShouldReportStringComparisonOnFailure()
    {
        // Arrange
        var str1 = GetRandomString();
        var str2 = GetAnother(str1, () => GetRandomString());

        // Act
        Expect(
                () =>
                {
                    Expect(str1)
                        .To.Equal(str2);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing("case-sensitive, byte-wise");

        Expect(
                () =>
                {
                    Expect(str1)
                        .To.Equal(str2, StringComparison.Ordinal);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing("case-sensitive, byte-wise");

        Expect(
                () =>
                {
                    Expect(str1)
                        .To.Equal(str2, StringComparison.CurrentCulture);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing("case-sensitive, current culture");

        Expect(
                () =>
                {
                    Expect(str1)
                        .To.Equal(str2, StringComparison.CurrentCultureIgnoreCase);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing("case-insensitive, current culture");

        Expect(
                () =>
                {
                    Expect(str1)
                        .To.Equal(str2, StringComparison.InvariantCulture);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing("case-sensitive, invariant culture");

        Expect(
                () =>
                {
                    Expect(str1)
                        .To.Equal(str2, StringComparison.InvariantCultureIgnoreCase);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing("case-insensitive, invariant culture");

        Expect(
                () =>
                {
                    Expect(str1)
                        .To.Equal(str2, StringComparison.OrdinalIgnoreCase);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing("case-insensitive, byte-wise");

        // Assert
    }
}