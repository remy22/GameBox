using System;
using GameBox.Services;
using GameBox.ObjectSerialization;
using GameBox.Graphics.Types.Wrappers;
using GameBox.Graphics.Types;
using System.Collections.Generic;

namespace GameBox.Context
{
    internal static class Config
    {
        private const string configFileName = "config.xml";
        private static NodeData configNode = null;

        internal static void LoadConfig()
        {
            Logger.debugInfo("Opening config file (" + configFileName + ")...");
            Logger.debug("Checking existance...");
            bool exists = new GBFile(configFileName).FileExists();
            if (exists)
            {
                configNode = NodeData.ReadFile(configFileName);
            }
            else
            {
                Logger.debugInfo(configFileName + " not found. Defaults values will be used");
                configNode = new NodeData();
            }
        }

        internal static NodeData Data
        {
            get { return configNode; }
        }
    }
}
