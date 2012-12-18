using System;
using System.Collections.Generic;

namespace GameBox.Processes
{
    class ProcessManager
    {
        private List<Process> processes = new List<Process>();
        private Process activeProcess = null;

        public ProcessManager()
        {
        }

        public void Load(string dir_, string fileName_)
        {
            try
            {
                Process p = new Process();
                p.Load(dir_, fileName_);
                bool isValid = p.CheckValidity();
                if (isValid)
                {
                    processes.Add(p);
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

        internal void ActivateFirstProcess()
        {
            if (activeProcess == null && processes.Count > 0)
            {
                activeProcess = processes[0];
            }
            else if (activeProcess != null)
            {
                GBDebug.WriteLine("There is an active process already");
            }
            else
            {
                GBDebug.WriteLine("There is no processes on que list");
            }
        }

        internal void Start()
        {
            ActivateFirstProcess();
            if (activeProcess != null)
            {
                activeProcess.Start();
            }
        }
    }
}
