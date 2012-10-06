using System;
using GameBox.Graphics;
using GameBox.Services;

namespace GameBox
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Logger.debugInfo("Welcome to GameBox");

			Window device = new Window();
            device.Run();
        }
	}
}
