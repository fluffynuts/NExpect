using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Imported.PeanutButter.Utils;

namespace NExpect.Exceptions
{
    internal static class StackFrameExtensions
    {
        private static readonly Assembly _thisAssembly =
            typeof(UnmetExpectationException).GetAssembly();


        internal static bool IsFromOrReferencesThisAssembly(
            this StackFrame frame
        )
        {
            var methodInfo = frame.GetMethod();
            if (Equals(methodInfo?.DeclaringType?.GetAssembly(), _thisAssembly))
                return true;
            var parameters = methodInfo?.GetParameters() ?? new ParameterInfo[0];
            return parameters.Any(
                p => Equals(p.ParameterType.GetAssembly(), _thisAssembly)
            );
        }
    }
}