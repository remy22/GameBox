using System;
using System.Collections.Generic;
using OpenTK;

namespace GameBox.Processes
{
    static class ProcessManager
    {
        private static List<Process> processes = new List<Process>();
        private static Process activeProcess = null;

        public static void Load(string dir_, string fileName_)
        {
            try
            {
                Process p = new Process();
                p.Load(dir_, fileName_);
                bool isValid = p.CheckValidity();
                if (isValid)
                {
                    processes.Add(p);
                    activeProcess = p;
                    p.ReadScenesData();
                    activeProcess = null;
                    GBInfo.WriteLine("Module " + fileName_ + " loaded successfully");
                }
            }
            catch (GBException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new GBException(GBException.Reason.UnableToLoadModule, e);
            }
        }

        internal static void ActivateFirstProcess()
        {
            if (activeProcess == null && processes.Count > 0) {
                activeProcess = processes[0];
            } else if (activeProcess != null) {
                GBDebug.WriteLine("There is an active process already");
            } else {
                GBDebug.WriteLine("There is no processes on que list");
            }
        }

        internal static void Start()
        {
            ActivateFirstProcess();
            if (activeProcess != null) {
                activeProcess.Start();
            }
        }

        internal static Process ActiveProcess {
            get { return activeProcess; }
        }

        internal static void OnUpdateFrame(FrameEventArgs e) {
        }

        internal static void OnResize(EventArgs e) {
            activeProcess.OnResize(e);
        }

        internal static void OnRenderFrame(FrameEventArgs e) {
            activeProcess.OnRenderFrame(e);
        }

        internal static void FinishProcess()
        {

        }
    }
}
