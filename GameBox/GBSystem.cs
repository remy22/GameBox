using OpenTK;
using GameBox.XMLSerialization;
using GameBox.Processes;
using System;

namespace GameBox
{
	public static class GBSystem
	{
        private static GBWindow window = null;
        private static ProcessManager pManager = null;
        private static GBXMLContainer modulesXMLData = null;

        internal static void Init()
        {
            GBProperties.Init();
            GBInfo.WriteLine("Creating OpenGL window...");
			window = GBWindow.CreateFromProperties();
            pManager = new ProcessManager();
            window.Run();
		}

		internal static GBWindow Window
		{
			get { return window; }
		}

		static void LoadModulesData ()
		{
			modulesXMLData = GBXMLContainer.LoadOrNull ("modules.xml");
			if (modulesXMLData == null)
			{
				// Create the default modules.
				modulesXMLData = GBXMLContainer.LoadFromString(
					"<modules>" +
						"<module>" +
							"<FileName>Game1.dll</FileName>" +
							"<BaseDir>.</BaseDir>" +
						"</module>" +
					"</modules>"
					);
			}
		}

        static void LoadModules()
        {
            GBInfo.WriteLine("Loading modules...");
            pManager.Load("../../../Brocker/bin/Debug", "Brocker.exe");
        }

        internal static void OnLoadWindow(EventArgs e)
        {
            GBInfo.WriteLine("Loading window...");
            LoadModules();
            pManager.Start();
        }

        internal static void OnUpdateFrame(FrameEventArgs e)
        {
        }

	}
}
