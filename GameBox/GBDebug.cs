using System;
using System.Diagnostics;

namespace GameBox
{
    internal static class GBDebug
    {
        [Conditional("DEBUG")]
        internal static void WriteLine(object obj)
        {
            Debug.WriteLine(obj);
        }

        [Conditional("DEBUG")]
        internal static void WriteLineIf(bool condition, object obj)
        {
            Debug.WriteLineIf(condition, obj);
        }

        [Conditional("DEBUG")]
        internal static void Execute(object obj)
        {
        }

		[Conditional("DEBUG")]
		internal static void Warning(object obj)
		{
			Debug.WriteLine(obj);
		}
		
		[Conditional("DEBUG")]
		internal static void WarningIf(bool condition, object obj)
		{
			Debug.WriteLineIf(condition, obj);
		}

		[Conditional("DEBUG")]
		internal static void Assert(bool condition, object obj)
		{
			Debug.Assert(condition,obj.ToString());
		}
    }
}
