using System;
using GameBox.Graphics;
using GameBox.ObjectSerialization;
using GameBox.Context;
using GameBox.Services;

namespace GameBox
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Logger.debugInfo("Welcome to GameBox");
            Logger.debugInfo("Reading configuration...");
            Config.LoadConfig();

            DeviceDelegate.create();
            DeviceDelegate.StaticRun();

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
