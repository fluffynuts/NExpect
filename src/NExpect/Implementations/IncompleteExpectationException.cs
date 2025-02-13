using System;
using System.Collections.Generic;
using System.Linq;
using Imported.PeanutButter.Utils;

namespace NExpect.Implementations;

/// <summary>
/// Thrown when an incomplete expectation is caught by
/// the ExpectationSweeper
/// </summary>
public class IncompleteExpectationException
    : Exception
{
    /// <summary>
    /// The items which were not properly
    /// terminated.
    /// </summary>
    public SweepableItem[] SweepableItems { get; }

    private const string ERROR =
        """        
        WARNING! Your tests are not testing what you think they are testing!
        -------------------------------------------------------------------
        
        One or more expectations has been left incomplete!
        Search the test code for the following patterns:

        1. Standalone Expect(), eg `Expect(foo == 1);`
            - NExpect doesn't do 'short' expectations like the above:
              - what you're looking for is `Expect(foo).To.Equal(1);`
              - what you ended up with was the start of a boolean
                expectation, eg `Expect(foo == 1).To.Be.True();`
                  
        2. Expectation ending with a property, eg `Expect(foo).To.Be;`
            This would require other supporting code as this won't compile
                alone, but it's possible.
                
        3. Exception testing where you've done something like
            `Expect(func).To.Throw<MyException>()
                .With.Property(e => e.Id == expectedId);`
            - `.With.Property(...)` is used to derive a value from the exception
                for further testing. The above should be fixed to:
                `.With.Property(e => e.Id).Equal.To(expectedId);`
                
        4. Collection assertion ending with only the count specification, eg
            `Expect(someCollection).To.Contain.Only(2);`
            In this case, you should terminate with something like:
            - `.Items();` // asserts there are only 2 items in the collection
            - `.Matched.By(o => ...)` // asserts that the collection has 2 items,
                both matched by the provided matcher
            - `.Deep.Equal.To` // asserts 2 items, both deep equal to the 
                provided value
            - `.Intersection.Equal.To` // asserts 2 items, both intersection
                equal to the provided value (only tests matching properties)
            - `.With.Property(ex => ...)` after `Expect(func).To.Throw<TException>()`
               - this is for switching context to testing the property, _NOT_ an
                 assertion against the property
            - or perhaps some other collection matcher

        The following list will tell you the type of item which wasn't 
        completed as well as a full stack trace to where it was created. 
        Somewhere in that stack trace should be the name of your test which
        is probably not testing what you would like it to be testing.
        """;

    internal IncompleteExpectationException(SweepableItem[] sweepableItems)
        : base(GenerateErrorFor(sweepableItems))
    {
        SweepableItems = sweepableItems;
    }

    internal static string GenerateErrorFor(SweepableItem[] sweepableItems)
    {
        return $"{ERROR}{DumpItems(sweepableItems)}";
    }

    private static string DumpItems(
        SweepableItem[] sweepableItems
    )
    {
        var list = new List<string>
        {
            sweepableItems.Length == 1
                ? "Unterminated item:"
                : "Unterminated items:"
        };

        foreach (var item in sweepableItems)
        {
            var itemType = item.GetType();
            list.Add($"- {itemType.PrettyName()}");
            list.Add(Indent(item.StackTrace.ToString()));
            list.Add("");
        }

        return list.JoinWith("\n");
    }

    private static string Indent(string toString)
    {
        var lines = toString.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(l => $"  {l.Trim()}")
            .ToArray();
        return string.Join("\n", lines);
    }
}