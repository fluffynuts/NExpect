using System;

namespace NExpect.Exceptions
{
    /// <summary>
    /// Easier interface for setting environment variables which NExpect will observe
    /// </summary>
    public static class NExpectEnvironment
    {
        /// <summary>
        /// These are the names of the environment variables which NExpect will observe
        /// </summary>
        public static class Variables
        {
            /// <summary>
            /// NExpect will attempt to hide stack frames which are not interesting,
            /// ie they have come from within the bowels of NExpect or some matcher.
            /// If you want all of this extra noise, set NEXPECT_FULL_STACK_FRAMES
            /// to any non-empty value
            /// </summary>
            public const string FULL_STACK_FRAMES = "NEXPECT_FULL_STACK_FRAMES";
            /// <summary>
            /// PeanutButter's DeepEqualityTester will, by default, test date time kinds
            /// when traversing an object hierarchy. If you don't care about mismatched
            /// DateTimeKinds on your DateTime values, then set
            /// DEEP_EQUALITY_IGNORES_DATE_TIME_KIND to any non-empty value
            /// </summary>
            public const string DEEP_EQUALITY_IGNORES_DATE_TIME_KIND = "DEEP_EQUALITY_IGNORES_DATETIME_KIND";
            /// <summary>
            /// When NExpect has to wait for the result of a task, there is likely a time
            /// after which the test should be considered failed due to timeout. If your tests
            /// are failing from timeout and you don't mind that they're taking a really long
            /// time, set TASK_TIMEOUT_MS to a number indicating the number of milliseconds
            /// to wait before considering a task timed out
            /// </summary>
            public const string TASK_TIMEOUT_MS = "NEXPECT_TASK_TIMEOUT_MS";
            /// <summary>
            /// When emitting errors, NExpect will attempt to make the error text fit
            /// appropriately on the display device. In the case of a well-behaved console,
            /// there should be a COLS environment variable -- which will be respected. For
            /// every other case, you may set MAX_LINE_LENGTH to the number of characters
            /// you'd like to max out a line at. NExpect will attempt to respect
            /// word boundaries too.
            /// </summary>
            public const string MAX_LINE_LENGTH = "NEXPECT_MAX_LINE_LENGTH";
        }

        /// <summary>
        /// Establishes the default values for NExpect environment variables, where
        /// appropriate
        /// </summary>
        public static class Defaults
        {
            /// <summary>
            /// The default timeout for tasks that NExpect is waiting on: 5 seconds
            /// </summary>
            public const int TASK_TIMEOUT_MS = 5000;
            /// <summary>
            /// Default max-width for an equality failure message line.
            /// When the message would run over this length, it will be split
            /// onto multiple lines for easier reading
            /// </summary>
            public const int MAX_LINE_LENGTH = 72;
        }

        /// <summary>
        /// Determines the max line length (in number of characters) for the current
        /// display device, according to:
        /// - environment variable COLS
        /// - environment variable NEXPECT_MAX_LINE_LENGTH
        /// = fallback to 72 characters
        /// </summary>
        public static int MaxLineLength
        {
            get
            {
                var value = 
                    // respect COLS as set by term, if available
                    Environment.GetEnvironmentVariable("COLS") ??
                    // respect NExpect-specific var
                    Environment.GetEnvironmentVariable(Variables.MAX_LINE_LENGTH) ??
                    Defaults.MAX_LINE_LENGTH.ToString();
                return int.TryParse(value, out var result)
                    ? result
                    : Defaults.MAX_LINE_LENGTH;
            }
            set => Environment.SetEnvironmentVariable(
                Variables.MAX_LINE_LENGTH,
                value.ToString());
        }

        /// <summary>
        /// Determines the configured task timeout in milliseconds for when
        /// NExpect is waiting on a task. Defaults to 5000 (5 seconds).
        /// </summary>
        public static int TaskTimeoutMs
        {
            get
            {
                var env = Environment.GetEnvironmentVariable(Variables.TASK_TIMEOUT_MS) ?? "";
                return int.TryParse(env, out var result)
                    ? result
                    : Defaults.TASK_TIMEOUT_MS;
            }

            set => Environment.SetEnvironmentVariable(
                Variables.TASK_TIMEOUT_MS,
                value.ToString());
        }

        /// <summary>
        /// Determines if Deep Equality Testing should ignore DateTimeKind values
        /// on DateTime values. Since this comes from PeanutButter, there's no
        /// NEXPECT_ prefix
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static bool DeepEqualityIgnoresDateTimeKind
        {
            get => HaveEnvVar(Variables.DEEP_EQUALITY_IGNORES_DATE_TIME_KIND);
            set => SetEnvVar(Variables.DEEP_EQUALITY_IGNORES_DATE_TIME_KIND, value);
        }

        /// <summary>
        /// Determines whether or not full stack frames should be shown for NExpect
        /// UnmetExpectations. By default, NExpect will attempt to suppress stack
        /// frames which are not interesting in the scope of your test by omitting
        /// frames which appear to come from within NExpect or a matcher. You may
        /// disable that behavior by setting FullStackFrames to true and enjoy the
        /// noise!
        /// </summary>
        public static bool FullStackFrames
        {
            get => HaveEnvVar(Variables.FULL_STACK_FRAMES);
            set => SetEnvVar(Variables.FULL_STACK_FRAMES, value);
        }

        private static void SetEnvVar(string varName, bool value)
        {
            Environment.SetEnvironmentVariable(
                varName,
                value
                    ? "1"
                    : null);
        }

        private static bool HaveEnvVar(string varName)
        {
            return Environment.GetEnvironmentVariable(varName) != null;
        }
    }
}