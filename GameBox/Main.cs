using System;
using GameBox.Graphics;
using GameBox.ObjectSerialization;
using GameBox.Context;

namespace GameBox
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Console.WriteLine("Welcome to GameBox");
            Console.WriteLine("Reading configuration...");
            Config.LoadConfig();

            DeviceDelegate.create();

//            StaticVBODelegate device = new StaticVBODelegate();
//            device.Run();

            /*
            DeviceDelegate device = new DeviceDelegate();
            device.Run();
            
            DeviceExample device = new DeviceExample();
            device.Run();
             */
        }
	}
}
