using System;
using System.Collections.Generic;
using System.Reflection;
using GameBox.Services;

namespace GameBox.Context
{
    internal static class GlobalContext
    {
        private static List<ProcessContext> processes = new List<ProcessContext>();
        private static ProcessContext mainApp = null;

        static GlobalContext()
        {
        }

        internal static void Init()
        {
            // Create the main app process context.
            processes.Add(new ProcessContext("mainApp", Assembly.GetExecutingAssembly()));
            mainApp = processes[0];
        }

        internal static ProcessContext MainApp
        {
            get { return mainApp; }
        }

        private static bool ProcessIDAlreadyExists(string pId)
        {
            foreach (ProcessContext pc in processes)
            {
                if (pc.Name.ToLower().Equals(pId.ToLower()))
                    return true;
            }

            return false;
        }

        internal static bool AddProcess(ProcessContext pc)
        {
            bool exists = ProcessIDAlreadyExists(pc.Name);
            if (!exists)
            {
                // Add it to the list.
                processes.Add(pc);
                return true;
            }

            Logger.debugError("Trying to add process with id:" + pc.Name + ", that already exists");
            return false;
        }
    }
}
