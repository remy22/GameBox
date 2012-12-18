using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using GameBox.XMLSerialization;

namespace GameBox.Processes
{
    public class Process
    {
        internal enum ProcessState
        {
            NotLoaded,
            PendingValidation,
            StartPending,
            Running,
            InitError
        }
        private Assembly assembly = null;
        private string completeFileName = string.Empty;
		private string completePropertiesFileName = string.Empty;
		        private string ProcessMainClass = "Brocker.BrockerProcess";
        private GBProcess userProcess = null;
        private ProcessState prState = ProcessState.NotLoaded;

        public Process()
        {
        }

		public void LoadMetadata(string dir, string fileName)
		{
			GBDebug.Assert(prState == ProcessState.NotLoaded, "Process state error. It is " + prState + ". It must be " + ProcessState.NotLoaded);
			completePropertiesFileName = GBFileSystem.CompleteFileNameForFile(dir, fileName);
			if (GBFileSystem.FileExists(completePropertiesFileName))
			{
				GBXMLContainer obj = GBXMLContainer.LoadOrNull(completePropertiesFileName);
				GBDebug.WriteLine(obj);
			}
			else
			{
				prState = ProcessState.InitError;
				throw new GBException(GBException.Reason.FileNotFound);
			}
		}
        public void Load(string dir,string fileName)
        {
            GBDebug.Assert(prState == ProcessState.NotLoaded, "Process state error. It is " + prState + ". It must be " + ProcessState.NotLoaded);
            completeFileName = GBFileSystem.CompleteFileNameForFile(dir, fileName);
            if (GBFileSystem.FileExists(completeFileName))
            {
                assembly = Assembly.LoadFrom(completeFileName);
                GBDebug.Assert(assembly != null, "Assembly " + fileName + " cannot be loaded");
                prState = ProcessState.PendingValidation;
            }
            else
            {
                prState = ProcessState.InitError;
                throw new GBException(GBException.Reason.FileNotFound);
            }
        }

        public bool CheckValidity()
        {
            try
            {
                GBDebug.WriteLine(assembly);
                GBDebug.Assert(prState == ProcessState.PendingValidation, "Process state error. It is " + prState + ". It must be " + ProcessState.PendingValidation);
                userProcess = (GBProcess)assembly.CreateInstance(ProcessMainClass);
                if (userProcess == null)
                {
                    prState = ProcessState.InitError;
                    GBInfo.WriteLine(ProcessMainClass + " not found.");
                    return false;
                }
                prState = ProcessState.StartPending;
                return true;
            }
            catch (Exception)
            {
                prState = ProcessState.InitError;
                return false;
            }
        }

        internal void Start()
        {
            GBDebug.Assert(prState == ProcessState.StartPending, "Process state error. It is " + prState + ". It must be " + ProcessState.StartPending);
            GBInfo.WriteLine("Starting process " + assembly.GetName());
            userProcess.Start();
        }
    }
}
