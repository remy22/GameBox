using System;
using System.Collections.Generic;
using OpenTK;
using GameBox.Events;

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
                activeProcess.AddEvent(new GBEvent("System.Control.StartScene"));
            }
        }

        internal static Process ActiveProcess {
            get { return activeProcess; }
        }

        internal static void OnUpdateFrame(FrameEventArgs e) {
        }

        private static bool finishActiveProcess = false;

        internal static void FinishActiveProcess()
        {
            finishActiveProcess = true;
        }

        internal static void UpdateProcesses()
        {
            if (finishActiveProcess)
            {
                if (activeProcess != null)
                {
                    activeProcess.rManager.DeleteAllResources();
                    processes.Remove(activeProcess);
                    activeProcess = null;
                    GC.Collect();
                    ActivateFirstProcess();
                }
                finishActiveProcess = false;
            }
        }

        internal static void OnResize(EventArgs e) {
            if (activeProcess != null)
                activeProcess.OnResize(e);
        }

        internal static void OnRenderFrame(FrameEventArgs e) {
            if (activeProcess != null)
                activeProcess.OnRenderFrame(e);
        }

        public static void AddEvent(GBEvent evnt)
        {
            if (activeProcess != null)
                activeProcess.AddEvent(evnt);
        }
    }
}
