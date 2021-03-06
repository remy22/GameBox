﻿using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using GameBox.XMLSerialization;
using GameBox.Graphics;
using OpenTK;
using GameBox.Resources;
using GameBox.Events;

namespace GameBox.Processes
{
    public class Process
    {
        private const string MetaDataExtension = "xml";
        private const string ModuleExtension = "exe";

        internal enum ProcessState
        {
            NotLoaded,
            PendingValidation,
            StartPending,
            Running,
            InitError
        }

        private Assembly assembly = null;
        private GBXMLContainer metadata = null;
        private string completeFileName = string.Empty;
		private string completePropertiesFileName = string.Empty;
		private string ProcessMainClass = string.Empty;
        private string baseDir = string.Empty;
        private GBProcess userProcess = null;
        private ProcessState prState = ProcessState.NotLoaded;
        private List<Scene> sceneList = new List<Scene>();
        private Scene currentScene = null;
        private string name = string.Empty;

        internal ResourceManager rManager = new ResourceManager();
        internal PatternObjects patternObjects = new PatternObjects();

        public Process()
        {
        }

        internal string BaseDir
        {
            get { return baseDir; }
        }

        public string Name
        {
            get { return name; }
        }

		public void LoadMetadata(string fileName)
		{
			GBDebug.Assert(prState == ProcessState.NotLoaded, "Process state error. It is " + prState + ". It must be " + ProcessState.NotLoaded);
            completePropertiesFileName = GBFileSystem.CompleteFileNameForFile(baseDir, fileName + "." + MetaDataExtension);
			if (GBFileSystem.FileExists(completePropertiesFileName))
			{
				metadata = GBXMLContainer.LoadOrNull(completePropertiesFileName);
				GBDebug.WriteLine(metadata);
			}
			else
			{
				prState = ProcessState.InitError;
				throw new GBException(GBException.Reason.FileNotFound);
			}
		}

        public void LoadModuleFromFile(string fileName)
        {
            GBDebug.Assert(prState == ProcessState.NotLoaded, "Process state error. It is " + prState + ". It must be " + ProcessState.NotLoaded);
            name = fileName;
            completeFileName = GBFileSystem.CompleteFileNameForFile(baseDir, fileName + "." + ModuleExtension);
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

        public void Load(string dir, string fileName)
        {
            baseDir = dir;
            LoadMetadata(fileName);
            if (prState == ProcessState.NotLoaded)
            {
                LoadModuleFromFile(fileName);
                if (prState == ProcessState.PendingValidation)
                {
                    GBXMLContainer temp = metadata["Info"];
                    GBInfo.WriteLine("Name: " + temp["Name"].Text);
                    GBInfo.WriteLine("Version: " + temp["Version"].Text + "." + temp["Subversion"].Text + "." + temp["Release"].Text + ":" + temp["Build"].Text);
                    GBInfo.WriteLine("Author: " + temp["Author"].Text);

                    ProcessMainClass = metadata["MetaData"]["StartWith"].Text;
                    GBInfo.WriteLine("EntryPoint: " + ProcessMainClass);
                }
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

        internal void ReadScenesData()
        {
            GBXMLContainer scenes = metadata["MetaData"]["Scenes"];
            foreach (GBXMLContainer sceneData in scenes.Children)
            {
                Scene newScene = new Scene(sceneData);
                sceneList.Add(newScene);
                if (newScene.IsFirst)
                {
                    currentScene = newScene;
                }

            }
        }

        internal void Start()
        {
            GBDebug.Assert(prState == ProcessState.StartPending, "Process state error. It is " + prState + ". It must be " + ProcessState.StartPending);
            GBInfo.WriteLine("Starting process " + assembly.GetName());

            // FIXME: Doing nothing
            userProcess.Start();

            if (currentScene != null)
                currentScene.LoadNotLoadedResources();

            prState = ProcessState.Running;
        }

        internal Scene CurrentScene
        {
            get { return currentScene; }
        }

        internal void Render() {
            currentScene.StartFrame();
        }

        internal void OnResize(EventArgs e) {
            currentScene.SetProjection();
        }

        internal void OnRenderFrame(FrameEventArgs e) {
            RenderingContext.RenderingProcess = this;
            RenderingContext.e = e;
            RenderingContext.currentEvents = eventList[eventListIndexToProcess];
            Render();
            SwapEventList();
        }

        private List<GBEvent>[] eventList = { new List<GBEvent>(), new List<GBEvent>() };
        private int eventListIndexToAdd = 0;
        private int eventListIndexToProcess = 1;

		public void AddEvent(GBEvent evnt)
		{
            eventList[eventListIndexToAdd].Add(evnt);
		}

        private void SwapEventList()
        {
            eventListIndexToAdd = (eventListIndexToAdd + 1) % 2;
            eventListIndexToProcess = (eventListIndexToProcess + 1) % 2;
            eventList[eventListIndexToAdd].Clear();
        }


        public void DispatchAction(GBEvent evnt, string action, GBXMLContainer actionData)
        {
            if (action.StartsWith("Process."))
            {
                string shortAction = action.Substring("Process.".Length);
                switch (shortAction)
                {
                    case "Finish":
                        GBDebug.WriteLine("TODO: Finishing process");
                        ProcessManager.FinishActiveProcess();
                        break;
                }
            }
        }

        ~Process()
        {
            GBDebug.WriteLine("Deleting " + this + ":" + Name);
        }
    }
}
