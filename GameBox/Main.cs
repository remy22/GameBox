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

			DeviceDelegate device = new DeviceDelegate();
            device.Run();
        }
	}
}
