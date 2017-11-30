using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NExpect.Tests
{
    internal class TestUtils
    {
        internal static void ForceMessageLineBreaks()
        {
            Environment.SetEnvironmentVariable("COLS", "1");
        }

        internal static void ForceNoMessageLineBreaks()
        {
            Environment.SetEnvironmentVariable("COLS", "99999");
        }

        internal static void WithNoLineBreaks(Action action)
        {
            var beforeTest = Environment.GetEnvironmentVariable("COLS");
            try
            {
                action();
            }
            finally
            {
                if (beforeTest != null)
                    Environment.SetEnvironmentVariable("COLS", beforeTest);
            }
        }
    }
}