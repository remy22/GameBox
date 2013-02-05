using OpenTK;
using GameBox.XMLSerialization;
using GameBox.Processes;
using System;

namespace GameBox
{
	public static class GBSystem
	{
        private static GBWindow window = null;
        private static GBXMLContainer modulesXMLData = null;
        private static double ellapsedSinceStart = 0;
        private static double ellapsedSinceLastUpdate = 0;
        private static double ellapsedSinceLastFrame = 0;

        internal static void Init()
        {
            GBProperties.Init();
            GBInfo.WriteLine("Creating OpenGL window...");
			window = GBWindow.CreateFromProperties();
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
							"<FileName>Brocker</FileName>" +
                            "<BaseDir>../../../Brocker/bin/Debug</BaseDir>" +
						"</module>" +
					"</modules>"
					);
			}
		}

        static void LoadModules()
        {
            GBInfo.WriteLine("Loading modules...");
            foreach (GBXMLContainer module in modulesXMLData.Children)
            {
                string fileName = module["FileName"].Text;
                string baseDir = module["BaseDir"].Text;
                GBInfo.WriteLine("Loading module " + fileName);
                GBInfo.WriteLine("with Base dir " + baseDir);
                ProcessManager.Load(baseDir, fileName);
            }
        }

        internal static void OnLoadWindow(EventArgs e)
        {
            GBInfo.WriteLine("Loading window...");
            LoadModulesData();
            LoadModules();
            ProcessManager.Start();
        }

        internal static void OnUpdateFrame(FrameEventArgs e)
        {
            ellapsedSinceLastUpdate = e.Time;
            ProcessManager.OnUpdateFrame(e);
        }

        internal static void OnResize(EventArgs e)
        {
            ProcessManager.OnResize(e);
        }

        internal static void OnRenderFrame(FrameEventArgs e)
        {
            ellapsedSinceLastFrame = e.Time;
            ellapsedSinceStart += e.Time;
            ProcessManager.OnRenderFrame(e);
            ProcessManager.UpdateProcesses();
        }

        internal static double EllapsedSinceStart
        {
            get { return ellapsedSinceStart; }
        }

        internal static double EllapsedLastUpdate
        {
            get { return ellapsedSinceLastUpdate; }
        }

        internal static double EllapsedSinceLastFrame
        {
            get { return ellapsedSinceLastFrame; }
        }
    }
}
