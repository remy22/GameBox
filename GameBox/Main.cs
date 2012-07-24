using System;
using GameBox.InternalServices;

namespace GameBox
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
            Console.WriteLine("Welcome to GameBox");
            Console.WriteLine("Reading configuration...");

            Configuration.create().Init();

        }
	}
}
