using System;
using System.Diagnostics;

namespace GameBox.Services
{
    internal static class Logger
    {

        [Conditional("DEBUG")]
        internal static void debugIf(bool condition, object str)
        {
            if (condition)
                debug(str);
        }

        [Conditional("DEBUG")]
        internal static void debugErrorIf(bool condition, object str)
        {
            if (condition)
                debugError(str);
        }

        [Conditional("DEBUG")]
        internal static void debug(object str)
        {
			Console.WriteLine(str);
        }

        [Conditional("DEBUG")]
        internal static void debugInfo(object str)
        {
            Console.WriteLine(str);
        }

        [Conditional("DEBUG")]
        internal static void debugError(object str)
        {
            Console.WriteLine(str);
        }

        [Conditional("DEBUG")]
        internal static void logException(Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }

    }
}
