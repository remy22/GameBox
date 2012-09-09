using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBox.Graphics.Nodes;
using System.Reflection;
using GameBox.Services;

namespace GameBox.Context
{
    internal class ProcessContext
    {
        internal enum AppState
        {
            Loaded,
            Ready,
            Running,
            Closing,
            Finished
        }

        private string name = "noNamedProcess";
        private SceneManager sManager = null;
        private Assembly pAssembly = null;
        private AppState appState = AppState.Loaded;

        internal ProcessContext(string processName,Assembly processAssembly)
        {
            name = processName;
            pAssembly = processAssembly;
        }

        internal void Init()
        {

            Logger.debugErrorIf(appState != AppState.Loaded, "App " + this.name + " is not in the correct state to be initialized");

            if (appState == AppState.Loaded)
                appState = AppState.Ready;
        }

        internal void Start()
        {

            Logger.debugErrorIf(appState != AppState.Ready, "App " + this.name + " is not in the correct state to be started");

            if (appState == AppState.Ready)
                appState = AppState.Running;
        }

        internal string Name
        {
            get { return name; }
        }

        internal SceneManager SManager
        {
            get { return sManager; }
        }
    }
}
