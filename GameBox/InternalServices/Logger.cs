using System;
using System.Collections.Generic;

namespace GameBox.InternalServices
{
    internal static class Logger
    {
        private static Dictionary<string,KeyValuePair<uint, bool> > acceptedLogs = new Dictionary<string, KeyValuePair<uint, bool> >();
        private static uint currentId = 0;

        internal static void Init()
        {
        }

        internal static void Destroy()
        {
        }

        private static bool ShouldShowLog(uint serviceName_)
        {
            foreach (var s in acceptedLogs.Values)
            {
                if (s.Key == serviceName_)
                {
                    return s.Value;
                }
            }

            return false;
        }

        internal static uint AddServiceToDebugLog(string serviceName_)
        {
            KeyValuePair<uint, bool> current;
            bool exists = acceptedLogs.TryGetValue(serviceName_, out current);
            if (exists)
                return current.Key;

            current = new KeyValuePair<uint, bool>(currentId, true);
            acceptedLogs[serviceName_] = current;
            currentId++;
            return current.Key;
        }

        internal static void debug(uint serivce, string str_)
        {
            if (ShouldShowLog(serivce))
                Console.WriteLine(str_);
        }

        internal static void info(string str_)
        {
            Console.WriteLine(str_);
        }

        internal static void release(string str_)
        {
            Console.WriteLine(str_);
        }
    }
}
