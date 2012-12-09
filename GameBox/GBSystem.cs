using OpenTK;

namespace GameBox
{
	public static class GBSystem
	{
		private static GBWindow window;

		public static void Init ()
		{
			GBProperties.Init();

		}

		public static void CreateWindow()
		{

			GBInfo.WriteLine("Creating OpenGL window...");
			window = GBWindow.CreateFromProperties();
			window.Run();
		}


		internal static GBWindow Window
		{
			get { return window; }
		}
	}
}

