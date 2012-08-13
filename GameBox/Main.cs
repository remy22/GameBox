using System;
using GameBox.InternalServices;
using GameBox.Graphics;

namespace GameBox
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Console.WriteLine("Welcome to GameBox");
            Console.WriteLine("Reading configuration...");

            Configuration.Init();
            Configuration.Destroy();

            StaticVBODelegate device = new StaticVBODelegate();
            device.Run();

            /*
            DeviceDelegate device = new DeviceDelegate();
            device.Run();
            
            DeviceExample device = new DeviceExample();
            device.Run();
             */
        }
	}
}
